using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
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
                
                List<QuestaoHelper> questaoHelper = new List<QuestaoHelper>();
                foreach (var item in questao){
                    QuestaoHelper obj = new QuestaoHelper();
                    obj.QUESTAO = item;
                    obj.Imagem = Library.ConvertByteToImage( item.IMAGEM);
                    questaoHelper.Add(obj);
                }

                // Montar questão e respostas em um campo
                int i = 0;
                char[] letrasResposta = { 'A', 'B', 'C', 'D', 'E' };
                int countResposta;
                foreach (var item in questao)
                {
                    StringBuilder strResposta = new StringBuilder();
                    
                    strResposta.Append("\n Questão " + (i + 1) + " - " + item.DESCRICAO + "\n ");

                    if (item.IMAGEM != null && item.IMAGEM.ToArray().Count() > 0)
                        strResposta.Append("\n @&imagem \n");

                    countResposta = 0; 
                    foreach (var itemResposta in item.RESPOSTA)
                    {
                        strResposta.Append("\n (" + letrasResposta[countResposta] + ") " + itemResposta.TEXTO);
                        countResposta++;
                    }
                    questao[i].DESCRICAO =  strResposta.ToString();
                    
                    i++;
                }
                
                //var questaoDataTable = Library.ConvertListToDataTable(questao);

                questao

                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("Questoes", questao));

                reportViewer.DocumentMapCollapsed = true;
                Utils.RenderReportToPDF(Context, reportViewer, "Prova");
            }
        }
    }
}