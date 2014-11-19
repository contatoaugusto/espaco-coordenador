using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EC.Modelo;

namespace EC.Dado
{
    public class DSemestre
    {

        public List<SEMESTRE> Consultar()
        {
            using (ECEntities db = new ECEntities())
            {
                var list = db.SEMESTRE.ToList();
                /*
                List<SEMESTRE> list = new List<SEMESTRE>();
                
                foreach (var tipo in c)
                {
                    SEMESTRE obj = new SEMESTRE();
                    obj.ID_SEMESTRE = tipo.ID_SEMESTRE;
                    obj.ANO = tipo.ANO;
                    obj.SEMESTRE1 = tipo.SEMESTRE1;
                    obj.ATIVO = tipo.ATIVO;

                    list.Add(obj);
                }
                */
                return list;
            }
        }

        public SEMESTRE ConsultarAtivo()
        {
            using (ECEntities db = new ECEntities())
            {
                try
                {
                    return db.SEMESTRE.First(rs => rs.ATIVO == true);

                }
                catch (Exception e)
                {
                    return null;
                }

            }
        }

        public SEMESTRE ConsultarById(int id)
        {
            using (ECEntities db = new ECEntities())
            {
                return db.SEMESTRE.First(rs => rs.ID_SEMESTRE == id);
            }
        }

        public void Salvar(SEMESTRE se)
        {
            using (var db = new ECEntities())
            {

                AtualizarStatus(false);
                
                db.SEMESTRE.Add(se);
                db.SaveChanges();
                db.Dispose();
            }
        }

        public void Atualizar(SEMESTRE q)
        {
            using (ECEntities db = new ECEntities())
            {

                AtualizarStatus(false);

                var original = db.SEMESTRE.First(rs => rs.ID_SEMESTRE == q.ID_SEMESTRE);

                original.ID_SEMESTRE = q.ID_SEMESTRE;
                original.ANO = q.ANO;
                original.SEMESTRE1 = q.SEMESTRE1;
                original.ATIVO = q.ATIVO;

                db.SaveChanges();
            }
        }


        public void AtualizarStatus(bool status)
        {
            using (var db = new ECEntities())
            {

                var semestre = db.SEMESTRE.ToList();
                semestre.ForEach(a => a.ATIVO = status);

                db.SaveChanges();
            }
        }
    }
}
