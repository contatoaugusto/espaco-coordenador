using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EC.Modelo;
using EC.Dado;

namespace EC.Dado
{
    public class DProva
    {

        public QUESTAO FindRandomQuestao(int idAmc, int idCurso)
        {
            DQuestao questao = new DQuestao();
            var qry = questao.ConsultarQuestaoProvaByAmcCurso(idAmc, idCurso);
            
            int count = qry.Count();
            int index = new Random().Next(count);
            return qry.Skip(index).FirstOrDefault();
        }

        public bool GerarProvaRandomicamente(int idAmc, int idCurso, int qtdeQuestoes, int idFuncionario)
        {

            // Grava Prova
            PROVA prova = new PROVA();
            prova.DATA_CRIACAO = DateTime.Now;
            prova.QTDE_QUESTOES = qtdeQuestoes;

            // Funcionario cadastro
            DFuncionario dfuncionario = new DFuncionario();

            prova.ID_FUNCIONARIO_CADATRO = dfuncionario.ConsultarById(idFuncionario).ID_FUNCIONARIO;

            prova.OBSERVACAO = "Prova criada automaticamente com questões de forma randômica pelo sistema";

            Salvar(prova);

            ECEntities db = new ECEntities();
            var result = new List<QUESTAO>();
            int []questoesControle = new int [32];
            for (int i = 0; i < qtdeQuestoes; i++)
            {
                var questao = FindRandomQuestao(idAmc, idCurso);
                // Não armazenar questão repetida
                if (questao != null && questoesControle.Contains(questao.ID_QUESTAO))
                    i--;
                else
                {
                    var q = db.QUESTAO.First(rs => rs.ID_QUESTAO == questao.ID_QUESTAO);
                    q.ID_PROVA = prova.ID_PROVA;
                    q.NU_SEQUENCIA_PROVA = i;

                    db.SaveChanges();

                    questoesControle[i] = questao.ID_QUESTAO;
                }
            }

            return true;
        }

        public List<PROVA> Consultar()
        {
            using (ECEntities db = new ECEntities())
            {
                return db.PROVA.ToList();
            }
        }

        
        public void Salvar(PROVA obj)
        {
            using (ECEntities db = new ECEntities())
            {
                db.PROVA.Add(obj);
                db.SaveChanges();

                db.SaveChanges();
                db.Dispose();

                //return obj.ID_PROVA;
            }
        }

    }
}
