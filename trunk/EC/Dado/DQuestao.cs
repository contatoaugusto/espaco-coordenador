using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Dado;
using EC.Modelo;
using EC.Dado;



namespace EC.Dado
{
    public class DQuestao
    {
        public List<AMC> ConsultarAmc()
        {
            using (ALICE2Entities db = new ALICE2Entities())
            {
                var c = db.AMC.ToList();
                List<Modelo.AMC> ltAmc = new List<AMC>();
                foreach (var tipo in c)
                {
                   AMC amc = new AMC();
                   amc.ID_AMC = tipo.ID_AMC;
                   amc.ANO = tipo.ANO;
                   amc.SEMESTRE = tipo.SEMESTRE;
                    ltAmc.Add(amc);
                }

                return ltAmc;
            }
        }

        public List<DISCIPLINA> ConsultarDisciplina()
        {
            using (ALICE2Entities db = new ALICE2Entities())
            {
                var d = db.DISCIPLINA.ToList();
                List<Modelo.DISCIPLINA> ltDisciplina = new List<DISCIPLINA>();
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
            using (ALICE2Entities db = new ALICE2Entities())
            {
                var f = db.FUNCIONARIO.ToList();
                List<Modelo.FUNCIONARIO> ltFuncionario = new List<FUNCIONARIO>();
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
            using (ALICE2Entities db = new ALICE2Entities())
            {
                var q = db.QUESTAO.Where(rs => rs.ID_DISCIPLINA == objquestao.ID_DISCIPLINA && rs.ID_FUNCIONARIO == objquestao.ID_FUNCIONARIO && rs.ID_AMC == objquestao.ID_AMC);
                    
                List<EC.Modelo.QUESTAO> ltQuestao = new List<EC.Modelo.QUESTAO>();

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
                    using (ALICE2Entities db = new ALICE2Entities())
                    {
                        var r = db.RESPOSTA.ToList();
                        List<EC.Modelo.RESPOSTA> ltResposta = new List<EC.Modelo.RESPOSTA>();

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
            using (ALICE2Entities db = new ALICE2Entities())
            {
                //Salva a questão
                db.QUESTAO.Add(q);
                db.SaveChanges();

                //Retorna o id novo gerado na inserção
                var id = q.ID_QUESTAO;
                

                foreach(RESPOSTA resposta in q.RESPOSTA)
                {
                    resposta.ID_QUESTAO = id;
                    db.RESPOSTA.Add(resposta);
                }
                db.SaveChanges();
                db.Dispose();
            }
        }     
    }
}