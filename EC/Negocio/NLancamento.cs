using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Dado;
using EC.Modelo;
using EC.Negocio;


namespace EC.Negocio
{
    public class NLancamento
    {
        public static List<Modelo.TIPO_LANCAMENTO> ConsultarTipolancamento()
        {
            return (new DLancamento()).ConsultarTipolancamento();
        }

        public static List<Modelo.TURMA> ConsultarTurma()
        {
            return (new DLancamento()).ConsultarTurma();
        }

        public static List<Modelo.PESSOA> ConsultarPessoa()
        {
            return (new DEvento()).ConsultarPessoa();
        }
        public static List<Modelo.LANCAMENTO> ConsultarLancamento()
        {
            return (new DLancamento()).ConsultarLancamento();
        }

        //public static List<Modelo.TIPO_TURMA> ConsultarTipoTurma()
        //{
        //    return (new DLancamento()).ConsultarTipoTurma();
        //}

        public static void Salvar(LANCAMENTO l)
        {
            DLancamento dlancamento = new DLancamento();
            dlancamento.Salvar(l);
        }
    }

}