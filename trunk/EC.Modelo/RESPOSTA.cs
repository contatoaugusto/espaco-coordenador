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
    
    public partial class RESPOSTA
    {
        public int ID_RESPOSTA { get; set; }
        public Nullable<int> ID_QUESTAO { get; set; }
        public string TEXTO { get; set; }
        public Nullable<bool> RESPOSTA_CORRETA { get; set; }
    
        public virtual QUESTAO QUESTAO { get; set; }
    }
}
