using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EC.Modelo;
using EC.Dado;

namespace EC.Negocio
{
    public class NProva
    {

        public static bool GerarProvaRandomicamente(int idAmc, int idCurso, int qtdeQuestoes, int idFuncionario)
        {
            return (new DProva()).GerarProvaRandomicamente(idAmc, idCurso, qtdeQuestoes, idFuncionario);
        }

        public static List<PROVA> Consultar()
        {
            return (new DProva()).Consultar();
        }

        //public static List<PROVA> ConsultarBySemestre(int idSemestre)
        //{
        //    return (new DProva()).ConsultarBySemestre(idSemestre);
        //}

        public static void Salvar(PROVA obj)
        {
            DProva prova = new DProva();
            prova.Salvar(obj);
        }

    }
}
