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
    public partial class AtaReunião : System.Web.UI.Page
    {
        public static List<ASSUNTO_TRATADO> assuntos;
        public static List<COMPROMISSO> compromissos;

        protected void Page_Load(object sender, EventArgs e)
        {
            {
                {
                    if (IsPostBack)
                        return;
                    CarregarListaAno();
                    CarregarListaMes();
                    CarregarListaDia();
                    CarregarTipoAssunto();
                    CarregarReuniao();
                    CarregarPessoa();
                    assuntos = new List<ASSUNTO_TRATADO>();
                    compromissos = new List<COMPROMISSO>();
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
        private void CarregarTipoAssunto()
        {
            ddlTipoAssunto.DataSource = EC.Negocio.NReuniao.ConsultarTipoAssunto();
            ddlTipoAssunto.DataTextField = "DESCRICAO";
            ddlTipoAssunto.DataValueField = "ID_TIPOASSTRATADO";
            ddlTipoAssunto.DataBind();

            ddlTipoAssunto.Items.Insert(0, new ListItem("", ""));
        }

        private void CarregarPessoa()
        {
            ddlPessoa.DataSource = EC.Negocio.NAcao.ConsultarPessoa();
            ddlPessoa.DataTextField = "NOME";
            ddlPessoa.DataValueField = "ID_PESSOA";
            ddlPessoa.DataBind();
            ListItem r = new ListItem();
            r.Text = "";
            r.Value = "";
            ddlPessoa.Items.Insert(0, r);
        }

        private void CarregarReuniao()
        {
            ddlReuniao.DataSource = EC.Negocio.NAcao.ConsultarReuniao();
            ddlReuniao.DataTextField = "TITULO";
            ddlReuniao.DataValueField = "ID_REUNIAO";
            ddlReuniao.DataBind();

            ddlReuniao.Items.Insert(0, new ListItem("", ""));
        }

        protected void btnIncluirAssunto_Click(object sender, ImageClickEventArgs e)
        {
            {
               ASSUNTO_TRATADO assunto = new ASSUNTO_TRATADO();
                assunto.DESCRICAO = TxtAssunto.Text;
              //  assunto.ID_TIPOASSTRATADO = Convert.ToInt32(ddlTipoAssunto.SelectedValue);
                assunto.ITEM = assuntos.Count + 1;
                assuntos.Add(assunto);
                grdAssunto.DataSource = assuntos;
                grdAssunto.DataBind();

            }
        }

      
        protected void btnIncluircompromisso_Click1(object sender, ImageClickEventArgs e)
        {
            {
                COMPROMISSO compromisso = new COMPROMISSO();
                compromisso.DESCRICAO = TxtCompromisso.Text;
                compromisso.DATA = new DateTime(int.Parse(ddlAno.Text), int.Parse(ddlMes.Text), int.Parse(ddlDia.Text));
               // compromisso.ID_PESSOA = Convert.ToInt32(ddlPessoa.SelectedValue);
                compromisso.ITEM = compromissos.Count + 1;
                compromissos.Add(compromisso);
                grdCompromisso.DataSource = compromissos;
                grdCompromisso.DataBind();
            }
        }

        protected void btnSalvarReuniao_Click(object sender, EventArgs e)
        {
         {
            REUNIAO reuniao = new REUNIAO();
           
            reuniao.ID_REUNIAO = int.Parse(ddlReuniao.SelectedValue);
            reuniao.ASSUNTO_TRATADO = assuntos;
            reuniao.COMPROMISSO = compromissos;
            NReuniao.Salvar(reuniao);
            Response.Redirect("ConsultarAta.aspx", true);
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ConsultarAta.aspx", true);
        }
    }
}


   
