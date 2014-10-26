using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EC.Negocio;
using EC.Common;
using EC.Modelo;
using Microsoft.Reporting.WebForms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace UI.Web.EC.Paginas
{
    public partial class Prova : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            CarregarAmc();
        }


        private void CarregarAmc()
        {

            foreach (var item in NQuestão.ConsultarAmc())   
            {
                ddlAmc.Items.Add(new ListItem(item.SEMESTRE + "º sem/" + item.ANO, item.ID_AMC.ToString()));
            }
        }

        protected void ddlAmc_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SqlDataSource_Questoes.SelectParameters["ID_AMC"].DefaultValue = ddlAmc.SelectedValue.ToString();
        }


        protected void btnGerarProva_Click(object sender, EventArgs e)
        {
            var idCurso = ((SessionUsuario)Session["USUARIO"]).IdCurso;
            var idFuncionario = ((SessionUsuario)Session["USUARIO"]).USUARIO.FUNCIONARIO.ID_FUNCIONARIO;
            // Selecionar as questões dessa AMC e do curso do usuario logado (professor ou coordenador)
            NProva.GerarProvaRandomicamente(Library.ToInteger(ddlAmc.SelectedValue), idCurso, Library.ToInteger(txtQuantidadeQuestoes.Text), idFuncionario);
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Imprimir")
            {
                var reportViewer = new ReportViewer();
                string reportPath = string.Empty;
                reportPath = Context.Server.MapPath("") + @"\Relatorios\Prova.rdlc";
                reportViewer.LocalReport.ReportPath = reportPath;
                reportViewer.LocalReport.EnableExternalImages = true;

                List<QUESTAO> questao = NQuestão.ConsultarQuestaoByProva(e.CommandArgument.ToInt32());
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("Questoes", questao));

                reportViewer.DocumentMapCollapsed = true;
                Utils.RenderReportToPDF(Context, reportViewer, "historicoescolar");
            }
        }
    }
}