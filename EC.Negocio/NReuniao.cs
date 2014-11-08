﻿using System;
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

        public static void Salvar(REUNIAO r)
        {
            DReuniao dreuniao = new DReuniao();
            dreuniao.Salvar(r);
        }

        public static void Atualiza(REUNIAO q)
        {
            (new DReuniao()).Atualiza(q);
        }
    }
}
 