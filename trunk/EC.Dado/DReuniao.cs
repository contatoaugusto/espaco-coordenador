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
                    reuniao.ID_SEMESTRE = tipo.ID_SEMESTRE;

                    reuniao.SEMESTRE = db.SEMESTRE.First(rs => rs.ID_SEMESTRE == tipo.ID_SEMESTRE);
                    reuniao.TIPO_REUNIAO = db.TIPO_REUNIAO.First(rs => rs.ID_TIPOREUNIAO == tipo.ID_TIPOREUNIAO);

                    ltReuniao.Add(reuniao);
                }

                return ltReuniao;
            }
        }

        public List<REUNIAO_PARTICIPANTE> ConsultarParticipante(int idReuniao)
        {
            using (ECEntities db = new ECEntities())
            {
                var participantes = db.REUNIAO_PARTICIPANTE.Where(rs => rs.ID_REUNIAO == idReuniao);

                List<REUNIAO_PARTICIPANTE> ltParticipante = new List<REUNIAO_PARTICIPANTE>();

                foreach (var tipo in participantes)
                {
                    REUNIAO_PARTICIPANTE participante = new REUNIAO_PARTICIPANTE();
                    participante.ID_PARTICIPANTE = tipo.ID_PARTICIPANTE;
                    participante.ID_REUNIAO = tipo.ID_REUNIAO;
                    participante.ID_PESSOA = tipo.ID_PESSOA;
                    participante.PRESENCA = tipo.PRESENCA;

                    participante.PESSOA = db.PESSOA.First(rs => rs.ID_PESSOA == tipo.ID_PESSOA);

                    ltParticipante.Add(participante);
                }

                return ltParticipante;
            }
        }

        
        public REUNIAO ConsultarById(int idReuniao)
        {
            using (ECEntities db = new ECEntities())
            {
                return db.REUNIAO.First(rs => rs.ID_REUNIAO == idReuniao);

                //List<REUNIAO> ltReuniao = new List<REUNIAO>();

                //foreach (var tipo in r)
                //{
                //    REUNIAO reuniao = new REUNIAO();
                //    reuniao.ID_REUNIAO = tipo.ID_REUNIAO;
                //    reuniao.ID_TIPOREUNIAO = tipo.ID_TIPOREUNIAO;
                //    reuniao.LOCAL = tipo.LOCAL;
                //    reuniao.DATAHORA = tipo.DATAHORA;
                //    reuniao.TITULO = tipo.TITULO;
                //    reuniao.ID_SEMESTRE = tipo.ID_SEMESTRE;

                //    reuniao.SEMESTRE = db.SEMESTRE.First(rs => rs.ID_SEMESTRE == tipo.ID_SEMESTRE);
                //    reuniao.TIPO_REUNIAO = db.TIPO_REUNIAO.First(rs => rs.ID_TIPOREUNIAO == tipo.ID_TIPOREUNIAO);

                //    ltReuniao.Add(reuniao);
                //}

                //return ltReuniao;
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

        public void Atualiza(REUNIAO q)
        {
            using (ECEntities db = new ECEntities())
            {

                var originalReniao = db.REUNIAO.First(rs => rs.ID_REUNIAO == q.ID_REUNIAO);

                originalReniao.ID_TIPOREUNIAO   = q.ID_TIPOREUNIAO;
                originalReniao.LOCAL            = q.LOCAL;
                originalReniao.DATAHORA         = q.DATAHORA;
                originalReniao.TITULO           = q.TITULO;
                originalReniao.ID_SEMESTRE      = q.ID_SEMESTRE;

                //Pautas dessa questão
                foreach (var pauta in q.REUNIAO_PAUTA)
                {
                    REUNIAO_PAUTA obj = new REUNIAO_PAUTA(); 
                    
                    if (pauta.ID_PAUTA > 0)
                        obj = db.REUNIAO_PAUTA.First(rs => rs.ID_PAUTA == pauta.ID_PAUTA);

                    obj.ID_REUNIAO = originalReniao.ID_REUNIAO;
                    obj.DESCRICAO = pauta.DESCRICAO;
                    obj.ITEM = pauta.ITEM;
                    originalReniao.REUNIAO_PAUTA.Add(obj);
                }

                //Participantes
                foreach (var participante in q.REUNIAO_PARTICIPANTE)
                {
                    REUNIAO_PARTICIPANTE obj = new REUNIAO_PARTICIPANTE();

                    if (participante.ID_PARTICIPANTE > 0)
                        obj = db.REUNIAO_PARTICIPANTE.First(rs => rs.ID_PARTICIPANTE == participante.ID_PARTICIPANTE);

                    obj.ID_REUNIAO = originalReniao.ID_REUNIAO;
                    obj.PRESENCA  = participante.PRESENCA;
                    obj.ID_PESSOA = participante.ID_PESSOA;
                    originalReniao.REUNIAO_PARTICIPANTE.Add(obj);
                }

                db.SaveChanges();
            }
        }
    }
}