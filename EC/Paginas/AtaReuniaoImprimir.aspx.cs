using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EC.Modelo;
using EC.Negocio;
using EC.Common;

namespace UI.Web.EC.Paginas
{
    public partial class AtaReuniaoImprimir : System.Web.UI.Page
    {

        private int idReuniao
        {
            get { return Library.ToInteger(ViewState["idReuniao"]); }
            set { ViewState["idReuniao"] = value; }
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

            {
                    if (IsPostBack)
                        return;
                    CarregarListaAno();
                    CarregarListaMes();
                    CarregarListaDia();
                    CarregarReuniao();
                    
                    if (Request.QueryString["idReuniao"] != null)
                    {
                        idReuniao = Request.QueryString["idReuniao"].ToInt32();
                        ddlReuniao.SelectedValue = idReuniao.ToString();
                        ddlReuniao_SelectedIndexChanged(null,null);
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

            
            ddlDiaFechamento.DataSource = ListaDia;
            ddlDiaFechamento.DataBind();
            ddlDiaFechamento.Items.Insert(0, new ListItem("", ""));
        }


        private void CarregarListaMes()
        {
            List<string> ListaMes = new List<string>();
            for (int i = 1; i <= 12; i++)
            {
                ListaMes.Add(i.ToString());
            }

            
            ddlMesFechamento.DataSource = ListaMes;
            ddlMesFechamento.DataBind();
            ddlMesFechamento.Items.Insert(0, new ListItem("", ""));
        }
        private void CarregarListaAno()
        {
            List<string> ListaAno = new List<string>();
            for (int i = 2014; i <= DateTime.Now.Year; i++)
            {
                ListaAno.Add(i.ToString());
            }

           
            ddlAnoFechamento.DataSource = ListaAno;
            ddlAnoFechamento.DataBind();
            ddlAnoFechamento.Items.Insert(0, new ListItem("", ""));
        }
        
        
        private void CarregarReuniao()
        {
            ddlReuniao.DataSource = NAcao.ConsultarReuniao();
            ddlReuniao.DataTextField = "TITULO";
            ddlReuniao.DataValueField = "ID_REUNIAO";
            ddlReuniao.DataBind();

            ddlReuniao.Items.Insert(0, new ListItem("Selecione", ""));
        }

       
        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ConsultarAta.aspx", true);
        }

        protected void ddlReuniao_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarDadosReuniao();
        }

        protected void CarregarDadosReuniao()
        {
            var reuniao = NReuniao.ConsultarById(Library.ToInteger(ddlReuniao.SelectedValue));
            lblNumeroReunião.Text = reuniao.ID_REUNIAO.ToString();
            lblDataTeuniao.Text = reuniao.DATAHORA.ToDate().Day.ToString() + "/" + reuniao.DATAHORA.ToDate().Month.ToString() + "/" + reuniao.DATAHORA.ToDate().Year.ToString();
            lblHoraReuniao.Text = reuniao.DATAHORA.ToDate().Hour.ToString() + ":" + reuniao.DATAHORA.ToDate().Minute.ToString();
            lblLocalReuniao.Text = reuniao.LOCAL;

            var participantes = NReuniaoParticipante.ConsultarByReuniao(reuniao.ID_REUNIAO);
            grdParticipantesReuniao.DataSource = participantes;
            grdParticipantesReuniao.DataBind();

            grdAssunto.DataSource = NReuniaoAssuntoTratado.ConsultarByReuniao(reuniao.ID_REUNIAO);
            grdAssunto.DataBind();

            grdCompromisso.DataSource = NReuniaoCompromisso.ConsultarByReuniao(reuniao.ID_REUNIAO);
            grdCompromisso.DataBind();

            var pautas = NReuniaoPauta.ConsultarByReuniao(reuniao.ID_REUNIAO);
            string strpauta = "<ul>";
            foreach (var pauta in pautas)
            {
                strpauta += "<li>" + pauta.ITEM + " - " + pauta.DESCRICAO + "</li>";
            }
            lblPauta.Text = strpauta + "</ul>";

            lblResponsavelAta.Text = ((SessionUsuario)Session[Const.USUARIO]).USUARIO.FUNCIONARIO.PESSOA.NOME;
            hddResponsavelAta.Value = ((SessionUsuario)Session[Const.USUARIO]).USUARIO.FUNCIONARIO.ID_FUNCIONARIO.ToString();
                        
            pnlAta.Visible = true;
        }

    }
}


   
