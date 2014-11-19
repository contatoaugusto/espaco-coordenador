﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EC.Common;
using EC.Negocio;
using EC.Modelo;
using EC.UI.WebControls;

namespace UI.Web.EC.Paginas
{
    public partial class ConsultarLancamento : System.Web.UI.Page
    {
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

                CarregarFuncionario();
                CarregarTipolancamento();
                CarregarLancamento();
             }
        }

        private void CarregarFuncionario()
        {
            var lista = NFuncionario.ConsultarFuncionario();

            foreach (FUNCIONARIO func in lista)
            {
                ddlFuncionario.Items.Add(new ListItem(func.PESSOA.NOME, func.ID_FUNCIONARIO.ToString()));
            }

            ddlFuncionario.Items.Insert(0, new ListItem("Selecione", ""));
        }

      
        private void CarregarTipolancamento()
        {
            ddlTipolancamento.DataSource = NLancamento.ConsultarTipolancamento();
            ddlTipolancamento.DataTextField = "DESCRICAO";
            ddlTipolancamento.DataValueField = "ID_TIPOLANCAMENTO";
            ddlTipolancamento.DataBind();
            ddlTipolancamento.Items.Insert(0, new ListItem("Selecione", ""));
        }

        private void CarregarLancamento()
        {
            grdLancamento.DataSource = NLancamento.ConsultarLancamento();
            grdLancamento.DataBind();
        }

        protected void btnovo_Click(object sender, EventArgs e)
        {
            Response.Redirect("Lancamento.aspx", true);
        }

        protected void btConsultar_Click(object sender, EventArgs e)
        {

        }

        protected void grdLancamento_SelectedIndexChanged(object sender, GridViewPageEventArgs e)
        {
         
            grdLancamento.PageIndex = e.NewPageIndex;
            CarregarLancamento();
        }
        }

    }
