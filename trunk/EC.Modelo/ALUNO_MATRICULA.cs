//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EC.Modelo
{
    using System;
    using System.Collections.Generic;
    
    public partial class ALUNO_MATRICULA
    {
        public ALUNO_MATRICULA()
        {
            this.ALUNO_AMC = new HashSet<ALUNO_AMC>();
            this.REPRESENTANTE_TURMA = new HashSet<REPRESENTANTE_TURMA>();
        }
    
        public int ID_ALUNO_MATRICULA { get; set; }
        public int ID_ALUNO { get; set; }
        public Nullable<int> ID_TURMA { get; set; }
        public Nullable<System.DateTime> DATA_MATRICULA { get; set; }
        public Nullable<bool> PRESENCA { get; set; }
    
        public virtual ALUNO ALUNO { get; set; }
        public virtual ICollection<ALUNO_AMC> ALUNO_AMC { get; set; }
        public virtual TURMA TURMA { get; set; }
        public virtual ICollection<REPRESENTANTE_TURMA> REPRESENTANTE_TURMA { get; set; }
    }
}