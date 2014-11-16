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
    public partial class ProfessorQuestoes : System.Web.UI.Page
    {

        private int idAmc
        {
            get { return Library.ToInteger(ViewState["idAmc"]); }
            set { ViewState["idAmc"] = value; }
        }

        private int[] idFuncionarioProfessorArray;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["idAmc"] != null)
                    idAmc = Request.QueryString["idAmc"].ToInt32();
            }

            if (idAmc > 0)
            {
                GeraRelatorio();
                GeraRelatorioQuestoesProfessorPorProva();
            }
        }

        private void GeraRelatorio()
        {
            var funcrionarioProfessor = NFuncionario.ConsultarProfessor();
            var questoes = NQuestao.ConsultarQuestaoByAmc(idAmc);
            
            int qtdeQuestoes = questoes.Count;
            int dtdeProfessor = funcrionarioProfessor.Count;

            double[] yValues = new double[dtdeProfessor];
            string[] xValues = new string[dtdeProfessor];
            idFuncionarioProfessorArray = new int[dtdeProfessor];
            

            int contador = 0;
            foreach (var funcionario in funcrionarioProfessor)
            {

                // Quantidade de perguntas por professor em prova
                double qtdeQuestaoProfessor = 0;
                foreach (var questao in questoes)
                {
                    if (questao.FUNCIONARIO.ID_FUNCIONARIO == funcionario.ID_FUNCIONARIO)
                        qtdeQuestaoProfessor++;
                }

                yValues[contador] = qtdeQuestaoProfessor == 0 ? 0.00 : Math.Round((qtdeQuestaoProfessor * 100) / qtdeQuestoes, 2);
                xValues[contador] = funcionario.PESSOA.NOME;
                idFuncionarioProfessorArray[contador] = funcionario.ID_FUNCIONARIO;

                contador++;
            }
            
            chartProfessorQuestao.Series["ProfessorQuestao"].IsValueShownAsLabel = true;
            chartProfessorQuestao.Series["ProfessorQuestao"].IsXValueIndexed = true;

            chartProfessorQuestao.Series["ProfessorQuestao"].Points.DataBindXY(xValues, yValues);
            //for (int i = 0; i < xValues.Length; i++)
            //{
            //    // Add series.
            //    Series series = this.chartProfessorQuestao.Series.Add(xValues[i]);

            //    // Add point.
            //    series.Points.Add(yValues[i]);
            //    series.Url = "www.bing.com";
            //}

                       
            var amc = NAmc.ConsultarById(idAmc);
            Title title = new Title("Percentual de Questões Criadas por Professor na AMC " +
                amc.SEMESTRE.SEMESTRE1 + "º sem/" + amc.SEMESTRE.ANO +
                " (" + qtdeQuestoes + " Questões)", Docking.Top, new Font("Verdana", 12), Color.Black);
            chartProfessorQuestao.Titles.Add(title);

            Random random = new Random();
            foreach (var item in chartProfessorQuestao.Series[0].Points)
            {
                Color c = Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
                item.Color = c;
            }
        }

        private void GeraRelatorioQuestoesProfessorPorProva()
        {
            var funcrionarioProfessor = NFuncionario.ConsultarProfessor();
            var questoes = NQuestao.ConsultarQuestaoProvaByAmc(idAmc);

            int qtdeQuestoes = questoes.Count;

            double[] yValues = new double[funcrionarioProfessor.Count];
            string[] xValues = new string[funcrionarioProfessor.Count];

            int contador = 0;
            foreach (var funcionario in funcrionarioProfessor)
            {

                // Quantidade de perguntas por professor em prova
                double qtdeQuestaoProfessor = 0;
                foreach (var questao in questoes)
                {
                    if (questao.FUNCIONARIO.ID_FUNCIONARIO == funcionario.ID_FUNCIONARIO)
                        qtdeQuestaoProfessor++;
                }

                yValues[contador] = qtdeQuestaoProfessor == 0 ? 0.00 : Math.Round((qtdeQuestaoProfessor * 100) / qtdeQuestoes, 2);
                xValues[contador] = funcionario.PESSOA.NOME;

                contador++;
            }

            chartProfessorQuestao2.Series["ProfessorQuestao2"].IsValueShownAsLabel = true;
            chartProfessorQuestao2.Series["ProfessorQuestao2"].Points.DataBindXY(xValues, yValues);

            var amc = NAmc.ConsultarById(idAmc);
            Title title = new Title("Percentual de Questões por Professor Utilizadas na Prova da " +
                amc.SEMESTRE.SEMESTRE1 + "º sem/" + amc.SEMESTRE.ANO +
                " (" + qtdeQuestoes + " Questões)", Docking.Top, new Font("Verdana", 12), Color.Black);
            chartProfessorQuestao2.Titles.Add(title);


            Random random = new Random();
            foreach (var item in chartProfessorQuestao2.Series[0].Points)
            {
                Color c = Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
                item.Color = c;
            }
        }

        protected void chartProfessorQuestao_Load(object sender, ChartPaintEventArgs e)
        {
            if (e.ChartElement is Series)
            {

                Series series = (Series)e.ChartElement;
                System.Drawing.PointF position = System.Drawing.PointF.Empty;
                //Attach a href attribute to series and this will show the hand symbol
                series.MapAreaAttributes = string.Format("onclick=\"#\" href=\"www.bing.com\"\"alt=\"Show Report\"", "");
            }
        }

        protected void chartProfessorQuestao_DataBound(object sender, EventArgs e)
        {
            int points = chartProfessorQuestao.Series[0].Points.Count;
            for (int i = 0; i < points; i++ )
            {
                int idFuncionarioProfessor = idFuncionarioProfessorArray[i]; //chartProfessorQuestao.Series[0].Points[i].AxisLabel;
                chartProfessorQuestao.Series[0].Points[i].Url = "~/Paginas/BancoQuestao.aspx?idFuncionarioProfessor=" + idFuncionarioProfessor + "&idAmc=" + idAmc;
            }
        }

        protected void chartProfessorQuestao2_DataBound(object sender, EventArgs e)
        {
            int points = chartProfessorQuestao2.Series[0].Points.Count;
            for (int i = 0; i < points; i++)
            {
                int idFuncionarioProfessor = idFuncionarioProfessorArray[i]; //chartProfessorQuestao.Series[0].Points[i].AxisLabel;
                chartProfessorQuestao2.Series[0].Points[i].Url = "~/Paginas/BancoQuestao.aspx?idFuncionarioProfessor=" + idFuncionarioProfessor + "&idAmc=" + idAmc;
            }
        }

    }
}