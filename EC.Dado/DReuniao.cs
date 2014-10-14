using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Modelo;

namespace EC.Dado
{
    public class DReuniao
    {
        public List<TIPO_REUNIAO> ConsultarTipoReuniao()
        {
            using (ECEntities db = new ECEntities())
            {
                var tp = db.TIPO_REUNIAO.ToList();
                List<TIPO_REUNIAO> ltTipoReuniao = new List<TIPO_REUNIAO>();
                foreach (var tipo in tp)
                {
                    TIPO_REUNIAO tipoReuniao = new TIPO_REUNIAO();
                    tipoReuniao.ID_TIPOREUNIAO = tipo.ID_TIPOREUNIAO;
                    tipoReuniao.DESCRICAO = tipo.DESCRICAO;

                    ltTipoReuniao.Add(tipoReuniao);
                }

                return ltTipoReuniao;
            }
        }

        public List<TIPO_ASSUNTO_TRATADO> ConsultarTipoAssunto()
        {
            using (ECEntities db = new ECEntities())
            {
                var tp = db.TIPO_ASSUNTO_TRATADO.ToList();
                List<TIPO_ASSUNTO_TRATADO> ltTipoAssunto = new List<TIPO_ASSUNTO_TRATADO>();
                foreach (var tipo in tp)
                {
                    TIPO_ASSUNTO_TRATADO tipoAssunto = new TIPO_ASSUNTO_TRATADO();
                    tipoAssunto.ID_TIPOASSTRATADO = tipo.ID_TIPOASSTRATADO;
                    tipoAssunto.DESCRICAO = tipo.DESCRICAO;

                    ltTipoAssunto.Add(tipoAssunto);
                }

                return ltTipoAssunto;
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
                    ltPessoa.Add(pessoa);
                }

                return ltPessoa;
            }

        }

        public List<REUNIAO> ConsultarReuniao(REUNIAO objreuniao)
        {
            using (ECEntities db = new ECEntities())
            {
                var r = db.REUNIAO.Where(rs => rs.ID_TIPOREUNIAO == objreuniao.ID_TIPOREUNIAO);

                List<REUNIAO> ltReuniao = new List<REUNIAO>();

                foreach (var tipo in r)
                {
                    REUNIAO reuniao = new REUNIAO();
                    reuniao.ID_REUNIAO = tipo.ID_REUNIAO;
                    reuniao.ID_TIPOREUNIAO = tipo.ID_TIPOREUNIAO;
                    reuniao.LOCAL = tipo.LOCAL;
                    reuniao.DATAHORA = tipo.DATAHORA;
                    reuniao.TITULO = tipo.TITULO;
                    ltReuniao.Add(reuniao);
                }

                return ltReuniao;
            }
        }

        public void Salvar(REUNIAO r)
        {
            try
            {
                using (ECEntities db = new ECEntities())
                {
                    //Salva a questão
                    db.REUNIAO.AddObject(r);
                    db.SaveChanges();
                    db.Dispose();
                }

            }
            catch (Exception e)
            {

            }

        }

    }
}