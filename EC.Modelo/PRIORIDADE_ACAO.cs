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
    
    public partial class PRIORIDADE_ACAO
    {
        public PRIORIDADE_ACAO()
        {
            this.ACAO = new HashSet<ACAO>();
        }
    
        public int ID_PRIORIDADE { get; set; }
        public string DESCRICAO { get; set; }
    
        public virtual ICollection<ACAO> ACAO { get; set; }
    }
}