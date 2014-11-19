using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Modelo
{
    public class RelatorioVagas
    {       
        public DISCIPLINA Disciplina { get; set; }
        public List<TURMA> Turma { get; set; }
        public int QuantidadeVagas { get; set; }
        public int QuantidadeAluno { get; set; }
        public int Saldo { get; set; }
        public int Percentual { get; set; }
    }
}
