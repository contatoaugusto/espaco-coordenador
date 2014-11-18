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
    public partial class AtaReunião : System.Web.UI.Page
    {
        public static List<REUNIAO_ASSUNTO_TRATADO> assuntos;
        public static List<REUNIAO_COMPROMISSO> compromissos;

        private int idAta
        {
            get { return Library.ToInteger(ViewState["idAta"]); }
            set { ViewState["idAta"] = value; }
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
                {
                    if (IsPostBack)
                        return;
                    CarregarListaAno();
                    CarregarListaMes();
                    CarregarListaDia();
                    CarregarTipoAssunto();
                    CarregarReuniao();
                    CarregarPessoa();
                    assuntos = new List<REUNIAO_ASSUNTO_TRATADO>();
                    compromissos = new List<REUNIAO_COMPROMISSO>();
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

            ddlDiaFechamento.DataSource = ListaDia;
            ddlDiaFechamento.DataBind();
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
            ddlMesFechamento.DataSource = ListaMes;
            ddlMesFechamento.DataBind();
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
            ddlAnoFechamento.DataSource = ListaAno;
            ddlAnoFechamento.DataBind();
        }
        private void CarregarTipoAssunto()
        {
            ddlTipoAssunto.DataSource = NReuniao.ConsultarTipoAssunto();
            ddlTipoAssunto.DataTextField = "DESCRICAO";
            ddlTipoAssunto.DataValueField = "ID_TIPOASSTRATADO";
            ddlTipoAssunto.DataBind();

            ddlTipoAssunto.Items.Insert(0, new ListItem("Selecione", ""));
        }

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

        private void CarregarReuniao()
        {
            ddlReuniao.DataSource = NAcao.ConsultarReuniao();
            ddlReuniao.DataTextField = "TITULO";
            ddlReuniao.DataValueField = "ID_REUNIAO";
            ddlReuniao.DataBind();

            ddlReuniao.Items.Insert(0, new ListItem("Selecione", ""));
        }

        protected void btnIncluirAssunto_Click(object sender, ImageClickEventArgs e)
        {
            {
                REUNIAO_ASSUNTO_TRATADO assunto = new REUNIAO_ASSUNTO_TRATADO();
                assunto.DESCRICAO = TxtAssunto.Text;
                assunto.ID_TIPOASSTRATADO = Library.ToInteger(ddlTipoAssunto.SelectedValue);
                assunto.TIPO_ASSUNTO_TRATADO = NReuniao.ConsultarTipoAssuntoById( Library.ToInteger(ddlTipoAssunto.SelectedValue));
                assunto.ITEM = assuntos.Count + 1;
                assuntos.Add(assunto);
                grdAssunto.DataSource = assuntos;
                grdAssunto.DataBind();

            }
        }

      
        protected void btnIncluircompromisso_Click1(object sender, ImageClickEventArgs e)
        {
            {
                REUNIAO_COMPROMISSO compromisso = new REUNIAO_COMPROMISSO();
                compromisso.DESCRICAO = TxtCompromisso.Text;
                compromisso.DATA = new DateTime(int.Parse(ddlAno.Text), int.Parse(ddlMes.Text), int.Parse(ddlDia.Text));
                compromisso.ID_PESSOA = Library.ToInteger(ddlPessoa.SelectedValue);
                compromisso.PESSOA = NPessoa.ConsultarById( Library.ToInteger(ddlPessoa.SelectedValue));
                compromisso.ITEM = compromissos.Count + 1;
                compromissos.Add(compromisso);
                grdCompromisso.DataSource = compromissos;
                grdCompromisso.DataBind();
            }
        }

        protected void btnSalvarReuniao_Click(object sender, EventArgs e)
        {
         
            REUNIAO reuniao = new REUNIAO();    
           
            reuniao.ID_REUNIAO = int.Parse(ddlReuniao.SelectedValue);

            //EntityCollection<REUNIAO_ASSUNTO_TRATADO> reuniao_assuntos = new EntityCollection<REUNIAO_ASSUNTO_TRATADO>();
            //foreach (var obj in assuntos)
            //{
            //    REUNIAO_ASSUNTO_TRATADO reuniao_assunto_tratado = new REUNIAO_ASSUNTO_TRATADO();
            //    reuniao_assunto_tratado.DESCRICAO = obj.DESCRICAO;
            //    reuniao_assunto_tratado.ITEM = obj.ITEM;
            //    reuniao_assunto_tratado.ID_TIPOASSTRATADO = obj.ID_TIPOASSTRATADO;
            //    reuniao_assuntos.Add(reuniao_assunto_tratado);
            //}
            reuniao.REUNIAO_ASSUNTO_TRATADO = assuntos;// reuniao_assuntos;

            //EntityCollection<REUNIAO_COMPROMISSO> reuniao_compromissos = new EntityCollection<REUNIAO_COMPROMISSO>();
            //foreach (var obj in compromissos)
            //{
            //    REUNIAO_COMPROMISSO reuniao_compromisso = new REUNIAO_COMPROMISSO();
            //    reuniao_compromisso.ID_PESSOA = obj.ID_PESSOA;
            //    reuniao_compromisso.DESCRICAO = obj.DESCRICAO;
            //    reuniao_compromisso.ITEM = obj.ITEM;
            //    reuniao_compromisso.DATA = obj.DATA;

            //    reuniao_compromissos.Add(reuniao_compromisso);
            //}
            reuniao.REUNIAO_COMPROMISSO = compromissos;// reuniao_compromissos;
            

            NReuniao.Atualiza(reuniao);
            {
                REUNIAO_ATA reuniaoAta = new REUNIAO_ATA();
                reuniaoAta.ID_REUNIAO = reuniao.ID_REUNIAO;
                reuniaoAta.ID_FUNCIONARIO_RESPOSAVEL = hddResponsavelAta.Value.ToInt32();
                lblDataTeuniao.Text = reuniao.DATAHORA.ToDate().Day.ToString() + "/" + reuniao.DATAHORA.ToDate().Month.ToString() + "/" + reuniao.DATAHORA.ToDate().Year.ToString();
                reuniaoAta.DATA_FECHAMENTO = new DateTime(int.Parse(ddlAno.Text), int.Parse(ddlMes.Text), int.Parse(ddlDia.Text), 0, 0, 0);

                NReuniaoAta.Salvar(reuniaoAta);

                ClientScript.RegisterClientScriptBlock(GetType(), "Alert", "<script>alert('" + Const.MENSAGEM_INCLUSAO_SUCESSO + "'); history.go(-2);</script>");
            }
            
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

            var participantes = NReuniao.ConsultarParticipante(reuniao.ID_REUNIAO);
            grdParticipantesReuniao.DataSource = participantes;
            grdParticipantesReuniao.DataBind();

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

        protected void grdAssunto_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Excluir")
            {
                try
                {
                    int id = Convert.ToInt32(e.CommandArgument);

                    // Remove o item da lista
                    foreach (var assunto in assuntos)
                    {
                        if (assunto.ITEM == id)
                        {
                            assuntos.Remove(assunto);
                            // O item existe no banco, precisa ser deletado
                            if (assunto.ID_ASSTRAT > 0)
                                NReuniao.ExcluiAssuntoTratado(assunto.ID_ASSTRAT);

                            break;
                        }
                    }
                    // Reordena número dos titems
                    for (int i = 0; i <= assuntos.Count; i++)
                    {
                        assuntos[i].ITEM = i + 1;
                    }

                    grdAssunto.DataSource = assuntos;
                    grdAssunto.DataBind();

                    ClientScript.RegisterClientScriptBlock(GetType(), "Alert", "<script>alert('" + Const.MENSAGEM_EXCLUSAO_SUCESSO + "');</script>");
                }

                catch (Exception ex)
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "Alert", "<script>alert('" + Const.MENSAGEM_EXCLUSAO_ERRO + "');</script>");
                }
            }
        }

        protected void grdCompromisso_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Excluir")
            {
                try
                {
                    int id = Convert.ToInt32(e.CommandArgument);

                    // Remove o item da lista
                    foreach (var compromisso in compromissos)
                    {
                        if (compromisso.ITEM == id)
                        {
                            compromissos.Remove(compromisso);
                            // O item existe no banco, precisa ser deletado
                            if (compromisso.ID_COMPROMISSO > 0)
                                NReuniao.ExcluiCompromisso(compromisso.ID_COMPROMISSO);

                            break;
                        }
                    }
                    // Reordena número dos titems
                    for (int i = 0; i <= compromissos.Count; i++)
                    {
                        compromissos[i].ITEM = i + 1;
                    }

                    grdCompromisso.DataSource = compromissos;
                    grdCompromisso.DataBind();

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


   
