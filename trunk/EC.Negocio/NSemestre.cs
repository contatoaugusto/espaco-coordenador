using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EC.Modelo;
using EC.Dado;

namespace EC.Negocio
{
    public class NSemestre
    {
        public static List<SEMESTRE> Consultar()
        {
            return (new DSemestre()).Consultar();
        }

        public static SEMESTRE ConsultarById(int id)
        {
            return (new DSemestre()).ConsultarById(id);
        }
        
        public static SEMESTRE ConsultarAtivo()
        {
            return (new DSemestre()).ConsultarAtivo();
        }

        public static void Salvar(SEMESTRE se)
        {
            (new DSemestre()).Salvar(se);
        }

        public static void Atualizar(SEMESTRE se)
        {
            (new DSemestre()).Atualizar(se);
        } 
    }
}
