using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EC.Negocio;
using EC.Modelo;
using EC.Common;
using System.Data;

namespace UI.Web.EC.Paginas
{
    public partial class Semestre : System.Web.UI.Page
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

            if (!IsPostBack)
            {
                BindDataGrid();
                CarregarDados();
                
            }
        }

        public void BindDataGrid()
        {
            var semestre = NSemestre.Consultar();

            gridViewSemestre.DataSource = semestre;
            gridViewSemestre.DataBind();

        }

        private void CarregarDados()
        {
            var semestre = NSemestre.ConsultarAtivo(); //NSemestre.Consultar().OrderByDescending(rs => rs.ID_SEMESTRE).First();

            if (semestre != null && semestre.ID_SEMESTRE > 0)
            {
                pnlSemestreAberto.Visible = true;
                lblAnoInicio.Text = "2014";
                lblSemestreInicio.Text = "1";
                lblAnoCorrente.Text = semestre.ANO.ToString();
                lblSemestreCorrente.Text = semestre.SEMESTRE1.ToString();
            }
            else
                pnlSemestreAberto.Visible = false;

            BindDataGrid();
        }

    
        protected void BtnAbrirSemestreClick(object sender, EventArgs e)
        {

            var se = new SEMESTRE();
            se.SEMESTRE1 = Library.ToInteger( lblSemestreCorrente.Text.Equals("1") ? "2" : "1");
            se.ANO = Library.ToInteger(lblSemestreCorrente.Text) == 2 ? Library.ToInteger(new DateTime(Convert.ToInt32(lblAnoCorrente.Text), 1, 1).AddYears(1).Year) : Library.ToInteger(lblAnoCorrente.Text);
            se.ATIVO = true;

            if (se.ANO == 0)
                se.ANO = DateTime.Now.Year;
            
            NSemestre.Salvar(se);

            AddSemestreSessao(se.ID_SEMESTRE);

            CarregarDados();
        }

        protected void ATIVO_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            
            // Ativar esse semestre
            if (cb.Checked)
            {
                GridViewRow row = (GridViewRow)cb.Parent.Parent; // pegar a linha pai desta checkbox
                int codigo = Convert.ToInt32(gridViewSemestre.DataKeys[row.RowIndex].Value); //pegar o código da datakey da linha

                SEMESTRE semestre = new SEMESTRE();
                semestre = NSemestre.ConsultarById(codigo);
                semestre.ATIVO = true;

                NSemestre.Atualizar(semestre);

                AddSemestreSessao(semestre.ID_SEMESTRE);

                ClientScript.RegisterClientScriptBlock(GetType(), "Alert", "<script>alert('"+Const.MENSAGEM_INCLUSAO_SUCESSO +"');</script>");
            }
        }


        private void AddSemestreSessao(int idSemestre)
        {
            SessionUsuario usuario = Utils.GetUsuarioLogado();
            usuario.IdSemestre = idSemestre;
            Session[Const.USUARIO] = usuario;
        }

    }
}