using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web.UI;
using EC.UI.WebControls;
using EC.Modelo;
using EC.Negocio;
using EC.Common;
using alertControl = EC.Common.Alert;

namespace UI.Web.EC.Coordenador
{
    public partial class Avatar : UserControl
    {
        private const string uploadImagePath = "D:\\CEUB\\Projetos CEUB\\Projetos\\Fabrica\\Solucoes\\EspacoAluno\\CodigoFonte\\Desenvolvimento-Clean\\UniCeub.EspacoAluno\\UI.Web.EspacoAluno\\Temp\\UploadImage\\";
        private const string uploadImageUrl = "~/Temp/UploadImage/";

        public int idUsuario
        {
            get { return ((USUARIO)Session["USUARIO"]).ID_USUARIO; }
        }
        public long idPessoa
        {
            get { return ((USUARIO)Session["USUARIO"]).FUNCIONARIO.ID_PESSOA.ToInt64(); }
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindControl();
            }
            else if (icModalVisible) mpeFotoAluno.Show();
        }

        public override void BindControl()
        {
            if (((USUARIO)Session["USUARIO"]).FUNCIONARIO.ID_PESSOA == 0) return;

            //var fotoUsuario = new SGI.DataContext.Controller.Coorporativo.FotoUsuario().BindByPessoa(Library.ToInteger(idPessoa));
            //if(fotoUsuario == null)
            //    imgAluno.ImageUrl = string.Format("~/includes/fotopessoa.ashx?{0}", idPessoa);
            //else
            imgAluno.ImageUrl = string.Format("~/includes/fotofotousuario.ashx?{0}", ((USUARIO)Session["USUARIO"]).ID_USUARIO);
        }
        protected void repeater_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {
            //if (e.CommandName.Equals("DEL"))
            //{
            //    int idFotoUsuario = Library.ToInteger(e.CommandArgument);

            //    new SGI.DataContext.Controller.Coorporativo.FotoUsuario().Del(idFotoUsuario);
            //    divIncluirImagem.Visible = true;
            //    BindAvatar();
            //}
        }
        protected void cevFotoUsuario_CustomEvent(object sender, EventArgs e)
        {
            int idFotoUsuario = Library.ToInteger(cevFotoUsuario.EventArgument);
            //new SGI.DataContext.Controller.Coorporativo.FotoUsuario().SetMostraSiteHoje(idUsuario, idFotoUsuario);
            Response.Redirect("Default.aspx", false);
            //BindControl();
        }
        private void BindAvatar()
        {
            //DataTable data = new SGI.DataContext.Controller.Coorporativo.FotoUsuario().BindListByPessoa(Convert.ToInt32(idPessoa)).ToDataTable();
            //repeater.DataSource = data;
            //repeater.DataBind();

            VerifyFoto();
        }
        public void VerifyFoto()
        {
            if (repeater.Items.Count < 5)
                divCropImage.Visible = !(divUploadImage.Visible = true);
            else
            {
                //messageBox.Show(new alertControl("919").Description, "Atenção", MessageBoxType.Warning);
                divIncluirImagem.Visible = false;
            }
        }
        protected void imgAluno_Click(object sender, ImageClickEventArgs e)
        {
            BindAvatar();
            icModalVisible = true;
            mpeFotoAluno.Show();
        }
        protected void customEventHandler_CustomEvent(object sender, EventArgs e)
        {
            //Verifica se foi selecionado um arquivo para upload
            if (fileUpload.HasFile)
            {
                divUploadImage.Visible = false;
                //Verifica o tamanho másximo da imagem para upload: 3MB
                if (fileUpload.PostedFile.ContentLength > (2 * (1024 * 1000)))
                {
                    //alerterror.Show("Verificar tamanho da imagem");
                    divCropImage.Visible = !(divUploadImage.Visible = true);
                    return;
                }

                divCropImage.Visible = !(divUploadImage.Visible = false);

                string fileName = string.Format("Temp/UploadImage/{0}{1}", ((USUARIO)Session["USUARIO"]).ID_USUARIO, System.IO.Path.GetExtension(fileUpload.FileName));
                string tmp = System.IO.Path.Combine(Server.MapPath(""), fileName.Replace("/", "\\"));

                fileUpload.SaveAs(tmp);

                imageCrop.ImageUrl = ResolveClientUrl("~/" + fileName);
            }
        }
        protected void cehUpload_CustomEvent(object sender, EventArgs e)
        {
            //Verifica se foi selecionado um arquivo para upload
            if (fileUpload.HasFile)
            {
                divUploadImage.Visible = false;
                //Verifica o tamanho másximo da imagem para upload: 3MB
                if (fileUpload.PostedFile.ContentLength > (2 * (1024 * 1000)))
                {
                    divCropImage.Visible = !(divUploadImage.Visible = true);
                    // messageBox.Show(new alertControl("900").Description, "Erro", MessageBoxType.Error);
                    return;
                }

                System.Drawing.Image img = Library.ConvertByteToImage(fileUpload.FileBytes);
                if (img.Width <= 32)
                {
                    divCropImage.Visible = !(divUploadImage.Visible = true);
                    //messageBox.Show(new alertControl("923").Description, "Erro", MessageBoxType.Error);
                    img.Dispose();
                    return;
                }

                img.Dispose();
                CarregarImagem(string.Format("{0}{1}", ((USUARIO)Context.Session["USUARIO"]).ID_USUARIO, System.IO.Path.GetExtension(fileUpload.FileName)), fileUpload.FileBytes);
                divCropImage.Visible = !(divUploadImage.Visible = false);
            }
        }
        protected void btnOriginal_Click(object sender, EventArgs e)
        {
            System.Drawing.Image croppedImage = System.Drawing.Image.FromFile(uploadImagePath + _imageFileName);

            if (ValidaLogin() && croppedImage != null)
            {
                int height = (croppedImage.Width * 70) / croppedImage.Height;
                SaveImage(croppedImage.GetThumbnailImage(70, height, null, new IntPtr()));
                croppedImage.Dispose();
            }
        }
        protected void btnCrop_Click(object sender, EventArgs e)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(ResolveClientUrl(uploadImagePath + _imageFileName));
            float proportion = (((float)imageCrop.Width.Value / (float)originalImage.Width));
            originalImage = originalImage.GetThumbnailImage(Library.ToInteger(imageCrop.Width.Value), Library.ToInteger(originalImage.Height * proportion), null, new IntPtr());
            System.Drawing.Image croppedImage = RecortarImagem(Library.ToInteger(hdfX.Value)
                , Library.ToInteger(hdfY.Value)
                , Library.ToInteger(hdfW.Value)
                , Library.ToInteger(hdfH.Value)
                , originalImage);

