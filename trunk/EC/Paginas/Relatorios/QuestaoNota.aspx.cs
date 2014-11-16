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

namespace UI.Web.EC.Paginas.Relatorios
{
    public partial class QuestaoNota : System.Web.UI.Page
    {

        private int idAmc
        {
            get { return Library.ToInteger(ViewState["idAmc"]); }
            set { ViewState["idAmc"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Request.QueryString["idAmc"] != null)
                    idAmc = Request.QueryString["idAmc"].ToInt32();
            }

            if (idAmc > 0)
                GeraRelatorio();
        }

        private void GeraRelatorio()
        {
            var notas = NFuncionario.ConsultarProfessor();
            var questoes = NQuestao.ConsultarQuestaoProvaByAmc(idAmc);
            var alunosMatrcicula = NAlunoAmc.ConsultarByAmc(idAmc);


            int[] yValues = new int[4];
            string[] xValues = new string[4];

            string[] mencoes = ConfigurationManager.AppSettings["Mencoes"].Replace(" ", string.Empty).Split(',');//"SS", "MS", "MM", "MI" };
            
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

    }
}