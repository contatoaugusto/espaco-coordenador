using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Dado;
using EC.Modelo;

namespace EC.Negocio
{
    public class NAlunoAmc
    {
        public static List<ALUNO_AMC> Consultar()
        {
            return (new DAlunoAmc()).Consultar();
        }

        public static ALUNO_AMC ConsultarById(int idAlunoAmc)
        {
            return (new DAlunoAmc()).ConsultarById(idAlunoAmc);
        }
        public static List<ALUNO_AMC> ConsultarByAmc(int idAmc)
        {
            return (new DAlunoAmc()).ConsultarByAmc(idAmc);
        }

        public static List<ALUNO_AMC> ConsultarByAmcMencao(int idAmc, string mencao)
        {
            return (new DAlunoAmc()).ConsultarByAmcMencao(idAmc, mencao);
        }
    }
}
