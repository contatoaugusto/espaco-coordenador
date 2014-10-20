//SGI
using System;
using EC.Common;
using EC.UI.WebControls;

namespace UI.Web.EC.Coordenador
{
    public partial class Aluno_SenhaEspacoAluno : UI.Web.EC.Page
    {
        public Aluno_SenhaEspacoAluno()
            : base("Content")
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
                //txtPassWordActual.Focus();
        }
        protected void ButtonConfirm_Click(object sender, EventArgs e)
        {
            //if (txtPassWordActual.Text.Trim().Length == 0)
            //{
            //    messageBox.Show(new Alert("219").Description, "Atenção", MessageBoxType.Warning);
            //    return;
            //}
            //else if (txtNewPassWord.Text.Trim() != txtConfirmPassWord.Text.Trim())
            //{
            //    messageBox.Show(new Alert("126").Description, "Atenção", MessageBoxType.Warning);
            //    return;
            //}
            //else if (txtNewPassWord.Text.Trim().Length < 6)
            //{
            //    messageBox.Show(new Alert("818").Description, "Atenção", MessageBoxType.Warning);
            //    return;
            //}

            ////Atualiza a senha do Espaço Aluno
            //if (!new SGI.DataContext.Controller.Sistema.Usuario().ChangePassword(SGI.Common.Session.coAcesso, txtPassWordActual.Text.Trim(), txtNewPassWord.Text.Trim()))
            //{
            //    messageBox.Show(new Alert("821").Description, "Atenção", SGI.UI.WebControls.MessageBoxType.Warning);
            //    return;
            //}

            //messageBox.Show(new Alert("820").Description + "<BR/> Atualizada em " + DateTime.Now.ToString("dd/MM/yyyy") + " às " + DateTime.Now.ToString("HH:mm:ss"), "Atenção", SGI.UI.WebControls.MessageBoxType.Information);
            return;
        }
    }
}