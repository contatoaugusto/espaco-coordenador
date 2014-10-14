using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EC.Modelo;
using EC.Dado;

namespace EC.Negocio
{
    public class NCursoCoordenador
    {
        public static List<CURSO_COORDENADOR> ConsultarCursoCoordenador()
        {
            return (new DCursoCoordenador()).ConsultarCursoCoordenador();
        }
        public static List<CURSO> ConsultarCursoByCoordenador(int idCoordenador)
        {
            return (new DCursoCoordenador()).ConsultarCursoByCoordenador(idCoordenador);
        }

        public static void Salvar(CURSO_COORDENADOR c)
        {
            DCursoCoordenador coordenador = new DCursoCoordenador();
            coordenador.Salvar(c);
        }
    }
}
