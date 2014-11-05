using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EC.Negocio;
using EC.Modelo;

namespace UI.Web.EC.Paginas
{
    public partial class ConsultarAcao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

        
            //CarregarEvento();
            //CarregarAmc();
            // CarregarReuniao();  
            CarregarPessoa();
            CarregarStatus();
            CarregarPrioridade();
            CarregarAcao();
          
           
        }

        //private void CarregarAmc()
        //{
        //    ddlAmc.DataSource = EC.Negocio.NAcao.ConsultarAmc();
        //    ddlAmc.DataTextField = "ANO";
        //    ddlAmc.DataValueField = "ID_AMC";
        //    ddlAmc.DataBind();
        //    ListItem r = new ListItem();
        //    r.Text = "";
        //    r.Value = "";
        //    ddlAmc.Items.Insert(0, r);
        //}

        //private void CarregarEvento()
        //{
        //    ddlEvento.DataSource = EC.Negocio.NAcao.ConsultarEvento();
        //    ddlEvento.DataTextField = "DESCRICAO";
        //    ddlEvento.DataValueField = "ID_EVENTO";
        //    ddlEvento.DataBind();
        //    ListItem r = new ListItem();
        //    r.Text = "";
        //    r.Value = "";
        //    ddlEvento.Items.Insert(0, r);
        //}
        //private void CarregarReuniao()
        //{
        //    ddlReuniao.DataSource = EC.Negocio.NAcao.ConsultarReuniao();
        //    ddlReuniao.DataTextField = "TITULO";
        //    ddlReuniao.DataValueField = "ID_REUNIAO";
        //    ddlReuniao.DataBind();
        //    ListItem r = new ListItem();
        //    r.Text = "";
        //    r.Value = "";
        //    ddlReuniao.Items.Insert(0,r);
        //}
        private void CarregarPessoa()
        {
            ddlPessoa.DataSource = NAcao.ConsultarPessoa();
            ddlPessoa.DataTextField = "NOME";
            ddlPessoa.DataValueField = "ID_PESSOA";
            ddlPessoa.DataBind();
            ListItem r = new ListItem();
            r.Text = "";
            r.Value = "";
            ddlPessoa.Items.Insert(0, r);
        }
        private void CarregarStatus()
        {
            ddlStatus.DataSource = NAcao.ConsultarStatus();
            ddlStatus.DataTextField = "DESCRICAO";
            ddlStatus.DataValueField = "ID_STATUS";
            ddlStatus.DataBind();
            ListItem r = new ListItem();
            r.Text = "";
            r.Value = "";
            ddlStatus.Items.Insert(0, r);
        }
        private void CarregarPrioridade()
        {
            ddlPrioridade.DataSource = NAcao.ConsultarPrioridade();
            ddlPrioridade.DataTextField = "DESCRICAO";
            ddlPrioridade.DataValueField = "ID_PRIORIDADE";
            ddlPrioridade.DataBind();
            ListItem r = new ListItem();
            r.Text = "";
            r.Value = "";
            ddlPrioridade.Items.Insert(0, r);
        }

        private void CarregarAcao()
        {
           ACAO acao = new ACAO();
            grdAcao.DataSource = NAcao.ConsultarAcao(acao);
            grdAcao.DataBind();
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
             {
            if (ddlPessoa.SelectedValue == "" && ddlPrioridade.SelectedValue == "" && ddlStatus.SelectedValue == "" )
            {
                //MENSAGEM
            }
            else
            {
                ACAO acao = new ACAO();
                if (ddlPessoa.SelectedValue != "")
                 acao.ID_PESSOA = Convert.ToInt32(ddlPessoa.SelectedValue);
                if (ddlPrioridade.SelectedValue != "")
                   acao.ID_PRIORIDADE = Convert.ToInt32(ddlPrioridade.SelectedValue);
                if (!string.IsNullOrEmpty(ddlStatus.SelectedValue))
                   acao.ID_STATUS = Convert.ToInt32(ddlStatus.SelectedValue);
                

                var lista = NAcao.ConsultarAcao(acao);

                grdAcao.DataSource = lista;
                grdAcao.DataBind();
            }            
        }
    }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
              Response.Redirect("Acao.aspx", true);
        }
        }
  }

       