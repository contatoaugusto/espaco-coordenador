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
    public partial class ConsultarEvento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
            {
                CarregarPessoa();
                CarregarTipoEvento();
                CarregarEvento();

            }
        }
          
        
        private void CarregarTipoEvento()
        {
            ddlTipoEvento.DataSource = NEvento.ConsultarTipoEvento();
            ddlTipoEvento.DataTextField = "DESCRICAO";
            ddlTipoEvento.DataValueField = "ID_TIPOEVENTO";
            ddlTipoEvento.DataBind();

            ddlTipoEvento.Items.Insert(0, new ListItem("Selecione", ""));
        }


        private void CarregarPessoa()
        {
            ddlPessoa.DataSource = NAcao.ConsultarPessoa();
            ddlPessoa.DataTextField = "NOME";
            ddlPessoa.DataValueField = "ID_PESSOA";
            ddlPessoa.DataBind();
            ListItem r = new ListItem();
            r.Text = "";
            r.Value = "";
            ddlPessoa.Items.Insert(0, new ListItem("Selecione", ""));
        }

        private void CarregarEvento()
        {
            EVENTO evento = new EVENTO();
            grdEvento.DataSource = NEvento.ConsultarEvento(evento);
            grdEvento.DataBind();

        }

     
        protected void btnNovo_Click(object sender, EventArgs e)
        {
            Response.Redirect("Evento.aspx", true);
        }

        protected void btConsultar_Click(object sender, EventArgs e)
        {
           
            if (ddlPessoa.SelectedValue == "" && ddlTipoEvento.SelectedValue == "")
            {
                //MENSAGEM
            }
            else
            {
               EVENTO evento = new EVENTO();
                if (ddlPessoa.SelectedValue != "")
                    evento.ID_PESSOA = Convert.ToInt32(ddlPessoa.SelectedValue);
                if (ddlPessoa.SelectedValue != "")
                    evento.ID_TIPOEVENTO = Convert.ToInt32(ddlTipoEvento.SelectedValue);
             
                var lista = NEvento.ConsultarEvento(evento);

                grdEvento.DataSource = lista;
                grdEvento.DataBind();
      }
        }

        protected void btnovo_Click(object sender, EventArgs e)
        {
            Response.Redirect("Evento.aspx", true);
        }
     }

   }
   

