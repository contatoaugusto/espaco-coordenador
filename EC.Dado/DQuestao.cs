using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Modelo;

namespace EC.Dado
{
    public class DQuestao
    {
        public List<AMC> ConsultarAmc()
        {
            using (ECEntities db = new ECEntities())
            {
                var c = db.AMC.ToList();
                List<AMC> ltAmc = new List<AMC>();
                foreach (var tipo in c)
                {
                    AMC amc = new AMC();
                    amc.ID_AMC = tipo.ID_AMC;
                    amc.ANO = tipo.ANO;
                    amc.SEMESTRE = tipo.SEMESTRE;
                    amc.DATA_APLICACAO = tipo.DATA_APLICACAO;
                    
                    ltAmc.Add(amc);
                }

                return ltAmc;
            }
        }

        public List<DISCIPLINA> ConsultarDisciplina()
        {
            using (ECEntities db = new ECEntities())
            {
                var d = db.DISCIPLINA.ToList();
                List<DISCIPLINA> ltDisciplina = new List<DISCIPLINA>();
                foreach (var tipo in d)
                {
                      DISCIPLINA disciplina = new DISCIPLINA();
                      disciplina.ID_DISCIPLINA = tipo.ID_DISCIPLINA;
                      disciplina.DESCRICAO = tipo.DESCRICAO;  
                      ltDisciplina.Add(disciplina);
                }

                return ltDisciplina;
            }
        }

        

        public List<QUESTAO> ConsultarQuestao(QUESTAO objquestao)
        {
            using (ECEntities db = new ECEntities())
            {
                var q = db.QUESTAO.Where(rs => rs.ID_DISCIPLINA == objquestao.ID_DISCIPLINA && rs.ID_FUNCIONARIO == objquestao.ID_FUNCIONARIO && rs.ID_AMC == objquestao.ID_AMC);
                    
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
                

                //foreach(RESPOSTA resposta in q.RESPOSTA)
                //{
                //    resposta.ID_QUESTAO = id;
                //    db.RESPOSTA.AddObject(resposta);
                //}
                db.SaveChanges();
                db.Dispose();
            }
        }     
    }
}