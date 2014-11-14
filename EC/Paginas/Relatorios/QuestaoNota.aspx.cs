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

            GeraRelatorio();
        }

        private void GeraRelatorio()
        {
            if (idAmc > 0)
            {
                var notas = NFuncionario.ConsultarProfessor();
                var questoes = NQuestao.ConsultarQuestaoProvaByAmc(idAmc);
                var alunosMatrcicula = NAlunoAmc.ConsultarByAmc(idAmc);


                int[] yValues = new int[4];
                string[] xValues = new string[4];

                string []mencoes = {"SS","MS","MM","MI"};

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
        }

    }
}