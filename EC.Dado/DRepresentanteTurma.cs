using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Modelo;

namespace EC.Dado
{
    public class DRepresentanteTurma
    {
        public List<REPRESENTANTE_TURMA> Consultar()
        {
            using (ECEntities db = new ECEntities())
            {
                var l = db.REPRESENTANTE_TURMA.ToList();
                List<REPRESENTANTE_TURMA> ltRepresentante = new List<REPRESENTANTE_TURMA>();

                foreach (var tipo in l)
                {
                    REPRESENTANTE_TURMA representante = new REPRESENTANTE_TURMA();
                    representante.ID_REPRESENTANTE = tipo.ID_REPRESENTANTE;
                    representante.ID_TIPOREPRESENTANTE = tipo.ID_TIPOREPRESENTANTE;
                    representante.ID_ALUNO_MATRICULA = tipo.ID_ALUNO_MATRICULA;
                    representante.ID_TURMA = tipo.ID_TURMA;

                    representante.TURMA = db.TURMA.First(rs => rs.ID_TURMA == tipo.ID_TURMA);
                    representante.TURMA.SEMESTRE = db.SEMESTRE.First(rs => rs.ID_SEMESTRE == representante.TURMA.ID_SEMESTRE);
                    representante.TURMA.TIPO_TURMA = db.TIPO_TURMA.First(rs => rs.ID_TIPOTURMA == representante.TURMA.ID_TIPOTURMA);

                    representante.ALUNO_MATRICULA = db.ALUNO_MATRICULA.First(rs => rs.ID_ALUNO_MATRICULA == tipo.ID_ALUNO_MATRICULA);
                    representante.ALUNO_MATRICULA.ALUNO = db.ALUNO.First(rs => rs.ID_ALUNO == representante.ALUNO_MATRICULA.ID_ALUNO);
                    representante.ALUNO_MATRICULA.ALUNO.PESSOA = db.PESSOA.First(rs => rs.ID_PESSOA == representante.ALUNO_MATRICULA.ALUNO.PESSOA.ID_PESSOA);

                    ltRepresentante.Add(representante);
                }

                return ltRepresentante;
            }
        }


        public List<REPRESENTANTE_TURMA> ConsultarByAluno(int idAluno)
        {
            using (ECEntities db = new ECEntities())
            {
                var l = db.REPRESENTANTE_TURMA.Where(rs => rs.ALUNO_MATRICULA.ID_ALUNO == idAluno);

                List<REPRESENTANTE_TURMA> ltRepresentante = new List<REPRESENTANTE_TURMA>();

                foreach (var tipo in l)
                {
                    REPRESENTANTE_TURMA representante = new REPRESENTANTE_TURMA();
                    representante.ID_REPRESENTANTE = tipo.ID_REPRESENTANTE;
                    representante.ID_TIPOREPRESENTANTE = tipo.ID_TIPOREPRESENTANTE;
                    representante.ID_ALUNO_MATRICULA = tipo.ID_ALUNO_MATRICULA;
                    representante.ID_TURMA = tipo.ID_TURMA;

                    representante.TURMA = db.TURMA.First(rs => rs.ID_TURMA == tipo.ID_TURMA);
                    representante.TURMA.SEMESTRE = db.SEMESTRE.First(rs => rs.ID_SEMESTRE == representante.TURMA.ID_SEMESTRE);
                    representante.TURMA.TIPO_TURMA = db.TIPO_TURMA.First(rs => rs.ID_TIPOTURMA == representante.TURMA.ID_TIPOTURMA);

                    ltRepresentante.Add(representante);
                }

                return ltRepresentante;
            }
        }

        public REPRESENTANTE_TURMA ConsultarById(int idRepresentanteTurma)
        {
            using (ECEntities db = new ECEntities())
            {
                return db.REPRESENTANTE_TURMA.First(rs => rs.ID_REPRESENTANTE == idRepresentanteTurma);
            }
        }

        public void Salvar(REPRESENTANTE_TURMA q)
        {
            using (ECEntities db = new ECEntities())
            {
                db.REPRESENTANTE_TURMA.Add(q);
                db.SaveChanges();

                var id = q.ID_REPRESENTANTE;

                db.Dispose();
            }
        }     
    }
}
