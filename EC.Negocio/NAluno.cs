using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EC.Modelo;
using EC.Dado;

namespace EC.Negocio
{
    public class NAluno
    {

        public static List<ALUNO> Consultar()
        {
            return (new DAluno()).Consultar();
        }

        public static List<ALUNO> ConsultarByTurma(int idTurma)
        {
            return (new DAluno()).ConsultarByTurma(idTurma);
        }
        
        public static ALUNO ConsultarById(int idAluno)
        {
            return (new DAluno()).ConsultarById(idAluno);
        }

        public static ALUNO ConsultarByRA(int nuRA)
        {
            return (new DAluno()).ConsultarByRA(nuRA);
        }
    }
}
