using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EC.Modelo;
using EC.Negocio;
using EC.Common;

namespace UI.Web.EC.Paginas
{
    public partial class ConsultarAta : System.Web.UI.Page
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

            }
        }
        
        
        private void CarregarReuniao()
        {
            ddlReuniao.DataSource = NAcao.ConsultarReuniao();
            ddlReuniao.DataTextField = "TITULO";
            ddlReuniao.DataValueField = "ID_REUNIAO";
            ddlReuniao.DataBind();

            ddlReuniao.Items.Insert(0, new ListItem("Selecione", ""));
        }

        protected void ddlReuniao_SelectedIndexChanged(object sender, EventArgs e)
        {
            var reuniaoAta = NReuniaoAta.ConsultarByReuniao(Library.ToInteger(ddlReuniao.SelectedValue));
            if (reuniaoAta != null)
            {
                lblReuniao.Text = reuniaoAta.REUNIAO.TITULO;
                lblResponsavel.Text = reuniaoAta.FUNCIONARIO.PESSOA.NOME;
                lblReuniao.Text = reuniaoAta.DATA_FECHAMENTO.ToDate().Day.ToString() + "/" + reuniaoAta.DATA_FECHAMENTO.ToDate().Month.ToString() + "/" + reuniaoAta.DATA_FECHAMENTO.ToDate().Year.ToString();
                hddIdAta.Value = reuniaoAta.ID_REUNIAO.ToString();
                pnlAta.Visible = true;
            }
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Paginas/AtaReuniao.aspx?idReuniao=" + hddIdAta.Value);
        }
    }
}

   