using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EC.Modelo;

namespace EC.Dado
{
    public class DCurso
    {
        public List<CURSO> ConsultarCurso()
        {
            using (ECEntities db = new ECEntities())
            {
                var c = db.CURSO.ToList();
                List<CURSO> list = new List<CURSO>();
                foreach (var tipo in c)
                {
                    CURSO obj = new CURSO();
                    obj.ID_CURSO = tipo.ID_CURSO;
                    obj.DESCRICAO = tipo.DESCRICAO;

                    var disciplinas = db.DISCIPLINA.Where(rs => rs.ID_CURSO == tipo.ID_CURSO);

                    foreach (var disc in disciplinas)
                    {
                        obj.DISCIPLINA.Add(disc);

                    }
                    list.Add(obj);
                }

                return list;
            }
        }


        public CURSO ConsultarById(int id)
        {
            using (ECEntities db = new ECEntities())
            {
                try
                {
                    var objCurso = db.CURSO.First(rs => rs.ID_CURSO == id);

                    var disciplinas = db.DISCIPLINA.Where(rs => rs.ID_CURSO == id);

                    foreach (var disciplina in disciplinas)
                    {
                        DISCIPLINA objDisciplina = new DISCIPLINA();
                        objDisciplina.ID_DISCIPLINA = disciplina.ID_DISCIPLINA;
                        objDisciplina.DESCRICAO = disciplina.DESCRICAO;

                        //objDisciplina.CURSO = db.CURSO.First(rs => rs.ID_CURSO == disciplina.ID_CURSO);

                        objCurso.DISCIPLINA.Add(objDisciplina);
                    }

                    return objCurso;
                }
                catch (Exception e)
                {
                    return null;
                }

            }
        }


        public List<CURSO> ConsultarByIdFuncionarioCoordenador(int idFuncionarioCoordenador)
        {
            using (ECEntities db = new ECEntities())
            {
                try
                {
                    List<CURSO> cursosList = new List<CURSO>();

                    var cursoCoordenador = db.CURSO_COORDENADOR.Where(rs => rs.ID_FUNCIONARIO == idFuncionarioCoordenador);

                    foreach (var cc in cursoCoordenador)
                    {

                        var cursos = db.CURSO.Where(rs => rs.ID_CURSO == cc.ID_CURSO);

                        foreach (var curso in cursos)
                        {
                            CURSO objCurso = new CURSO();
                            objCurso.ID_CURSO = curso.ID_CURSO;
                            objCurso.DESCRICAO = curso.DESCRICAO;

                            var disciplinas = db.DISCIPLINA.Where(rs => rs.ID_CURSO == curso.ID_CURSO);

                            foreach (var disciplina in disciplinas)
                            {
                                DISCIPLINA objDisciplina = new DISCIPLINA();
                                objDisciplina.ID_DISCIPLINA = disciplina.ID_DISCIPLINA;
                                objDisciplina.ID_CURSO = disciplina.ID_CURSO;
                                objDisciplina.DESCRICAO = disciplina.DESCRICAO;

                                objCurso.DISCIPLINA.Add(objDisciplina);
                            }


                            cursosList.Add(objCurso);
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

        public void Salvar(CURSO q)
        {
            using (ECEntities db = new ECEntities())
            {
                //Salva a questão
                db.CURSO.AddObject(q);
                db.SaveChanges();

                //Retorna o id novo gerado na inserção
                var id = q.ID_CURSO;

                db.SaveChanges();
                db.Dispose();
            }
        }  

    }
}
