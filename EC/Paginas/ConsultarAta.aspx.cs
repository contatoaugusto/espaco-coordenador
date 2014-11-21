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
            //ddlReuniao.DataSource = NAcao.ConsultarReuniao();

            var reunioes = NReuniao.Consultar();
            
            ddlReuniao.Items.Clear();
            ddlReuniao.Items.Add(new ListItem("Selecione", "0"));

            foreach (var reuniao in reunioes)
            {
                var ata = NReuniaoAta.ConsultarByReuniao(reuniao.ID_REUNIAO);
                if (ata != null)
                    ddlReuniao.Items.Add(new ListItem(reuniao.TITULO, reuniao.ID_REUNIAO.ToString()));
            }

            // ddlReuniao.Items.Insert(0, new ListItem("Selecione", ""));
        }

        protected void ddlReuniao_SelectedIndexChanged(object sender, EventArgs e)
        {
            var reuniaoAta = NReuniaoAta.ConsultarByReuniao(Library.ToInteger(ddlReuniao.SelectedValue));
            if (reuniaoAta != null)
            {
                lblReuniao.Text = reuniaoAta.REUNIAO.TITULO;
                lblResponsavel.Text = reuniaoAta.FUNCIONARIO.PESSOA.NOME;
                lblReuniao.Text = reuniaoAta.REUNIAO.DATAHORA.ToDate().Day.ToString() + "/" + reuniaoAta.REUNIAO.DATAHORA.ToDate().Month.ToString() + "/" + reuniaoAta.REUNIAO.DATAHORA.ToDate().Year.ToString();
                hddIdAta.Value = reuniaoAta.ID_REUNIAO.ToString();
                
                lblFechamento.Text = reuniaoAta.DATA_FECHAMENTO.ToDate().Year == 1900 ? "Sem fechamento" : reuniaoAta.DATA_FECHAMENTO.ToDate().Day.ToString() + "/" + reuniaoAta.DATA_FECHAMENTO.ToDate().Month.ToString() + "/" + reuniaoAta.DATA_FECHAMENTO.ToDate().Year.ToString();
                
                pnlAta.Visible = true;
            }
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Paginas/AtaReuniao.aspx?idReuniao=" + hddIdAta.Value);
        }
    }
}

   