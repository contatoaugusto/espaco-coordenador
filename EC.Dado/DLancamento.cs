﻿using System;
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


                    lancamento.FUNCIONARIO = db.FUNCIONARIO.First(rs => rs.ID_FUNCIONARIO == tipo.FUNCIONARIO.ID_FUNCIONARIO);
                    lancamento.DISCIPLINA = db.DISCIPLINA.First(rs => rs.ID_DISCIPLINA == tipo.DISCIPLINA.ID_DISCIPLINA);

                    //questao.FUNCIONARIO = new FUNCIONARIO();
                    //questao.FUNCIONARIO.PESSOA = new PESSOA();
                    //questao.FUNCIONARIO.PESSOA.ID_PESSOA = tipo.FUNCIONARIO.PESSOA.ID_PESSOA;
                    //questao.FUNCIONARIO.PESSOA.NOME = tipo.FUNCIONARIO.PESSOA.NOME;
                    //questao.FUNCIONARIO.PESSOA.TELEFONE = tipo.FUNCIONARIO.PESSOA.TELEFONE;
                    //questao.FUNCIONARIO.PESSOA.EMAIL = tipo.FUNCIONARIO.PESSOA.EMAIL;

                    //questao.DISCIPLINA = new DISCIPLINA();
                    //questao.DISCIPLINA.ID_DISCIPLINA = tipo.DISCIPLINA.ID_DISCIPLINA;
                    //questao.DISCIPLINA.ID_CURSO = tipo.DISCIPLINA.ID_CURSO;
                    //questao.DISCIPLINA.DESCRICAO = tipo.DISCIPLINA.DESCRICAO;


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