using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Dado;
using EC.Modelo;


namespace EC.Negocio
{
    public class NReuniaoParticipante
    {
        public static List<REUNIAO_PARTICIPANTE> Consultar()
        {
            return (new DReuniaoParticipante()).Consultar();
        }

        public static REUNIAO_PARTICIPANTE ConsultarById(int id)
        {
            return (new DReuniaoParticipante()).ConsultarById(id);
        }

        public static List<REUNIAO_PARTICIPANTE> ConsultarByReuniao(int idReuniao)
        {
            return (new DReuniaoParticipante()).ConsultarByReuniao(idReuniao);
        }

        public static void Exclui(int idParticipante)
        {
            (new DReuniaoParticipante()).ExcluiParticipante(idParticipante);
        }

        public static void Salvar(REUNIAO_PARTICIPANTE r)
        {
            (new DReuniaoParticipante()).Salvar(r);
        }
        public static void Salvar(List<REUNIAO_PARTICIPANTE> r)
        {
            (new DReuniaoParticipante()).Salvar(r);
        }

        public static void Salvar(List<REUNIAO_PARTICIPANTE> r, int idReuniao)
        {
            (new DReuniaoParticipante()).Salvar(r, idReuniao);
        }
    }
}
 