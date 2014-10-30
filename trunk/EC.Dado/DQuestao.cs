using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Modelo;

namespace EC.Dado
{
    public class DQuestao
    {

        public List<QUESTAO> ConsultarQuestao()
        {
            using (ECEntities db = new ECEntities())
            {
                var q = db.QUESTAO.ToList();

                List<QUESTAO> ltQuestao = new List<QUESTAO>();
                
                foreach (var tipo in q)
                {
                    QUESTAO questao = new QUESTAO();
                    questao.ID_QUESTAO = tipo.ID_QUESTAO;
                    questao.DESCRICAO = tipo.DESCRICAO;
                    questao.IMAGEM = tipo.IMAGEM;

                    questao.AMC = tipo.AMC;
                    questao.PROVA = tipo.PROVA;

                    //questao.FUNCIONARIO = db.FUNCIONARIO.First(rs => rs.ID_FUNCIONARIO == tipo.FUNCIONARIO.ID_FUNCIONARIO);
                    //questao.DISCIPLINA = db.DISCIPLINA.First(rs => rs.ID_DISCIPLINA == tipo.DISCIPLINA.ID_DISCIPLINA);

                   
                    questao.FUNCIONARIO = new FUNCIONARIO();
                    questao.FUNCIONARIO.PESSOA = new PESSOA();
                    questao.FUNCIONARIO.PESSOA.ID_PESSOA = tipo.FUNCIONARIO.PESSOA.ID_PESSOA;
                    questao.FUNCIONARIO.PESSOA.NOME = tipo.FUNCIONARIO.PESSOA.NOME;
                    questao.FUNCIONARIO.PESSOA.TELEFONE = tipo.FUNCIONARIO.PESSOA.TELEFONE;
                    questao.FUNCIONARIO.PESSOA.EMAIL = tipo.FUNCIONARIO.PESSOA.EMAIL;

                    questao.DISCIPLINA = new DISCIPLINA();
                    questao.DISCIPLINA.ID_DISCIPLINA = tipo.DISCIPLINA.ID_DISCIPLINA;
                    questao.DISCIPLINA.ID_CURSO = tipo.DISCIPLINA.ID_CURSO;
                    questao.DISCIPLINA.DESCRICAO = tipo.DISCIPLINA.DESCRICAO;
                    

                    ltQuestao.Add(questao);
                }

                return ltQuestao;
            }
        }
        
        public List<QUESTAO> ConsultarQuestao(QUESTAO objquestao)
        {
            using (ECEntities db = new ECEntities())
            {
                var q = db.QUESTAO.Where(rs => rs.ID_DISCIPLINA == objquestao.ID_DISCIPLINA && rs.ID_FUNCIONARIO == objquestao.ID_FUNCIONARIO && rs.ID_AMC == objquestao.ID_AMC);

                if (q.Count() == 0)
                {
                    if (objquestao.ID_AMC != null && objquestao.ID_AMC > 0 &&
                        objquestao.ID_DISCIPLINA != null && objquestao.ID_DISCIPLINA > 0)
                        q = db.QUESTAO.Where(rs => rs.ID_AMC == objquestao.ID_AMC && rs.ID_DISCIPLINA == objquestao.ID_DISCIPLINA);

                    else
                        if (objquestao.ID_AMC != null && objquestao.ID_AMC > 0 &&
                            objquestao.ID_FUNCIONARIO != null && objquestao.ID_FUNCIONARIO > 0)
                            q = db.QUESTAO.Where(rs => rs.ID_AMC == objquestao.ID_AMC && rs.ID_FUNCIONARIO == objquestao.ID_FUNCIONARIO);
                        else
                            if (objquestao.ID_DISCIPLINA != null && objquestao.ID_DISCIPLINA > 0 &&
                                objquestao.ID_FUNCIONARIO != null && objquestao.ID_FUNCIONARIO > 0)
                                q = db.QUESTAO.Where(rs => rs.ID_DISCIPLINA == objquestao.ID_DISCIPLINA && rs.ID_FUNCIONARIO == objquestao.ID_FUNCIONARIO);
                            else
                                if (objquestao.ID_AMC != null && objquestao.ID_AMC > 0)
                                    q = db.QUESTAO.Where(rs => rs.ID_AMC == objquestao.ID_AMC);
                                else
                                    if (objquestao.ID_FUNCIONARIO != null && objquestao.ID_FUNCIONARIO > 0)
                                        q = db.QUESTAO.Where(rs => rs.ID_FUNCIONARIO == objquestao.ID_FUNCIONARIO);
                                    else
                                        if (objquestao.ID_DISCIPLINA != null && objquestao.ID_DISCIPLINA > 0)
                                            q = db.QUESTAO.Where(rs => rs.ID_DISCIPLINA == objquestao.ID_DISCIPLINA);
                }
                
                List<QUESTAO> ltQuestao = new List<QUESTAO>();

                foreach (var tipo in q)
                {     
                    QUESTAO questao = new QUESTAO();
                    questao.ID_QUESTAO = tipo.ID_QUESTAO;
                    questao.DESCRICAO = tipo.DESCRICAO;
                    questao.FUNCIONARIO = new FUNCIONARIO();
                    questao.FUNCIONARIO.PESSOA = new PESSOA();
                    questao.FUNCIONARIO.PESSOA.NOME = tipo.FUNCIONARIO.PESSOA.NOME;
                    questao.DISCIPLINA = new DISCIPLINA();
                    questao.DISCIPLINA.ID_DISCIPLINA = tipo.DISCIPLINA.ID_DISCIPLINA;
                    questao.DISCIPLINA.DESCRICAO = tipo.DISCIPLINA.DESCRICAO;
                    ltQuestao.Add(questao);
                }

                return ltQuestao;
            }
        }

