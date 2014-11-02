using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC.Dado;
using EC.Modelo;


namespace EC.Negocio
{
    public class NCargo
    {

        public static List<CARGO> Consultar()
        {
            return (new DCargo()).Consultar();
        }

        public static CARGO ConsultarById(int idCargo)
        {
            return (new DCargo()).ConsultarById(idCargo);
        }

        public static List<CARGO> ConsultarByPessoa(int idPessoa)
        {
            return (new DCargo()).ConsultarByPessoa(idPessoa);
        }
    }
}