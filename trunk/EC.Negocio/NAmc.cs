﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Dado;
using EC.Modelo;

namespace EC.Negocio
{
    public class NAmc
    {
         public static List<AMC> ConsultarAmc()
        {
            return (new DQuestao()).ConsultarAmc();
        }

         public static bool Salvar(AMC obj)
         {
             bool retorno = true;
             DAmc dObj = new DAmc();

             if (dObj.ConsultarAmc(obj).Count > 0)
                 return false;
             
             dObj.Salvar(obj);

             return retorno;
         }
    }
}