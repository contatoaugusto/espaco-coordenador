using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Dado;
using EC.Modelo;


namespace EC.Negocio
{
    public class NReuniaoCompromisso
    {
        public static List<REUNIAO_COMPROMISSO> Consultar()
        {
            return (new DReuniaoCompromissos()).Consultar();
        }

        public static REUNIAO_COMPROMISSO ConsultarById(int id)
        {
            return (new DReuniaoCompromissos()).ConsultarById(id);
        }

        public static List<REUNIAO_COMPROMISSO> ConsultarByReuniao(int idReuniao)
        {
            return (new DReuniaoCompromissos()).ConsultarByReuniao(idReuniao);
        }

        public static void Exclui(int idParticipante)
        {
            (new DReuniaoCompromissos()).ExcluiCompromisso(idParticipante);
        }

        public static void Salvar(REUNIAO_COMPROMISSO r)
        {
            (new DReuniaoCompromissos()).Salvar(r);
        }
        public static void Salvar(List<REUNIAO_COMPROMISSO> r)
        {
            (new DReuniaoCompromissos()).Salvar(r);
        }

    }
}
 