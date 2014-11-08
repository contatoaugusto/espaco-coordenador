using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Dado;
using EC.Modelo;

namespace EC.Negocio
{
    public class NAlunoMatricula
    {
        public static List<ALUNO_MATRICULA> Consultar()
        {
            return (new DAlunoMatricula()).Consultar();
        }

        public static ALUNO_MATRICULA ConsultarById(int idAlunoMatricula)
        {
            return (new DAlunoMatricula()).ConsultarById(idAlunoMatricula);
        }

         //public static bool Salvar(ALUNO_MATRICULA obj)
         //{
         //    bool retorno = true;
         //    DAlunoMatricula dObj = new DAlunoMatricula();

         //    dObj.Salvar(obj);

         //    return retorno;
         //}

         public static ALUNO_MATRICULA ConsultarTByAluno(int idAluno)
         {
             return (new DAlunoMatricula()).ConsultarByAluno(idAluno);
         }
    }
}
