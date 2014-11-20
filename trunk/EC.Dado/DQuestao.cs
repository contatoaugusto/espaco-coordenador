using System;
using System.Data.Objects.DataClasses;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Modelo;
using System.Transactions;

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

        //public QUESTAO ConsultarById(int idQuestao)
        //{
        //    using (ECEntities db = new ECEntities())
        //    {
        //        var questao = db.QUESTAO.First(rs => rs.ID_QUESTAO == idQuestao);

        //        questao.AMC = db.AMC.First(rs => rs.ID_AMC == questao.ID_AMC);
        //        //questao.PROVA = tipo.PROVA;

        //        //questao.FUNCIONARIO = new FUNCIONARIO();
        //        //questao.FUNCIONARIO.PESSOA = new PESSOA();
        //        //questao.FUNCIONARIO.PESSOA.ID_PESSOA = tipo.FUNCIONARIO.PESSOA.ID_PESSOA;
        //        //questao.FUNCIONARIO.PESSOA.NOME = tipo.FUNCIONARIO.PESSOA.NOME;
        //        //questao.FUNCIONARIO.PESSOA.TELEFONE = tipo.FUNCIONARIO.PESSOA.TELEFONE;
        //        //questao.FUNCIONARIO.PESSOA.EMAIL = tipo.FUNCIONARIO.PESSOA.EMAIL;

        //        //questao.DISCIPLINA = new DISCIPLINA();
        //        //questao.DISCIPLINA.ID_DISCIPLINA = tipo.DISCIPLINA.ID_DISCIPLINA;
        //        //questao.DISCIPLINA.ID_CURSO = tipo.DISCIPLINA.ID_CURSO;
        //        //questao.DISCIPLINA.DESCRICAO = tipo.DISCIPLINA.DESCRICAO;


        //        //var respostas = db.RESPOSTA.Where(rs => rs.ID_QUESTAO == idQuestao);

        //        //foreach (var resposta in respostas)
        //        //{
        //        //    RESPOSTA resp = new RESPOSTA();
        //        //    resp.ID_RESPOSTA = resposta.ID_RESPOSTA;
        //        //    resp.ID_QUESTAO = resposta.ID_QUESTAO;
        //        //    resp.TEXTO = resposta.TEXTO;
        //        //    resp.RESPOSTA_CORRETA = resposta.RESPOSTA_CORRETA;

        //        //    questao.RESPOSTA.Add(resp);
        //        //}

        //        return questao;
        //    }
        //}
        public QUESTAO ConsultarById(int idQuestao)
        {
            using (ECEntities db = new ECEntities())
            {
                var q = db.QUESTAO.Where(rs => rs.ID_QUESTAO == idQuestao);

                QUESTAO questao = new QUESTAO();

                foreach (var tipo in q)
                {

                    questao.ID_QUESTAO = tipo.ID_QUESTAO;
                    questao.DESCRICAO = tipo.DESCRICAO;
                    questao.IMAGEM = tipo.IMAGEM;
                    questao.ID_AMC = tipo.ID_AMC;
                    questao.ID_DISCIPLINA = tipo.ID_DISCIPLINA;
                    questao.ID_FUNCIONARIO = tipo.ID_FUNCIONARIO;
                    
                    questao.DISCIPLINA = db.DISCIPLINA.First(rs => rs.ID_DISCIPLINA == tipo.ID_DISCIPLINA);
                    questao.FUNCIONARIO = db.FUNCIONARIO.First(rs => rs.ID_FUNCIONARIO == tipo.ID_FUNCIONARIO); //funcionario.ConsultarById(tipo.FUNCIONARIO.ID_FUNCIONARIO);

                    //Respostas dessa questão
                    var resp = db.RESPOSTA.Where(rs => rs.ID_QUESTAO == idQuestao);

                    foreach (var r in resp)
                    {
                        RESPOSTA obj = new RESPOSTA();
                        obj.ID_RESPOSTA = r.ID_RESPOSTA;
                        obj.ID_QUESTAO = r.ID_QUESTAO;
                        obj.TEXTO = r.TEXTO;
                        obj.RESPOSTA_CORRETA = r.RESPOSTA_CORRETA;
                        questao.RESPOSTA.Add(obj);
                    }
                }

                return questao;
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
                    questao.NU_SEQUENCIA_PROVA = tipo.NU_SEQUENCIA_PROVA;
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
                    questao.ID_AMC = tipo.ID_AMC;
                    questao.NU_SEQUENCIA_PROVA = tipo.NU_SEQUENCIA_PROVA;
                    questao.ID_DISCIPLINA = tipo.ID_DISCIPLINA;
                    questao.ID_FUNCIONARIO = tipo.ID_FUNCIONARIO;

                    questao.DISCIPLINA = db.DISCIPLINA.First(rs => rs.ID_DISCIPLINA == tipo.ID_DISCIPLINA);
                    questao.FUNCIONARIO = db.FUNCIONARIO.First(rs => rs.ID_FUNCIONARIO == tipo.ID_FUNCIONARIO); //funcionario.ConsultarById(tipo.FUNCIONARIO.ID_FUNCIONARIO);

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

        public List<QUESTAO> ConsultarQuestaoProvaByAmc(int idAmc)
        {
            using (ECEntities db = new ECEntities())
            {
                var q = db.QUESTAO.Where(rs => rs.ID_AMC == idAmc && rs.ID_PROVA != null);

                List<QUESTAO> ltQuestao = new List<QUESTAO>();

                foreach (var tipo in q)
                {
                    QUESTAO questao = new QUESTAO();
                    questao.ID_QUESTAO = tipo.ID_QUESTAO;
                    questao.DESCRICAO = tipo.DESCRICAO;
                    questao.IMAGEM = tipo.IMAGEM;
                    questao.ID_AMC = tipo.ID_AMC;
                    questao.NU_SEQUENCIA_PROVA = tipo.NU_SEQUENCIA_PROVA;
                    questao.ID_DISCIPLINA = tipo.ID_DISCIPLINA;
                    questao.ID_FUNCIONARIO = tipo.ID_FUNCIONARIO;

                    questao.DISCIPLINA = db.DISCIPLINA.First(rs => rs.ID_DISCIPLINA == tipo.ID_DISCIPLINA);
                    questao.FUNCIONARIO = db.FUNCIONARIO.First(rs => rs.ID_FUNCIONARIO == tipo.ID_FUNCIONARIO); //funcionario.ConsultarById(tipo.FUNCIONARIO.ID_FUNCIONARIO);

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
                var q = db.QUESTAO.Where(rs => rs.ID_AMC == idAmc && rs.DISCIPLINA.CURSO.ID_CURSO == idCurso && rs.ID_PROVA != null).OrderBy(rs => rs.NU_SEQUENCIA_PROVA);

                List<QUESTAO> ltQuestao = new List<QUESTAO>();

                foreach (var tipo in q)
                {
                    QUESTAO questao = new QUESTAO();
                    questao.ID_QUESTAO = tipo.ID_QUESTAO;
                    questao.DESCRICAO = tipo.DESCRICAO;
                    questao.IMAGEM = tipo.IMAGEM;
                    questao.ID_AMC = tipo.ID_AMC;
                    questao.NU_SEQUENCIA_PROVA = tipo.NU_SEQUENCIA_PROVA;
                    questao.ID_DISCIPLINA = tipo.ID_DISCIPLINA;
                    questao.ID_FUNCIONARIO = tipo.ID_FUNCIONARIO;

                    questao.DISCIPLINA = db.DISCIPLINA.First(rs => rs.ID_DISCIPLINA == tipo.ID_DISCIPLINA);
                    questao.FUNCIONARIO = db.FUNCIONARIO.First(rs => rs.ID_FUNCIONARIO == tipo.ID_FUNCIONARIO); //funcionario.ConsultarById(tipo.FUNCIONARIO.ID_FUNCIONARIO);


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

        public List<QUESTAO> ConsultarQuestaoByNomeProfessor(string nomeProfessor)
        {
            using (ECEntities db = new ECEntities())
            {
                var q = 
                    (from questao in db.QUESTAO
                     join func in db.FUNCIONARIO on questao.ID_FUNCIONARIO equals func.ID_FUNCIONARIO
                     join pessoa in db.PESSOA on func.ID_PESSOA equals pessoa.ID_PESSOA
                     where pessoa.NOME == nomeProfessor
                     select new
                     {
                         questao.ID_QUESTAO,
                         questao.DESCRICAO,
                         questao.IMAGEM,
                         questao.ID_DISCIPLINA,
                         questao.ID_FUNCIONARIO,
                         questao.ID_AMC,
                         questao.NU_SEQUENCIA_PROVA
                     });

                List<QUESTAO> ltQuestao = new List<QUESTAO>();

                foreach (var tipo in q)
                {
                    QUESTAO questao = new QUESTAO();
                    questao.ID_QUESTAO = tipo.ID_QUESTAO;
                    questao.DESCRICAO = tipo.DESCRICAO;
                    questao.IMAGEM = tipo.IMAGEM;
                    questao.ID_AMC = tipo.ID_AMC;
                    questao.NU_SEQUENCIA_PROVA = tipo.NU_SEQUENCIA_PROVA;
                    questao.ID_DISCIPLINA = tipo.ID_DISCIPLINA;
                    questao.ID_FUNCIONARIO = tipo.ID_FUNCIONARIO;

                    questao.DISCIPLINA = db.DISCIPLINA.First(rs => rs.ID_DISCIPLINA == tipo.ID_DISCIPLINA);
                    questao.FUNCIONARIO = db.FUNCIONARIO.First(rs => rs.ID_FUNCIONARIO == tipo.ID_FUNCIONARIO); //funcionario.ConsultarById(tipo.FUNCIONARIO.ID_FUNCIONARIO);

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

        public List<QUESTAO> ConsultarQuestaoByAmc(int idAmc)
        {
            using (ECEntities db = new ECEntities())
            {
                var q = db.QUESTAO.Where(rs => rs.ID_AMC == idAmc);

                List<QUESTAO> ltQuestao = new List<QUESTAO>();

                foreach (var tipo in q)
                {
                    QUESTAO questao = new QUESTAO();
                    questao.ID_QUESTAO = tipo.ID_QUESTAO;
                    questao.DESCRICAO = tipo.DESCRICAO;
                    questao.IMAGEM = tipo.IMAGEM;
                    questao.ID_AMC = tipo.ID_AMC;
                    questao.NU_SEQUENCIA_PROVA = tipo.NU_SEQUENCIA_PROVA;
                    questao.ID_DISCIPLINA = tipo.ID_DISCIPLINA;
                    questao.ID_FUNCIONARIO = tipo.ID_FUNCIONARIO;

                    questao.DISCIPLINA = db.DISCIPLINA.First(rs => rs.ID_DISCIPLINA == tipo.ID_DISCIPLINA);
                    questao.FUNCIONARIO = db.FUNCIONARIO.First(rs => rs.ID_FUNCIONARIO == tipo.ID_FUNCIONARIO); //funcionario.ConsultarById(tipo.FUNCIONARIO.ID_FUNCIONARIO);


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

        //public List<QUESTAO> ConsultarQuestaoProvaByAmcCurso(int idAmc, int idCurso)
        //{
        //    using (ECEntities db = new ECEntities())
        //    {
        //        var q = db.QUESTAO.Where(rs => rs.ID_AMC == idAmc && rs.DISCIPLINA.ID_CURSO == idCurso && rs.ID_PROVA == null);

        //        List<QUESTAO> ltQuestao = new List<QUESTAO>();

        //        foreach (var tipo in q)
        //        {
        //            QUESTAO questao = new QUESTAO();
        //            questao.ID_QUESTAO = tipo.ID_QUESTAO;
        //            questao.DESCRICAO = tipo.DESCRICAO;
        //            questao.FUNCIONARIO = new FUNCIONARIO();
        //            questao.FUNCIONARIO.PESSOA = new PESSOA();
        //            questao.FUNCIONARIO.PESSOA.NOME = tipo.FUNCIONARIO.PESSOA.NOME;
        //            questao.DISCIPLINA = new DISCIPLINA();
        //            questao.DISCIPLINA.ID_DISCIPLINA = tipo.DISCIPLINA.ID_DISCIPLINA;
        //            questao.DISCIPLINA.DESCRICAO = tipo.DISCIPLINA.DESCRICAO;
        //            ltQuestao.Add(questao);
        //        }

        //        return ltQuestao;
        //    }
        //}

        
        public void Salvar(QUESTAO q)
        {   
            using (ECEntities db = new ECEntities())
            {
                
                db.QUESTAO.Add(q);
                
                db.SaveChanges();

                //Retorna o id novo gerado na inserção
                var id = q.ID_QUESTAO;
            }
        }

        public void Excluir(int id)
        {
            using (ECEntities db = new ECEntities())
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var resp = db.RESPOSTA.Where(rs => rs.ID_QUESTAO == id);
                    foreach (var i in resp)
                    {
                        db.RESPOSTA.Remove(i);
                    }

                    var dis = db.QUESTAO.FirstOrDefault(rs => rs.ID_QUESTAO == id);
                    db.QUESTAO.Remove(dis);

                    db.SaveChanges();

                    scope.Complete();
                }
            }
        }

        public void Atualiza(QUESTAO q)
        {
            using (ECEntities db = new ECEntities())
            {

                var originalQuestao = db.QUESTAO.First(rs => rs.ID_QUESTAO == q.ID_QUESTAO);

                originalQuestao.ID_DISCIPLINA = q.ID_DISCIPLINA;
                originalQuestao.ID_AMC = q.ID_AMC;
                originalQuestao.ID_FUNCIONARIO = q.ID_FUNCIONARIO;
                originalQuestao.DESCRICAO = q.DESCRICAO;
                originalQuestao.IMAGEM = q.IMAGEM;

                //Respostas dessa questão
                //originalQuestao.RESPOSTA = new EntityCollection<RESPOSTA>();
                //foreach (var r in q.RESPOSTA)
                //{
                //    RESPOSTA obj = db.RESPOSTA.First(rs => rs.ID_RESPOSTA == r.ID_RESPOSTA);
                //    obj.TEXTO = r.TEXTO;
                //    obj.RESPOSTA_CORRETA = r.RESPOSTA_CORRETA;

                //    //RESPOSTA obj = new RESPOSTA();
                //    //obj.ID_RESPOSTA = r.ID_RESPOSTA;
                //    //obj.ID_QUESTAO = q.ID_QUESTAO;
                //    //obj.TEXTO = r.TEXTO;
                //    //obj.RESPOSTA_CORRETA = r.RESPOSTA_CORRETA;

                //    //originalQuestao.RESPOSTA.Attach(obj);
                //}
                
                db.SaveChanges();
            }
        }

    }
}