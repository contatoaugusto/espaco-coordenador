using System;
using System.Data.Objects.DataClasses;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Modelo;
using System.Transactions;

namespace EC.Dado
{
    public class DAlunoAmc
    {

        public List<ALUNO_AMC> Consultar()
        {
            using (ECEntities db = new ECEntities())
            {
                var q = db.ALUNO_AMC.ToList();

                List<ALUNO_AMC> ltAlunoAmc = new List<ALUNO_AMC>();
                
                foreach (var tipo in q)
                {
                    ALUNO_AMC alunoAmc = new ALUNO_AMC();
                    alunoAmc.ID_ALUNO_AMC = tipo.ID_ALUNO_AMC;
                    alunoAmc.NOTA = tipo.NOTA;
                    alunoAmc.CLASSIFICACAO = tipo.CLASSIFICACAO;

                    alunoAmc.AMC = tipo.AMC;
                    alunoAmc.ALUNO_MATRICULA = tipo.ALUNO_MATRICULA;

                    ltAlunoAmc.Add(alunoAmc);
                }

                return ltAlunoAmc;
            }
        }

 
        public ALUNO_AMC ConsultarById(int idAlunoAmc)
        {
            using (ECEntities db = new ECEntities())
            {
                var q = db.ALUNO_AMC.Where(rs => rs.ID_ALUNO_AMC == idAlunoAmc);

                ALUNO_AMC alunoAmc = new ALUNO_AMC();

                foreach (var tipo in q)
                {
                    alunoAmc.ID_ALUNO_AMC = tipo.ID_ALUNO_AMC;
                    alunoAmc.NOTA = tipo.NOTA;
                    alunoAmc.CLASSIFICACAO = tipo.CLASSIFICACAO;

                    alunoAmc.AMC = tipo.AMC;
                    alunoAmc.ALUNO_MATRICULA = tipo.ALUNO_MATRICULA;

                }

                return alunoAmc;
            }
        }

        public List<ALUNO_AMC> ConsultarByAmc(int idAmc)
        {
            using (ECEntities db = new ECEntities())
            {
                var q = db.ALUNO_AMC.Where(rs => rs.ID_AMC == idAmc);

                List<ALUNO_AMC> ltAlunoAmc = new List<ALUNO_AMC>();

                foreach (var tipo in q)
                {
                    ALUNO_AMC alunoAmc = new ALUNO_AMC();

                    alunoAmc.ID_ALUNO_AMC = tipo.ID_ALUNO_AMC;
                    alunoAmc.NOTA = tipo.NOTA;
                    alunoAmc.CLASSIFICACAO = tipo.CLASSIFICACAO;

                    alunoAmc.AMC = db.AMC.First(rs => rs.ID_AMC == tipo.ID_AMC);
                    alunoAmc.ALUNO_MATRICULA = db.ALUNO_MATRICULA.First(rs => rs.ID_ALUNO_MATRICULA == tipo.ALUNO_MATRICULA.ID_ALUNO_MATRICULA);

                    ltAlunoAmc.Add(alunoAmc);
                }

                return ltAlunoAmc;
            }
        }
        

    }
}