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
        
    }
}