using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EC.Common;
using EC.Negocio;
using EC.Modelo;

namespace UI.Web.EC.Paginas
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private int idAmc
        {
            get { return Library.ToInteger(ViewState["idAmc"]); }
            set { ViewState["idAmc"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            // Controle Acesso
            int []cargoComAcesso = {2};
            string mensagem = ControleAcesso.verificaAcesso(cargoComAcesso);
            if (!mensagem.IsNullOrEmpty())
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "Alert", "<script>alert('" + mensagem + "');location.replace('../default.aspx')</script>");
            }

            if (!IsPostBack)
            {
                BindControl();
                BindDataGrid();

                CarregarListaDia();
                CarregarListaMes();
                CarregarListaAno();
                CarregarListaHora();
                CarregarListaMinuto();

                if (Request.QueryString["idAmc"] != null)
                {
                    idAmc = Request.QueryString["idAmc"].ToInt32();
                    CarregarEdicao();
                }
            }
         }

        private void CarregarEdicao()
        {
            var amc = NAmc.ConsultarById(idAmc);

            lblSemestreCorrente.Text = amc.SEMESTRE.SEMESTRE1 + "º sem/" + amc.SEMESTRE.ANO;
            lblCursoUnico.Text = ((SessionUsuario)Session[Const.USUARIO]).NmCurso;

            CarregarListaAno();
            ddlAno.SelectedValue = amc.DATA_APLICACAO.ToDate().Year.ToString();
            CarregarListaMes();
            ddlMes.SelectedValue = amc.DATA_APLICACAO.ToDate().Month.ToString();
            CarregarListaDia();
            ddlDia.SelectedValue = amc.DATA_APLICACAO.ToDate().Day.ToString();
            CarregarListaHora();
            ddlHora.SelectedValue = amc.DATA_APLICACAO.ToDate().Hour.ToString();
            CarregarListaMinuto();
            ddlMinuto.SelectedValue = amc.DATA_APLICACAO.ToDate().Minute.ToString();
        }

        public void BindControl()
        {
            CarregaSemestreCorrenteAMC();
        }

        public void BindDataGrid()
        {
            GridView_AMC.DataSource = NAmc.ConsultarAmc();
            GridView_AMC.DataBind();
        }


        protected void CarregaSemestreCorrenteAMC()
        {

            var semestre = NSemestre.ConsultarAtivo();

            if (semestre != null && semestre.ID_SEMESTRE > 0)
            {
                pnlAmc.Visible = true;
                lblSemestreCorrente.Text = semestre.SEMESTRE1 + "º sem/" + semestre.ANO;
                lblCursoUnico.Text = ((SessionUsuario)Session[Const.USUARIO]).NmCurso;
            }
            else
                pnlAmc.Visible = false;
        }

        protected void Salvar_Click1(object sender, EventArgs e)
        {
            if (Utils.GetUsuarioLogado().IdSemestre == 0 ||
                (NAmc.ConsultarAmcBySemestre(Utils.GetUsuarioLogado().IdSemestre).Count > 0 &&
                idAmc == 0))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "Alert", "<script>alert('"+ Const.MENSAGEM_CADASTRO_REPETIDO +"');</script>");
                return;
            }
            else
            {
                AMC amc = new AMC();

                string horaMinuto = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":00";

                amc.ID_SEMESTRE = ((SessionUsuario)Session[Const.USUARIO]).IdSemestre;
                amc.DATA_APLICACAO = new DateTime(int.Parse(ddlAno.Text), int.Parse(ddlMes.Text), int.Parse(ddlDia.Text), int.Parse(ddlHora.Text), int.Parse(ddlMinuto.Text), 0);

                if (idAmc > 0)
                {
                    amc.ID_AMC = idAmc;
                    NAmc.Atualiza(amc);
                }
                else
                {
                    if (NAmc.Salvar(amc))
                        ClientScript.RegisterClientScriptBlock(GetType(), "Alert", "<script>alert('" + Const.MENSAGEM_INCLUSAO_SUCESSO + "');</script>");
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(GetType(), "Alert", "<script>alert('" + Const.MENSAGEM_CADASTRO_REPETIDO + "');</script>");
                        return;
                    }
                }
            }    
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
        private void CarregarListaHora()
        {
            List<string> ListaHora = new List<string>();
            for (int i = 0; i <= 24; i++)
            {
                ListaHora.Add(i.ToString());
            }

            ddlHora.DataSource = ListaHora;
            ddlHora.DataBind();
            ddlHora.Items.Insert(0, new ListItem("", ""));
        }

        private void CarregarListaMinuto()
        {
            List<string> ListaMinuto = new List<string>();
            for (int i = 0; i <= 60; i++)
            {
                ListaMinuto.Add(i.ToString());
            }

            ddlMinuto.DataSource = ListaMinuto;
            ddlMinuto.DataBind();
            ddlMinuto.Items.Insert(0, new ListItem("", ""));
        }

        protected void GridView_AMC_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Alterar")
            {

                Context.Response.Redirect("~/Paginas/Amc.aspx?idAmc=" + Library.ToInteger(e.CommandArgument));
            }
        }
    }
}