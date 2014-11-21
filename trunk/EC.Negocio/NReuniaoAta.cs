using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Dado;
using EC.Modelo;

namespace EC.Negocio
{
    public class NReuniaoAta
    {
        public static List<REUNIAO_ATA> Consulta()
        {
            return (new DReuniaoAta()).Consulta();
        }


        public static REUNIAO_ATA ConsultarById(int idReuniaoAta)
        {
            return (new DReuniaoAta()).ConsultarById(idReuniaoAta);
        }

        public static REUNIAO_ATA ConsultarByReuniao(int idReuniao)
        {
            return (new DReuniaoAta()).ConsultarByReuniao(idReuniao);
        }

        public static void Salvar(REUNIAO_ATA r)
        {
            (new DReuniaoAta()).Salvar(r);
        }

        public static void Atualiza(REUNIAO_ATA r)
        {
            (new DReuniaoAta()).Atualiza(r);
        }
    }
}
 