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
                var c = db.SEMESTRE.ToList();
                
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

                return list;
            }
        }

        public SEMESTRE ConsultarAtivo()
        {
            using (ECEntities db = new ECEntities())
            {
                return db.SEMESTRE.First(rs => rs.ATIVO == true);
            }
        }

        public SEMESTRE ConsultarById(int id)
        {
            using (ECEntities db = new ECEntities())
            {
                return db.SEMESTRE.First(rs => rs.ID_SEMESTRE == id);
            }
        }
    }
}
