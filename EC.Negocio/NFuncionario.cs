using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EC.Modelo;
using EC.Dado;

namespace EC.Negocio
{
    public class NFuncionario
    {

        public static List<FUNCIONARIO> ConsultarFuncionario()
        {
            return (new DFuncionario()).ConsultarFuncionario();
        }


        public static FUNCIONARIO ConsultarById(int idFuncionario)
        {
            return (new DFuncionario()).ConsultarById(idFuncionario);
        }
    }
}
