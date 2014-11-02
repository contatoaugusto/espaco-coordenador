using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EC.Modelo;

namespace EC.Dado
{
    public class DProfessor
    {

        public List<DISCIPLINA_PROFESSOR> ConsultarDisciplinaProfessor()
        {
            using (ECEntities db = new ECEntities())
            {
                var c = db.DISCIPLINA_PROFESSOR.ToList();
                List<DISCIPLINA_PROFESSOR> list = new List<DISCIPLINA_PROFESSOR>();
                
                foreach (var tipo in c)
                {
                    DISCIPLINA_PROFESSOR obj = new DISCIPLINA_PROFESSOR();
                    obj.ID_DISCIPLINA_PROFESSOR = tipo.ID_DISCIPLINA_PROFESSOR;
                    obj.ID_DISCIPLINA = tipo.ID_DISCIPLINA;
                    obj.ID_FUNCIONARIO = tipo.ID_FUNCIONARIO;

                    list.Add(obj);
                }

                return list;
            }
        }


        public DISCIPLINA_PROFESSOR ConsultarDisciplinaProfessorByProfessor(int idFuncionarioProfessor)
        {
            using (ECEntities db = new ECEntities())
            {
                try
                {
                    var q = db.DISCIPLINA_PROFESSOR.First(rs => rs.ID_FUNCIONARIO == idFuncionarioProfessor);
                    return q;
                }
                catch (Exception e)
                {
                    return null;
                }

            }
        }

        public List<CURSO> ConsultarCursoByProfessor(int idFuncionarioProfessor)
        {
            using (ECEntities db = new ECEntities())
            {
                var disicplinaProfessorList = db.DISCIPLINA_PROFESSOR.Where(rs => rs.ID_FUNCIONARIO == idFuncionarioProfessor);
                
                List<CURSO> list = new List<CURSO>();

                foreach (var tipo in disicplinaProfessorList)
                {
                    var disciplina = db.DISCIPLINA.First(rs => rs.ID_DISCIPLINA == tipo.ID_DISCIPLINA);

                    var cursoList = db.CURSO.Where(rs => rs.ID_CURSO == disciplina.ID_CURSO);

                    foreach (var cursoObj in cursoList)
                    {
                        CURSO cursoModelo = new CURSO();
                        cursoModelo.ID_CURSO = cursoObj.ID_CURSO;
                        cursoModelo.DESCRICAO = cursoObj.DESCRICAO;
                        list.Add(cursoModelo);
                    }
                }

                return list;
            }
        }

    }
}
