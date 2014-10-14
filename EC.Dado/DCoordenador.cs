using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EC.Modelo;

namespace EC.Dado
{
    public class DCursoCoordenador
    {

        public List<CURSO_COORDENADOR> ConsultarCursoCoordenador()
        {
            using (ECEntities db = new ECEntities())
            {
                var c = db.CURSO_COORDENADOR.ToList();
                List<CURSO_COORDENADOR> list = new List<CURSO_COORDENADOR>();
                foreach (var tipo in c)
                {
                    CURSO_COORDENADOR obj = new CURSO_COORDENADOR();
                    obj.ID_CURSO_COORDENADOR = tipo.ID_CURSO_COORDENADOR;
                    obj.ID_FUNCIONARIO = tipo.ID_FUNCIONARIO;
                    obj.ID_CURSO = tipo.ID_CURSO;
                    list.Add(obj);
                }

                return list;
            }
        }


        public CURSO_COORDENADOR ConsultarCursoCoordenadorById(int idFuncionarioCoordenador)
        {
            using (ECEntities db = new ECEntities())
            {
                try
                {
                    var q = db.CURSO_COORDENADOR.First(rs => rs.ID_FUNCIONARIO == idFuncionarioCoordenador);
                    return q;
                }
                catch (Exception e)
                {
                    return null;
                }

            }
        }

        public List<CURSO> ConsultarCursoByCoordenador(int idFuncionarioCoordenador)
        {
            using (ECEntities db = new ECEntities())
            {
                var coordenadorList = db.CURSO_COORDENADOR.Where(rs => rs.ID_FUNCIONARIO == idFuncionarioCoordenador);
                
                List<CURSO> list = new List<CURSO>();
                foreach (var tipo in coordenadorList)
                {
                    CURSO obj = db.CURSO.First(rs => rs.ID_CURSO == tipo.ID_CURSO);
                    list.Add(obj);
                }

                return list;
            }
        }

        public void Salvar(CURSO_COORDENADOR q)
        {
            using (ECEntities db = new ECEntities())
            {
                //Salva a questão
                db.CURSO_COORDENADOR.AddObject(q);
                db.SaveChanges();

                //Retorna o id novo gerado na inserção
                var id = q.ID_CURSO_COORDENADOR;

                db.SaveChanges();
                db.Dispose();
            }
        }  

    }
}
