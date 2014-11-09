using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Dado;
using EC.Modelo;

namespace EC.Negocio
{
    public class NAmc
    {
        public static List<AMC> ConsultarAmc()
        {
            return (new DAmc()).ConsultarAmc();
        }

        public static List<AMC> ConsultarAmc(AMC obj)
        {
            return (new DAmc()).ConsultarAmc(obj);
        }

        public static AMC ConsultarById(int idAmc)
        {
            return (new DAmc()).ConsultarById(idAmc);
        }
         public static bool Salvar(AMC obj)
         {
             bool retorno = true;
             DAmc dObj = new DAmc();

             if (dObj.ConsultarAmc(obj).Count > 0)
                 return false;
             
             dObj.Salvar(obj);

             return retorno;
         }

         public static void Atualiza(AMC obj)
         {
             (new DAmc()).Atualiza(obj);
         }

         public static List<AMC> ConsultarAmcBySemestre(int idSemestre)
         {
             return (new DAmc()).ConsultarAmcBySemestre(idSemestre);
         }
    }
}
