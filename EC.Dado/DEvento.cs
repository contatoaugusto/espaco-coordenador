using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Modelo;

namespace EC.Dado
{
    public class DEvento
    {
        public List<TIPO_EVENTO> ConsultarTipoEvento()
        {
            using (ECEntities db = new ECEntities())
            {
                var tp = db.TIPO_EVENTO.ToList();
                List<TIPO_EVENTO> ltTipoEvento = new List<TIPO_EVENTO>();
                foreach (var tipo in tp)
                {
                   TIPO_EVENTO tipoEvento = new TIPO_EVENTO();
                     tipoEvento.ID_TIPOEVENTO = tipo.ID_TIPOEVENTO;
                     tipoEvento.DESCRICAO = tipo.DESCRICAO;
                     ltTipoEvento.Add(tipoEvento);
                }

                return ltTipoEvento;
            }
        }

        public List<EVENTO> ConsultarEvento(EVENTO objevento)
        {
            using (ECEntities db = new ECEntities())
            {
                var q = db.EVENTO.Where(rs => rs.ID_PESSOA == objevento.ID_PESSOA && rs.ID_TIPOEVENTO == objevento.ID_TIPOEVENTO);

                List<EVENTO> ltEvento = new List<EVENTO>();

                foreach (var tipo in q)
                {
                    EVENTO evento = new EVENTO();
                    evento.ID_EVENTO = tipo.ID_EVENTO;
                    evento.ID_TIPOEVENTO = tipo.ID_TIPOEVENTO;
                    evento.ID_PESSOA = tipo.ID_PESSOA;
                    evento.INICIO = tipo.INICIO;
                    evento.LOCAL = tipo.LOCAL;
                    evento.NOME = tipo.NOME;
                    
                    ltEvento.Add(evento);
                }

                return ltEvento;
            }
        }


        public void Salvar(EVENTO e)
        {
            try
            {
                using (ECEntities db = new ECEntities())
                {
                    //Salva o evento
                    db.EVENTO.Add(e);
                    db.SaveChanges();
                    db.Dispose();
                }

            }
            catch (Exception )
            {

            }
        }

               public void Atualiza(EVENTO e)
        {
            using (ECEntities db = new ECEntities())
            {

                var evento = db.EVENTO.First(rs => rs.ID_EVENTO == e.ID_EVENTO);
              
                evento.ID_TIPOEVENTO = e.ID_TIPOEVENTO;
                evento.NOME = e.NOME;
                evento.DESCRICAO = e.DESCRICAO;
                evento.LOCAL = e.LOCAL;
                evento.INICIO = e.INICIO;
                evento.CONCLUSAO = e.CONCLUSAO;
                evento.ID_PESSOA = e.ID_PESSOA;
                evento.ID_SEMESTRE = e.ID_SEMESTRE;
              
                db.SaveChanges();
            }
            }
        }
}

  



   
