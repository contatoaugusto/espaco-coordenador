using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Dado;
using EC.Modelo;


namespace EC.Negocio
{
    public class NReuniaoPauta
    {
        public static List<REUNIAO_PAUTA> Consultar()
        {
            return (new DReuniaoPauta()).Consultar();
        }

        public static REUNIAO_PAUTA ConsultarById(int idPauta)
        {
            return (new DReuniaoPauta()).ConsultarById(idPauta);
        }

        public static List<REUNIAO_PAUTA> ConsultarByReuniao(int idReuniao)
        {
            return (new DReuniaoPauta()).ConsultarByReuniao(idReuniao);
        }

        public static void Salvar(REUNIAO_PAUTA r)
        {
            (new DReuniaoPauta()).Salvar(r);
        }
        public static void Salvar(List<REUNIAO_PAUTA> r)
        {
            (new DReuniaoPauta()).Salvar(r);
        }

        public static void Salvar(List<REUNIAO_PAUTA> r, int idReuniao)
        {
            (new DReuniaoPauta()).Salvar(r, idReuniao);
        }

        public static void ExcluiPauta(int idPauta)
        {
            (new DReuniaoPauta()).ExcluiPauta(idPauta);
        }
    }
}
 