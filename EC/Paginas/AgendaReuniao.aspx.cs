using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EC.Dado;
using EC.Modelo;
using EC.Negocio;

namespace EC.Reuniao
{
    public partial class AgendaReuniao : System.Web.UI.Page
    {
        public static List<PAUTA_REUNIAO> pautas;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarListaDia();
                CarregarListaMes();
                CarregarListaAno();
                CarregarListaHora();
                CarregarListaMinuto();
                CarregarTipoReuniao();
                pautas = new List<PAUTA_REUNIAO>();
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

        private void CarregarTipoReuniao()
        {
            ddlTipoReuniao.DataSource = EC.Negocio.NReuniao.ConsultarTipoReuniao();
            ddlTipoReuniao.DataTextField = "DESCRICAO";
            ddlTipoReuniao.DataValueField = "ID_TIPOREUNIAO";
            ddlTipoReuniao.DataBind();

            ddlTipoReuniao.Items.Insert(0, new ListItem("", ""));
        }

           //List<PAUTA_REUNIAO> listaPauta = new List<PAUTA_REUNIAO>();

           //PAUTA_REUNIAO pauta1 = new PAUTA_REUNIAO();
           //pauta1.DESCRICAO = TxtPauta.Text;

            //PAUTA_REUNIAO pauta2 = new PAUTA_REUNIAO();
            //pauta2.DESCRICAO = TxtPauta2.Text;

            //PAUTA_REUNIAO pauta3 = new PAUTA_REUNIAO();
            //pauta3.DESCRICAO = TxtPauta3.Text;

            //PAUTA_REUNIAO pauta4 = new PAUTA_REUNIAO();
            //pauta4.DESCRICAO = TxtPauta4.Text;

            //PAUTA_REUNIAO pauta5 = new PAUTA_REUNIAO();
            //pauta5.DESCRICAO = TxtPauta5.Text;

            //listaPauta.Add(pauta1);
            //listaPauta.Add(pauta2);
            //listaPauta.Add(pauta1);
            //listaPauta.Add(pauta2);
            //listaPauta.Add(pauta1);

            //reuniao.PAUTA_REUNIAO = listaPauta;
            //NReuniao.Salvar(reuniao);
            //Response.Redirect("ConsultarReuniao.aspx", true);
     
        protected void btnSalvarReuniao_Click(object sender, EventArgs e)
        {
            {
            REUNIAO reuniao = new REUNIAO();
            reuniao.TITULO = TxtTitulo.Text;
            reuniao.ID_TIPOREUNIAO = int.Parse(ddlTipoReuniao.SelectedValue);
            reuniao.LOCAL = TxtLocal.Text;
            reuniao.SEMESTRE = int.Parse(ddlSemestre.Text);
            reuniao.DATAHORA = new DateTime(int.Parse(ddlAno.Text), int.Parse(ddlMes.Text), int.Parse(ddlDia.Text), int.Parse(ddlHora.Text), int.Parse(ddlMinuto.Text), 0);
            reuniao.PAUTA_REUNIAO = pautas;
            NReuniao.Salvar(reuniao);
            Response.Redirect("ConsultarReuniao.aspx", true);
            }
   
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
         {
            PAUTA_REUNIAO pauta = new PAUTA_REUNIAO();
            pauta.DESCRICAO = TxtPauta.Text;
            pauta.ITEM = pautas.Count + 1;
            pautas.Add(pauta);
            grdPauta.DataSource = pautas;
            grdPauta.DataBind();
            
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ConsultarReuniao.aspx", true);
        }
    }
}