            if (ValidaLogin())
            {
                if (croppedImage != null)
                {
                    int height = (croppedImage.Width * 70) / croppedImage.Height;
                    SaveImage(croppedImage.GetThumbnailImage(70, height, null, new IntPtr()));
                    croppedImage.Dispose();
                }
                //else
                //    alerterror.Show("917");
            }

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            divCropImage.Visible = !(divUploadImage.Visible = true);
            imageCrop.ImageUrl = string.Empty;
        }

        private bool ValidaLogin()
        {
            //if (txtcoAcesso.Text.Trim().Length == 0)
            //    return false;

            //SGI.Common.AlertList alertList = new SGI.DataContext.Controller.Sistema.Usuario().LoginApoioAluno(SGI.Common.Session.coAcesso, txtcoAcesso.Text);
            //if (alertList.HasAlert())
            //{
            //    messageBox.Show(alertList[0].Description, "Erro", SGI.UI.WebControls.MessageBoxType.Error);
            //    return false;
            //}

            return true;
        }
        private void SaveImage(System.Drawing.Image image)
        {
            var usuario = ((USUARIO)Session["USUARIO"]);
            usuario.FOTO = Library.ConvertImageToByte(image);
            NUsuario.Salvar(usuario);

            //var o = new SGI.BusinessObject.Coorporativo.FotoUsuario();
            //o.idFotoUsuario = 0;
            //o.imFotoUsuario = Library.ConvertImageToByte(image);
            //o.idUsuario = idUsuario;
            //o.dtUpload = DateTime.Now;
            //o.icMostraSiteHoje = true;

            //SGI.Common.AlertList message = new SGI.DataContext.Controller.Coorporativo.FotoUsuario().Set(o);
            //if (message.HasAlert())
            //{
            //    if (repeater.Items.Count < 5)
            //        messageBox.Show(new Alert("899").Description, "Erro", SGI.UI.WebControls.MessageBoxType.Error);
            //    else
            //    {
            //        messageBox.Show(message[0].Description, "Atenção", SGI.UI.WebControls.MessageBoxType.Warning);
            //        divIncluirImagem.Visible = false;
            //    }
            //    return;
            //}

            divCropImage.Visible = !(divUploadImage.Visible = true);
            BindAvatar();
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            icModalVisible = false;
            mpeFotoAluno.Hide();
        }
        protected System.Drawing.Image CarregarImagem(string fileName, byte[] imageStream)
        {
            FileInfo imagem = CreateAleatoryFile(new FileInfo(fileName).Extension, uploadImagePath, imageStream);
            _imageFileName = imagem.Name;

            System.Drawing.Image image = Library.ConvertByteToImage(imageStream);
            imageCrop.Width = (image.Width > 400 ? 400 : image.Width);
            imageCrop.ImageUrl = ResolveClientUrl("~/" + uploadImageUrl + _imageFileName);
            return image;
        }
        protected System.Drawing.Image RecortarImagem(int x, int y, int width, int height, System.Drawing.Image originalImage)
        {
            Rectangle croppedRectangle = new Rectangle(-x
                , -y
                , width + x
                , height + y);

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
            string prefixFile = "AF";
            DirectoryInfo dirInf = new DirectoryInfo(directoryPath);
            if (!dirInf.Exists) return null;
            //Aqui procuro todos os arquivos com a extensão informada.
            List<FileInfo> files = new List<FileInfo>(dirInf.GetFiles(prefixFile + "*" + fileExtension, SearchOption.TopDirectoryOnly));
            DateTime dtNow = DateTime.Now;
            foreach (FileInfo file in files)
                //Aqui verifico se o arquivo está estático por um período maior que o tempo de duração da sessão
                if (file.LastWriteTime < dtNow.AddMinutes(-Session.Timeout))
                    file.Delete();

            //Caso nenhum arquivo esteja disponível para uso, um novo é criado.
            FileInfo newFile = new FileInfo(directoryPath + prefixFile + Guid.NewGuid().ToString() + (files.Count + 1) + fileExtension);
            using (FileStream aleatoryFileStream = newFile.Create())
                aleatoryFileStream.Write(fileStream, 0, fileStream.Length);
            return newFile;
        }
    }
}