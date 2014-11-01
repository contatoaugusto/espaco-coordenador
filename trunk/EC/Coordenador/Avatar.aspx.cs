using System;
using System.Web;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web.UI.WebControls;
using EC.Common;
using EC.Modelo;
using EC.Negocio;
using EC.UI.WebControls;
using alertControl = EC.Common.Alert;

namespace UI.Web.EC.Coordenador
{
    public partial class Avatar1 : Page
    {

        public Avatar1()
            : base("Content")
        {

        }

        #region Properties

        public string PathUpload
        { get; set; }

        public long idPessoa
        {
            get { return ((SessionUsuario)Session[Const.USUARIO]).USUARIO.ID_USUARIO.ToInt64(); } //SGI.Common.Session.idPessoa; }
        }
        public bool icModalVisible
        {
            get { return Library.ToBoolean(ViewState["1"]); }
            set { ViewState["1"] = value; }
        }
        private byte[] _imageStream
        {
            get { return (byte[])ViewState["2"]; }
            set { ViewState["2"] = value; }
        }
        private string _imageFileName
        {
            get { return Library.ToString(ViewState["3"]); }
            set { ViewState["3"] = value; }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //Verificacao de aluno nao logado.

            if (UI.Web.EC.Utils.CoordenadorNaoLogado())
            {
                Response.Redirect(UI.Web.EC.Utils.PortalUrl);

            }
            else
            {
                //alert.Visible = false;

                //PathUpload = Path.Combine(AppSettings.PathRoot /*AppSettings.PathUpload*/ , @"temp\uploadimage\");
                //PathUpload = Path.Combine(HttpContext.Current.Server.MapPath("."), @"temp\uploadimage\");
                PathUpload = Path.Combine("C:\\", @"temp\uploadimage\");
                
                if (!IsPostBack)
                {
                    BindPage();
                    VerifyFoto();
                }
            }
        }

        public void BindPage()
        {
            //DataTable data = NUsuario.ConsultarById(((USUARIO)Session["USUARIO"]).ID_USUARIO);
            //repeater.DataSource = NUsuario.ConsultarById(((USUARIO)Session["USUARIO"]).ID_USUARIO);
            //repeater.DataBind();
        }
        protected void repeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //if (e.CommandName.Equals("DEL"))
            //{
            //    int idFotoUsuario = Library.ToInteger(e.CommandArgument);

            //    new SGI.DataContext.Controller.Coorporativo.FotoUsuario().Del(idFotoUsuario);
            //    divIncluirImagem.Visible = true;
            //    BindPage();
            //}
        }
        protected void cevFotoUsuario_CustomEvent(object sender, EventArgs e)
        {
            string eventArgument = Request.Form["__EVENTARGUMENT"];
            int idFotoUsuario = Library.ToInteger(eventArgument);
            //new SGI.DataContext.Controller.Coorporativo.FotoUsuario().SetMostraSiteHoje(((USUARIO)Session["USUARIO"]).ID_USUARIO, idFotoUsuario);

            //Excluo cache da imagem
            string imageCache = string.Format(@"{0}\{1}.tmp", string.Format(@"{0}_imagecache\", AppSettings.PathUpload), ((SessionUsuario)Session[Const.USUARIO]).USUARIO.ID_USUARIO);

            if (File.Exists(imageCache))
            {
                try
                {
                    File.Delete(imageCache);
                }
                catch { }
            }

            Response.Redirect("avatar.aspx");
            //BindControl();
        }

        public void VerifyFoto()
        {
            //if (repeater.Items.Count < 5)
                divCropImage0.Visible = !(divUploadImage0.Visible = true);
            //else
            //{
            //    alert.Show(new EC.UI.WebControls.Alert("919").Description);
            //    divIncluirImagem.Visible = false;
            //}
        }

        private void HideUpload()
        {
            divUploadImage0.Visible = divUploadImage1.Visible = false;
        }

        private void ShowUpload()
        {
            divUploadImage0.Visible = divUploadImage1.Visible = true;
        }

        private void HideCrop()
        {
            divUploadPhotoButtons.Visible = /*divCropImage0.Visible = divCropImage1.Visible =*/ divCropImage2.Visible = /*divCropImage3.Visible = divCropImage4.Visible =*/ false;
        }

        private void ShowCrop()
        {
            divUploadPhotoButtons.Visible = /*divCropImage0.Visible = divCropImage1.Visible =*/ divCropImage2.Visible = /*divCropImage3.Visible = divCropImage4.Visible =*/ true;
        }

        protected void cehUpload_CustomEvent(object sender, EventArgs e)
        {
            //Verifica se foi selecionado um arquivo para upload
            if (fileUpload.HasFile)
            {
                HideUpload();
                //Verifica o tamanho másximo da imagem para upload: 3MB
                if (fileUpload.PostedFile.ContentLength > (2 * (1024 * 1000)))
                {

                    messageBox.Show(new alertControl("900").Description, "Erro", MessageBoxType.Error);
                    return;
                }

                System.Drawing.Image img = Library.ConvertByteToImage(fileUpload.FileBytes);
                if (img.Width <= 32)
                {
                    messageBox.Show(new alertControl("923").Description, "Erro", MessageBoxType.Error);
                    img.Dispose();
                    return;
                }

                img.Dispose();
                CarregarImagem(string.Format("{0}{1}", ((SessionUsuario)Session[Const.USUARIO]).USUARIO.ID_USUARIO, System.IO.Path.GetExtension(fileUpload.FileName)), fileUpload.FileBytes);
                ShowCrop();
            }
        }
        protected void btnOriginal_Click(object sender, EventArgs e)
        {
            System.Drawing.Image croppedImage = System.Drawing.Image.FromFile(PathUpload + _imageFileName);

            if (ValidaLogin() && croppedImage != null)
            {
                int height = (croppedImage.Width * 70) / croppedImage.Height;
                SaveImage(croppedImage.GetThumbnailImage(70, height, null, new IntPtr()));
                croppedImage.Dispose();

                HideCrop();
                ShowUpload();
            }
        }
        protected void btnCrop_Click(object sender, EventArgs e)
        {
            if (Library.ToInteger(hdfW.Value) == 0 & Library.ToInteger(hdfH.Value) == 0)
            {
                //messageBox.Show("Para recortar a imagem é necessário selecionar uma região da imagem.", "Atenção");
                return;
            }

            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(ResolveClientUrl(PathUpload + _imageFileName));

            originalImage = originalImage.GetThumbnailImage(Library.ToInteger(imageCrop.Width.Value), Library.ToInteger(originalImage.Height), null, new IntPtr());
            System.Drawing.Image croppedImage = RecortarImagem(Library.ToInteger(hdfX.Value), Library.ToInteger(hdfY.Value), Library.ToInteger(hdfW.Value), Library.ToInteger(hdfH.Value), originalImage);

            if (ValidaLogin())
            {
                if (croppedImage != null)
                {
                    SaveImage(croppedImage.GetThumbnailImage(54, 65, null, new IntPtr()));
                    croppedImage.Dispose();

                    HideCrop();
                    ShowUpload();
                }
                //else
                //    alert.Show("917");
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ShowUpload();
            HideCrop();
            imageCrop.ImageUrl = string.Empty;
        }

        private bool ValidaLogin()
        {
            //if (txtcoAcesso.Text.Trim().Length == 0)
            //    return false;

            //var returnObject = new SGI.UI.Process.Sistema.UsuarioProcess().LoginEspacoAluno(SessionAluno.nuRA, txtcoAcesso.Text);
            //if (returnObject.AlertList.HasAlert())
            //{
            //    messageBox.Show(returnObject.AlertList[0].Description, "Atenção", SGI.UI.WebControls.MessageBoxType.Warning);
            //    return false;
            //}

            return true;
        }
        private void SaveImage(System.Drawing.Image image)
        {
            //var o = new SGI.BusinessObject.Coorporativo.FotoUsuario();
            var usuario = NUsuario.ConsultarById(Utils.GetUsuarioLogado().USUARIO.ID_USUARIO);

            usuario.FOTO = Library.ConvertImageToByte(image);
            NUsuario.Atualiza(usuario);

            string imageCache = string.Format(@"{0}{1}.tmp", Utils.PathImagesCache, usuario.ID_USUARIO);
            File.Delete(imageCache);

            //o.idFotoUsuario = 0;
            //o.imFotoUsuario = Library.ConvertImageToByte(image);
            //o.idUsuario = SessionAluno.idUsuario;
            //o.dtUpload = DateTime.Now;
            //o.icMostraSiteHoje = true;

            //var message = new SGI.DataContext.Controller.Coorporativo.FotoUsuario().Set(o);
            //if (message.HasAlert())
            //{
            //    if (repeater.Items.Count < 5)
            //        messageBox.Show(new Alert("899").Description, "Erro", SGI.UI.WebControls.MessageBoxType.Error);
            //    else
            //    {
            //        messageBox.Show(message[0].Description, "Atenção", SGI.UI.WebControls.MessageBoxType.Warning);
            //        HideUpload();
            //    }
            //    return;
            //}

            ShowCrop();
            BindPage();
        }
        protected System.Drawing.Image CarregarImagem(string fileName, byte[] imageStream)
        {
            FileInfo imagem = CreateAleatoryFile(new FileInfo(fileName).Extension, PathUpload, imageStream);
            _imageFileName = imagem.Name;

            System.Drawing.Image image = Library.ConvertByteToImage(imageStream);
            imageCrop.Width = (image.Width > 600 ? 600 : image.Width);
            imageCrop.ImageUrl = ResolveClientUrl(PathUpload + _imageFileName);
            return image;
        }
        protected System.Drawing.Image RecortarImagem(int x, int y, int width, int height, System.Drawing.Image originalImage)
        {
            Rectangle croppedRectangle = new Rectangle(-x, -y, width + x, height + y);

            System.Drawing.Image croppedImage = new System.Drawing.Bitmap(width, height, originalImage.PixelFormat);
            //Aqui faço a cópia da imagem, aplicando o recorte com os parâmetros informados pelo 'croppedRectangle'
            System.Drawing.Graphics clippedImage = System.Drawing.Graphics.FromImage(croppedImage);
            clippedImage.DrawImageUnscaled(originalImage, croppedRectangle);
            clippedImage.Flush();
            clippedImage.Dispose();
            return croppedImage;
        }
        /// <summary>
        /// Esse método re-utiliza ou cria um arquivo aleatóriamente dentro de um diretório especificado, e com a extensão especificada.
        /// A reutilização se dá para arquivos que não tenham sofrido alterações em um período correspondente ao 'Timeout' da sessão.
        /// </summary>
        /// <param name="fileExtension">extensão do arquivo à ser reutilizado/criado, exemplo: ".jpg"</param>
        /// <param name="directoryPath">diretório utilizado pelos arquivos, a string deverá conter um finalizador '/' ou '\', exemplo: "C:\\temp\\".</param>
        /// <param name="fileStream">conteúdo do arquivo.</param>
        /// <returns></returns>
        protected FileInfo CreateAleatoryFile(string fileExtension, string directoryPath, byte[] fileStream)
        {
            DirectoryInfo dirInf = new DirectoryInfo(directoryPath);
            if (!dirInf.Exists)
                return null;
            //Aqui procuro todos os arquivos com a extensão informada.
            List<FileInfo> files = new List<FileInfo>(dirInf.GetFiles("*" + fileExtension, SearchOption.TopDirectoryOnly));
            DateTime dtNow = DateTime.Now;
            foreach (FileInfo file in files)
                //Aqui verifico se o arquivo está estático por um período maior que o tempo de duração da sessão
                if (file.LastWriteTime < dtNow.AddMinutes(-Session.Timeout))
                    file.Delete();

            //Caso nenhum arquivo esteja disponível para uso, um novo é criado.
            FileInfo newFile = new FileInfo(directoryPath + Guid.NewGuid().ToString() + (files.Count + 1) + fileExtension);
            using (FileStream aleatoryFileStream = newFile.Create())
                aleatoryFileStream.Write(fileStream, 0, fileStream.Length);
            return newFile;
        }



    }
}