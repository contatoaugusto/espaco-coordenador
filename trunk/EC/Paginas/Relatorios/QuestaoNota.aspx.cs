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
                GeraRelatorio();
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

            Random random = new Random();
            foreach (var item in chart1.Series[0].Points)
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
                string mencao = chart1.Series[0].Points[i].AxisLabel;
                chart1.Series[0].Points[i].Url = "~/Paginas/QuestaoNotaAluno.aspx?mencao=" + mencao + "&idAmc=" + idAmc;
            }
        }

    }
}