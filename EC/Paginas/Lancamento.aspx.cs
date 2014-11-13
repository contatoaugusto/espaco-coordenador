using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EC.Common;
using EC.Negocio;
using EC.Modelo;
using EC.UI.WebControls;

namespace UI.Web.EC.Paginas
{
    public partial class Lancamento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            CarregarListaAno();
            CarregarListaMes();
            CarregarListaDia();
            CarregarDisciplina();
            CarregarTipolancamento();
            CarregarTurma();
            //CarregarFuncionario();
           // CarregarLancamento();
           
            
        }

        private void CarregarListaDia()
        {
            List<string> ListaDia = new List<string>();
            for (int i = 1; i <= 31; i++)
            {
                ListaDia.Add(i.ToString());
            }

            ddlDia.DataSource = ListaDia;
            ddlDia.DataBind();
            ddlDia.Items.Insert(0, new ListItem("", ""));
        }

        private void CarregarListaMes()
        {
            List<string> ListaMes = new List<string>();
            for (int i = 1; i <= 12; i++)
            {
                ListaMes.Add(i.ToString());
            }

            ddlMes.DataSource = ListaMes;
            ddlMes.DataBind();
            ddlMes.Items.Insert(0, new ListItem("", ""));
        }

        private void CarregarListaAno()
        {
            List<string> ListaAno = new List<string>();
            for (int i = 2014; i <= DateTime.Now.Year; i++)
            {
                ListaAno.Add(i.ToString());
            }

            ddlAno.DataSource = ListaAno;
            ddlAno.DataBind();
            ddlAno.Items.Insert(0, new ListItem("", ""));
        }

       

        private void CarregarTurma()
        {
            var turmas = NLancamento.ConsultarTurma();

            ddlTurma.Items.Add(new ListItem("Selecione", "0"));
            foreach (var turma in turmas)
            {
               // ddlTurma.Items.Add(new ListItem(turma.SEMESTRE.SEMESTRE1 + "º sem/" + turma.SEMESTRE.ANO, turma.ID_TURMA.ToString()));
                ddlTurma.Items.Insert(0, new ListItem("Selecione", ""));
            }
          
        }

        private void CarregarDisciplina()
        {

            var disciplina = NDisciplina.ConsultarByCurso(Utils.GetUsuarioLogado().IdCurso);
            ddlDisciplina.Items.Clear();

            ddlDisciplina.Items.Add(new ListItem("Selecione", "0"));

            foreach (DISCIPLINA dis in disciplina)
            {
                ddlDisciplina.Items.Add(new ListItem(dis.DESCRICAO, dis.ID_DISCIPLINA.ToString()));
            }

            CarregarFuncionario(ddlDisciplina.SelectedValue.ToInt32());
        }


        private void CarregarFuncionario(int idDisciplina)
        {
            var lista = NDisciplina.ConsultarProfessorByDisciplina(idDisciplina); //NQuestão.ConsultarFuncionario();

            ddlFuncionario.Items.Clear();

            ddlFuncionario.Items.Add(new ListItem("Selecione", "0"));

            foreach (FUNCIONARIO func in lista)
            {
                ddlFuncionario.Items.Add(new ListItem(func.PESSOA.NOME, func.ID_FUNCIONARIO.ToString()));
            }
        }

        private void CarregarTipolancamento()
        {
            ddlTipolancamento.DataSource = NLancamento.ConsultarTipolancamento();
            ddlTipolancamento.DataTextField = "DESCRICAO";
            ddlTipolancamento.DataValueField = "ID_TIPOLANCAMENTO";
            ddlTipolancamento.DataBind();
            ddlTipolancamento.Items.Insert(0, new ListItem("Selecione", ""));
        }

       // private void CarregarLancamento()
        //{
         //   grdLancamento.DataSource = NLancamento.ConsultarLancamento();
          //  grdLancamento.DataBind();
      //  }

        protected void Button1_Click(object sender, EventArgs e)
        {
            LANCAMENTO lancamento = new LANCAMENTO();
            lancamento.ID_TIPOLANCAMENTO = int.Parse(ddlTipolancamento.SelectedValue);
            lancamento.ID_TURMA = int.Parse(ddlTurma.SelectedValue);
            lancamento.JUSTIFICATIVA = TxtJustificativa.Text;
            lancamento.PROVIDENCIA = TxtProvidencias.Text;
            lancamento.DATA_LANCAMENTO = new DateTime(int.Parse(ddlAno.Text), int.Parse(ddlMes.Text), int.Parse(ddlDia.Text));
            NLancamento.Salvar(lancamento);
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "Alert", "<script>alert('" + Const.MENSAGEM_INCLUSAO_SUCESSO + "'); history.go(-2);</script>");
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        { 
            Response.Redirect("ConsultarLancamento.aspx", true);
        }
        }
    }

