using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Dado;
using EC.Modelo;
using EC.Negocio;


namespace EC.Negocio
{
    public class NEvento
    {
      public static List<Modelo.TIPO_EVENTO> ConsultarTipoEvento()
        {
            return (new DEvento()).ConsultarTipoEvento();
        }

      public static List<Modelo.PESSOA> ConsultarPessoa()
      {
          return (new DEvento()).ConsultarPessoa();
      }

      public static List<Modelo.EVENTO> ConsultarEvento(EVENTO evento)
      {
          return (new DEvento()).ConsultarEvento(evento);
      }

        public static void Salvar(EVENTO e)
        {
            DEvento devento= new DEvento();
            devento.Salvar(e);
        }

        }
    }
