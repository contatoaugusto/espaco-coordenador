using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EC.Dado;
using EC.Modelo;

namespace EC.Negocio
{
    public class NDisciplina
    {
        public static List<DISCIPLINA> Consultar()
        {
            return (new DDisciplina()).Consultar();
        }

        public static List<DISCIPLINA> ConsultarByCurso(int idCurso)
        {
            return (new DDisciplina()).ConsultarByCurso(idCurso);
        }

        public static DISCIPLINA ConsultarById(int id)
        {
            return (new DDisciplina()).ConsultarById(id);

        }

        public static List<FUNCIONARIO> ConsultarProfessorByDisciplina(int idDisciplina)
        {
            return (new DDisciplina()).ConsultarProfessorByDisciplina(idDisciplina);

        }

    }
}
