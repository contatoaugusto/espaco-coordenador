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
                    amc.DATA_APLICACAO = tipo.DATA_APLICACAO;
                    amc.ID_SEMESTRE = tipo.ID_SEMESTRE;

                    amc.SEMESTRE = db.SEMESTRE.First(rs => rs.ID_SEMESTRE == tipo.ID_SEMESTRE);

                    ltAmc.Add(amc);
                }
                return ltAmc;
            }
        }

        
        public List<AMC> ConsultarAmc(AMC obj)
        {
            using (ECEntities db = new ECEntities())
            {
                var q = db.AMC.Where(rs => rs.ID_AMC == obj.ID_AMC || (rs.DATA_APLICACAO == obj.DATA_APLICACAO && rs.ID_SEMESTRE == obj.ID_SEMESTRE));

                List<AMC> ltAmc = new List<AMC>();
                foreach (var tipo in q)
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

        public AMC ConsultarById(int idAmc)
        {
            using (ECEntities db = new ECEntities())
            {
                var amc = db.AMC.First(rs => rs.ID_AMC == idAmc);
                amc.SEMESTRE = db.SEMESTRE.First(rs => rs.ID_SEMESTRE == amc.ID_SEMESTRE);
                
                return amc;
            }
        }


        public List<AMC> ConsultarAmcBySemestre(int idSemestre)
        {
            using (ECEntities db = new ECEntities())
            {
                var q = db.AMC.Where(rs => rs.SEMESTRE.ID_SEMESTRE == idSemestre);

                List<AMC> ltAmc = new List<AMC>();
                foreach (var tipo in q)
                {
                    AMC amc = new AMC();
                    amc.ID_AMC = tipo.ID_AMC;
                    amc.DATA_APLICACAO = tipo.DATA_APLICACAO;
                    amc.ID_SEMESTRE = tipo.ID_SEMESTRE;
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

        public void Atualiza(AMC q)
        {
            using (ECEntities db = new ECEntities())
            {

                var originalAmc = db.AMC.First(rs => rs.ID_AMC == q.ID_AMC);

                originalAmc.DATA_APLICACAO = q.DATA_APLICACAO;
                originalAmc.ID_SEMESTRE = q.ID_SEMESTRE;
                
                db.SaveChanges();
            }
        }
    }
}
