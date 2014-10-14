using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Dado;
using EC.Modelo;

namespace EC.Negocio
{
    public class NAcao
         {

        public static List<REUNIAO> ConsultarReuniao()
        {
            return (new DAcao()).ConsultarReuniao();
        }

        public static List<EVENTO> ConsultarEvento()
        {
            return (new DAcao()).ConsultarEvento();
        }
        public static List<AMC> ConsultarAmc()
        {
            return (new DAcao()).ConsultarAmc();
        }
        public static List<STATUS_ACAO> ConsultarStatus()
        {
            return (new DAcao()).ConsultarStatus();
        }

        public static List<PRIORIDADE_ACAO> ConsultarPrioridade()
        {
            return (new DAcao()).ConsultarPrioridade();
        }


        public static List<PESSOA> ConsultarPessoa()
        {
            return (new DAcao()).ConsultarPessoa();
        }

        public static List<ACAO> ConsultarAcao(ACAO acao)
        {
            return (new DAcao()).ConsultarAcao(acao);
        }

        public static void Salvar(ACAO a)
        {
            DAcao dacao = new DAcao();
            dacao.Salvar(a);
        }

        //public static List<Modelo.ACAO> ListarAcao(ACAO a)
        //{
        //    DAcao dacao = new DAcao();
        //    return dacao.ConsultarAcao(a);
        //}
  
    }
}