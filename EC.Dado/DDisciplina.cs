using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EC.Modelo;

namespace EC.Dado
{
    public class DDisciplina
    {

        public List<DISCIPLINA> Consultar()
        {
            using (ECEntities db = new ECEntities())
            {
                var c = db.DISCIPLINA.ToList();
                List<DISCIPLINA> list = new List<DISCIPLINA>();
                foreach (var tipo in c)
                {
                    DISCIPLINA obj = new DISCIPLINA();
                    obj.ID_DISCIPLINA = tipo.ID_DISCIPLINA;
                    obj.ID_CURSO = tipo.ID_CURSO;
                    obj.DESCRICAO = tipo.DESCRICAO;
                    
                    list.Add(obj);
                }

                return list;
            }
        }

        public List<DISCIPLINA> ConsultarByCurso(int idCurso)
        {
            using (ECEntities db = new ECEntities())
            {
                List<DISCIPLINA> list = new List<DISCIPLINA>();
                var disciplinas = db.DISCIPLINA.Where(rs => rs.ID_CURSO == idCurso);

                foreach (var disciplina in disciplinas)
                {
                    DISCIPLINA objDisciplina = new DISCIPLINA();
                    objDisciplina.ID_DISCIPLINA = disciplina.ID_DISCIPLINA;
                    objDisciplina.ID_CURSO = disciplina.ID_CURSO;
                    objDisciplina.DESCRICAO = disciplina.DESCRICAO;

                    list.Add(objDisciplina);

                }
                return list;
            }
        }

        public DISCIPLINA ConsultarById(int id)
        {
            using (ECEntities db = new ECEntities())
            {
                try
                {
                    var objDisciplina = db.DISCIPLINA.First(rs => rs.ID_DISCIPLINA == id);

                    return objDisciplina;
                }
                catch (Exception e)
                {
                    return null;
                }

            }
        }


        public List<FUNCIONARIO> ConsultarProfessorByDisciplina(int idDisciplina)
        {
            using (ECEntities db = new ECEntities())
            {
                try
                {
                    List<FUNCIONARIO> cursosList = new List<FUNCIONARIO>();

                    var disciplinaProfessor = db.DISCIPLINA_PROFESSOR.Where(rs => rs.ID_DISCIPLINA == idDisciplina);

                    foreach (var dp in disciplinaProfessor)
                    {

                        var funcionarios = db.FUNCIONARIO.Where(rs => rs.ID_FUNCIONARIO == dp.ID_FUNCIONARIO);

                        foreach (var funcionario in funcionarios)
                        {
                            FUNCIONARIO objFuncionario = new FUNCIONARIO();
                            objFuncionario.ID_FUNCIONARIO = funcionario.ID_FUNCIONARIO;
                            objFuncionario.MATRICULA = funcionario.MATRICULA;

                            objFuncionario.CARGO = db.CARGO.First(rs => rs.ID_CARGO == funcionario.ID_CARGO);
                            objFuncionario.PESSOA = db.PESSOA.First(rs => rs.ID_PESSOA == funcionario.ID_PESSOA);

                            cursosList.Add(objFuncionario);
                        }
                    }

                    return cursosList;
                }
                catch (Exception e)
                {
                    return null;
                }

            }
        }

        public void Salvar(DISCIPLINA q)
        {
            using (ECEntities db = new ECEntities())
            {
                //Salva a questão
                db.DISCIPLINA.AddObject(q);
                db.SaveChanges();

                //Retorna o id novo gerado na inserção
                var id = q.ID_DISCIPLINA;

                db.SaveChanges();
                db.Dispose();
            }
        }  
    }
}
