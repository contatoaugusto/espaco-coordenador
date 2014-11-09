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
            {
                if (IsPostBack)
                    return;
               
                CarregarReuniao();
                CarregarSemestre();
               
            }
        }

        private void CarregarSemestre()
        {
            ddlSemestre.DataSource = NSemestre.Consultar();
            ddlSemestre.DataTextField = "";
            ddlSemestre.DataValueField = "ID_SEMESTRE";
            ddlSemestre.DataBind();

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