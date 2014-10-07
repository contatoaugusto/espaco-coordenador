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
            ddlAmc.DataSource = EC.Negocio.NQuestão.ConsultarAmc();
            ddlAmc.DataTextField = "ANO";
            ddlAmc.DataValueField = "ID_AMC";
            ddlAmc.DataBind();

            ddlAmc.Items.Insert(0, new ListItem("", ""));
        }

        private void CarregarDisciplina()
        {
            ddlDisciplina.DataSource = EC.Negocio.NQuestão.ConsultarDisciplina();
            ddlDisciplina.DataTextField = "DESCRICAO";
            ddlDisciplina.DataValueField = "ID_DISCIPLINA";
            ddlDisciplina.DataBind();

            ddlDisciplina.Items.Insert(0, new ListItem("", ""));
        }


        private void CarregarFuncionario()
        {
            var lista = EC.Negocio.NQuestão.ConsultarFuncionario();

            foreach (FUNCIONARIO func in lista)
            {
                ddlFuncionario.Items.Add(new ListItem(func.PESSOA.NOME, func.ID_FUNCIONARIO.ToString()));
            }

            ddlFuncionario.Items.Insert(0, new ListItem("", ""));
        }


        private void CarregarQuestao()
        {
            QUESTAO questao = new QUESTAO();
            grdQuestao.DataSource = EC.Negocio.NQuestão.ConsultarQuestao(questao);
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

                var lista = NQuestão.ConsultarQuestao(questao);

                grdQuestao.DataSource = lista;
                grdQuestao.DataBind();
            }            
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
             Response.Redirect("Questao.aspx", true);
        }
        }
    }
