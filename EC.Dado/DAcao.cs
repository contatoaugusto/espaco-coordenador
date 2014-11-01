using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Modelo;

namespace EC.Dado
{
    public class DAcao
    {
        public List<REUNIAO> ConsultarReuniao()
        {
            using (ECEntities db = new ECEntities())
            {
                var r = db.REUNIAO.ToList();
                List<REUNIAO> ltReuniao = new List<REUNIAO>();
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
            using (ECEntities db = new ECEntities())
            {
                var m = db.AMC.ToList();
                List<AMC> ltAmc = new List<AMC>();
                foreach (var tipo in m)
                {
                    AMC amc = new AMC();
                    amc.ID_AMC = tipo.ID_AMC;
                    //amc.ANO = tipo.ANO;
                    //amc.SEMESTRE = tipo.SEMESTRE;
                    amc.DATA_APLICACAO = tipo.DATA_APLICACAO;
                    amc.ID_SEMESTRE = tipo.ID_SEMESTRE;
                    ltAmc.Add(amc);
                }

                return ltAmc;
            }
        }

        public List<EVENTO> ConsultarEvento()
        {
            using (ECEntities db = new ECEntities())
            {
                var e = db.EVENTO.ToList();
                List<EVENTO> ltEvento = new List<EVENTO>();
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
            using (ECEntities db = new ECEntities())
            {
                var s = db.STATUS_ACAO.ToList();
                List<STATUS_ACAO> ltStatus = new List<STATUS_ACAO>();
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
            using (ECEntities db = new ECEntities())
            {
                var i = db.PRIORIDADE_ACAO.ToList();
                List<PRIORIDADE_ACAO> ltPrioridade = new List<PRIORIDADE_ACAO>();
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
            using (ECEntities db = new ECEntities())
            {
                var p = db.PESSOA.ToList();
                List<PESSOA> ltPessoa = new List<PESSOA>();

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
            using (ECEntities db = new ECEntities())
            {
                var q = db.ACAO.Where(rs => rs.ID_PESSOA == objacao.ID_PESSOA && rs.ID_PRIORIDADE == objacao.ID_PRIORIDADE && rs.ID_STATUS == objacao.ID_STATUS);

                List<ACAO> ltAcao = new List<ACAO>();

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

            using (ECEntities db = new ECEntities())
            {
                db.ACAO.AddObject(a);
                db.SaveChanges();
            }
        }
        public void Alterar(ACAO a)
        {

            using (ECEntities db = new ECEntities())
            {
                db.ACAO.AddObject(a);
                db.SaveChanges();
            }
        }
    }

}

