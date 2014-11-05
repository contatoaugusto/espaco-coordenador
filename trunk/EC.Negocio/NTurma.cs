using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Dado;
using EC.Modelo;

namespace EC.Negocio
{
    public class NTurma
    {
        public static List<TURMA> ConsultarAmc()
        {
            return (new DTurma()).Consultar();
        }

        public static List<AMC> ConsultarAmc(AMC obj)
        {
            return (new DAmc()).ConsultarAmc(obj);
        }
         public static bool Salvar(TURMA obj)
         {
             bool retorno = true;
             DTurma dObj = new DTurma();

             dObj.Salvar(obj);

             return retorno;
         }

         public static List<TURMA> ConsultarTurmaBySemestre(int idSemestre)
         {
             return (new DTurma()).ConsultarTurmaBySemestre(idSemestre);
         }
    }
}
