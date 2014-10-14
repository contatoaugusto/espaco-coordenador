using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Modelo;
//using EC.Negocio;


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
         public List<PESSOA> ConsultarPessoa()
        {
            using (ECEntities db = new ECEntities())
            {
                var p = db.PESSOA.ToList();
                List<PESSOA> ltPessoa = new List<PESSOA>();

                foreach (var tipo in p)
                {
                    PESSOA pessoa = new PESSOA();
                    pessoa.ID_PESSOA = tipo.ID_PESSOA;
                    pessoa.NOME = tipo.NOME;
                    ltPessoa.Add(pessoa);
                }

                return ltPessoa;
            }

        }

         public List<TURMA> ConsultarTurma()
         {
             using (ECEntities db = new ECEntities())
             {
                 var l = db.TURMA.ToList();
                 List<TURMA> ltTurma = new List<TURMA>();

                 foreach (var tipo in l)
                 {
                     TURMA turma = new TURMA();
                     turma.ID_TURMA = tipo.ID_TURMA;
                     turma.ID_DISCIPLINA = tipo.ID_DISCIPLINA;
                     turma.ID_FUNCIONARIO = tipo.ID_FUNCIONARIO;
                     turma.ID_TIPOTURMA = tipo.ID_TIPOTURMA;
                     turma.SEMESTRE = tipo.SEMESTRE;
                     turma.ANO = tipo.ANO;


                     ltTurma.Add(turma);
                 }

                 return ltTurma;
             }
         }

        public List<LANCAMENTO> ConsultarLancamento()
        {
            using (ECEntities db = new ECEntities())
            {
                var l = db.LANCAMENTO.ToList();
                List<LANCAMENTO> ltLancamento = new List<LANCAMENTO>();

                foreach (var tipo in l)
                {
                    LANCAMENTO lancamento = new LANCAMENTO();
                    lancamento.ID_LANCAMENTO = tipo.ID_LANCAMENTO;
                    ltLancamento.Add(lancamento);
                }

                return ltLancamento;
            }
        }

          public void Salvar(LANCAMENTO l)
        {

            using (ECEntities db = new ECEntities())
            {
                db.LANCAMENTO.AddObject(l);
                db.SaveChanges();
    }
    }
}
}