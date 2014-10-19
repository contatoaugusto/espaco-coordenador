using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using EC.Common;
using EC.Negocio;

namespace UI.Web.EC
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            form1.SubmitDisabledControls = true;
            form1.DefaultFocus = "txtMatricula";
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            btnLogin.Enabled = false;
            int matricula = Library.ToInteger(txtMatricula.Text); //FormatLogin();
            string coSenha = txtcoAcesso.Text;

            if (matricula == 0)
            {
                btnLogin.Enabled = true;
                alert.Show("Informe o código de acesso do usuário.");
                return;
            }

            if (coSenha.Length == 0)
            {
                btnLogin.Enabled = true;
                alert.Show("Informe a senha de acesso do usuário.");
                return;
            }

            var u = NUsuario.ConsultarUsuarioByLoging(matricula, true);

            if (u == null)
            {
                btnLogin.Enabled = true;
                alert.Show("O Usuário ou a senha que você digitou não foi reconhecido(a). ");
                return;
            }

            if (u.FUNCIONARIO.CARGO.ID_CARGO != 2)
            {
                btnLogin.Enabled = true;
                alert.Show("O funcionário não é coordenador de curso. ");
                return;
            }

            if ((txtMatricula.Text == u.FUNCIONARIO.MATRICULA.ToString()) && (txtcoAcesso.Text == u.SENHA)) 
            {
                FormsAuthentication.RedirectFromLoginPage(txtMatricula.Text, true);
                System.Web.HttpCookie cookie = new System.Web.HttpCookie("espacocorrdenador@uniceub.br", matricula.ToString());
                cookie.Expires = DateTime.Now.AddDays(30);
                Response.Cookies.Add(cookie);

                Session["USUARIO"] = u;

                // Curso
                var curso = NCursoCoordenador.ConsultarCursoByCoordenador(u.FUNCIONARIO.ID_FUNCIONARIO).First();
                Session["ID_CURSO"] = curso.ID_CURSO;
                Session["NM_CURSO"] = curso.DESCRICAO;

                //Session objSession = new Session();
                //Session.id = u.idPessoa;
                //EC.Common.Session.idPerfil = u.idPerfil;
                //EC.Common.Session.coAcesso = u.coAcesso;
                //EC.Common.Session.nmPessoa = u.nmPessoa;
                //EC.Common.Session.edEmailProfissional = u.edEmailProfissional;
                //EC.Common.Session.edEmailPessoal = u.edEmailPessoal;
                //EC.Common.Session.edEmailPessoal = u.edEmailPessoal;
                //EC.Common.Session.icCoordenador = u.icCoordenador;
                //Session["sgCentroCusto"] = u.sgCentroCusto;

                Response.Write(string.Format("<script>window.parent.location.href='{0}';</script>", ResolveClientUrl("~/Default.aspx")));

            }
            else
            {
                btnLogin.Enabled = true;
                alert.Show("O Usuário ou a senha que você digitou não foi reconhecido(a). ");
                return;
            }
        }

        //protected void btnLogin_Click(object sender, EventArgs e)
        //{
        //    btnLogin.Enabled = false;
        //    string coAcesso = FormatLogin();
        //    string coSenha = txtPwd.Text;

        //    if (coAcesso.Length == 0)
        //    {
        //        btnOk.Enabled = true;
        //        alert.Show("Informe o código de acesso do usuário.");
        //        return;
        //    }

        //    if (coSenha.Length == 0)
        //    {
        //        btnOk.Enabled = true;
        //        alert.Show("Informe a senha de acesso do usuário.");
        //        return;
        //    }

        //    // VERIFICAR SE A SENHA NÃO É A PADRÃO
        //    AlertList err = new EC.DataContext.Controller.Sistema.Usuario().NewLogin(coAcesso, coSenha);

        //    if (err.HasAlert())
        //    {
        //        btnOk.Enabled = true;
        //        alert.Show(err[0].Key);
        //        return;
        //    }

            
        //    var u = new SGI.DataContext.Controller.Sistema.Usuario().BindByLogin(coAcesso);

        //    if (u == null)
        //    {
        //        btnOk.Enabled = true;
        //        alert.Show("O Usuário ou a senha que você digitou não foi reconhecido(a). ");
        //        return;
        //    }


        //    System.Web.HttpCookie cookie = new System.Web.HttpCookie(keyCookie, coAcesso);
        //    cookie.Expires = DateTime.Now.AddDays(30);
        //    Response.Cookies.Add(cookie);


        //    Session[Const.IDUSUARIO] = u.idUsuario;
        //    EC.Common.Session.idPessoa = u.idPessoa;
        //    EC.Common.Session.idPerfil = u.idPerfil;
        //    EC.Common.Session.coAcesso = u.coAcesso;
        //    EC.Common.Session.nmPessoa = u.nmPessoa;
        //    EC.Common.Session.edEmailProfissional = u.edEmailProfissional;
        //    EC.Common.Session.edEmailPessoal = u.edEmailPessoal;
        //    EC.Common.Session.edEmailPessoal = u.edEmailPessoal;
        //    EC.Common.Session.icCoordenador = u.icCoordenador;
        //    Session["sgCentroCusto"] = u.sgCentroCusto;

        //    Response.Write(string.Format("<script>window.parent.location.href='{0}';</script>", ResolveClientUrl("~/Default.aspx")));
        //}

        //private string FormatLogin()
        //{
        //    try
        //    {
        //        string coAcesso = txtcoAcesso.Text.Trim();

        //        if (Library.isNumeric(coAcesso))
        //        {
        //            if (coAcesso.Trim().Length < 6)
        //            {
        //                coAcesso = ((string)("000000")).Substring(0, 6 - coAcesso.Length) + coAcesso.Trim();
        //            }
        //        }

        //        txtcoAcesso.Text = coAcesso;

        //        return coAcesso;
        //    }
        //    catch
        //    {
        //        alert.Text = "Erro no formato do código de acesso.";
        //        alert.Show();
        //        return txtLogin.Text;
        //    }
        //}
    }
}