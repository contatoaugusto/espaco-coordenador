using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EC.Dado;
using EC.Modelo;
using EC.Negocio;


namespace EC.Paginas
{
    public partial class Questao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            {
                if (IsPostBack)
                    return;

                CarregarDisciplina();
                CarregarFuncionario();
                CarregarAmc();
            
            }
        }
        private void CarregarAmc()
        {
            ddlAmc.DataSource = EC.Negocio.NQuestão.ConsultarAmc();
            ddlAmc.DataTextField = "ANO";
            ddlAmc.DataValueField = "ID_AMC";
            ddlAmc.DataBind();
        }

        private void CarregarDisciplina()
        {
            ddlDisciplina.DataSource = EC.Negocio.NQuestão.ConsultarDisciplina();
            ddlDisciplina.DataTextField = "DESCRICAO";
            ddlDisciplina.DataValueField = "ID_DISCIPLINA";
            ddlDisciplina.DataBind();
        }

               
        private void CarregarFuncionario()
        {
            var lista = EC.Negocio.NQuestão.ConsultarFuncionario();

            foreach (FUNCIONARIO func in lista)
            {
                ddlFuncionario.Items.Add(new ListItem(func.PESSOA.NOME, func.ID_FUNCIONARIO.ToString()));
            }
        }

    
        protected void Button1_Click1(object sender, EventArgs e)
        {
            System.IO.Stream file = upLoad.PostedFile.InputStream;
            Byte[] buffer = new byte[file.Length];
            file.Read(buffer, 0,(int)file.Length);
            file.Close();
            
            QUESTAO questao = new QUESTAO();
            questao.ID_AMC = int.Parse(ddlAmc.SelectedValue);
            questao.ID_DISCIPLINA = int.Parse(ddlDisciplina.SelectedValue);
            questao.ID_FUNCIONARIO = int.Parse(ddlFuncionario.SelectedValue);
            questao.DESCRICAO = TxtDescricao.Text;
            questao.IMAGEM = buffer;
            List<RESPOSTA> listaResposta = new List<RESPOSTA>();
            
            RESPOSTA resposta1 = new RESPOSTA();
            resposta1.TEXTO = TxtEscolha1.Text;
            resposta1.RESPOSTA_CORRETA = Correta1.Checked;

            RESPOSTA resposta2 = new RESPOSTA();
            resposta2.TEXTO = TxtEscolha2.Text;
            resposta2.RESPOSTA_CORRETA = Correta2.Checked;

            RESPOSTA resposta3 = new RESPOSTA();
            resposta3.TEXTO = TxtEscolha3.Text;
            resposta3.RESPOSTA_CORRETA = Correta3.Checked;

            RESPOSTA resposta4 = new RESPOSTA();
            resposta4.TEXTO = TxtEscolha4.Text;
            resposta4.RESPOSTA_CORRETA = Correta4.Checked;

            RESPOSTA resposta5 = new RESPOSTA();
            resposta5.TEXTO = TxtEscolha5.Text;
            resposta5.RESPOSTA_CORRETA = Correta5.Checked;

            listaResposta.Add(resposta1);
            listaResposta.Add(resposta2);
            listaResposta.Add(resposta1);
            listaResposta.Add(resposta2);
            listaResposta.Add(resposta1);
          
            questao.RESPOSTA = listaResposta;
            NQuestão.Salvar(questao);
            Response.Redirect("BancoQuestao.aspx", true);
            }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("BancoQuestao.aspx", true);
        }

        }
    }
