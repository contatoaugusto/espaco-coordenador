using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Modelo;

namespace EC.Dado
{
    public class DUsuario
    {
        public List<USUARIO> ConsultarUsuario()
        {
            using (ECEntities db = new ECEntities())
            {
                var c = db.USUARIO.ToList();
                List<USUARIO> list = new List<USUARIO>();
                foreach (var tipo in c)
                {
                    USUARIO obj = new USUARIO();
                    obj.ID_USUARIO = tipo.ID_USUARIO;
                    obj.ATIVO = tipo.ATIVO;
                    obj.SENHA = tipo.SENHA;
                    obj.FOTO = tipo.FOTO;
                    obj.ID_FUNCIONARIO = tipo.ID_FUNCIONARIO;
                    list.Add(obj);
                }

                return list;
            }
        }


        public USUARIO ConsultarUsuarioByLoging(int matricula, bool icAtivo)
        {
            using (ECEntities db = new ECEntities())
            {
                try
                {
                    var q = db.USUARIO.First(rs => rs.FUNCIONARIO.MATRICULA == matricula && rs.ATIVO == icAtivo);

                    q.FUNCIONARIO = db.FUNCIONARIO.First(rs => rs.ID_FUNCIONARIO == q.ID_FUNCIONARIO);
                    q.FUNCIONARIO.PESSOA = db.PESSOA.First(rs => rs.ID_PESSOA == q.FUNCIONARIO.ID_PESSOA);
                    q.FUNCIONARIO.CARGO = db.CARGO.First(rs => rs.ID_CARGO == q.FUNCIONARIO.ID_CARGO);
                    
                    //List<USUARIO> list = new List<USUARIO>();

                    //foreach (var tipo in q)
                    //{
                    //    USUARIO obj = new USUARIO();
                    //    obj.ID_USUARIO = tipo.ID_USUARIO;
                    //    obj.USUARIO1 = tipo.USUARIO1;
                    //    obj.ATIVO = tipo.ATIVO;
                    //    obj.SENHA = tipo.SENHA;
                    //    obj.ID_PESSOA = tipo.ID_PESSOA;
                    //    obj.FOTO = tipo.FOTO;
                    //    list.Add(obj);
                    //}
                    return q;
                }
                catch (Exception e)
                {
                    return null;
                }
                
            }
        }

        public void Salvar(USUARIO q)
        {
            using (ECEntities db = new ECEntities())
            {
                //Salva a questão
                db.USUARIO.AddObject(q);
                db.SaveChanges();

                //Retorna o id novo gerado na inserção
                var id = q.ID_USUARIO;

                db.SaveChanges();
                db.Dispose();
            }
        }  

    }
}
