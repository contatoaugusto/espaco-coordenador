using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Modelo;

namespace EC.Dado
{
    public class DAmc
    {
        public List<AMC> ConsultarAmc()
        {
            using (ECEntities db = new ECEntities())
            {
                var c = db.AMC.ToList();
                List<AMC> ltAmc = new List<AMC>();
                foreach (var tipo in c)
                {
                    AMC amc = new AMC();
                    amc.ID_AMC = tipo.ID_AMC;
                    //amc.ANO = tipo.ANO;
                    //amc.SEMESTRE = tipo.SEMESTRE;
                    amc.DATA_APLICACAO = tipo.DATA_APLICACAO;
                    amc.ID_SEMESTRE = tipo.ID_SEMESTRE;
                    ltAmc.Add(amc);
                }

                return ltAmc;
            }
        }

        
        public List<AMC> ConsultarAmc(AMC obj)
        {
            using (ECEntities db = new ECEntities())
            {
                var q = db.AMC.Where(rs => rs.ID_AMC == obj.ID_AMC || (rs.SEMESTRE == obj.SEMESTRE && rs.ANO == obj.ANO && rs.DATA_APLICACAO == obj.DATA_APLICACAO));

                List<AMC> ltAmc = new List<AMC>();
                foreach (var tipo in q)
                {
                    AMC amc = new AMC();
                    amc.ID_AMC = tipo.ID_AMC;
                    amc.ANO = tipo.ANO;
                    amc.SEMESTRE = tipo.SEMESTRE;
                    ltAmc.Add(amc);
                }

                return ltAmc;
            }
        }

       
        public void Salvar(AMC q)
        {
            using (ECEntities db = new ECEntities())
            {
                //Salva a questão
                db.AMC.AddObject(q);
                db.SaveChanges();

                //Retorna o id novo gerado na inserção
                var id = q.ID_AMC;

                db.Dispose();
            }
        }     
    }
}
