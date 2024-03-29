﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EC.Negocio;
using EC.Modelo;
using EC.Common;

namespace UI.Web.EC.Paginas
{
    public partial class ConsultarReuniao : System.Web.UI.Page
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

               CarregarReuniao();
               CarregarTipoReuniao();
       
            }
        }

        private void CarregarTipoReuniao()
        {
            ddlTipoReuniao.DataSource = NReuniao.ConsultarTipoReuniao();
            ddlTipoReuniao.DataTextField = "DESCRICAO";
            ddlTipoReuniao.DataValueField = "ID_TIPOREUNIAO";
            ddlTipoReuniao.DataBind();

            ddlTipoReuniao.Items.Insert(0, new ListItem("Selecione", ""));
        }

        private void CarregarReuniao()
        {
            REUNIAO reuniao = new REUNIAO();
            grdReuniao.DataSource = NReuniao.ConsultarReuniao(reuniao);
            grdReuniao.DataBind();

        }

        protected void btNovo_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgendaReuniao.aspx", true);
        }

        protected void btConsultar_Click(object sender, EventArgs e)
        {
            {
                if (ddlTipoReuniao.SelectedValue == "")
                {
                    //MENSAGEM
                }
                else
                {
                    CarregarReunioes();
                }
            }
        }

        private void CarregarReunioes()
        {
            REUNIAO reuniao = new REUNIAO();
            if (ddlTipoReuniao.SelectedValue != "")
                reuniao.ID_TIPOREUNIAO = Convert.ToInt32(ddlTipoReuniao.SelectedValue);

            var lista = NReuniao.ConsultarReuniao(reuniao);

            grdReuniao.DataSource = lista;
            grdReuniao.DataBind();
        }

        protected void grdItens_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdReuniao.PageIndex = e.NewPageIndex;
            CarregarReunioes();
        }

        protected void grdReuniao_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                Context.Response.Redirect("~/Paginas/AgendaReuniao.aspx?idReuniao=" + Library.ToInteger(e.CommandArgument));
            }

            if (e.CommandName == "Excluir")
            {
                REUNIAO r = new REUNIAO();
                r.ID_REUNIAO = Convert.ToInt32(e.CommandArgument);

                NReuniao.Excluir(r);
                CarregarReunioes();
            }            
        }

        protected void grdReuniao_DataBound(object sender, EventArgs e)
        {

        }

        protected void HyperLink1_DataBinding(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)(sender);

            var ata = NReuniaoAta.ConsultarByReuniao(Eval("ID_REUNIAO").ToInt32());
            if (ata != null)
                btn.Enabled = false;
        }
    }
}