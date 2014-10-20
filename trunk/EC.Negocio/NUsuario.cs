using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EC.Modelo;
using EC.Dado;

namespace EC.Negocio
{
    public class NUsuario
    {

        public static List<USUARIO> ConsultarUsuario()
        {
            return (new DUsuario()).ConsultarUsuario();
        }

        public static USUARIO ConsultarById(int id)
        {
            return (new DUsuario()).ConsultarById(id);
        }

        public static USUARIO ConsultarUsuarioByLoging(int matricula, bool icAtivo)
        {
            return (new DUsuario()).ConsultarUsuarioByLoging(matricula, icAtivo);
        }

        public static void Salvar(USUARIO u)
        {
            if (u != null)
            {
                DUsuario dusuari = new DUsuario();
                dusuari.Salvar(u);
            }
        }

        public static void Atualiza(USUARIO u)
        {
            if (u != null)
            {
                DUsuario dusuari = new DUsuario();
                dusuari.Atualiza(u);
            }
        }
    }
}
