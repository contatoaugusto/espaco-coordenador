using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EC.Negocio;
using EC.Modelo;
using EC.Common;
using System.Configuration;

namespace UI.Web.EC.Paginas
{
    public partial class QuestaoNotaAluno : System.Web.UI.Page
    {
        private string mencao
        {
            get { return Library.ToString(ViewState["mencao"]); }
            set { ViewState["mencao"] = value; }
        }

        private int idAmc
        {
            get { return Library.ToInteger(ViewState["idAmc"]); }
            set { ViewState["idAmc"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            {
                if (IsPostBack)
                    return;

                CarregarMencao();
                CarregarAmc();

                if (Request.QueryString["mencao"] != null && Request.QueryString["idAmc"] != null)
                {
                    mencao = Request.QueryString["mencao"];
                    idAmc = Request.QueryString["idAmc"].ToInt32();
                    
                    ddlAmc.SelectedValue = idAmc.ToString();
                    ddlMencao.SelectedValue = mencao;

                    btnConsultar_Click(null, null);
                }
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

        private void CarregarMencao()
        {
            var lista = ConfigurationManager.AppSettings["Mencoes"].Replace(" ", string.Empty).Split(',');
            ddlMencao.Items.Clear();
            ddlMencao.Items.Add(new ListItem("Selecione", ""));

            foreach (string obj in lista)
            {
                ddlMencao.Items.Add(new ListItem(obj, obj));
            }
        }


        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(mencao) && string.IsNullOrEmpty(ddlAmc.SelectedValue))
            {
                //MENSAGEM
                return;
            }
            else
            {
                CarregarDataGrid();
            }
        }

        private void CarregarDataGrid()
        {
            var lista = NAlunoAmc.ConsultarByAmcMencao(ddlAmc.SelectedValue.ToInt32(), ddlMencao.SelectedValue.ToString());

            grdAlunoMatricula.DataSource = lista;
            grdAlunoMatricula.DataBind();
        }


        protected void grdItens_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdAlunoMatricula.PageIndex = e.NewPageIndex;
            CarregarDataGrid();
        }

        protected void grdAlunoMatricula_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Paginas/Relatorios/QuestaoNota.aspx", true);
        }

    }
}
