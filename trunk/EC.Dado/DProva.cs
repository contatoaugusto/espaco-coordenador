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

            var result = new List<QUESTAO>();
            for (int i = 0; i < qtdeQuestoes; i++)
            {
                var questao = FindRandomQuestao(idAmc, idCurso);
                // Não armazenar questão repetida
                if (result.Contains(questao))
                    i--;
                else
                {
                    questao.ID_PROVA = prova.ID_PROVA;
                    
                    ECEntities db = new ECEntities();
                    db.QUESTAO.AddObject(questao);
                    //result.Add(questao);

                    db.SaveChanges();
                    db.Dispose();

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
                db.AddToPROVA(obj);
                db.SaveChanges();

                db.SaveChanges();
                db.Dispose();

                //return obj.ID_PROVA;
            }
        }

    }
}
