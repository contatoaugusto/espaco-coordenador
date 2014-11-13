﻿using System;
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

            EntityCollection<REUNIAO_ASSUNTO_TRATADO> reuniao_assuntos = new EntityCollection<REUNIAO_ASSUNTO_TRATADO>();
            foreach (var obj in assuntos)
            {
                REUNIAO_ASSUNTO_TRATADO reuniao_assunto_tratado = new REUNIAO_ASSUNTO_TRATADO();
                reuniao_assunto_tratado.DESCRICAO = obj.DESCRICAO;
                reuniao_assunto_tratado.ITEM = obj.ITEM;
                reuniao_assunto_tratado.ID_TIPOASSTRATADO = obj.ID_TIPOASSTRATADO;
                reuniao_assuntos.Add(reuniao_assunto_tratado);
            }
            reuniao.REUNIAO_ASSUNTO_TRATADO = reuniao_assuntos;

            EntityCollection<REUNIAO_COMPROMISSO> reuniao_compromissos = new EntityCollection<REUNIAO_COMPROMISSO>();
            foreach (var obj in compromissos)
            {
                REUNIAO_COMPROMISSO reuniao_compromisso = new REUNIAO_COMPROMISSO();
                reuniao_compromisso.ID_PESSOA = obj.ID_PESSOA;
                reuniao_compromisso.DESCRICAO = obj.DESCRICAO;
                reuniao_compromisso.ITEM = obj.ITEM;
                reuniao_compromisso.DATA = obj.DATA;

                reuniao_compromissos.Add(reuniao_compromisso);
            }
            reuniao.REUNIAO_COMPROMISSO = reuniao_compromissos;
            
            NReuniao.Salvar(reuniao);
            {
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
            List<PESSOA> pessoas = new List<PESSOA>();
            foreach (var participante in participantes)
            {
                PESSOA p = new PESSOA();
                p.ID_PESSOA = participante.PESSOA.ID_PESSOA;
                p.NOME = participante.PESSOA.NOME;
                p.TELEFONE = participante.PESSOA.TELEFONE;
                p.EMAIL = participante.PESSOA.EMAIL;
                pessoas.Add(p);
            }

            grdParticipantesReuniao.DataSource = pessoas;
            grdParticipantesReuniao.DataBind();

            var pautas = NReuniaoPauta.ConsultarByReuniao(reuniao.ID_REUNIAO);
            string strpauta = "<ul>";
            foreach (var pauta in pautas)
            {
                strpauta += "<li>" + pauta.ITEM + " - " + pauta.DESCRICAO + "</li>";
            }
            lblPauta.Text = strpauta + "</ul>";

            lblAtaElaboradaPor.Text = ((SessionUsuario)Session[Const.USUARIO]).USUARIO.FUNCIONARIO.PESSOA.NOME;
            
            
            pnlAta.Visible = true;
        }
      
    }
}


   