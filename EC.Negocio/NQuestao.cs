using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Dado;
using EC.Modelo;


namespace EC.Negocio
{
    public class NQuestao
    {

        public static List<QUESTAO> ConsultarQuestao()
        {
            return (new DQuestao()).ConsultarQuestao();
        }

        public static List<QUESTAO> ConsultarQuestao(QUESTAO questao)
        {
            return (new DQuestao()).ConsultarQuestao(questao);
        }

        public static List<QUESTAO> ConsultarQuestaoProvaByAmc(int idAmc)
        {
            return (new DQuestao()).ConsultarQuestaoProvaByAmc(idAmc);
        }

        public static List<QUESTAO> ConsultarQuestaoByAmc(int idAmc)
        {
            return (new DQuestao()).ConsultarQuestaoByAmc(idAmc);
        }

        public static List<QUESTAO> ConsultarQuestaoByNomeProfessor(string nomeProfessor)
        {
            return (new DQuestao()).ConsultarQuestaoByNomeProfessor(nomeProfessor);
        }

        public static List<QUESTAO> ConsultarQuestaoProvaByAmcCurso(int idAmc, int idCurso)
        {
            return (new DQuestao()).ConsultarQuestaoProvaByAmcCurso(idAmc, idCurso);
        }

        public static QUESTAO ConsultarById(int idQuestao)
        {
            return (new DQuestao()).ConsultarById(idQuestao);
        }

        public static List<QUESTAO> ConsultarQuestaoByProva(int idProva)
        {
            return (new DQuestao()).ConsultarQuestaoByProva(idProva);
        }

        //public static List<QUESTAO> ConsultarQuestaoByProfessor(List<FUNCIONARIO> funcionarios)
        //{
        //    return (new DQuestao()).ConsultarQuestaoByProfessor(funcionarios);
        //}

        public static void Salvar(QUESTAO q)
        {
            (new DQuestao()).Salvar(q);
        }
        public static void Atualiza(QUESTAO q)
        {
            (new DQuestao()).Atualiza(q);
        }

        public static void Excluir(int id)
        {
            (new DQuestao()).Excluir(id);
        }
        
    }
}