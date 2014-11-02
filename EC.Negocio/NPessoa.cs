using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EC.Modelo;
using EC.Dado;

namespace EC.Negocio
{
    public class NPessoa
    {
        public static List<PESSOA> Consultar()
        {
            return (new DPessoa()).Consultar();
        }

        public static List<PESSOA> Consultar(string prefixText, int count)
        {
            return (new DPessoa()).Consultar(prefixText, count);
        }

        public static PESSOA ConsultarById(int id)
        {
            return (new DPessoa()).ConsultarById(id);
        }

    } 
}
