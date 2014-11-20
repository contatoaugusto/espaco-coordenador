using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Modelo;

namespace EC.Dado
{
    public class DReuniaoParticipante
    {
        public List<REUNIAO_PARTICIPANTE> Consultar()
        {
            using (ECEntities db = new ECEntities())
            {
                return db.REUNIAO_PARTICIPANTE.ToList();
            }
        }

       
        public REUNIAO_PARTICIPANTE ConsultarById(int idParicipante)
        {
            using (ECEntities db = new ECEntities())
            {
                return db.REUNIAO_PARTICIPANTE.First(rs => rs.ID_PARTICIPANTE == idParicipante);
            }
        }

        public List<REUNIAO_PARTICIPANTE> ConsultarByReuniao(int idReuniao)
        {
            using (ECEntities db = new ECEntities())
            {
                var participantes = db.REUNIAO_PARTICIPANTE.Where(rs => rs.ID_REUNIAO == idReuniao);

                List<REUNIAO_PARTICIPANTE> ltParticipante = new List<REUNIAO_PARTICIPANTE>();

                foreach (var tipo in participantes)
                {
                    if (tipo.ID_PARTICIPANTE != null && tipo.ID_PARTICIPANTE != 0)
                    {
                        REUNIAO_PARTICIPANTE participante = new REUNIAO_PARTICIPANTE();
                        participante.ID_PARTICIPANTE = tipo.ID_PARTICIPANTE;
                        participante.ID_REUNIAO = tipo.ID_REUNIAO;
                        participante.ID_PESSOA = tipo.ID_PESSOA;
                        participante.PRESENCA = tipo.PRESENCA;

                        participante.PESSOA = db.PESSOA.First(rs => rs.ID_PESSOA == tipo.ID_PESSOA);

                        ltParticipante.Add(participante);
                    }
                }

                return ltParticipante;
            }
        }

       
        public void ExcluiParticipante(int idParticipante)
        {
            using (ECEntities db = new ECEntities())
            {
                var originalParticipante = db.REUNIAO_PARTICIPANTE.First(rs => rs.ID_PARTICIPANTE == idParticipante);
                if (originalParticipante != null)
                {
                    var idReuniao = originalParticipante.ID_REUNIAO;
                    db.REUNIAO_PARTICIPANTE.Remove(originalParticipante);

                    db.SaveChanges();
                }
            }
        }

        public void Salvar(REUNIAO_PARTICIPANTE r)
        {
            try
            {
                using (ECEntities db = new ECEntities())
                {
                    //Salva a questão
                    db.REUNIAO_PARTICIPANTE.Add(r);
                    db.SaveChanges();
                    db.Dispose();
                }

            }
            catch (Exception e)
            {

            }
        }

        public void Salvar(List<REUNIAO_PARTICIPANTE> objetos)
        {
            try
            {
                using (ECEntities db = new ECEntities())
                {
                    foreach (var participante in objetos)
                    {
                        REUNIAO_PARTICIPANTE obj = new REUNIAO_PARTICIPANTE();

                        if (participante.ID_PARTICIPANTE > 0)
                            obj = db.REUNIAO_PARTICIPANTE.First(rs => rs.ID_PARTICIPANTE == participante.ID_PARTICIPANTE);

                        obj.ID_REUNIAO = participante.ID_REUNIAO;
                        obj.PRESENCA = participante.PRESENCA;
                        obj.ID_PESSOA = participante.PESSOA.ID_PESSOA;
                        
                        Salvar(obj);
                    }
                }

            }
            catch (Exception e)
            {

            }
        }

        public void Salvar(List<REUNIAO_PARTICIPANTE> objetos, int idReuniao)
        {
            try
            {
                using (ECEntities db = new ECEntities())
                {
                    foreach (var participante in objetos)
                    {
                        REUNIAO_PARTICIPANTE obj = new REUNIAO_PARTICIPANTE();

                        if (participante.ID_PARTICIPANTE > 0)
                            obj = db.REUNIAO_PARTICIPANTE.First(rs => rs.ID_PARTICIPANTE == participante.ID_PARTICIPANTE);
                        else
                            obj.ID_REUNIAO = idReuniao;
                        
                        obj.PRESENCA = participante.PRESENCA;
                        obj.ID_PESSOA = participante.PESSOA.ID_PESSOA;

                        if (participante.ID_PARTICIPANTE > 0)
                            db.SaveChanges();
                        else
                        {
                            db.REUNIAO_PARTICIPANTE.Add(obj);
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