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

namespace UI.Web.EC.Reuniao
{
    public partial class AgendaReuniao : System.Web.UI.Page
    {
        public static EntityCollection<REUNIAO_PAUTA> pautas;
        public static List<PESSOA> participantes;

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
                CarregarSemestre();
                CarregarPessoaParticipante();
                pautas = new EntityCollection<REUNIAO_PAUTA>();
                participantes = new List<PESSOA>();
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

        private void CarregarSemestre()
        {
            var semestre = NSemestre.ConsultarAtivo();
            lblSemestreCorrente.Text = semestre.SEMESTRE1 + "º sem/" + semestre.ANO;
            hddSemestreCorrente.Value = semestre.ID_SEMESTRE.ToString();
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
            ddlTipoReuniao.DataSource = NReuniao.ConsultarTipoReuniao();
            ddlTipoReuniao.DataTextField = "DESCRICAO";
            ddlTipoReuniao.DataValueField = "ID_TIPOREUNIAO";
            ddlTipoReuniao.DataBind();

            ddlTipoReuniao.Items.Insert(0, new ListItem("Selecione", "0"));
        }

        private void CarregarPessoaParticipante()
        {
            ddlParticipante.DataSource = NPessoa.Consultar();
            ddlParticipante.DataTextField = "NOME";
            ddlParticipante.DataValueField = "ID_PESSOA";
            ddlParticipante.DataBind();

            ddlParticipante.Items.Insert(0, new ListItem("Selecione", "0"));
        }


        protected void btnSalvarReuniao_Click(object sender, EventArgs e)
        {
            REUNIAO reuniao = new REUNIAO();
            reuniao.TITULO = txtTitulo.Text;
            reuniao.ID_TIPOREUNIAO = int.Parse(ddlTipoReuniao.SelectedValue);
            reuniao.LOCAL = txtLocal.Text;
            reuniao.ID_SEMESTRE = int.Parse(hddSemestreCorrente.Value);
            reuniao.DATAHORA = new DateTime(int.Parse(ddlAno.Text), int.Parse(ddlMes.Text), int.Parse(ddlDia.Text), int.Parse(ddlHora.Text), int.Parse(ddlMinuto.Text), 0);
            reuniao.REUNIAO_PAUTA = pautas;

            EntityCollection<REUNIAO_PARTICIPANTE> reuniao_participantes = new EntityCollection<REUNIAO_PARTICIPANTE>();
            foreach (var obj in participantes)
            {
                REUNIAO_PARTICIPANTE reuniao_part = new REUNIAO_PARTICIPANTE();
                reuniao_part.ID_PESSOA =  obj.ID_PESSOA;
                reuniao_participantes.Add(reuniao_part);
            }
            reuniao.REUNIAO_PARTICIPANTE = reuniao_participantes;

            NReuniao.Salvar(reuniao);

            ClientScript.RegisterClientScriptBlock(GetType(), "Alert", "<script>alert('" + Const.MENSAGEM_INCLUSAO_SUCESSO + "');</script>");
            Response.Redirect("ConsultarReuniao.aspx", true);
        }

        protected void Validacoes()
        {
            //Verificar campos obrigatorios

            // Verificar se não tem reunião igual
        }


        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            REUNIAO_PAUTA pauta = new REUNIAO_PAUTA();
            pauta.DESCRICAO = txtPauta.Text;
            pauta.ITEM = pautas.Count + 1;
            pautas.Add(pauta);
            grdPauta.DataSource = pautas;
            grdPauta.DataBind();
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ConsultarReuniao.aspx", true);
        }

        protected void imgAdicionaParticipante_Click(object sender, ImageClickEventArgs e)
        {
            PESSOA pessoa = new PESSOA();
            pessoa = NPessoa.ConsultarById(Library.ToInteger(ddlParticipante.SelectedValue));

            participantes.Add(pessoa);
            grdParticipante.DataSource = participantes;
            grdParticipante.DataBind();

        }
        public static string GetCargos(int idPessoa){
            string cargo = "";
            var cargoList = NCargo.ConsultarByPessoa(idPessoa);

            foreach (var obj in cargoList) {
                cargo += obj.DESCRICAO + "\n";
            }

            return cargo;
        }
        //http://www.ajaxtutorials.com/general/using-autocomplete-in-the-ajax-toolkit/
        //public static string[] GetNames(string prefixText, int count)
        //{
        //    NPessoa.Consultar(prefixText, count);
            
        //    return .Select(n => n.NOME).Take(count)
        //}
    }
}