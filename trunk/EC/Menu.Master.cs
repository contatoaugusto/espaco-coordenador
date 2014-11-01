using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EC.Modelo;
using EC.Negocio;
using EC.Common;

namespace UI.Web.EC
{
    public partial class Menu : System.Web.UI.MasterPage
    {

        protected override void OnInit(EventArgs e)
        {
            base.OnPreRender(e);

            Literal l = new Literal();
            l.Text = "<script type=\"text/javascript\" src=\"" + ResolveClientUrl("~/Includes/jQuery.js") + "\"></script>";
            Page.Header.Controls.Add(l);

            l = new Literal();
            l.Text = "<script type=\"text/javascript\" src=\"" + ResolveClientUrl("~/Includes/basic.js") + "\"></script>";
            Page.Header.Controls.Add(l);


            l = new Literal();
            l.Text = "<script type=\"text/javascript\" src=\"" + ResolveClientUrl("~/Templates/Clean/Includes/Initialize.js") + "\"></script>";
            Page.Header.Controls.Add(l);

            SessionUsuario sessionUsuario = (SessionUsuario)Session[Const.USUARIO];
            
            if (sessionUsuario == null)
                Response.Redirect ("~/Login.aspx");

            lblMatricula.Text = sessionUsuario.USUARIO.FUNCIONARIO.MATRICULA.ToString();
            lblNomeCoordenador.Text = sessionUsuario.USUARIO.FUNCIONARIO.PESSOA.NOME;
            lblCursoUnico.Text = sessionUsuario.NmCurso;
            
            // Semestre corrente
            var semestre = NSemestre.ConsultarAtivo();
            if (semestre != null)
                lblAnoSemestreCorrente.Text = semestre.SEMESTRE1 + "º sem/" + semestre.ANO;
            else
                lblAnoSemestreCorrente.Text = "Não cadastrado";

            // Cursos do coordenador
            List<CURSO> listCurso = NCursoCoordenador.ConsultarCursoByCoordenador(sessionUsuario.USUARIO.FUNCIONARIO.ID_FUNCIONARIO);
            if (listCurso.Count > 0 && listCurso != null)
            {

                StringBuilder oStr = new StringBuilder();

                oStr.Append("<li class='mega'><a id='curso' href='#'><span>Cursos <img src='/App_Themes/Default/Images/set-meusdados.png' alt=\"\" /></span></a>");
                oStr.Append("<div style=\"width:auto\"><ul>");
                foreach (CURSO item in listCurso)
                {
                    oStr.Append("<br/><li'><a href='#'><span>" + item.DESCRICAO + "</span></a></li>");
                }
                oStr.Append("</ul></div></li>");
                lblCurso.Text = oStr.ToString();
                lblCurso.Visible = true;
            }
            //btnPesquisar.ImageUrl = Utils.GetUrlImageTheme("btn-pesquisar.gif");
            //btnPesquisar.OnClientClick = "searchInBing('" + ResolveClientUrl("~/pesquisa.aspx") + "', '" + txtdePesquisa.ClientID + "');return false;";

            imgAluno.ImageUrl = string.Format("content/avatar.ashx?{0}", sessionUsuario.USUARIO.ID_USUARIO);
            imgAluno.OnClientClick = string.Format("redirect('{0}');return false;", ResolveClientUrl("~/Coordenador/avatar.aspx"));


        }

        private void selecionaCurso() { 
        
        
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            AddScriptToHeader("/scripts/jquery-1.8.3.js",
                "/scripts/jquery.hoverintent.js",
                "/scripts/jquery.poshytip.js",
                "/scripts/jquery.watermark.js",
                "/scripts/preloadCssImages.jQuery_v5.js",
                "/scripts/encoder.js",
                "/scripts/modal.js",
                "/scripts/api.js",
                "/scripts/global.js",
                "/scripts/core.js",
                "/scripts/script.js");

            if (Request.Url.AbsolutePath.ToLower().IndexOf("/Paginas/Default.aspx") > -1)
            {
                AddScriptToHeader("/Scripts/jquery-ui-custom.min.js");

                AddCssToHeader("/includes/smoothness/jquery-ui.custom.css");

            }

        }

        public string AccountName
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["AccountName"]; }
        }

        public string TokenAccess
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["TokenAccess"]; }
        }

        private void AddScriptToHeader(params string[] items)
        {
            foreach (var item in items)
            {
                Page.Header.Controls.Add(new Literal
                {
                    Text = string.Format("<script src=\"{0}\" type=\"text/javascript\"></script>", item)
                });
            }
        }

        private void AddCssToHeader(params string[] items)
        {
            foreach (var item in items)
            {
                Page.Header.Controls.Add(new Literal
                {
                    Text = string.Format("<link href=\"{0}\" rel=\"stylesheet\" type=\"text/css\" />", item)
                });
            }
        }

    }
}