        public List<QUESTAO> ConsultarQuestaoByProva(int idProva)
        {
            using (ECEntities db = new ECEntities())
            {
                var q = db.QUESTAO.Where(rs => rs.ID_PROVA == idProva);

                List<QUESTAO> ltQuestao = new List<QUESTAO>();

                foreach (var tipo in q)
                {
                    QUESTAO questao = new QUESTAO();
                    questao.ID_QUESTAO = tipo.ID_QUESTAO;
                    questao.DESCRICAO = tipo.DESCRICAO;
                    questao.IMAGEM = tipo.IMAGEM;

                    //DDisciplina disciplina = new DDisciplina();
                    questao.DISCIPLINA = tipo.DISCIPLINA; //disciplina.ConsultarById(tipo.DISCIPLINA.ID_DISCIPLINA);

                    //DFuncionario funcionario = new DFuncionario();
                    questao.FUNCIONARIO = tipo.FUNCIONARIO; //funcionario.ConsultarById(tipo.FUNCIONARIO.ID_FUNCIONARIO);

                    //Respostas dessa questão
                    DResposta resposta = new DResposta();
                    var resp = resposta.ConsultarRespostaByQuestao(tipo.ID_QUESTAO);

                    foreach (var r in resp)
                    {
                        RESPOSTA obj = new RESPOSTA();
                        obj.ID_RESPOSTA = r.ID_RESPOSTA;
                        obj.ID_QUESTAO = r.ID_QUESTAO;
                        obj.TEXTO = r.TEXTO;
                        obj.RESPOSTA_CORRETA = r.RESPOSTA_CORRETA;
                        questao.RESPOSTA.Add(obj);
                    }

                    ltQuestao.Add(questao);
                }

                return ltQuestao;
            }
        }

        public List<QUESTAO> ConsultarQuestaoProvaByAmcCurso(int idAmc, int idCurso)
        {
            using (ECEntities db = new ECEntities())
            {
                var q = db.QUESTAO.Where(rs => rs.ID_AMC == idAmc && rs.DISCIPLINA.ID_CURSO == idCurso && rs.ID_PROVA == null);

                List<QUESTAO> ltQuestao = new List<QUESTAO>();

                foreach (var tipo in q)
                {
                    QUESTAO questao = new QUESTAO();
                    questao.ID_QUESTAO = tipo.ID_QUESTAO;
                    questao.DESCRICAO = tipo.DESCRICAO;
                    questao.FUNCIONARIO = new FUNCIONARIO();
                    questao.FUNCIONARIO.PESSOA = new PESSOA();
                    questao.FUNCIONARIO.PESSOA.NOME = tipo.FUNCIONARIO.PESSOA.NOME;
                    questao.DISCIPLINA = new DISCIPLINA();
                    questao.DISCIPLINA.ID_DISCIPLINA = tipo.DISCIPLINA.ID_DISCIPLINA;
                    questao.DISCIPLINA.DESCRICAO = tipo.DISCIPLINA.DESCRICAO;
                    ltQuestao.Add(questao);
                }

                return ltQuestao;
            }
        }

        
        public void Salvar(QUESTAO q)
        {   
            using (ECEntities db = new ECEntities())
            {
                //Salva a questão
                db.QUESTAO.AddObject(q);
                db.SaveChanges();

                //Retorna o id novo gerado na inserção
                var id = q.ID_QUESTAO;
            }
        }     
    }
}