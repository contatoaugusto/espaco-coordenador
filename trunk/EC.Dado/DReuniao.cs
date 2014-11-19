using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Modelo;
using System.Transactions;

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

 
        
        public REUNIAO ConsultarById(int idReuniao)
        {
            using (ECEntities db = new ECEntities())
            {
                var reuniao = db.REUNIAO.FirstOrDefault(rs => rs.ID_REUNIAO == idReuniao);

                reuniao.SEMESTRE = db.SEMESTRE.First(rs => rs.ID_SEMESTRE == reuniao.ID_SEMESTRE);
                reuniao.TIPO_REUNIAO = db.TIPO_REUNIAO.First(rs => rs.ID_TIPOREUNIAO == reuniao.ID_TIPOREUNIAO);
                reuniao.REUNIAO_PAUTA = db.REUNIAO_PAUTA.Where(rs => rs.ID_REUNIAO == reuniao.ID_REUNIAO).ToList();

                reuniao.REUNIAO_PARTICIPANTE = db.REUNIAO_PARTICIPANTE.Where(rs => rs.ID_REUNIAO == reuniao.ID_REUNIAO).ToList();
                
                var participantes = db.REUNIAO_PARTICIPANTE.Where(rs => rs.ID_REUNIAO == reuniao.ID_REUNIAO).ToList();
                foreach (var participante in participantes)
                {
                    participante.PESSOA = db.PESSOA.First(rs => rs.ID_PESSOA == participante.ID_PESSOA);
                    reuniao.REUNIAO_PARTICIPANTE.Add(participante);
                }


                reuniao.REUNIAO_ASSUNTO_TRATADO = db.REUNIAO_ASSUNTO_TRATADO.Where(rs => rs.ID_REUNIAO == reuniao.ID_REUNIAO).ToList();

                var compromissos = db.REUNIAO_COMPROMISSO.Where(rs => rs.ID_REUNIAO == reuniao.ID_REUNIAO).ToList();
                foreach (var compromisso in compromissos)
                {
                    compromisso.PESSOA = db.PESSOA.First(rs => rs.ID_PESSOA == compromisso.ID_PESSOA);
                    reuniao.REUNIAO_COMPROMISSO.Add(compromisso);
                }

                return reuniao;
            }
        }


        public void Salvar(REUNIAO r)
        {
            try
            {
                using (ECEntities db = new ECEntities())
                {

                    // Verifica se existe reunião anterior do mesmo tipo para configurar sequencial
                    var sequenciaReuniaoAnteriorMesmoTipo =  
                        db.REUNIAO.Where(rs => rs.ID_SEMESTRE == r.ID_SEMESTRE && rs.ID_TIPOREUNIAO == r.ID_TIPOREUNIAO).Max(o => o.SEQUENCIA) ;

                    if (sequenciaReuniaoAnteriorMesmoTipo == null || sequenciaReuniaoAnteriorMesmoTipo == 0)
                        sequenciaReuniaoAnteriorMesmoTipo = 1;
                    else
                        sequenciaReuniaoAnteriorMesmoTipo += 1;

                    r.SEQUENCIA = sequenciaReuniaoAnteriorMesmoTipo;

                    //Salva a questão
                    db.REUNIAO.Add(r);
                    db.SaveChanges();
                    db.Dispose();
                }

            }
            catch (Exception e)
            {

            }
        }

        public void Excluir(REUNIAO q)
        {
            using (ECEntities db = new ECEntities())
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var originalPauta = db.REUNIAO_PAUTA.Where(rs => rs.ID_REUNIAO == q.ID_REUNIAO);
                    foreach (var i in originalPauta)
                    {
                        db.REUNIAO_PAUTA.Remove(i);
                    }

                    var originalParticipante = db.REUNIAO_PARTICIPANTE.Where(rs => rs.ID_REUNIAO == q.ID_REUNIAO);
                    foreach (var p in originalParticipante)
                    {
                        db.REUNIAO_PARTICIPANTE.Remove(p);
                    }

                    var originalReniao = db.REUNIAO.First(rs => rs.ID_REUNIAO == q.ID_REUNIAO);
                    db.REUNIAO.Remove(originalReniao);
                    db.SaveChanges();

                    scope.Complete();
                }
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
                    obj.PRESENCA = participante.PRESENCA;
                    obj.ID_PESSOA = participante.PESSOA.ID_PESSOA;
                    originalReniao.REUNIAO_PARTICIPANTE.Add(obj);
                }

                //Assuntos
                //foreach (var assunto in q.REUNIAO_ASSUNTO_TRATADO)
                //{
                //    REUNIAO_ASSUNTO_TRATADO obj = new REUNIAO_ASSUNTO_TRATADO();

                //    if (assunto.ID_ASSTRAT > 0)
                //        obj = db.REUNIAO_ASSUNTO_TRATADO.First(rs => rs.ID_ASSTRAT == assunto.ID_ASSTRAT);

                //    obj.ID_REUNIAO = originalReniao.ID_REUNIAO;
                //    obj.DESCRICAO = assunto.DESCRICAO;
                //    obj.ITEM = assunto.ITEM;
                //    obj.ID_TIPOASSTRATADO = assunto.ID_TIPOASSTRATADO;
                //    originalReniao.REUNIAO_ASSUNTO_TRATADO.Add(obj);
                //}

                //Compromissos
                //foreach (var compromisso in q.REUNIAO_COMPROMISSO)
                //{
                //    REUNIAO_COMPROMISSO obj = new REUNIAO_COMPROMISSO();

                //    if (compromisso.ID_COMPROMISSO > 0)
                //        obj = db.REUNIAO_COMPROMISSO.First(rs => rs.ID_COMPROMISSO == compromisso.ID_COMPROMISSO);

                //    obj.ID_REUNIAO = originalReniao.ID_REUNIAO;
                //    obj.ID_PESSOA = compromisso.ID_PESSOA;
                //    obj.DESCRICAO = compromisso.DESCRICAO;
                //    obj.ITEM = compromisso.ITEM;
                //    obj.DATA = compromisso.DATA;
                //    originalReniao.REUNIAO_COMPROMISSO.Add(obj);
                //}

                db.SaveChanges();
            }
        }
       
    }
}