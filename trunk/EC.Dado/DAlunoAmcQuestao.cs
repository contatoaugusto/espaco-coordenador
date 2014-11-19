using System;
using System.Data.Objects.DataClasses;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Modelo;
using System.Transactions;

namespace EC.Dado
{
    public class DAlunoAmcQuestao
    {

        public List<ALUNO_AMC_QUESTAO> Consultar()
        {
            using (ECEntities db = new ECEntities())
            {
                var q = db.ALUNO_AMC_QUESTAO.ToList();

                List<ALUNO_AMC_QUESTAO> ltAlunoAmcQuestao = new List<ALUNO_AMC_QUESTAO>();
                
                foreach (var tipo in q)
                {
                    ALUNO_AMC_QUESTAO alunoAmc = new ALUNO_AMC_QUESTAO();
                    alunoAmc.ID_ALUNO_AMC_QUESTAO = tipo.ID_ALUNO_AMC_QUESTAO;
                    alunoAmc.ID_QUESTAO = tipo.ID_QUESTAO;
                    alunoAmc.ID_ALUNO_AMC = tipo.ID_ALUNO_AMC;
                    alunoAmc.ACERTO = tipo.ACERTO;

                    ltAlunoAmcQuestao.Add(alunoAmc);
                }

                return ltAlunoAmcQuestao;
            }
        }

 
        public ALUNO_AMC_QUESTAO ConsultarById(int idAlunoAmcQuestao)
        {
            using (ECEntities db = new ECEntities())
            {
                return db.ALUNO_AMC_QUESTAO.First(rs => rs.ID_ALUNO_AMC_QUESTAO == idAlunoAmcQuestao);

            }
        }

       

        public List<ALUNO_AMC_QUESTAO> ConsultarByAmc(int idAmc)
        {
            using (ECEntities db = new ECEntities())
            {
                var q = db.ALUNO_AMC_QUESTAO.Where(rs => rs.QUESTAO.ID_AMC == idAmc);

                List<ALUNO_AMC_QUESTAO> ltAlunoAmcQuestao = new List<ALUNO_AMC_QUESTAO>();

                foreach (var tipo in q)
                {
                    ALUNO_AMC_QUESTAO alunoAmcQuestao = new ALUNO_AMC_QUESTAO();

                    alunoAmcQuestao.ID_ALUNO_AMC_QUESTAO = tipo.ID_ALUNO_AMC_QUESTAO;
                    alunoAmcQuestao.ID_QUESTAO = tipo.ID_QUESTAO;
                    alunoAmcQuestao.ID_ALUNO_AMC = tipo.ID_ALUNO_AMC;
                    alunoAmcQuestao.ACERTO = tipo.ACERTO;

                    ltAlunoAmcQuestao.Add(alunoAmcQuestao);
                }

                return ltAlunoAmcQuestao;
            }
        }

        public List<ALUNO_AMC_QUESTAO> ConsultarByQuestao(int idQUestao)
        {
            using (ECEntities db = new ECEntities())
            {
                var q = db.ALUNO_AMC_QUESTAO.Where(rs => rs.QUESTAO.ID_QUESTAO == idQUestao);

                List<ALUNO_AMC_QUESTAO> ltAlunoAmcQuestao = new List<ALUNO_AMC_QUESTAO>();

                foreach (var tipo in q)
                {
                    ALUNO_AMC_QUESTAO alunoAmcQuestao = new ALUNO_AMC_QUESTAO();

                    alunoAmcQuestao.ID_ALUNO_AMC_QUESTAO = tipo.ID_ALUNO_AMC_QUESTAO;
                    alunoAmcQuestao.ID_QUESTAO = tipo.ID_QUESTAO;
                    alunoAmcQuestao.ID_ALUNO_AMC = tipo.ID_ALUNO_AMC;
                    alunoAmcQuestao.ACERTO = tipo.ACERTO;

                    ltAlunoAmcQuestao.Add(alunoAmcQuestao);
                }

                return ltAlunoAmcQuestao;
            }
        }
    }
}