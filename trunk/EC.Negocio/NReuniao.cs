using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Dado;
using EC.Modelo;
//using UI.Web.EC.Negocio;



namespace EC.Negocio
{
    public class NReuniao
    {
        public static List<TIPO_REUNIAO> ConsultarTipoReuniao()
        {
            return (new DReuniao()).ConsultarTipoReuniao();
        }

        public static List<REUNIAO> ConsultarReuniao(REUNIAO reuniao)
        {
            return (new DReuniao()).ConsultarReuniao(reuniao);
        }

        public static REUNIAO ConsultarById(int idReuniao)
        {
            return (new DReuniao()).ConsultarById(idReuniao);
        }

        public static List<REUNIAO_PARTICIPANTE> ConsultarParticipante(int idReuniao)
        {
            return (new DReuniao()).ConsultarParticipante(idReuniao);
        }


        public static List<TIPO_ASSUNTO_TRATADO> ConsultarTipoAssunto()
        {
            return (new DReuniao()).ConsultarTipoAssunto();
        }

        public static TIPO_ASSUNTO_TRATADO ConsultarTipoAssuntoById(int idTipoAssuntoTratado)
        {
            return (new DReuniao()).ConsultarTipoAssuntoById(idTipoAssuntoTratado);
        }

        public static void Salvar(REUNIAO r)
        {
            DReuniao dreuniao = new DReuniao();
            dreuniao.Salvar(r);
        }

        public static void Atualiza(REUNIAO q)
        {
            (new DReuniao()).Atualiza(q);
        }

        public static void Excluir(REUNIAO q)
        {
            (new DReuniao()).Excluir(q);
        }

        public static void ExcluiAssuntoTratado(int idAssuntoTratado)
        {
            (new DReuniao()).ExcluiAssuntoTratado(idAssuntoTratado);
        }
        public static void ExcluiCompromisso(int idCompromisso)
        {
            (new DReuniao()).ExcluiCompromisso(idCompromisso);
        }

        public static void ExcluiPauta(int idPauta)
        {
            (new DReuniao()).ExcluiPauta(idPauta);
        }

        public static void ExcluiParticipante(int idParticipante)
        {
            (new DReuniao()).ExcluiParticipante(idParticipante);
        }
    }
}
 