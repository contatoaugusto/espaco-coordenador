using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Modelo;

namespace EC.Dado
{
    public class DReuniaoPauta
    {
        public List<REUNIAO_PAUTA> Consultar()
        {
            using (ECEntities db = new ECEntities())
            {
                var rauniaoPauta = db.REUNIAO_PAUTA.ToList();
                
                List<REUNIAO_PAUTA> ltReuniaoPauta = new List<REUNIAO_PAUTA>();

                foreach (var tipo in rauniaoPauta)
                {
                    REUNIAO_PAUTA reuniaoPauta = new REUNIAO_PAUTA();
                    reuniaoPauta.ID_PAUTA = tipo.ID_PAUTA;
                    reuniaoPauta.ID_REUNIAO = tipo.ID_REUNIAO;
                    reuniaoPauta.DESCRICAO = tipo.DESCRICAO;
                    reuniaoPauta.ITEM = tipo.ITEM;

                    ltReuniaoPauta.Add(reuniaoPauta);
                }

                return ltReuniaoPauta;
            }
        }

       
        public REUNIAO_PAUTA ConsultarById(int idPauta)
        {
            using (ECEntities db = new ECEntities())
            {
                return db.REUNIAO_PAUTA.First(rs => rs.ID_PAUTA == idPauta);
            }
        }

        public List<REUNIAO_PAUTA> ConsultarByReuniao(int idReuniao)
        {
            using (ECEntities db = new ECEntities())
            {
                var rauniaoPauta = db.REUNIAO_PAUTA.Where(rs => rs.ID_REUNIAO == idReuniao);

                List<REUNIAO_PAUTA> ltReuniaoPauta = new List<REUNIAO_PAUTA>();

                foreach (var tipo in rauniaoPauta)
                {
                    REUNIAO_PAUTA reuniaoPauta = new REUNIAO_PAUTA();
                    reuniaoPauta.ID_PAUTA = tipo.ID_PAUTA;
                    reuniaoPauta.ID_REUNIAO = tipo.ID_REUNIAO;
                    reuniaoPauta.DESCRICAO = tipo.DESCRICAO;
                    reuniaoPauta.ITEM = tipo.ITEM;

                    ltReuniaoPauta.Add(reuniaoPauta);
                }

                return ltReuniaoPauta;
            }
        }
        
        public void Salvar(REUNIAO_PAUTA r)
        {
            try
            {
                using (ECEntities db = new ECEntities())
                {
                    //Salva a questão
                    db.REUNIAO_PAUTA.Add(r);
                    db.SaveChanges();
                    db.Dispose();
                }

            }
            catch (Exception e)
            {

            }
        }

        public void Salvar(List<REUNIAO_PAUTA> objetos)
        {
            try
            {
                using (ECEntities db = new ECEntities())
                {
                    //Pautas dessa questão
                    foreach (var pauta in objetos)
                    {
                        REUNIAO_PAUTA obj = new REUNIAO_PAUTA();

                        if (pauta.ID_PAUTA > 0)
                            obj = db.REUNIAO_PAUTA.First(rs => rs.ID_PAUTA == pauta.ID_PAUTA);

                        obj.ID_REUNIAO = pauta.ID_REUNIAO;
                        obj.DESCRICAO = pauta.DESCRICAO;
                        obj.ITEM = pauta.ITEM;
                        Salvar(obj);
                    }
                }

            }
            catch (Exception e)
            {

            }
        }

        public void Salvar(List<REUNIAO_PAUTA> objetos, int idReuniao)
        {
            try
            {
                using (ECEntities db = new ECEntities())
                {
                    //Pautas dessa questão
                    foreach (var pauta in objetos)
                    {
                        REUNIAO_PAUTA obj = new REUNIAO_PAUTA();

                        if (pauta.ID_PAUTA > 0)
                            obj = db.REUNIAO_PAUTA.First(rs => rs.ID_PAUTA == pauta.ID_PAUTA);

                        obj.ID_REUNIAO = idReuniao;
                        obj.DESCRICAO = pauta.DESCRICAO;
                        obj.ITEM = pauta.ITEM;
                        Salvar(obj);
                    }
                }

            }
            catch (Exception e)
            {

            }
        }


        public void ExcluiPauta(int idPauta)
        {
            using (ECEntities db = new ECEntities())
            {
                var originalPauta = db.REUNIAO_PAUTA.First(rs => rs.ID_PAUTA == idPauta);
                if (originalPauta != null)
                {

                    var idReuniao = originalPauta.ID_REUNIAO;
                    db.REUNIAO_PAUTA.Remove(originalPauta);

                    // Reordena número dos titems
                    var pautas = db.REUNIAO_PAUTA.Where(rs => rs.ID_REUNIAO == idReuniao).ToList();
                    int contador = 0;
                    foreach (var pauta in pautas)
                    {
                        pauta.ITEM = contador + 1;
                        contador++;
                    }

                    db.SaveChanges();
                }
            }
        }
    }
}