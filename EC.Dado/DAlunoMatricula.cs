using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Modelo;

namespace EC.Dado
{
    public class DAlunoMatricula
    {
        public List<ALUNO_MATRICULA> Consultar()
        {
            using (ECEntities db = new ECEntities())
            {
                var l = db.ALUNO_MATRICULA.ToList();
                List<ALUNO_MATRICULA> ltAlunoMatricula = new List<ALUNO_MATRICULA>();

                foreach (var tipo in l)
                {

                    ALUNO_MATRICULA alunoMatricula = new ALUNO_MATRICULA();
                    alunoMatricula.ID_ALUNO_MATRICULA = tipo.ID_ALUNO_MATRICULA;
                    alunoMatricula.ID_ALUNO = tipo.ID_ALUNO;
                    alunoMatricula.ID_TURMA = tipo.ID_TURMA;
                    alunoMatricula.DATA_MATRICULA = tipo.DATA_MATRICULA;
                    alunoMatricula.PRESENCA = tipo.PRESENCA;

                    alunoMatricula.TURMA = db.TURMA.First(rs => rs.ID_TURMA == tipo.ID_TURMA);
                    alunoMatricula.ALUNO = db.ALUNO.First(rs => rs.ID_ALUNO == tipo.ID_ALUNO);

                    ltAlunoMatricula.Add(alunoMatricula);
                }

                return ltAlunoMatricula;
            }
        }

        public ALUNO_MATRICULA ConsultarById(int idAlunoMatricula)
        {
            using (ECEntities db = new ECEntities())
            {
                return db.ALUNO_MATRICULA.First(rs => rs.ID_ALUNO_MATRICULA == idAlunoMatricula);
            }
        }

        public ALUNO_MATRICULA ConsultarByAluno(int idAluno)
        {
            using (ECEntities db = new ECEntities())
            {
                try
                {
                    return db.ALUNO_MATRICULA.First(rs => rs.ID_ALUNO == idAluno);
                }
                catch (Exception e) {
                    return null;
                }
            }
        }
               
        //public void Salvar(TURMA q)
        //{
        //    using (ECEntities db = new ECEntities())
        //    {
        //        db.TURMA.AddObject(q);
        //        db.SaveChanges();

        //        var id = q.ID_TURMA;

        //        db.Dispose();
        //    }
        //}     
    }
}
