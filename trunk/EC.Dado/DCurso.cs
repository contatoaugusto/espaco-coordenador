using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EC.Modelo;

namespace EC.Dado
{
    class DCurso
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
                    var q = db.CURSO.First(rs => rs.ID_CURSO == id);
                    return q;
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
