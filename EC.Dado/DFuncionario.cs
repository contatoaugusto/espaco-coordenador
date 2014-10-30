using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EC.Modelo;

namespace EC.Dado
{
    public class DFuncionario
    {

        public FUNCIONARIO ConsultarById(int id)
        {
            using (ECEntities db = new ECEntities())
            {
                return db.FUNCIONARIO.First(rs => rs.ID_FUNCIONARIO == id);
            }
        }


        public List<FUNCIONARIO> ConsultarFuncionario()
        {
            using (ECEntities db = new ECEntities())
            {
                var f = db.FUNCIONARIO.ToList();
                List<FUNCIONARIO> ltFuncionario = new List<FUNCIONARIO>();
                foreach (var tipo in f)
                {
                    FUNCIONARIO funcionario = new FUNCIONARIO();
                    funcionario.ID_FUNCIONARIO = tipo.ID_FUNCIONARIO;
                    //funcionario.PESSOA = new PESSOA();
                    //funcionario.PESSOA.NOME = tipo.PESSOA.NOME;
                    funcionario.PESSOA = tipo.PESSOA;
                    ltFuncionario.Add(funcionario);
                }

                return ltFuncionario;
            }
        }
    }
}
