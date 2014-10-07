using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Dado;
using EC.Modelo;


    public class DAcao
        {
        public List<REUNIAO> ConsultarReuniao()
        {
            using (ALICE2Entities db = new ALICE2Entities())
            {
                var r = db.REUNIAO.ToList();
                List<EC.Modelo.REUNIAO> ltReuniao = new List<EC.Modelo.REUNIAO>();
                foreach (var tipo in r)
                {
                    REUNIAO reuniao = new REUNIAO();
                    reuniao.ID_REUNIAO = tipo.ID_REUNIAO;
                    reuniao.TITULO = tipo.TITULO;
                    ltReuniao.Add(reuniao);
                }

                return ltReuniao;
            }
        }

        public List<AMC> ConsultarAmc()
        {
            using (ALICE2Entities db = new ALICE2Entities())
            {
                var m = db.AMC.ToList();
                List<EC.Modelo.AMC> ltAmc = new List<EC.Modelo.AMC>();
                foreach (var tipo in m)
                {
                    AMC amc = new AMC();
                    amc.ID_AMC = tipo.ID_AMC;
                    amc.ANO = tipo.ANO;
                    ltAmc.Add(amc);
                }

                return ltAmc;
            }
        }

        public List<EVENTO> ConsultarEvento()
        {
            using (ALICE2Entities db = new ALICE2Entities())
            {
                var e = db.EVENTO.ToList();
                List<EC.Modelo.EVENTO> ltEvento = new List<EC.Modelo.EVENTO>();
                foreach (var tipo in e)
                {
                    EVENTO evento = new EVENTO();
                    evento.ID_PESSOA = tipo.ID_PESSOA;
                    evento.ID_TIPOEVENTO = tipo.ID_TIPOEVENTO;
                    evento.INICIO = tipo.INICIO;
                    evento.NOME = tipo.NOME;
                    ltEvento.Add(evento);
                }

                return ltEvento;
            }
        }
        public List<STATUS_ACAO> ConsultarStatus()
        {
            using (ALICE2Entities db = new ALICE2Entities())
            {
                var s = db.STATUS_ACAO.ToList();
                List<EC.Modelo.STATUS_ACAO> ltStatus = new List<EC.Modelo.STATUS_ACAO>();
                foreach (var tipo in s)
                {
                 STATUS_ACAO status = new STATUS_ACAO();
                  status.ID_STATUS = tipo.ID_STATUS;
                  status.DESCRICAO = tipo.DESCRICAO;
                  ltStatus.Add(status);
                }

                return ltStatus;
            }
        }
    
        public List<PRIORIDADE_ACAO> ConsultarPrioridade()
        {
            using (ALICE2Entities db = new ALICE2Entities())
            {
                var i = db.PRIORIDADE_ACAO.ToList();
                List<EC.Modelo.PRIORIDADE_ACAO> ltPrioridade = new List<EC.Modelo.PRIORIDADE_ACAO>();
                foreach (var tipo in i)
                {
                  PRIORIDADE_ACAO prioridade = new PRIORIDADE_ACAO();
                  prioridade.ID_PRIORIDADE = tipo.ID_PRIORIDADE;
                  prioridade.DESCRICAO = tipo.DESCRICAO;
                  ltPrioridade.Add(prioridade);
                }

                return ltPrioridade;
            }
        }
             public List<PESSOA> ConsultarPessoa()
             {
                 using (ALICE2Entities db = new ALICE2Entities())
                 {
                     var p = db.PESSOA.ToList();
                     List<EC.Modelo.PESSOA> ltPessoa = new List<EC.Modelo.PESSOA>();

                     foreach (var tipo in p)
                     {
                        PESSOA pessoa = new PESSOA();
                        pessoa.ID_PESSOA = tipo.ID_PESSOA;
                        pessoa.NOME = tipo.NOME;
                        pessoa.EMAIL = tipo.EMAIL;
                        ltPessoa.Add(pessoa);
                     }

                     return ltPessoa;
                 }

             }

             public List<ACAO> ConsultarAcao(ACAO objacao)
             {
                 using (ALICE2Entities db = new ALICE2Entities())
                 {
                     var q = db.ACAO.Where(rs => rs.ID_PESSOA == objacao.ID_PESSOA && rs.ID_PRIORIDADE == objacao.ID_PRIORIDADE && rs.ID_STATUS == objacao.ID_STATUS);

                     List<EC.Modelo.ACAO> ltAcao = new List<EC.Modelo.ACAO>();

                     foreach (var tipo in q)
                     {
                       ACAO acao = new ACAO();
                       acao.ID_ACAO = tipo.ID_ACAO;
                       acao.ID_PESSOA = tipo.ID_PESSOA;
                       acao.ID_PRIORIDADE = tipo.ID_PRIORIDADE;
                       acao.ID_STATUS = tipo.ID_STATUS;
                       acao.INICIO = tipo.INICIO;
                       acao.CONCLUSAO = tipo.CONCLUSAO;
                       acao.TITULO = tipo.TITULO;
                       ltAcao.Add(acao);
                     }

                     return ltAcao;
                 }
             }

        public void Salvar(ACAO a)
        {

            using (ALICE2Entities db = new ALICE2Entities())
            {
                db.ACAO.Add(a);
                db.SaveChanges();
            }
        }
        public void Alterar(ACAO a)
        {

            using (ALICE2Entities db = new ALICE2Entities())
            {
                db.ACAO.Add(a);
                db.SaveChanges();
            }
        }  
      

    }

