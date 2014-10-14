using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EC.Negocio;
using EC.Modelo;

namespace UI.Web.EC.Paginas
{
    public partial class ConsultarReuniao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            {
                if (IsPostBack)
                    return;

               CarregarReuniao();
               CarregarTipoReuniao();
       
            }
        }

        private void CarregarTipoReuniao()
        {
            ddlTipoReuniao.DataSource = NReuniao.ConsultarTipoReuniao();
            ddlTipoReuniao.DataTextField = "DESCRICAO";
            ddlTipoReuniao.DataValueField = "ID_TIPOREUNIAO";
            ddlTipoReuniao.DataBind();

            ddlTipoReuniao.Items.Insert(0, new ListItem("", ""));
        }

        private void CarregarReuniao()
        {
            REUNIAO reuniao = new REUNIAO();
            grdReuniao.DataSource = NReuniao.ConsultarReuniao(reuniao);
            grdReuniao.DataBind();

        }

        protected void btNovo_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgendaReuniao.aspx", true);
        }

        protected void btConsultar_Click(object sender, EventArgs e)
        {
             {
            if (ddlTipoReuniao.SelectedValue == "" && ddlSemestre.SelectedValue == "")
            {
                //MENSAGEM
            }
            else
            {
               REUNIAO reuniao = new REUNIAO();
                if (ddlTipoReuniao.SelectedValue != "")
                    reuniao.ID_TIPOREUNIAO = Convert.ToInt32(ddlTipoReuniao.SelectedValue);
        
                var lista = NReuniao.ConsultarReuniao(reuniao);

                grdReuniao.DataSource = lista;
                grdReuniao.DataBind();
            }            
        }
        }
    }
}