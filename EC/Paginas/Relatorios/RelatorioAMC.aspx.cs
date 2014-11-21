using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EC.Negocio;
using EC.Modelo;
using EC.Common;

namespace UI.Web.EC.Paginas
{
    public partial class RelatorioAMC : System.Web.UI.Page
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
            {
                CarregarAmc();
                
            }
        }
          

        private void CarregarAmc()
        {
            var lista = NAmc.ConsultarAmc();
            ddlAmc.Items.Clear();
            ddlAmc.Items.Add(new ListItem("Selecione", ""));

            foreach (AMC obj in lista)
            {
                //if (obj.SEMESTRE.ATIVO)
                ddlAmc.Items.Add(new ListItem(obj.SEMESTRE.SEMESTRE1 + "º sem/" + obj.SEMESTRE.ANO, obj.ID_AMC.ToString()));
            }
        }

        protected void ddlTipoRelatorio_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void btConsultar_Click(object sender, EventArgs e)
        {
            int idAmc = ddlAmc.SelectedValue.ToInt32();
            int valorSelecionado = ddlTipoRelatorio.SelectedValue.ToInt32();
            string pagina = "";

            switch (valorSelecionado)
            {
                case 1:
                    pagina = "ProfessorQuestoes.aspx?idAmc=" + idAmc;
                    break;
                case 2:
                    pagina = "QuestaoNota.aspx?idAmc=" + idAmc;
                    break;
                case 3:
                    pagina = "QuestaoAproveitamento.aspx?idAmc=" + idAmc;
                    break;
            }

            Response.Redirect("~/Paginas/Relatorios/" + pagina);

        }
    }
}