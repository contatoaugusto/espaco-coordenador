using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EC.Common;
using EC.Negocio;
using EC.Modelo;

namespace UI.Web.EC.Paginas
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Controle Acesso
            int []cargoComAcesso = {2};
            string mensagem = ControleAcesso.verificaAcesso(cargoComAcesso);
            if (!mensagem.IsNullOrEmpty())
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "Alert", "<script>alert('" + mensagem + "');location.replace('../default.aspx')</script>");
            }

            if (!IsPostBack)
            {
                BindControl();
                BindDataGrid();
            }
         }

        public void BindControl()
        {
            CarregaSemestreCorrente();
        }

        public void BindDataGrid()
        {
            GridView_AMC.DataSource = NAmc.ConsultarAmc();
            GridView_AMC.DataBind();
        }


        protected void CarregaSemestreCorrente()
        {

            var semestre = NSemestre.ConsultarAtivo();

            if (semestre != null && semestre.ID_SEMESTRE > 0)
            {
                pnlAmc.Visible = true;
                lblSemestreCorrente.Text = semestre.SEMESTRE1 + "º sem/" + semestre.ANO;
                lblCursoUnico.Text = ((SessionUsuario)Session[Const.USUARIO]).NmCurso;
            }
            else
                pnlAmc.Visible = false;
        }

        protected void Salvar_Click1(object sender, EventArgs e)
        {
            if (Utils.GetUsuarioLogado().IdSemestre == 0 ||
                NAmc.ConsultarAmcBySemestre(Utils.GetUsuarioLogado().IdSemestre).Count > 0)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "Alert", "<script>alert('"+ Const.MENSAGEM_CADASTRO_REPETIDO +"');</script>");
                return;
            }
            else
            {
                AMC amc = new AMC();

                amc.ID_SEMESTRE = ((SessionUsuario)Session[Const.USUARIO]).IdSemestre;
                amc.DATA_APLICACAO = Library.ToDate(dtAplicacao.Text);

                if (NAmc.Salvar(amc))
                    ClientScript.RegisterClientScriptBlock(GetType(), "Alert", "<script>alert('" + Const.MENSAGEM_INCLUSAO_SUCESSO + "');</script>");
                else
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "Alert", "<script>alert('" + Const.MENSAGEM_CADASTRO_REPETIDO + "');</script>");
                    return;
                }
            }    
        }
    }
}