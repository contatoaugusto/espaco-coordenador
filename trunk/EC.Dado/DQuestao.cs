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

        public List<FUNCIONARIO> ConsultarFuncionario()
        {
            using (ECEntities db = new ECEntities())
            {
                var f = db.FUNCIONARIO.ToList();
                List<FUNCIONARIO> ltFuncionario = new List<FUNCIONARIO>();
                foreach (var tipo in f)
                {                    
                  FUNCIONARIO funcionario = new FUNCIONARIO();
                  funcionario.ID_FUNCIONARIO = tipo.ID_FUNCIONARIO;
                  funcionario.PESSOA = new PESSOA();
                  funcionario.PESSOA.NOME = tipo.PESSOA.NOME;
                  ltFuncionario.Add(funcionario);
                }

                return ltFuncionario;
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
                    questao.DISCIPLINA.DESCRICAO = tipo.DISCIPLINA.DESCRICAO;
                    ltQuestao.Add(questao);
                }

                return ltQuestao;
            }
        }

        public List<QUESTAO> ConsultarQuestaoGeraProva(int idAmc, int idCurso, int qtdeQuestoes)
        {
            using (ECEntities db = new ECEntities())
            {
                var q = db.QUESTAO.Where(rs => rs.ID_AMC == idAmc && rs.id == objquestao.ID_FUNCIONARIO && rs.ID_AMC == objquestao.ID_AMC);

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
                    questao.DISCIPLINA.DESCRICAO = tipo.DISCIPLINA.DESCRICAO;
                    ltQuestao.Add(questao);
                }

                return ltQuestao;
            }
        }

                public List<RESPOSTA> ConsultarResposta()
                {
                    using (ECEntities db = new ECEntities())
                    {
                        var r = db.RESPOSTA.ToList();
                        List<RESPOSTA> ltResposta = new List<RESPOSTA>();

                        foreach (var tipo in r)
                        {
                            RESPOSTA resposta = new RESPOSTA();
                            resposta.ID_QUESTAO = tipo.ID_RESPOSTA;
                            resposta.TEXTO = tipo.TEXTO;
                           ltResposta.Add(resposta);
                        }

                        return ltResposta;
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