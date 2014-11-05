using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Modelo;

namespace EC.Dado
{
    public class DTurma
    {
        public List<TURMA> Consultar()
        {
            using (ECEntities db = new ECEntities())
            {
                var l = db.TURMA.ToList();
                List<TURMA> ltTurma = new List<TURMA>();

                foreach (var tipo in l)
                {
                    TURMA turma = new TURMA();
                    turma.ID_TURMA = tipo.ID_TURMA;
                    turma.ID_TIPOTURMA = tipo.ID_TIPOTURMA;
                    turma.ID_SEMESTRE = tipo.ID_SEMESTRE;

                    turma.SEMESTRE = db.SEMESTRE.First(rs => rs.ID_SEMESTRE == tipo.ID_SEMESTRE);


                    ltTurma.Add(turma);
                }

                return ltTurma;
            }
        }


        public List<TURMA> ConsultarTurmaBySemestre(int idSemestre)
        {
            using (ECEntities db = new ECEntities())
            {
                var q = db.TURMA.Where(rs => rs.SEMESTRE.ID_SEMESTRE == idSemestre);

                List<TURMA> ltTurma = new List<TURMA>();
                foreach (var tipo in q)
                {
                    TURMA turma = new TURMA();
                    turma.ID_TURMA = tipo.ID_TURMA;
                    turma.ID_TIPOTURMA = tipo.ID_TIPOTURMA;
                    turma.ID_SEMESTRE = tipo.ID_SEMESTRE;

                    turma.SEMESTRE = db.SEMESTRE.First(rs => rs.ID_SEMESTRE == tipo.ID_SEMESTRE);

                    ltTurma.Add(turma);
                }

                return ltTurma;
            }
        }
       
        public void Salvar(TURMA q)
        {
            using (ECEntities db = new ECEntities())
            {
                db.TURMA.AddObject(q);
                db.SaveChanges();

                var id = q.ID_TURMA;

                db.Dispose();
            }
        }     
    }
}
