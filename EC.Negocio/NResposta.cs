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

        //public static List<RESPOSTA> ConsultarByQuestao(int idQuestao)
        //{
        //    return (new DResposta()).ConsultarByQuestao();
        //}

        //public static RESPOSTA ConsultarById(int idResposta)
        //{
        //    return (new DResposta()).ConsultarById();
        //}
    }
}
