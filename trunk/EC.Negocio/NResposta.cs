using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EC.Modelo;
using EC.Dado;

namespace EC.Negocio
{
    public class NResposta
    {

        public static List<RESPOSTA> ConsultarResposta()
        {
            return (new DResposta()).ConsultarResposta();
        }
    }
}
