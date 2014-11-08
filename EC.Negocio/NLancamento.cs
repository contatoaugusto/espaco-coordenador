using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Dado;
using EC.Modelo;

namespace EC.Negocio
{
    public class NLancamento
    {
        public static List<TIPO_LANCAMENTO> ConsultarTipolancamento()
        {
            return (new DLancamento()).ConsultarTipolancamento();
        }

        public static List<TURMA> ConsultarTurma()
        {
            return (new DTurma()).Consultar();
        }

        public static List<PESSOA> ConsultarPessoa()
        {
            return (new DPessoa()).Consultar();
        }
        public static List<LANCAMENTO> ConsultarLancamento()
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