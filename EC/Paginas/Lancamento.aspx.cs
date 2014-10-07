using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EC.Dado;
using EC.Modelo;
using EC.Negocio;

namespace EC.Paginas
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
            CarregarPessoa();
            CarregarTipolancamento();
            CarregarTurma();
            CarregarLancamento();
            
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
        }

        private void CarregarPessoa()
        {
            ddlPessoa.DataSource = EC.Negocio.NAcao.ConsultarPessoa();
            ddlPessoa.DataTextField = "NOME";
            ddlPessoa.DataValueField = "ID_PESSOA";
            ddlPessoa.DataBind();
        }

        private void CarregarTurma()
        {
            ddlTurma.DataSource = EC.Negocio.NLancamento.ConsultarTurma();
            ddlTurma.DataTextField = "ANO";
            ddlTurma.DataValueField = "ID_TURMA";
            ddlTurma.DataBind();
        }
        private void CarregarTipolancamento()
        {
            ddlTipolancamento.DataSource = EC.Negocio.NLancamento.ConsultarTipolancamento();
            ddlTipolancamento.DataTextField = "DESCRICAO";
            ddlTipolancamento.DataValueField = "ID_TIPOLANCAMENTO";
            ddlTipolancamento.DataBind();
        }

        private void CarregarLancamento()
        {
            grdLancamento.DataSource = EC.Negocio.NLancamento.ConsultarLancamento();
            grdLancamento.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            LANCAMENTO lancamento = new LANCAMENTO();
            lancamento.ID_TIPOLANCAMENTO = int.Parse(ddlTipolancamento.SelectedValue);
            lancamento.ID_TURMA = int.Parse(ddlTurma.SelectedValue);
            lancamento.JUSTIFICATIVA = TxtJustificativa.Text;
            lancamento.PROVIDENCIA = TxtProvidencias.Text;
            lancamento.DATA_LANCAMENTO = new DateTime(int.Parse(ddlAno.Text), int.Parse(ddlMes.Text), int.Parse(ddlDia.Text));
            NLancamento.Salvar(lancamento);
            Response.Redirect("Consultarlancamento.aspx", true);
        }
    }
}