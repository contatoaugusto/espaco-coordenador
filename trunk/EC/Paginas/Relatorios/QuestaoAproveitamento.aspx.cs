using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EC.Negocio;
using System.Web.UI.DataVisualization.Charting;
using EC.Common;

namespace UI.Web.EC.Paginas.Relatorios
{
    public partial class QuestaoAproveitamento : System.Web.UI.Page
    {

        private int idAmc
        {
            get { return Library.ToInteger(ViewState["idAmc"]); }
            set { ViewState["idAmc"] = value; }
        }

        private int[] idQuestaoArray;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Controle Acesso
            int[] cargoComAcesso = { 2 };
            string mensagem = ControleAcesso.verificaAcesso(cargoComAcesso);
            if (!mensagem.IsNullOrEmpty())
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "Alert", "<script>alert('" + mensagem + "');location.replace('../default.aspx')</script>");
            }

            if (!IsPostBack)
            {
                if (Request.QueryString["idAmc"] != null)
                    idAmc = Request.QueryString["idAmc"].ToInt32();
            }

            if (idAmc > 0)
            {
                GeraRelatorio();
            }
        }

        private void GeraRelatorio()
        {
            int idCursoCoordenadorSessao = ((SessionUsuario)Session[Const.USUARIO]).IdCurso;

            var questoesProva = NQuestao.ConsultarQuestaoProvaByAmcCurso(idAmc, idCursoCoordenadorSessao);
            var alunoAMCQuestao = NAlunoAmcQuestao.ConsultarByAmc(idAmc);

            int btdeQuestoes = questoesProva.Count;
            int[] yValues = new int[btdeQuestoes];
            int[] yValuesErros = new int[btdeQuestoes];
            int[] xValues = new int[btdeQuestoes];
            
            idQuestaoArray = new int[btdeQuestoes];
            

            int contador = 0;
            foreach (var questao in questoesProva)
            {

                // Quantidade de acertos por questão
                int qtdeAcertoQuestao = 0;
                int qtdeErrosQuestao = 0;
                foreach (var alunoQuestao in alunoAMCQuestao)
                {
                    if (alunoQuestao.ID_QUESTAO == questao.ID_QUESTAO &&  alunoQuestao.ACERTO == true)
                        qtdeAcertoQuestao++;
                    if (alunoQuestao.ID_QUESTAO == questao.ID_QUESTAO && alunoQuestao.ACERTO == false)
                        qtdeErrosQuestao++;
                }

                yValues[contador] = qtdeAcertoQuestao;
                yValuesErros[contador] = qtdeErrosQuestao;
                xValues[contador] = questao.NU_SEQUENCIA_PROVA.ToInt32();
                
                idQuestaoArray[contador] = questao.ID_QUESTAO;

                contador++;
            }

            chart1.Series["QuestaoAproveitamento"].IsValueShownAsLabel = true;
            chart1.Series["QuestaoAproveitamento"].IsXValueIndexed = true;

            chart1.Series["QuestaoAproveitamento"].Points.DataBindXY(xValues, yValues);
             
            // Erros
            chart2.Series["QuestaoAproveitamento"].IsValueShownAsLabel = true;
            chart2.Series["QuestaoAproveitamento"].IsXValueIndexed = true;
            chart2.Series["QuestaoAproveitamento"].Points.DataBindXY(xValues, yValuesErros);

            var amc = NAmc.ConsultarById(idAmc);
            Title title = new Title("Quantidade de acertos por Questao na AMC " +
                amc.SEMESTRE.SEMESTRE1 + "º sem/" + amc.SEMESTRE.ANO +
                " (" + btdeQuestoes + " Questões)", Docking.Top, new Font("Verdana", 12), Color.Black);
            chart1.Titles.Add(title);

            Title title2 = new Title("Quantidade de erros por Questao na AMC " +
                amc.SEMESTRE.SEMESTRE1 + "º sem/" + amc.SEMESTRE.ANO +
                " (" + btdeQuestoes + " Questões)", Docking.Top, new Font("Verdana", 12), Color.Black);
            chart2.Titles.Add(title2);

            ChartColor(chart1);
            ChartColor(chart2);
        }

        private void ChartColor(Chart chart)
        {
            Random random = new Random();
            foreach (var item in chart.Series[0].Points)
            {
                Color c = Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
                item.Color = c;
            }
        }

        
        protected void chart1_DataBound(object sender, EventArgs e)
        {
            int points = chart1.Series[0].Points.Count;
            for (int i = 0; i < points; i++)
            {
                int idQuestao = idQuestaoArray[i];
                chart1.Series[0].Points[i].Url = "~/Paginas/QUestaoAproveitamentoAlunos.aspx?idQuestao=" + idQuestao + "&idAmc=" + idAmc;
            }
        }

    }
}