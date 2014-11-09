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

        public List<FUNCIONARIO> ConsultarProfessor()
        {
            using (ECEntities db = new ECEntities())
            {
                var funcionarios = db.PESSOA.Join(db.FUNCIONARIO,
                    p => p.ID_PESSOA,
                    f => f.ID_PESSOA,
                    (p, f) =>
                    new { f.ID_FUNCIONARIO, f.MATRICULA, f.ID_CARGO, f.PESSOA, f.ID_PESSOA, f.ATIVO }).Where(rs => rs.ID_CARGO == 1).ToList();

                List<FUNCIONARIO> list = new List<FUNCIONARIO>();
                foreach (var tipo in funcionarios)
                {
                    FUNCIONARIO obj = new FUNCIONARIO();
                    obj.ID_FUNCIONARIO = tipo.ID_FUNCIONARIO;
                    obj.MATRICULA = tipo.MATRICULA;
                    obj.ID_CARGO = tipo.ID_CARGO;
                    obj.ID_PESSOA = tipo.ID_PESSOA;
                    obj.PESSOA = tipo.PESSOA;

                    list.Add(obj);
                }

                return list;
            }
        }

    }
}
