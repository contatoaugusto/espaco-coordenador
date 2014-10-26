using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Dado;
using EC.Modelo;


namespace EC.Negocio
{
    public class NQuestão
    {

        public static List<AMC> ConsultarAmc()
        {
            return (new DQuestao()).ConsultarAmc();
        }
        public static List<DISCIPLINA> ConsultarDisciplina()
        {
            return (new DQuestao()).ConsultarDisciplina();
        }

        public static List<QUESTAO> ConsultarQuestao(QUESTAO questao)
        {
            return (new DQuestao()).ConsultarQuestao(questao);
        }

        public static List<QUESTAO> ConsultarQuestaoByProva(int idProva)
        {
            return (new DQuestao()).ConsultarQuestaoByProva(idProva);
        }

        
        public static void Salvar(QUESTAO q)
        {
            DQuestao dquestao = new DQuestao();
            dquestao.Salvar(q);
        }
}
}