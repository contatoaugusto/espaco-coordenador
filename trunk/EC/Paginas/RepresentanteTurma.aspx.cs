using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EC.Common;
using EC.Negocio;
using EC.Modelo;

namespace UI.Web.EC.Paginas
{
    public partial class RepresentanteTurma : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //// Controle Acesso
            //int []cargoComAcesso = {2};
            //string mensagem = ControleAcesso.verificaAcesso(cargoComAcesso);
            //if (!mensagem.IsNullOrEmpty())
            //{
            //    ClientScript.RegisterClientScriptBlock(GetType(), "Alert", "<script>alert('" + mensagem + "');location.replace('../default.aspx')</script>");
            //}

            if (!IsPostBack)
            {
                //BindControl();
                //BindDataGrid();
            }
         }

        public void BindControl()
        {
            //CarregaSemestreCorrenteAMC();
        }

        public void BindDataGrid()
        {
        }


        protected void Salvar_Click1(object sender, EventArgs e)
        {

        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            var aluno = NAluno.ConsultarByRA(txtRA.Text.ToInt32());

            pnlRepresentante.Visible = true;
            lblRA.Text = aluno.RA.ToString();
            lblNome.Text = aluno.PESSOA.NOME;
            lblEmail.Text = aluno.PESSOA.EMAIL;
            lblTelefone.Text = aluno.PESSOA.TELEFONE;
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {

        }
    }
}