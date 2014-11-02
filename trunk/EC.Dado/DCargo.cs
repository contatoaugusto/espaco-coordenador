using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EC.Modelo;

namespace EC.Dado
{
    public class DCargo
    {
        public List<CARGO> Consultar()
        {
            using (ECEntities db = new ECEntities())
            {
                var c = db.CARGO.ToList();

                List<CARGO> list = new List<CARGO>();

                foreach (var tipo in c)
                {
                    CARGO obj = new CARGO();
                    obj.ID_CARGO = tipo.ID_CARGO;
                    obj.DESCRICAO = tipo.DESCRICAO;
                    list.Add(obj);
                }

                return list;
            }
        }


        public CARGO ConsultarById(int id)
        {
            using (ECEntities db = new ECEntities())
            {
                try
                {
                    return db.CARGO.First(rs => rs.ID_CARGO == id);
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public List<CARGO> ConsultarByPessoa(int idPessoa)
        {
            using (ECEntities db = new ECEntities())
            {
                try
                {
                    List<CARGO> cargoList = new List<CARGO>();

                    var funcionarios = db.FUNCIONARIO.Where(rs => rs.ID_PESSOA == idPessoa);
                    foreach (var func in funcionarios)
                    {
                        var cargos = db.CARGO.Where(rs => rs.ID_CARGO == func.ID_CARGO);

                        foreach (var c in cargos)
                        {
                            CARGO objCargo = new CARGO();
                            objCargo.ID_CARGO = c.ID_CARGO;
                            objCargo.DESCRICAO = c.DESCRICAO;

                            cargoList.Add(objCargo);
                        }
                    }

                    return cargoList;
                }
                catch (Exception e)
                {
                    return null;
                }

            }
        }
    }
}
