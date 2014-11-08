using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Dado;
using EC.Modelo;

namespace EC.Negocio
{
    public class NRepresentanteTurma
    {
        public static List<REPRESENTANTE_TURMA> Consultar()
        {
            return (new DRepresentanteTurma()).Consultar();
        }

        public static REPRESENTANTE_TURMA ConsultarById(int idRepresentanteTurma)
        {
            return (new DRepresentanteTurma()).ConsultarById(idRepresentanteTurma);
        }



        public static bool Salvar(REPRESENTANTE_TURMA obj)
         {
             bool retorno = true;
             DRepresentanteTurma dObj = new DRepresentanteTurma();

             dObj.Salvar(obj);

             return retorno;
         }

        public static List<REPRESENTANTE_TURMA> ConsultarByAluno(int idAluno)
         {
             return (new DRepresentanteTurma()).ConsultarByAluno(idAluno);
         }
    }
}
