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
using System.Configuration;
using System.IO;

namespace UI.Web.EC.Paginas.Relatorios
{
    public partial class QuestaoNota : System.Web.UI.Page
    {

        private int idAmc
        {
            get { return Library.ToInteger(ViewState["idAmc"]); }
            set { ViewState["idAmc"] = value; }
        }

        private int[] idAlunoArray;
        string tmpChartName = "QuantidadeAcertos.jpg";

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
                GeraRelatorioQuantidadeAcertos();
            }
        }

        private void GeraRelatorio()
        {
            var notas = NFuncionario.ConsultarProfessor();
            var questoes = NQuestao.ConsultarQuestaoProvaByAmc(idAmc);
            var alunosMatrcicula = NAlunoAmc.ConsultarByAmc(idAmc);


            string[] mencoes = ConfigurationManager.AppSettings["Mencoes"].Replace(" ", string.Empty).Split(',');//"SS", "MS", "MM", "MI" };

            int qtdeMencoes = mencoes.Length;
            int[] yValues = new int[qtdeMencoes];
            string[] xValues = new string[qtdeMencoes];

            int contador = 0;
            foreach (var mencao in mencoes)
            {

                // Quantidade de perguntas por professor em prova
                int qtdePorMencao = 0;
                foreach (var alunoMatr in alunosMatrcicula)
                {
                    if (alunoMatr.NOTA.Equals(mencao))
                        qtdePorMencao++;
                }
                yValues[contador] = qtdePorMencao;
                xValues[contador] = mencao;

                contador++;
            }

            chart1.Series["serie1"].IsValueShownAsLabel = true;
            chart1.Series["serie1"].Points.DataBindXY(xValues, yValues);

            ChartColor(chart1);
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

        private void GeraRelatorioQuantidadeAcertos()
        {
            int idCursoCoordenadorSessao = ((SessionUsuario)Session[Const.USUARIO]).IdCurso;

            var alunoAMC = NAlunoAmc.ConsultarByAmc(idAmc);
            // var alunoAMCQuestao = NAlunoAmcQuestao.ConsultarByAmc(idAmc);

            int quantidade = alunoAMC.Count;
            int[] yValues = new int[quantidade];
            string[] xValues = new string[quantidade];

            idAlunoArray = new int[quantidade];


            int contador = 0;
            foreach (var aluno in alunoAMC)
            {

                // Quantidade de perguntas por professor em prova
                //int qtdeAcertoPorQuestao = 0;
                //foreach (var alunoQuestao in alunoAMCQuestao)
                //{
                //    if (alunoQuestao.ID_ALUNO_AMC == aluno.ID_ALUNO_AMC && alunoQuestao.ACERTO == true)
                //        qtdeAcertoPorQuestao++;
                //}

                yValues[contador] = aluno.NUMERO_ACERTOS.ToInt32(); // qtdeAcertoPorQuestao;
                xValues[contador] = aluno.ALUNO_MATRICULA.ALUNO.PESSOA.NOME;

                idAlunoArray[contador] = aluno.ALUNO_MATRICULA.ALUNO.PESSOA.ID_PESSOA;

                contador++;
            }

            chart2.Series["serie2"].IsValueShownAsLabel = true;
            chart2.Series["serie2"].IsXValueIndexed = true;

            chart2.Series["serie2"].Points.DataBindXY(xValues, yValues);

            var amc = NAmc.ConsultarById(idAmc);
            Title title = new Title("Quantidade de acertos por aluno AMC " +
                amc.SEMESTRE.SEMESTRE1 + "º sem/" + amc.SEMESTRE.ANO +
                " (" + quantidade + " Alunos)", Docking.Top, new Font("Verdana", 12), Color.Black);
            chart2.Titles.Add(title);

            ChartColor(chart2);

            string imgPath = HttpContext.Current.Request.PhysicalApplicationPath + tmpChartName;
            chart2.SaveImage(imgPath);
        }


        protected void chart1_DataBound(object sender, EventArgs e)
        {
            int points = chart1.Series[0].Points.Count;
            for (int i = 0; i < points; i++)
            {
                string mencao = chart1.Series[0].Points[i].AxisLabel;
                chart1.Series[0].Points[i].Url = "~/Paginas/QuestaoNotaAluno.aspx?mencao=" + mencao + "&idAmc=" + idAmc;
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            string imgPath2 = Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/QuantidadeAcertos.jpg");
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment; filename=test.xlsx;");
            StringWriter stringWrite = new StringWriter();
            HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            string headerTable = @"<Table><tr><td><img src='" + imgPath2 + @"' \></td></tr></Table>";
            Response.Write(headerTable);
            Response.Write(stringWrite.ToString());
            Response.End();

        }
    }
}