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
    public partial class RelatorioAcao : System.Web.UI.Page
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

            if (IsPostBack)
                return;

            CarregarSemestre();
        }

        private void CarregarSemestre()
        {
            ddlSemestre.DataSource = NSemestre.Consultar();
            ddlSemestre.DataTextField = "";
            ddlSemestre.DataValueField = "ID_SEMESTRE";
            ddlSemestre.DataBind();

            ddlSemestre.Items.Insert(0, new ListItem("Selecione", ""));
        }

       
        }


        }
    
