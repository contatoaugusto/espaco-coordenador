using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Modelo;

namespace EC.Dado
{
    public class DReuniaoAssuntoTratado
    {
        public List<REUNIAO_ASSUNTO_TRATADO> Consultar()
        {
            using (ECEntities db = new ECEntities())
            {
                return db.REUNIAO_ASSUNTO_TRATADO.ToList();
                
                //List<REUNIAO_PAUTA> ltReuniaoPauta = new List<REUNIAO_PAUTA>();

                //foreach (var tipo in rauniaoPauta)
                //{
                //    REUNIAO_PAUTA reuniaoPauta = new REUNIAO_PAUTA();
                //    reuniaoPauta.ID_PAUTA = tipo.ID_PAUTA;
                //    reuniaoPauta.ID_REUNIAO = tipo.ID_REUNIAO;
                //    reuniaoPauta.DESCRICAO = tipo.DESCRICAO;
                //    reuniaoPauta.ITEM = tipo.ITEM;

                //    ltReuniaoPauta.Add(reuniaoPauta);
                //}

                //return ltReuniaoPauta;
            }
        }

       
        public REUNIAO_ASSUNTO_TRATADO ConsultarById(int idAssuntoTratado)
        {
            using (ECEntities db = new ECEntities())
            {
                return db.REUNIAO_ASSUNTO_TRATADO.First(rs => rs.ID_ASSTRAT == idAssuntoTratado);
            }
        }

        public List<REUNIAO_ASSUNTO_TRATADO> ConsultarByReuniao(int idReuniao)
        {
            using (ECEntities db = new ECEntities())
            {
                var rauniaoAssuntos = db.REUNIAO_ASSUNTO_TRATADO.Where(rs => rs.ID_REUNIAO == idReuniao);

                List<REUNIAO_ASSUNTO_TRATADO> ltReuniaoAssunto = new List<REUNIAO_ASSUNTO_TRATADO>();

                foreach (var tipo in rauniaoAssuntos)
                {
                    REUNIAO_ASSUNTO_TRATADO reuniaoAssunto = new REUNIAO_ASSUNTO_TRATADO();
                    reuniaoAssunto.ID_ASSTRAT = tipo.ID_ASSTRAT;
                    reuniaoAssunto.ID_REUNIAO = tipo.ID_REUNIAO;
                    reuniaoAssunto.DESCRICAO = tipo.DESCRICAO;
                    reuniaoAssunto.ITEM = tipo.ITEM;
                    reuniaoAssunto.TIPO_ASSUNTO_TRATADO = tipo.TIPO_ASSUNTO_TRATADO;

                    ltReuniaoAssunto.Add(reuniaoAssunto);
                }

                return ltReuniaoAssunto;
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

        public TIPO_ASSUNTO_TRATADO ConsultarTipoAssuntoById(int idTipoAssuntoTratado)
        {
            using (ECEntities db = new ECEntities())
            {
                return db.TIPO_ASSUNTO_TRATADO.First(rs => rs.ID_TIPOASSTRATADO == idTipoAssuntoTratado);
            }
        }


        public void ExcluiAssuntoTratado(int idAssuntoTratado)
        {
            using (ECEntities db = new ECEntities())
            {
                var originalAssuntoTratado = db.REUNIAO_ASSUNTO_TRATADO.First(rs => rs.ID_ASSTRAT == idAssuntoTratado);
                if (originalAssuntoTratado != null)
                {
                    db.REUNIAO_ASSUNTO_TRATADO.Remove(originalAssuntoTratado);
                    db.SaveChanges();
                }
            }
        }
        

        public void Salvar(REUNIAO_ASSUNTO_TRATADO r)
        {
            try
            {
                using (ECEntities db = new ECEntities())
                {
                    //Salva a questão
                    db.REUNIAO_ASSUNTO_TRATADO.Add(r);
                    db.SaveChanges();
                    db.Dispose();
                }

            }
            catch (Exception e)
            {

            }
        }

        public void Salvar(List<REUNIAO_ASSUNTO_TRATADO> objetos)
        {
            try
            {
                using (ECEntities db = new ECEntities())
                {
                    foreach (var assunto in objetos)
                    {
                        REUNIAO_ASSUNTO_TRATADO obj = new REUNIAO_ASSUNTO_TRATADO();

                        if (assunto.ID_ASSTRAT > 0)
                            obj = db.REUNIAO_ASSUNTO_TRATADO.First(rs => rs.ID_ASSTRAT == assunto.ID_ASSTRAT);
                        else
                            obj.ID_REUNIAO = assunto.ID_REUNIAO;
                        
                        obj.DESCRICAO = assunto.DESCRICAO;
                        obj.ITEM = assunto.ITEM;
                        obj.ID_TIPOASSTRATADO = assunto.ID_TIPOASSTRATADO;

                        if (assunto.ID_ASSTRAT > 0)
                            db.SaveChanges();
                        else
                        {
                            db.REUNIAO_ASSUNTO_TRATADO.Add(obj);
                            db.SaveChanges();
                            db.Dispose();
                        }
                    }
                }

            }
            catch (Exception e)
            {

            }
        }
    }
}