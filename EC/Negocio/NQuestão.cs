using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Dado;
using EC.Modelo;
using EC.Negocio;


namespace EC.Negocio
{
    public class NQuestão
    {

        public static List<Modelo.AMC> ConsultarAmc()
        {
            return (new DQuestao()).ConsultarAmc();
        }
        public static List<Modelo.DISCIPLINA> ConsultarDisciplina()
        {
            return (new DQuestao()).ConsultarDisciplina();
        }

        public static List<Modelo.FUNCIONARIO> ConsultarFuncionario()
        {
            return (new DQuestao()).ConsultarFuncionario();
        }

        public static List<Modelo.QUESTAO> ConsultarQuestao(QUESTAO questao)
        {
            return (new DQuestao()).ConsultarQuestao(questao);
        }

        public static List<EC.Modelo.RESPOSTA> ConsultarResposta()
        {
            return (new DQuestao()).ConsultarResposta();
        }

         public static void Salvar(QUESTAO q)
        {
         DQuestao dquestao = new DQuestao();
         dquestao.Salvar(q);
    }
}
}