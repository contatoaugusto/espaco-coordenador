using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EC.Negocio;
using EC.Modelo;
using EC.Common;

namespace UI.Web.EC.Paginas
{
    public partial class BancoQuestao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            {
                if (IsPostBack)
                    return;

                CarregarDisciplina();
                CarregarFuncionario();
                CarregarAmc();
                CarregarQuestao();

            }
        }
        private void CarregarAmc()
        {
            var lista = NAmc.ConsultarAmc();
            ddlAmc.Items.Clear();
            ddlAmc.Items.Add(new ListItem("Selecione", ""));

            foreach (AMC obj in lista)
            {
                if (obj.SEMESTRE.ATIVO)
                    ddlAmc.Items.Add(new ListItem(obj.SEMESTRE.SEMESTRE1 + "º sem/" + obj.SEMESTRE.ANO, obj.ID_AMC.ToString()));
            }
        }

        private void CarregarDisciplina()
        {
            ddlDisciplina.DataSource = NDisciplina.Consultar();
            ddlDisciplina.DataTextField = "DESCRICAO";
            ddlDisciplina.DataValueField = "ID_DISCIPLINA";
            ddlDisciplina.DataBind();

            ddlDisciplina.Items.Insert(0, new ListItem("Selecione", ""));
        }


        private void CarregarFuncionario()
        {
            var lista = NFuncionario.ConsultarFuncionario();

            foreach (FUNCIONARIO func in lista)
            {
                ddlFuncionario.Items.Add(new ListItem(func.PESSOA.NOME, func.ID_FUNCIONARIO.ToString()));
            }

            ddlFuncionario.Items.Insert(0, new ListItem("Selecione", ""));
        }


        private void CarregarQuestao()
        {
            QUESTAO questao = new QUESTAO();
            var questoes = NQuestao.ConsultarQuestao(questao);
            if (questoes.Count() == 0)
                questoes = NQuestao.ConsultarQuestao();

            grdQuestao.DataSource = questoes;
            grdQuestao.DataBind();
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            if (ddlDisciplina.SelectedValue == "" && ddlFuncionario.SelectedValue == "" && string.IsNullOrEmpty(ddlAmc.SelectedValue))
            {
                //MENSAGEM
            }
            else
            {
                QUESTAO questao = new QUESTAO();
                if (ddlDisciplina.SelectedValue != "")
                    questao.ID_DISCIPLINA = Convert.ToInt32(ddlDisciplina.SelectedValue);
                if (ddlFuncionario.SelectedValue != "")
                    questao.ID_FUNCIONARIO = Convert.ToInt32(ddlFuncionario.SelectedValue);
                if (!string.IsNullOrEmpty(ddlAmc.SelectedValue))
                    questao.ID_AMC = Convert.ToInt32(ddlAmc.SelectedValue);

                var lista = NQuestao.ConsultarQuestao(questao);

                grdQuestao.DataSource = lista;
                grdQuestao.DataBind();
            }
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            Response.Redirect("Questao.aspx", true);
        }


        protected void grdQuestao_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {

                Context.Response.Redirect("~/Paginas/Questao.aspx?idQuestao=" + Library.ToInteger(e.CommandArgument));
            }
        }
    }
}
