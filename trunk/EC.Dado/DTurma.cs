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
                var ltTurma = db.TURMA.Include("TIPO_TURMA").ToList();

                /*
                List<TURMA> ltTurma = new List<TURMA>();

                foreach (var tipo in l)
                {
                    TURMA turma = new TURMA();
                    turma.ID_TURMA = tipo.ID_TURMA;
                    turma.ID_TIPOTURMA = tipo.ID_TIPOTURMA;
                    turma.ID_SEMESTRE = tipo.ID_SEMESTRE;
                    turma.TIPO_TURMA = tipo.TIPO_TURMA;
                    
                    turma.SEMESTRE = db.SEMESTRE.First(rs => rs.ID_SEMESTRE == tipo.ID_SEMESTRE);


                    ltTurma.Add(turma);
                }
                */

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


        public List<TURMA> ConsultarTurmaByAlunoSemestre(int idAluno, int idSemestre)
        {
            using (ECEntities db = new ECEntities())
            {
                var alunoMatricula = db.ALUNO_MATRICULA.Where(rs => rs.ID_ALUNO == idAluno);

                List<TURMA> ltTurma = new List<TURMA>();

                foreach (var alunoMat in alunoMatricula)
                {
                    var turma = db.TURMA.First(rs => rs.ID_TURMA == alunoMat.ID_TURMA && rs.ID_SEMESTRE == idSemestre);
                    turma.TIPO_TURMA = db.TIPO_TURMA.First(rs => rs.ID_TIPOTURMA == turma.ID_TIPOTURMA);
                    //TURMA turma = new TURMA();
                    //turma.ID_TURMA = tipo.ID_TURMA;
                    //turma.ID_TIPOTURMA = tipo.ID_TIPOTURMA;
                    //turma.ID_SEMESTRE = tipo.ID_SEMESTRE;

                    //turma.SEMESTRE = db.SEMESTRE.First(rs => rs.ID_SEMESTRE == tipo.ID_SEMESTRE);

                    ltTurma.Add(turma);
                }

                return ltTurma;
            }
        }

        public void Salvar(TURMA q)
        {
            using (ECEntities db = new ECEntities())
            {
                db.TURMA.Add(q);
                db.SaveChanges();

                var id = q.ID_TURMA;

                db.Dispose();
            }
        }     
    }
}
