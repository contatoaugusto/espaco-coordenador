using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Dado;
using EC.Modelo;

namespace EC.Negocio
{
    public class NAlunoAmcQuestao
    {
        public static List<ALUNO_AMC_QUESTAO> Consultar()
        {
            return (new DAlunoAmcQuestao()).Consultar();
        }

        public static ALUNO_AMC_QUESTAO ConsultarById(int idAlunoAmcQuestao)
        {
            return (new DAlunoAmcQuestao()).ConsultarById(idAlunoAmcQuestao);
        }
        public static List<ALUNO_AMC_QUESTAO> ConsultarByAmc(int idAmc)
        {
            return (new DAlunoAmcQuestao()).ConsultarByAmc(idAmc);
        }

        public static List<ALUNO_AMC_QUESTAO> ConsultarByQuestao(int idQuestao)
        {
            return (new DAlunoAmcQuestao()).ConsultarByQuestao(idQuestao);
        }
    }
}
