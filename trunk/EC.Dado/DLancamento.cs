using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Modelo;

namespace EC.Dado
{
    public class DLancamento
    {
        //public List<TURMA> ConsultarTipoTurma()
        //{
        //    using (ECEntities db = new ECEntities())
        //    {
        //        var t = db.TIPO_TURMA.ToList();
        //        List<EC.Modelo.TIPO_TURMA> ltTipoTurma = new List<EC.Modelo.TIPO_TURMA>();

        //        foreach (var tipo in t)
        //        {
        //        TIPO_TURMA tipoTurma = new TIPO_TURMA();
        //        tipoTurma.ID_TIPOTURMA = tipo.ID_TIPOTURMA;
        //            tipoTurma.DESCRICAO = tipo.DESCRICAO;

        //            ltTipoTurma.Add(tipoTurma);
        //        }

        //        return ltTipoTurma;
        //    }

        //}


        public List<TIPO_LANCAMENTO> ConsultarTipolancamento()
        {
            using (ECEntities db = new ECEntities())
            {
                var tl = db.TIPO_LANCAMENTO.ToList();
                List<TIPO_LANCAMENTO> ltTipolancamento = new List<TIPO_LANCAMENTO>();

                foreach (var tipo in tl)
                {
                    TIPO_LANCAMENTO tipolancamento = new TIPO_LANCAMENTO();
                    tipolancamento.ID_TIPOLANCAMENTO = tipo.ID_TIPOLANCAMENTO;
                    tipolancamento.DESCRICAO = tipo.DESCRICAO;
                    ltTipolancamento.Add(tipolancamento);
                }

                return ltTipolancamento;
            }

        }


        public List<LANCAMENTO> ConsultarLancamento()
        {
            using (ECEntities db = new ECEntities())
            {
                var q = db.LANCAMENTO.ToList();

                List<LANCAMENTO> ltLancamento = new List<LANCAMENTO>();

                foreach (var tipo in q)
                {
                    LANCAMENTO lancamento = new LANCAMENTO();
                    lancamento.ID_LANCAMENTO = tipo.ID_LANCAMENTO;
                    lancamento.ID_TURMA = tipo.ID_TURMA;
                    lancamento.JUSTIFICATIVA = tipo.JUSTIFICATIVA;
                    lancamento.PROVIDENCIA = tipo.PROVIDENCIA;
                    lancamento.DATA_LANCAMENTO = tipo.DATA_LANCAMENTO;
                    
                    lancamento.FUNCIONARIO = new FUNCIONARIO();
                    lancamento.FUNCIONARIO.PESSOA = new PESSOA();
                    lancamento.FUNCIONARIO.PESSOA.ID_PESSOA = tipo.FUNCIONARIO.PESSOA.ID_PESSOA;
                    lancamento.FUNCIONARIO.PESSOA.NOME = tipo.FUNCIONARIO.PESSOA.NOME;
                    lancamento.FUNCIONARIO.PESSOA.TELEFONE = tipo.FUNCIONARIO.PESSOA.TELEFONE;
                    lancamento.FUNCIONARIO.PESSOA.EMAIL = tipo.FUNCIONARIO.PESSOA.EMAIL;

                    lancamento.DISCIPLINA = new DISCIPLINA();
                    lancamento.DISCIPLINA.ID_DISCIPLINA = tipo.DISCIPLINA.ID_DISCIPLINA;
                    lancamento.DISCIPLINA.ID_CURSO = tipo.DISCIPLINA.ID_CURSO;
                    lancamento.DISCIPLINA.DESCRICAO = tipo.DISCIPLINA.DESCRICAO;


                    ltLancamento.Add(lancamento);
                }

                return ltLancamento;
            }
        }

        public void Salvar(LANCAMENTO l)
        {

            using (ECEntities db = new ECEntities())
            {
                db.LANCAMENTO.Add(l);
                db.SaveChanges();
            }
        }

    }
}