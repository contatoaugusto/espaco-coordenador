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

        public static List<QUESTAO> ConsultarQuestao()
        {
            return (new DQuestao()).ConsultarQuestao();
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