using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Dado;
using EC.Modelo;

namespace EC.Negocio
{
    public class NCurso
    {


        public static List<CURSO> ConsultarCurso()
        {
            return (new DCurso()).ConsultarCurso();
        }


        public static CURSO ConsultarByCurso(int idCurso)
        {
            return (new DCurso()).ConsultarById(idCurso);

        }

        public static List<CURSO> ConsultarByIdFuncionarioCoordenador(int idFuncionarioCoordenador)
        {
            return (new DCurso()).ConsultarByIdFuncionarioCoordenador(idFuncionarioCoordenador);

        }

    }
}
