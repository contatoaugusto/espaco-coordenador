using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EC.Modelo;
using EC.Dado;

namespace EC.Negocio
{
    public class NDisciplinaProfessor
    {
        public static List<DISCIPLINA_PROFESSOR> Consultar()
        {
            return (new DProfessor()).ConsultarDisciplinaProfessor();
        }
        public static List<CURSO> ConsultarCursoByProfessor(int idProfessor)
        {
            return (new DProfessor()).ConsultarCursoByProfessor(idProfessor);
        }
        
    }
}
