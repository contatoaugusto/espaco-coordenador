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
    
    public partial class AMC
    {
        public AMC()
        {
            this.ACAO_AMC = new HashSet<ACAO_AMC>();
            this.ALUNO_AMC = new HashSet<ALUNO_AMC>();
            this.QUESTAO = new HashSet<QUESTAO>();
        }
    
        public int ID_AMC { get; set; }
        public Nullable<System.DateTime> DATA_APLICACAO { get; set; }
        public Nullable<int> ID_SEMESTRE { get; set; }
    
        public virtual ICollection<ACAO_AMC> ACAO_AMC { get; set; }
        public virtual ICollection<ALUNO_AMC> ALUNO_AMC { get; set; }
        public virtual SEMESTRE SEMESTRE { get; set; }
        public virtual ICollection<QUESTAO> QUESTAO { get; set; }
    }
}