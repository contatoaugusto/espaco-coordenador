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
    
    public partial class FUNCIONARIO
    {
        public FUNCIONARIO()
        {
            this.CURSO_COORDENADOR = new HashSet<CURSO_COORDENADOR>();
            this.DISCIPLINA_PROFESSOR = new HashSet<DISCIPLINA_PROFESSOR>();
            this.REUNIAO_ATA = new HashSet<REUNIAO_ATA>();
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
        public virtual ICollection<REUNIAO_ATA> REUNIAO_ATA { get; set; }
        public virtual ICollection<LANCAMENTO> LANCAMENTO { get; set; }
        public virtual ICollection<PROVA> PROVA { get; set; }
        public virtual ICollection<QUESTAO> QUESTAO { get; set; }
        public virtual ICollection<USUARIO> USUARIO { get; set; }
    }
}
