using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Dado;
using EC.Modelo;


namespace EC.Negocio
{
    public class NReuniaoAssuntoTratado
    {
        public static List<REUNIAO_ASSUNTO_TRATADO> Consultar()
        {
            return (new DReuniaoAssuntoTratado()).Consultar();
        }

        public static REUNIAO_ASSUNTO_TRATADO ConsultarById(int idAssuntoTratado)
        {
            return (new DReuniaoAssuntoTratado()).ConsultarById(idAssuntoTratado);
        }

        public static List<REUNIAO_ASSUNTO_TRATADO> ConsultarByReuniao(int idReuniao)
        {
            return (new DReuniaoAssuntoTratado()).ConsultarByReuniao(idReuniao);
        }

        public static void ExcluiAssuntoTratado(int idAssuntoTratado)
        {
            (new DReuniaoAssuntoTratado()).ExcluiAssuntoTratado(idAssuntoTratado);
        }

        public static void Salvar(REUNIAO_ASSUNTO_TRATADO r)
        {
            (new DReuniaoAssuntoTratado()).Salvar(r);
        }
        public static void Salvar(List<REUNIAO_ASSUNTO_TRATADO> r)
        {
            (new DReuniaoAssuntoTratado()).Salvar(r);
        }

    }
}
 