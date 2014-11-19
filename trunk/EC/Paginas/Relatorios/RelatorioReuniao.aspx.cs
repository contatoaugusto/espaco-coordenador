using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EC.Modelo;
using EC.Negocio;
using EC.Common;

namespace UI.Web.EC.Paginas
{
    public partial class RelatorioReuniao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Controle Acesso
            int[] cargoComAcesso = { 2 };
            string mensagem = ControleAcesso.verificaAcesso(cargoComAcesso);
            if (!mensagem.IsNullOrEmpty())
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "Alert", "<script>alert('" + mensagem + "');location.replace('../default.aspx')</script>");
            }
            {
                if (IsPostBack)
                    return;
               
                CarregarReuniao();
                CarregarSemestre();
               
            }
        }

        private void CarregarSemestre()
        {
            var lista = NSemestre.Consultar();
            foreach (var semestre in lista)
            {
                string descricao = string.Format("{0}º Sem / {1}", semestre.SEMESTRE1, semestre.ANO);
                ddlSemestre.Items.Add(new ListItem(descricao, semestre.ID_SEMESTRE.ToString()));
            }
            ddlSemestre.Items.Insert(0, new ListItem("Selecione", ""));
        }

        private void CarregarReuniao()
        {
            ddlReuniao.DataSource = NAcao.ConsultarReuniao();
            ddlReuniao.DataTextField = "TITULO";
            ddlReuniao.DataValueField = "ID_REUNIAO";
            ddlReuniao.DataBind();

            ddlReuniao.Items.Insert(0, new ListItem("Selecione", ""));
        }
    }
}