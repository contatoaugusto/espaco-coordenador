using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
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
    public partial class Questao : System.Web.UI.Page
    {

        private int idQuestao
        {
            get { return Library.ToInteger(ViewState["idQuestao"]); }
            set { ViewState["idQuestao"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Controle Acesso
            int[] cargoComAcesso = { 1, 2 };
            string mensagem = ControleAcesso.verificaAcesso(cargoComAcesso);
            if (!mensagem.IsNullOrEmpty())
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "Alert", "<script>alert('" + mensagem + "');location.replace('../default.aspx')</script>");
            }

            if (IsPostBack)
                return;

            CarregarDisciplina();
            CarregarAmc();

            if (Request.QueryString["idQuestao"] != null)
            {
                idQuestao = Request.QueryString["idQuestao"].ToInt32();
                CarregarEdicao();
            }

        }

        private void CarregarEdicao()
        {
            var questao = NQuestao.ConsultarById(idQuestao);

            ddlAmc.SelectedValue = questao.ID_AMC.ToString();
            ddlDisciplina.SelectedValue = questao.ID_DISCIPLINA.ToString();

            //Funcionário
            CarregarFuncionario(ddlDisciplina.SelectedValue.ToInt32());
            ddlFuncionario.SelectedValue = questao.ID_FUNCIONARIO.ToString();

            TxtDescricao.Text = questao.DESCRICAO;


            int i = 0;
            foreach (var resposta in questao.RESPOSTA) {
            
                switch (i)
                {
                    case 0:
                        TxtEscolha1.Text = resposta.TEXTO;
                        Correta1.Checked = (bool)resposta.RESPOSTA_CORRETA;
                        hddIdResposta1.Value = resposta.ID_RESPOSTA.ToString();
                        break;
                    case 1:
                        TxtEscolha2.Text = resposta.TEXTO;
                        Correta2.Checked = (bool)resposta.RESPOSTA_CORRETA;
                        hddIdResposta2.Value = resposta.ID_RESPOSTA.ToString();
                        break;
                    case 2:
                        TxtEscolha3.Text = resposta.TEXTO;
                        Correta3.Checked = (bool)resposta.RESPOSTA_CORRETA;
                        hddIdResposta3.Value = resposta.ID_RESPOSTA.ToString();
                        break;
                    case 3:
                        TxtEscolha4.Text = resposta.TEXTO;
                        Correta4.Checked = (bool)resposta.RESPOSTA_CORRETA;
                        hddIdResposta4.Value = resposta.ID_RESPOSTA.ToString();
                        break;
                    case 4:
                        TxtEscolha5.Text = resposta.TEXTO;
                        Correta5.Checked = (bool)resposta.RESPOSTA_CORRETA;
                        hddIdResposta5.Value = resposta.ID_RESPOSTA.ToString();
                        break;
                }

                i++;
            }
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

        private void CarregarDisciplina()
        {
  
            var disciplina = NDisciplina.ConsultarByCurso(Utils.GetUsuarioLogado().IdCurso);
            ddlDisciplina.Items.Clear();

            ddlDisciplina.Items.Add(new ListItem("Selecione", "0"));

            foreach (DISCIPLINA dis in disciplina)
            {
                ddlDisciplina.Items.Add(new ListItem(dis.DESCRICAO, dis.ID_DISCIPLINA.ToString()));
            }

            CarregarFuncionario(ddlDisciplina.SelectedValue.ToInt32());
        }

               
        private void CarregarFuncionario(int idDisciplina)
        {
            var lista = NDisciplina.ConsultarProfessorByDisciplina(idDisciplina); //NQuestão.ConsultarFuncionario();

            ddlFuncionario.Items.Clear();

            ddlFuncionario.Items.Add(new ListItem("Selecione", "0"));
            
            foreach (FUNCIONARIO func in lista)
            {
                ddlFuncionario.Items.Add(new ListItem(func.PESSOA.NOME, func.ID_FUNCIONARIO.ToString()));
            }
        }


        protected void Button1_Click1(object sender, EventArgs e)
        {
            if (ddlAmc.SelectedIndex == 0)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "Alert", "<script>alert('" + Const.MENSAGEM_PREENCHER_CAMPOS + "'); history.go(-1);</script>");
                return;
            }
            if (ddlDisciplina.SelectedIndex == 0)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "Alert", "<script>alert('" + Const.MENSAGEM_PREENCHER_CAMPOS + "'); history.go(-1);</script>");
                return;
            }
            if (ddlFuncionario.SelectedIndex == 0)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "Alert", "<script>alert('" + Const.MENSAGEM_PREENCHER_CAMPOS + "'); history.go(-1);</script>");
                return;
            }

            
            System.IO.Stream file = upLoad.PostedFile.InputStream;
            Byte[] buffer = new byte[file.Length];
            file.Read(buffer, 0, (int)file.Length);
            file.Close();

            QUESTAO questao = new QUESTAO();

            questao.ID_AMC = ddlAmc.SelectedValue.ToInt32();
            questao.ID_DISCIPLINA = ddlDisciplina.SelectedValue.ToInt32();
            questao.ID_FUNCIONARIO = ddlFuncionario.SelectedValue.ToInt32();
            questao.DESCRICAO = TxtDescricao.Text;
            questao.IMAGEM = buffer;

            EntityCollection<RESPOSTA> listaResposta = new EntityCollection<RESPOSTA>();

            RESPOSTA resposta1 = new RESPOSTA();
            resposta1.TEXTO = TxtEscolha1.Text;
            resposta1.RESPOSTA_CORRETA = Correta1.Checked;
            resposta1.ID_RESPOSTA = Library.ToInteger(hddIdResposta1.Value);

            RESPOSTA resposta2 = new RESPOSTA();
            resposta2.TEXTO = TxtEscolha2.Text;
            resposta2.RESPOSTA_CORRETA = Correta2.Checked;
            resposta2.ID_RESPOSTA = Library.ToInteger(hddIdResposta2.Value);

            RESPOSTA resposta3 = new RESPOSTA();
            resposta3.TEXTO = TxtEscolha3.Text;
            resposta3.RESPOSTA_CORRETA = Correta3.Checked;
            resposta3.ID_RESPOSTA = Library.ToInteger(hddIdResposta3.Value);

            RESPOSTA resposta4 = new RESPOSTA();
            resposta4.TEXTO = TxtEscolha4.Text;
            resposta4.RESPOSTA_CORRETA = Correta4.Checked;
            resposta4.ID_RESPOSTA = Library.ToInteger(hddIdResposta4.Value);

            RESPOSTA resposta5 = new RESPOSTA();
            resposta5.TEXTO = TxtEscolha5.Text;
            resposta5.RESPOSTA_CORRETA = Correta5.Checked;
            resposta5.ID_RESPOSTA = Library.ToInteger(hddIdResposta5.Value);

            listaResposta.Add(resposta1);
            listaResposta.Add(resposta2);
            listaResposta.Add(resposta3);
            listaResposta.Add(resposta4);
            listaResposta.Add(resposta5);

            if (idQuestao > 0)
            {
                questao.ID_QUESTAO = idQuestao;
                NQuestao.Atualiza(questao);
                NResposta.Salvar(listaResposta, questao.ID_QUESTAO);
                ClientScript.RegisterClientScriptBlock(GetType(), "Alert", "<script>alert('" + Const.MENSAGEM_ALTERACAO_SUCESSO + "'); history.go(-1);</script>");
            }
            else
            {
                NQuestao.Salvar(questao);
                NResposta.Salvar(listaResposta, questao.ID_QUESTAO);
                ClientScript.RegisterClientScriptBlock(GetType(), "Alert", "<script>alert('" + Const.MENSAGEM_INCLUSAO_SUCESSO + "'); history.go(-2);</script>");
            }   
           
            //Response.Redirect("Questao.aspx", true);
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("BancoQuestao.aspx", true);
        }

        protected void ddlDisciplina_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarFuncionario(Library.ToInteger(ddlDisciplina.SelectedValue));
        }

        }
    }
