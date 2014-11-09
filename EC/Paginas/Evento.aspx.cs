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
    public partial class Evento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

            CarregarListaDia();
            CarregarListaMes();
            CarregarListaAno();
            //CarregarListaDia1();
            //CarregarListaMes1();
            //CarregarListaAno1();
            CarregarListaHora();
            CarregarListaMinuto();
            CarregarTipoEvento();
            CarregarPessoa();
         

        }

        private void CarregarListaDia()
        {
            List<string> ListaDia = new List<string>();
            List<string> ListaDia1 = new List<string>();
            for (int i = 1; i <= 31; i++)
            {
                ListaDia.Add(i.ToString());
                ListaDia1.Add(i.ToString());
            }

            ddlDia.DataSource = ListaDia;
            ddlDia.DataBind();
            ddlDia.Items.Insert(0, new ListItem("", ""));

            ddlDia1.DataSource = ListaDia1;
            ddlDia1.DataBind();
            ddlDia1.Items.Insert(0, new ListItem("", ""));
        }


        private void CarregarListaMes()
        {
            List<string> ListaMes = new List<string>();
            List<string> ListaMes1 = new List<string>();
            for (int i = 1; i <= 12; i++)
            {
                ListaMes.Add(i.ToString());
                ListaMes1.Add(i.ToString());
            }

            ddlMes.DataSource = ListaMes;
            ddlMes.DataBind();
            ddlMes.Items.Insert(0, new ListItem("", ""));

            ddlMes1.DataSource = ListaMes1;
            ddlMes1.DataBind();
            ddlMes1.Items.Insert(0, new ListItem("", ""));
        }
        private void CarregarListaAno()
        {
            List<string> ListaAno = new List<string>();
            List<string> ListaAno1 = new List<string>();
            for (int i = 2014; i <= DateTime.Now.Year; i++)
            {
                ListaAno.Add(i.ToString());
                ListaAno1.Add(i.ToString());
            }

            ddlAno.DataSource = ListaAno;
            ddlAno.DataBind();
            ddlAno.Items.Insert(0, new ListItem("", ""));

            ddlAno1.DataSource = ListaAno1;
            ddlAno1.DataBind();
            ddlAno1.Items.Insert(0, new ListItem("", ""));
        }

        private void CarregarListaHora()
        {
            List<string> ListaHora = new List<string>();
            List<string> ListaHora1 = new List<string>();
            for (int i = 0; i <= 24; i++)
            {
                ListaHora.Add(i.ToString());
                ListaHora1.Add(i.ToString());
            }

            ddlHora.DataSource = ListaHora;
            ddlHora.DataBind();

            ddlHora.Items.Insert(0, new ListItem("", ""));

            ddlHora1.DataSource = ListaHora;
            ddlHora1.DataBind();
            ddlHora1.Items.Insert(0, new ListItem("", ""));

        }

        private void CarregarListaMinuto()
        {
            List<string> ListaMinuto = new List<string>();
            List<string> ListaMinuto1 = new List<string>();
            
            for (int i = 0; i <= 60; i++)
            {
                ListaMinuto.Add(i.ToString());
                ListaMinuto1.Add(i.ToString());
                ddlMinuto.Items.Insert(0, new ListItem("", ""));
            }

            ddlMinuto.DataSource = ListaMinuto;
            ddlMinuto.DataBind();
            ddlMinuto.Items.Insert(0, new ListItem("", ""));


            ddlMinuto1.DataSource = ListaMinuto1;
            ddlMinuto1.DataBind();
            ddlMinuto1.Items.Insert(0, new ListItem("", ""));
            
        }

        //private void CarregarListaDia1()
        //{
        //    List<string> ListaDia1 = new List<string>();
        //    for (int i = 1; i <= 31; i++)
        //    {
        //        ListaDia1.Add(i.ToString());
        //    }

        //    ddlDia1.DataSource = ListaDia1;
        //    ddlDia1.DataBind();
        //}


        //private void CarregarListaMes1()
        //{
        //    List<string> ListaMes1 = new List<string>();
        //    for (int i = 1; i <= 12; i++)
        //    {
        //        ListaMes1.Add(i.ToString());
        //    }

        //    ddlMes1.DataSource = ListaMes1;
        //    ddlMes1.DataBind();
        //}
        //private void CarregarListaAno1()
        //{
        //    List<string> ListaAno1 = new List<string>();
        //    for (int i = 2014; i <= DateTime.Now.Year; i++)
        //    {
        //        ListaAno1.Add(i.ToString());
        //    }

        //    ddlAno1.DataSource = ListaAno1;
        //    ddlAno1.DataBind();
        //}

        private void CarregarTipoEvento()
        {
            ddlTipoEvento.DataSource = NEvento.ConsultarTipoEvento();
            ddlTipoEvento.DataTextField = "DESCRICAO";
            ddlTipoEvento.DataValueField = "ID_TIPOEVENTO";
            ddlTipoEvento.DataBind();
            ddlTipoEvento.Items.Insert(0, new ListItem("Selecione", ""));
        }

        private void CarregarPessoa()
        {
            ddlPessoa.DataSource = NAcao.ConsultarPessoa();
            ddlPessoa.DataTextField = "NOME";
            ddlPessoa.DataValueField = "ID_PESSOA";
            ddlPessoa.DataBind();
            ddlPessoa.Items.Insert(0, new ListItem("Selecione", ""));
        
        }

      
        protected void Button1_Click(object sender, EventArgs e)
        {
            EVENTO evento = new EVENTO();
            evento.NOME = TxtNome.Text;
            evento.ID_TIPOEVENTO = int.Parse(ddlTipoEvento.SelectedValue);
            evento.DESCRICAO = TxtDescricao.TemplateSourceDirectory;
            evento.LOCAL = TxtLocal.Text;
            evento.INICIO = new DateTime(int.Parse(ddlAno.Text), int.Parse(ddlMes.Text), int.Parse(ddlDia.Text),int.Parse(ddlHora.Text), int.Parse(ddlMinuto.Text),0);
            evento.CONCLUSAO = new DateTime(int.Parse(ddlAno1.Text), int.Parse(ddlMes1.Text), int.Parse(ddlDia1.Text), int.Parse(ddlHora1.Text), int.Parse(ddlMinuto1.Text),0);
           // grdEvento.DataSource = NQuestão.ConsultarAmc(); ;
           // grdEvento.DataBind();
            
            NEvento.Salvar(evento);
    
        }
    }
}