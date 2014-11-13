using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Modelo;

namespace EC.Dado
{
    public class DAluno
    {
        public List<ALUNO> Consultar()
        {
            using (ECEntities db = new ECEntities())
            {
                var l = db.ALUNO.ToList();
                List<ALUNO> ltAluno = new List<ALUNO>();

                foreach (var tipo in l)
                {
                    ALUNO aluno = new ALUNO();
                    aluno.ID_ALUNO = tipo.ID_ALUNO;
                    aluno.RA  = tipo.RA;
                    aluno.ATIVO = tipo.ATIVO;
                    aluno.ID_PESSOA = tipo.ID_PESSOA;

                    aluno.PESSOA = db.PESSOA.First(rs => rs.ID_PESSOA == tipo.ID_PESSOA);

                    ltAluno.Add(aluno);
                }

                return ltAluno;
            }
        }

        public ALUNO ConsultarByRA(int nuRA)
        {
            using (ECEntities db = new ECEntities())
            {
                var aluno = db.ALUNO.First(rs => rs.RA == nuRA);
                aluno.PESSOA = db.PESSOA.First(rs => rs.ID_PESSOA == aluno.ID_PESSOA);
                return aluno;
            }
        }

        public ALUNO ConsultarById(int idAluno)
        {
            using (ECEntities db = new ECEntities())
            {
                var aluno = db.ALUNO.First(rs => rs.ID_ALUNO == idAluno);
                aluno.PESSOA = db.PESSOA.First(rs => rs.ID_PESSOA == aluno.ID_PESSOA);
                return aluno;
            }
        }

        public List<ALUNO> ConsultarByTurma(int idTurma)
        {
            using (ECEntities db = new ECEntities())
            {
                List<ALUNO> ltAluno = new List<ALUNO>();

                var alunoMatricula = db.ALUNO_MATRICULA.Where(rs => rs.ID_TURMA == idTurma);

                foreach (var am in alunoMatricula)
                {
                    var alunos = db.ALUNO.Where(rs => rs.ID_ALUNO == am.ID_ALUNO);

                    foreach (var tipo in alunos)
                    {
                        ALUNO aluno = new ALUNO();
                        aluno.ID_ALUNO = tipo.ID_ALUNO;
                        aluno.RA = tipo.RA;
                        aluno.ATIVO = tipo.ATIVO;
                        aluno.ID_PESSOA = tipo.ID_PESSOA;

                        aluno.PESSOA = db.PESSOA.First(rs => rs.ID_PESSOA == tipo.ID_PESSOA);

                        ltAluno.Add(aluno);
                    }
                }
                return ltAluno;
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
