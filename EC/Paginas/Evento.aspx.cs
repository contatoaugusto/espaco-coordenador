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
    public partial class Evento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            CarregarListaDia();
            CarregarListaMes();
            CarregarListaAno();
            CarregarListaDia1();
            CarregarListaMes1();
            CarregarListaAno1();
            CarregarTipoEvento();
            CarregarPessoa();
         

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

        private void CarregarListaDia1()
        {
            List<string> ListaDia1 = new List<string>();
            for (int i = 1; i <= 31; i++)
            {
                ListaDia1.Add(i.ToString());
            }

            ddlDia1.DataSource = ListaDia1;
            ddlDia1.DataBind();
        }


        private void CarregarListaMes1()
        {
            List<string> ListaMes1 = new List<string>();
            for (int i = 1; i <= 12; i++)
            {
                ListaMes1.Add(i.ToString());
            }

            ddlMes1.DataSource = ListaMes1;
            ddlMes1.DataBind();
        }
        private void CarregarListaAno1()
        {
            List<string> ListaAno1 = new List<string>();
            for (int i = 2014; i <= DateTime.Now.Year; i++)
            {
                ListaAno1.Add(i.ToString());
            }

            ddlAno1.DataSource = ListaAno1;
            ddlAno1.DataBind();
        }

        private void CarregarTipoEvento()
        {
            ddlTipoEvento.DataSource = EC.Negocio.NEvento.ConsultarTipoEvento();
            ddlTipoEvento.DataTextField = "DESCRICAO";
            ddlTipoEvento.DataValueField = "ID_TIPOEVENTO";
            ddlTipoEvento.DataBind();
        }

        private void CarregarPessoa()
        {
            ddlPessoa.DataSource = EC.Negocio.NAcao.ConsultarPessoa();
            ddlPessoa.DataTextField = "NOME";
            ddlPessoa.DataValueField = "ID_PESSOA";
            ddlPessoa.DataBind();
        }

      
        protected void Button1_Click(object sender, EventArgs e)
        {
            EVENTO evento = new EVENTO();
            evento.NOME = TxtNome.Text;
            evento.ID_TIPOEVENTO = int.Parse(ddlTipoEvento.SelectedValue);
            evento.DESCRICAO = TxtDescricao.TemplateSourceDirectory;
            evento.LOCAL = TxtLocal.Text;
            evento.INICIO = new DateTime(int.Parse(ddlAno.Text), int.Parse(ddlMes.Text), int.Parse(ddlDia.Text));
            evento.CONCLUSAO = new DateTime(int.Parse(ddlAno1.Text), int.Parse(ddlMes1.Text), int.Parse(ddlDia1.Text));
           // grdEvento.DataSource = EC.Negocio.NQuestão.ConsultarAmc(); ;
           // grdEvento.DataBind();
            
            NEvento.Salvar(evento);
    
        }
    }
}