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
    
    public partial class DISCIPLINA
    {
        public DISCIPLINA()
        {
            this.DISCIPLINA_PROFESSOR = new HashSet<DISCIPLINA_PROFESSOR>();
            this.LANCAMENTO = new HashSet<LANCAMENTO>();
            this.QUESTAO = new HashSet<QUESTAO>();
        }
    
        public int ID_DISCIPLINA { get; set; }
        public Nullable<int> ID_CURSO { get; set; }
        public string DESCRICAO { get; set; }
        public Nullable<bool> ATIVO { get; set; }
    
        public virtual CURSO CURSO { get; set; }
        public virtual ICollection<DISCIPLINA_PROFESSOR> DISCIPLINA_PROFESSOR { get; set; }
        public virtual ICollection<LANCAMENTO> LANCAMENTO { get; set; }
        public virtual ICollection<QUESTAO> QUESTAO { get; set; }
    }
}
