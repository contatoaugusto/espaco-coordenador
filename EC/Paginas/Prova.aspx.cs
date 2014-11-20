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
using System.Data;
using System.ComponentModel;

namespace UI.Web.EC.Paginas
{
    public partial class Prova : System.Web.UI.Page
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

            CarregarAmc();
            CarregaSemestreCorrente();
            CarregarProva();
        }


        private void CarregarProva()
        {
            grvProva.DataSource = NProva.Consultar();
            grvProva.DataBind();
        }

        private void CarregarAmc()
        {
            var lista = NAmc.ConsultarAmc();
            ddlAmc.Items.Clear();
            ddlAmc.Items.Add(new ListItem("Selecione", ""));

            int idAMCAtivo = 0;

            foreach (AMC obj in lista)
            {
                if (obj.SEMESTRE.ATIVO)
                {
                    ddlAmc.Items.Add(new ListItem(obj.SEMESTRE.SEMESTRE1 + "º sem/" + obj.SEMESTRE.ANO, obj.ID_AMC.ToString()));
                    if (obj.SEMESTRE.ID_SEMESTRE == ((SessionUsuario)Session[Const.USUARIO]).IdSemestre )
                        idAMCAtivo = obj.ID_AMC;
                }
            }

            ddlAmc.SelectedValue = idAMCAtivo.ToString();
        }

        protected void ddlAmc_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SqlDataSource_Questoes.SelectParameters["ID_AMC"].DefaultValue = ddlAmc.SelectedValue.ToString();
        }


        protected void CarregaSemestreCorrente()
        {

            var semestre = NSemestre.ConsultarAtivo();

            if (semestre != null && semestre.ID_SEMESTRE > 0)
            {
                pnlProva.Visible = true;
                //lblSemestreCorrente.Text = semestre.SEMESTRE1 + "º sem/" + semestre.ANO;
                lblCursoUnico.Text = ((SessionUsuario)Session[Const.USUARIO]).NmCurso;
            }
            else
                pnlProva.Visible = false;
        }


        protected void btnGerarProva_Click(object sender, EventArgs e)
        {
            var idCurso = ((SessionUsuario)Session[Const.USUARIO]).IdCurso;
            var idFuncionario = ((SessionUsuario)Session[Const.USUARIO]).USUARIO.FUNCIONARIO.ID_FUNCIONARIO;
            // Selecionar as questões dessa AMC e do curso do usuario logado (professor ou coordenador)
            NProva.GerarProvaRandomicamente(Library.ToInteger(ddlAmc.SelectedValue), idCurso, 32, idFuncionario);
            Response.Redirect("Prova.aspx", true);
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

                List<QUESTAO> questao = NQuestao.ConsultarQuestaoByProva(e.CommandArgument.ToInt32());
                
                // Montar questão e respostas em um campo
                int i = 0;
                char[] letrasResposta = { 'A', 'B', 'C', 'D', 'E' };
                int countResposta;

                DataTable table = new DataTable();
               
                DataColumn column;
                column = new DataColumn("DESCRICAO", typeof(string)) ;//item.DESCRICAO.GetType());
                column.AllowDBNull = true;
                table.Columns.Add(column);

                column = new DataColumn("IMAGEM", typeof(byte[]));// item.IMAGEM.GetType());
                column.AllowDBNull = true;
                table.Columns.Add(column);

                column = new DataColumn("RESPOSTA", typeof(string)); //item.DESCRICAO.GetType());
                column.AllowDBNull = true;
                table.Columns.Add(column);
                
                
                foreach (var item in questao)
                {
                    StringBuilder strQuestao = new StringBuilder();
                    StringBuilder strResposta = new StringBuilder();

                    strQuestao.Append("\n\nQuestão " + (i + 1) + " - " + item.DESCRICAO);

                    countResposta = 0; 
                    foreach (var itemResposta in item.RESPOSTA)
                    {
                        strResposta.Append("\n (" + letrasResposta[countResposta] + ") " + itemResposta.TEXTO);
                        countResposta++;
                    }
                    
                    string path = new Uri(Utils.PathImagesCache + "imagem_questao/HTTTStatusCode.jpg").AbsoluteUri;
                    table.Rows.Add(strQuestao.ToString(), item.IMAGEM, strResposta.ToString());

                    i++;
                }


                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("Questoes", table));//questao));

                reportViewer.DocumentMapCollapsed = true;
                Utils.RenderReportToPDF(Context, reportViewer, "Prova");
            }


            if (e.CommandName == "Gabarito")
            {
                //var reportViewer = new ReportViewer();
                //string reportPath = string.Empty;
                //reportPath = Context.Server.MapPath("") + @"\Relatorios\Gabarito.rdlc";
                //reportViewer.LocalReport.ReportPath = reportPath;
                //reportViewer.LocalReport.EnableExternalImages = true;

                //List<QUESTAO> questao = NQuestao.ConsultarQuestaoByProva(e.CommandArgument.ToInt32());

                //// Montar questão e respostas em um campo
                //int i = 0;
                //char[] letrasResposta = { 'A', 'B', 'C', 'D', 'E' };
                
                //DataTable table = new DataTable();
                //DataColumn column;

                //// Número questão
                //column = new DataColumn("QUESTAO", typeof(int));
                //column.AllowDBNull = true;
                //table.Columns.Add(column);

                //foreach (var item in letrasResposta)
                //{
                //    column = new DataColumn("RESPOSTA_" + item, typeof(string));
                //    column.AllowDBNull = true;
                //    table.Columns.Add(column);
                //}
                
                //foreach (var item in questao)
                //{
                //    table.Rows.Add((i + 1), letrasResposta[0], letrasResposta[1], letrasResposta[2], letrasResposta[3], letrasResposta[4]);

                //    i++;
                //}

                //reportViewer.LocalReport.DataSources.Add(new ReportDataSource("Gabarito", table));//questao));

                //reportViewer.DocumentMapCollapsed = true;

                var reportViewer = new ReportViewer();
                string reportPath = string.Empty;
                reportPath = Context.Server.MapPath("") + @"\Relatorios\GabaritoDefinitivo.rdlc";
                reportViewer.LocalReport.ReportPath = reportPath;
                reportViewer.LocalReport.EnableExternalImages = true;

                List<QUESTAO> questao = NQuestao.ConsultarQuestaoByProva(e.CommandArgument.ToInt32());
                List<RESPOSTA> respostas = NResposta.ConsultarByProva(e.CommandArgument.ToInt32());
                
                char[] letrasResposta = { 'A', 'B', 'C', 'D', 'E' };

                DataTable table = new DataTable();
                DataColumn column;

                // Número questão
                column = new DataColumn("QUESTAO", typeof(int));
                column.AllowDBNull = true;
                table.Columns.Add(column);

                column = new DataColumn("RESPOSTA_CORRETA", typeof(string));
                column.AllowDBNull = true;
                table.Columns.Add(column);

                column = new DataColumn("DISCIPLINA", typeof(string));
                column.AllowDBNull = true;
                table.Columns.Add(column);

                int i = 0;
                
                foreach (var item in questao)
                {
                    int x = 0;
                    foreach (var resposta in respostas){
                        if (resposta.ID_QUESTAO == item.ID_QUESTAO)
                        {
                            if (resposta.RESPOSTA_CORRETA == true)
                            {
                                break;
                            }
                            x++;
                        }
                    }

                    table.Rows.Add((i + 1), letrasResposta[x], item.DISCIPLINA.DESCRICAO);

                    i++;
                }

                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("Gabarito", table));//questao));

                reportViewer.DocumentMapCollapsed = true;
                Utils.RenderReportToPDF(Context, reportViewer, "Gabarito");


            }
        }
    }
}