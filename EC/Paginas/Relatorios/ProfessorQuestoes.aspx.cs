using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EC.Negocio;
using System.Web.UI.DataVisualization.Charting;

namespace UI.Web.EC.Paginas.Relatorios
{
    public partial class ProfessorQuestoes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            var funcrionarioProfessor = NFuncionario.ConsultarProfessor();
            var questoes = NQuestao.ConsultarQuestao();


            int[] yValues = new int[funcrionarioProfessor.Count];
            string[] xValues = new string[funcrionarioProfessor.Count];

            int contador = 0;
            foreach (var funcionario in funcrionarioProfessor)
            {

                // Quantidade de perguntas por professor em prova
                int qtdeQuestaoProfessor = 0;
                foreach (var questao in questoes)
                {
                    if (questao.FUNCIONARIO.ID_FUNCIONARIO == funcionario.ID_FUNCIONARIO)
                        qtdeQuestaoProfessor++;
                }
                yValues[contador] = qtdeQuestaoProfessor;
                xValues[contador] = funcionario.PESSOA.NOME;

                contador++;
            }

            chartProfessorQuestao.Series["ProfessorQuestao"].IsValueShownAsLabel = true;
            chartProfessorQuestao.Series["ProfessorQuestao"].Points.DataBindXY(xValues, yValues);

        }

    }
}