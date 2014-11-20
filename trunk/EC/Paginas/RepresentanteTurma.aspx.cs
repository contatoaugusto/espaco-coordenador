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
            // Controle Acesso
            int[] cargoComAcesso = { 2 };
            string mensagem = ControleAcesso.verificaAcesso(cargoComAcesso);
            if (!mensagem.IsNullOrEmpty())
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "Alert", "<script>alert('" + mensagem + "');location.replace('../default.aspx')</script>");
            }

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
            if (NRepresentanteTurma.ConsultarByAluno(hddAluno.Value.ToInt32()).Count > 0)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "Alert", "<script>alert('" + Const.MENSAGEM_CADASTRO_REPETIDO + "');</script>");
                return;
            }

            var alunoMatricula = NAlunoMatricula.ConsultarTByAluno(hddAluno.Value.ToInt32());

            if (alunoMatricula == null)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "Alert", "<script>alert('" + Const.MENSAGEM_CADASTRO_ALUNO_NAO_MATRICULADO + "');</script>");
                return;
            }

            REPRESENTANTE_TURMA representante = new REPRESENTANTE_TURMA();
            representante.ID_TIPOREPRESENTANTE = rdTitular.Checked ? 1 : 2;
            representante.ID_ALUNO_MATRICULA = alunoMatricula.ID_ALUNO_MATRICULA;
            representante.ID_TURMA = ddlTurma.SelectedValue.ToInt32();


            NRepresentanteTurma.Salvar(representante);

            ClientScript.RegisterClientScriptBlock(GetType(), "Alert", "<script>alert('" + Const.MENSAGEM_INCLUSAO_SUCESSO + "');</script>");
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            var aluno = NAluno.ConsultarByRA(txtRA.Text.ToInt32());

            pnlRepresentante.Visible = true;
            lblRA.Text = aluno.RA.ToString();
            lblNome.Text = aluno.PESSOA.NOME;
            hddAluno.Value = aluno.ID_ALUNO.ToString();

            CarregaSemestreCorrente();
            CarregaTurma();

            lblEmail.Text = aluno.PESSOA.EMAIL;
            lblTelefone.Text = aluno.PESSOA.TELEFONE;
            hddAluno.Value = aluno.ID_ALUNO.ToString();
        }


        protected void CarregaSemestreCorrente()
        {
            var semestre = NSemestre.ConsultarAtivo();

            if (semestre != null && semestre.ID_SEMESTRE > 0)
            {
                lblSemestreCorrente.Text = semestre.SEMESTRE1 + "º sem/" + semestre.ANO;
                hddSemestre.Value = semestre.ID_SEMESTRE.ToString();
            }
        }



        protected void CarregaTurma()
        {
            var turmas = NTurma.ConsultarTurmaByAlunoSemestre(hddAluno.Value.ToInt32(), hddSemestre.Value.ToInt32());

            ddlTurma.Items.Clear();

            ddlTurma.Items.Add(new ListItem("Selecione", "0"));

            if (turmas != null && turmas.Count > 0)
            {
                foreach (var turma in turmas)
                {
                    ddlTurma.Items.Add(new ListItem(string.Format("{0} / {1}", turma.TIPO_TURMA.DESCRICAO, turma.PERIODO_CURSO)));
                }
            }
            else {
                ClientScript.RegisterClientScriptBlock(GetType(), "Alert", "<script>alert('" + Const.MENSAGEM_CADASTRO_ALUNO_SEM_TURMA + "');</script>");
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {

        }
    }
}