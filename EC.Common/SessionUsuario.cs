using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EC.Modelo;

namespace EC.Common
{
    public class SessionUsuario
    {

        private USUARIO USER;

        private int idSemestre;
               
        public USUARIO USUARIO
        {
            get { return USER; }
            set { USER = value; }
        }

        private int idCurso;
        private string nmCurso;

        public string NmCurso
        {
            get { return nmCurso; }
            set { nmCurso = value; }
        }

        public int IdCurso
        {
            get { return idCurso; }
            set { idCurso = value; }
        }

        public int IdSemestre
        {
            get { return idSemestre; }
            set { idSemestre = value; }
        }
    }
}
