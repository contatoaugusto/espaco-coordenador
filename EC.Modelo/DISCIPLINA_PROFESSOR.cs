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
    
    public partial class DISCIPLINA_PROFESSOR
    {
        public int ID_DISCIPLINA_PROFESSOR { get; set; }
        public int ID_DISCIPLINA { get; set; }
        public int ID_FUNCIONARIO { get; set; }
    
        public virtual DISCIPLINA DISCIPLINA { get; set; }
        public virtual FUNCIONARIO FUNCIONARIO { get; set; }
    }
}