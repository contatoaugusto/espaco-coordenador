//------------------------------------------------------------------------------
// <auto-generated>
//    O código foi gerado a partir de um modelo.
//
//    Alterações manuais neste arquivo podem provocar comportamento inesperado no aplicativo.
//    Alterações manuais neste arquivo serão substituídas se o código for gerado novamente.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EC.Modelo
{
    using System;
    using System.Collections.Generic;
    
    public partial class CURSO
    {
        public CURSO()
        {
            this.CURSO_COORDENADOR = new HashSet<CURSO_COORDENADOR>();
            this.DISCIPLINA = new HashSet<DISCIPLINA>();
        }
    
        public int ID_CURSO { get; set; }
        public string DESCRICAO { get; set; }
    
        public virtual ICollection<CURSO_COORDENADOR> CURSO_COORDENADOR { get; set; }
        public virtual ICollection<DISCIPLINA> DISCIPLINA { get; set; }
    }
}
