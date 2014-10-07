using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Dado;
using EC.Modelo;


namespace EC.Dado
{
    public class DEvento
    {
        public List<TIPO_EVENTO> ConsultarTipoEvento()
        {
            using (ALICE2Entities db = new ALICE2Entities())
            {
                var tp = db.TIPO_EVENTO.ToList();
                List<Modelo.TIPO_EVENTO> ltTipoEvento = new List<TIPO_EVENTO>();
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
                    ltPessoa.Add(pessoa);
                }

                return ltPessoa;
            }

        }

        public List<EVENTO> ConsultarEvento(EVENTO objevento)
        {
            using (ALICE2Entities db = new ALICE2Entities())
            {
                var q = db.EVENTO.Where(rs => rs.ID_PESSOA == objevento.ID_PESSOA && rs.ID_TIPOEVENTO == objevento.ID_TIPOEVENTO);

                List<EC.Modelo.EVENTO> ltEvento = new List<EC.Modelo.EVENTO>();

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

           public void Salvar(EVENTO a)
        {

            using (ALICE2Entities db = new ALICE2Entities())
            {
                db.EVENTO.Add(a);
                db.SaveChanges();
    }
            }
        }
}

  



   
