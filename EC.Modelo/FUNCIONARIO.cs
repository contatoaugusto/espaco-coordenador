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
    
    public partial class FUNCIONARIO
    {
        public FUNCIONARIO()
        {
            this.CURSO_COORDENADOR = new HashSet<CURSO_COORDENADOR>();
            this.DISCIPLINA_PROFESSOR = new HashSet<DISCIPLINA_PROFESSOR>();
            this.LANCAMENTO = new HashSet<LANCAMENTO>();
            this.PROVA = new HashSet<PROVA>();
            this.QUESTAO = new HashSet<QUESTAO>();
            this.USUARIO = new HashSet<USUARIO>();
        }
    
        public int ID_FUNCIONARIO { get; set; }
        public Nullable<int> MATRICULA { get; set; }
        public Nullable<int> ID_CARGO { get; set; }
        public Nullable<int> ID_PESSOA { get; set; }
        public Nullable<bool> ATIVO { get; set; }
    
        public virtual CARGO CARGO { get; set; }
        public virtual ICollection<CURSO_COORDENADOR> CURSO_COORDENADOR { get; set; }
        public virtual ICollection<DISCIPLINA_PROFESSOR> DISCIPLINA_PROFESSOR { get; set; }
        public virtual PESSOA PESSOA { get; set; }
        public virtual ICollection<LANCAMENTO> LANCAMENTO { get; set; }
        public virtual ICollection<PROVA> PROVA { get; set; }
        public virtual ICollection<QUESTAO> QUESTAO { get; set; }
        public virtual ICollection<USUARIO> USUARIO { get; set; }
    }
}
