using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Dado;
using EC.Modelo;

namespace EC.Negocio
{
    public class NEvento
    {
      public static List<TIPO_EVENTO> ConsultarTipoEvento()
        {
            return (new DEvento()).ConsultarTipoEvento();
        }

      public static List<PESSOA> ConsultarPessoa()
      {
          return (new DPessoa()).Consultar();
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

        public static void Atualiza(EVENTO e)
        {
            (new DEvento()).Atualiza(e);
        }
        }
    }
