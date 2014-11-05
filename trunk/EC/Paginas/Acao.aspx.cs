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
    public partial class Acao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;

        
            CarregarEvento();
            CarregarAmc();
            CarregarPessoa();
            CarregarReuniao();
            CarregarListaDia();
            CarregarListaMes();
            CarregarListaAno();
            CarregarListaDia1();
            CarregarListaMes1();
            CarregarListaAno1();
            CarregarStatus();
            CarregarPrioridade();
        
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
            
            //ListItem r = new ListItem();
            //r.Text = "";
            //r.Value = "0";
            //ddlDia.Items.Insert(0, r);
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

            //ListItem r = new ListItem();
            //r.Text = "";
            //r.Value = "0";
            //ddlMes.Items.Insert(0, r);
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
            
            //ListItem r = new ListItem();
            //r.Text = "";
            //r.Value = "0";
            //ddlAno.Items.Insert(0, r);
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

            ddlDia1.Items.Insert(0, new ListItem("", ""));

            //ListItem r = new ListItem();
            //r.Text = "";
            //r.Value = "0";
            //ddlDia1.Items.Insert(0, r);
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

            ddlMes1.Items.Insert(0, new ListItem("", ""));

            //ListItem r = new ListItem();
            //r.Text = "";
            //r.Value = "0";
            //ddlMes1.Items.Insert(0, r);
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

            ddlAno1.Items.Insert(0, new ListItem("", ""));

            //ListItem r = new ListItem();
            //r.Text = "";
            //r.Value = "0";
            //ddlAno1.Items.Insert(0, r);
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

        //    ListItem r = new ListItem();
        //    r.Text = "";
        //    r.Value = "0";
        //    ddlAmc.Items.Insert(0, r);
        //}

        private void CarregarEvento()
        {
            ddlEvento.DataSource = NAcao.ConsultarEvento();
            ddlEvento.DataTextField = "NOME";
            ddlEvento.DataValueField = "ID_EVENTO";
            ddlEvento.DataBind();

             ddlEvento.Items.Insert(0, new ListItem("", ""));

            //ListItem r = new ListItem();
            //r.Text = "";
            //r.Value = "0";
            //ddlEvento.Items.Insert(0, r);
        }
        private void CarregarReuniao()
        {
            ddlReuniao.DataSource = NAcao.ConsultarReuniao();
            ddlReuniao.DataTextField = "TITULO";
            ddlReuniao.DataValueField = "ID_REUNIAO";
            ddlReuniao.DataBind();

            ddlReuniao.Items.Insert(0, new ListItem("", ""));
        }

        //    ListItem r = new ListItem();
        //    r.Text = "";
        //    r.Value = "0";
        //    ddlReuniao.Items.Insert(0,r);
        //}
        private void CarregarPessoa()
        {
            ddlPessoa.DataSource = NAcao.ConsultarPessoa();
            ddlPessoa.DataTextField = "NOME";
            ddlPessoa.DataValueField = "ID_PESSOA";
            ddlPessoa.DataBind();

            ddlPessoa.Items.Insert(0, new ListItem("", ""));

            //ListItem r = new ListItem();
            //r.Text = "";
            //r.Value = "0";
            //ddlPessoa.Items.Insert(0, r);
        }
        private void CarregarStatus()
        {
            ddlStatus.DataSource = NAcao.ConsultarStatus();
            ddlStatus.DataTextField = "DESCRICAO";
            ddlStatus.DataValueField = "ID_STATUS";
            ddlStatus.DataBind();

             ddlStatus.Items.Insert(0, new ListItem("", ""));

            //ListItem r = new ListItem();
            //r.Text = "";
            //r.Value = "0";
            //ddlStatus.Items.Insert(0, r);
        }
        private void CarregarPrioridade()
        {
            ddlPrioridade.DataSource = NAcao.ConsultarPrioridade();
            ddlPrioridade.DataTextField = "DESCRICAO";
            ddlPrioridade.DataValueField = "ID_PRIORIDADE";
            ddlPrioridade.DataBind();

            ddlPrioridade.Items.Insert(0, new ListItem("", ""));

            //ListItem r = new ListItem();
            //r.Text = "";
            //r.Value = "0";
            //ddlPrioridade.Items.Insert(0, r);
        }
       
        protected void Button1_Click(object sender, EventArgs e)
        {
            ACAO acao = new ACAO();
           
            acao.ID_STATUS= int.Parse(ddlStatus.SelectedValue);
            acao.ID_PRIORIDADE = int.Parse(ddlPrioridade.SelectedValue);
            acao.ID_PESSOA = int.Parse(ddlPessoa.SelectedValue);
            acao.TITULO = TxtTitulo.Text;
            acao.INICIO = new DateTime(int.Parse(ddlAno.Text), int.Parse(ddlMes.Text), int.Parse(ddlDia.Text));
            acao.CONCLUSAO = new DateTime(int.Parse(ddlAno1.Text), int.Parse(ddlMes1.Text), int.Parse(ddlDia1.Text));
          
            NAcao.Salvar(acao);
            Response.Redirect("ConsultarAcao.aspx", true);
        }

        protected void btVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ConsultarAcao.aspx", true);
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {

        }

        
     
  // protected void Button2_Click(object sender, EventArgs e)
        //   {
  // using (ALICE2Entities contexto = new ALICE2Entities()) 
  // {
  // int chave = Convert.ToInt32(TextBox1.Text);
  // var retornaAcao = contexto.ACAO.First(c => c.ID_ACAO ==chave);

  // ACAO acao = new ACAO();

                //   acao.ID_STATUS = int.Parse(ddlStatus.SelectedValue);
  // acao.ID_PRIORIDADE = int.Parse(ddlPrioridade.SelectedValue);
  // acao.ID_PESSOA = int.Parse(ddlPessoa.SelectedValue);
  // acao.TITULO = TxtTitulo.Text;
  // acao.INICIO = new DateTime(int.Parse(ddlAno.Text), int.Parse(ddlMes.Text), int.Parse(ddlDia.Text));
  // acao.CONCLUSAO = new DateTime(int.Parse(ddlAno1.Text), int.Parse(ddlMes1.Text), int.Parse(ddlDia1.Text));

  // retornaAcao.TITULO = acao.TITULO;
  // retornaAcao.INICIO = acao.INICIO;

                //   contexto.SaveChanges();
  // Response.Redirect("Acao.aspx", true);

  // } 

        }

      }
