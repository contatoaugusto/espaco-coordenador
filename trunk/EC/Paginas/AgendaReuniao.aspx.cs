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
        public static List<REUNIAO_PAUTA> pautas;
        public static List<REUNIAO_PARTICIPANTE> participantes;

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
                pautas = new List<REUNIAO_PAUTA>();
                participantes = new List<REUNIAO_PARTICIPANTE>();


                if (Request.QueryString["idReuniao"] != null)
                {
                    idReuniao = Request.QueryString["idReuniao"].ToInt32();
                    CarregarEdicao();
                }

            }   
         }


        private void CarregarEdicao()
        {
            var reuniao = NReuniao.ConsultarById(idReuniao);

            txtTitulo.Text = reuniao.TITULO;
            CarregarTipoReuniao();
            ddlTipoReuniao.SelectedValue = reuniao.ID_TIPOREUNIAO.ToString();
            txtLocal.Text = reuniao.LOCAL;
            hddSemestreCorrente.Value = reuniao.ID_SEMESTRE.ToString();
            CarregarListaAno();
            ddlAno.SelectedValue = reuniao.DATAHORA.ToDate().Year.ToString();
            CarregarListaMes();
            ddlMes.SelectedValue = reuniao.DATAHORA.ToDate().Month.ToString();
            CarregarListaDia();
            ddlDia.SelectedValue = reuniao.DATAHORA.ToDate().Day.ToString();
            CarregarListaHora();
            ddlHora.SelectedValue = reuniao.DATAHORA.ToDate().Hour.ToString();
            CarregarListaMinuto();
            ddlMinuto.SelectedValue = reuniao.DATAHORA.ToDate().Minute.ToString();

            pautas = NReuniaoPauta.ConsultarByReuniao(idReuniao);

            grdPauta.DataSource = pautas;
            grdPauta.DataBind();

            participantes = NReuniaoParticipante.ConsultarByReuniao(idReuniao);
            //List<PESSOA> pessoas = new List<PESSOA>();
            //foreach (var participante in participantes)
            //{
            //    PESSOA p = new PESSOA();
            //    p.ID_PESSOA = participante.PESSOA.ID_PESSOA;
            //    p.NOME = participante.PESSOA.NOME;
            //    p.TELEFONE = participante.PESSOA.TELEFONE;
            //    p.EMAIL = participante.PESSOA.EMAIL;
            //    pessoas.Add(p);
            //}

            grdParticipante.DataSource = participantes;
            grdParticipante.DataBind();

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
            //participantes = NPessoa.Consultar();
            ddlParticipante.DataSource = NPessoa.Consultar();
            ddlParticipante.DataTextField = "NOME";
            ddlParticipante.DataValueField = "ID_PESSOA";
            ddlParticipante.DataBind();

            ddlParticipante.Items.Insert(0, new ListItem("Selecione", "0"));
        }


        protected void btnSalvarReuniao_Click(object sender, EventArgs e)
        {

            var data = new DateTime(int.Parse(ddlAno.Text), int.Parse(ddlMes.Text), int.Parse(ddlDia.Text), int.Parse(ddlHora.Text), int.Parse(ddlMinuto.Text), 0);
            if (Library.CompareDate(data, DateTime.Now, DateTimeCompare.Minus) ){
                ClientScript.RegisterClientScriptBlock(GetType(), "Alert", "<script>alert('" + Const.MENSAGEM_DATA_INVALIDA + "');</script>");
                return;
            }

            REUNIAO reuniao = new REUNIAO();
            reuniao.TITULO = txtTitulo.Text;
            reuniao.ID_TIPOREUNIAO = int.Parse(ddlTipoReuniao.SelectedValue);
            reuniao.LOCAL = txtLocal.Text;
            reuniao.ID_SEMESTRE = int.Parse(hddSemestreCorrente.Value);
            reuniao.DATAHORA = data;

            
            //reuniao.REUNIAO_PAUTA = pautas;
            //reuniao.REUNIAO_PARTICIPANTE = participantes;

            if (idReuniao > 0)
            {
                reuniao.ID_REUNIAO = idReuniao;
                NReuniao.Atualiza(reuniao);
                ClientScript.RegisterClientScriptBlock(GetType(), "Alert", "<script>alert('" + Const.MENSAGEM_ALTERACAO_SUCESSO + "');</script>");
            }
            else {
                NReuniao.Salvar(reuniao);
                NReuniaoPauta.Salvar(pautas, reuniao.ID_REUNIAO);
                NReuniaoParticipante.Salvar(participantes, reuniao.ID_REUNIAO);
                ClientScript.RegisterClientScriptBlock(GetType(), "Alert", "<script>alert('" + Const.MENSAGEM_INCLUSAO_SUCESSO + "');</script>");
            }
           // Response.Redirect("AgendaReuniao.aspx", true);
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
            //PESSOA pessoa = new PESSOA();
            REUNIAO_PARTICIPANTE reuniaoParitcipante = new REUNIAO_PARTICIPANTE();
            reuniaoParitcipante.PESSOA = NPessoa.ConsultarById(Library.ToInteger(ddlParticipante.SelectedValue));

            participantes.Add(reuniaoParitcipante);
            grdParticipante.DataSource = participantes;
            grdParticipante.DataBind();

        }
        
        //http://www.ajaxtutorials.com/general/using-autocomplete-in-the-ajax-toolkit/
        //public static string[] GetNames(string prefixText, int count)
        //{
        //    NPessoa.Consultar(prefixText, count);
            
        //    return .Select(n => n.NOME).Take(count)
        //}


        protected void grdPauta_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Excluir")
            {
                try
                {
                    int id = Convert.ToInt32(e.CommandArgument);

                    // Remove o item da lista
                    foreach (var pauta in pautas)
                    {
                        if (pauta.ITEM == id)
                        {
                            pautas.Remove(pauta);
                            // O item existe no banco, precisa ser deletado
                            if (pauta.ID_PAUTA > 0)
                                NReuniaoPauta.ExcluiPauta(pauta.ID_PAUTA);

                            break;
                        }
                    }

                    pautas = NReuniaoPauta.ConsultarByReuniao(idReuniao);
                    grdPauta.DataSource = pautas;
                    grdPauta.DataBind();

                    ClientScript.RegisterClientScriptBlock(GetType(), "Alert", "<script>alert('" + Const.MENSAGEM_EXCLUSAO_SUCESSO + "');</script>");
                }

                catch (Exception ex)
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "Alert", "<script>alert('" + Const.MENSAGEM_EXCLUSAO_ERRO + "');</script>");
                }
            }
        }

        protected void grdParticipante_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Excluir")
            {
                try
                {
                    int idPessoa = Convert.ToInt32(e.CommandArgument);

                    // Remove o item da lista
                    foreach (var participante in participantes)
                    {
                        if (participante.ID_PESSOA == idPessoa)
                        {
                            participantes.Remove(participante);
                            // O item existe no banco, precisa ser deletado
                            if (participante.ID_PESSOA > 0)
                                NReuniaoParticipante.Exclui(participante.ID_PARTICIPANTE);

                            break;
                        }
                    }

                    participantes = NReuniaoParticipante.ConsultarByReuniao(idReuniao);
                    grdParticipante.DataSource = participantes;
                    grdParticipante.DataBind();

                    ClientScript.RegisterClientScriptBlock(GetType(), "Alert", "<script>alert('" + Const.MENSAGEM_EXCLUSAO_SUCESSO + "');</script>");
                }

                catch (Exception ex)
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "Alert", "<script>alert('" + Const.MENSAGEM_EXCLUSAO_ERRO + "');</script>");
                }
            }

        }
    }
}