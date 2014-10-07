using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Dado;
using EC.Modelo;
using EC.Negocio;



namespace EC.Negocio
{
    public class NReuniao
    {
        public static List<Modelo.TIPO_REUNIAO> ConsultarTipoReuniao()
        {
            return (new DReuniao()).ConsultarTipoReuniao();
        }

        public static List<Modelo.REUNIAO> ConsultarReuniao(REUNIAO reuniao)
        {
            return (new DReuniao()).ConsultarReuniao(reuniao);
        }

        public static List<Modelo.PESSOA> ConsultarPessoa()
        {
            return (new DEvento()).ConsultarPessoa();
        }

        public static List<Modelo.TIPO_ASSUNTO_TRATADO> ConsultarTipoAssunto()
        {
            return (new DReuniao()).ConsultarTipoAssunto();
        }

        public static void Salvar(REUNIAO r)
        {
            DReuniao dreuniao = new DReuniao();
            dreuniao.Salvar(r);
        }
    }
}
 