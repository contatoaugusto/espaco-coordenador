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
    
    public partial class PROVA
    {
        public PROVA()
        {
            this.QUESTAO = new HashSet<QUESTAO>();
        }
    
        public int ID_PROVA { get; set; }
        public System.DateTime DATA_CRIACAO { get; set; }
        public int ID_FUNCIONARIO_CADATRO { get; set; }
        public Nullable<System.DateTime> DATA_RESULTADO { get; set; }
        public string OBSERVACAO { get; set; }
        public Nullable<int> QTDE_QUESTOES { get; set; }
    
        public virtual FUNCIONARIO FUNCIONARIO { get; set; }
        public virtual ICollection<QUESTAO> QUESTAO { get; set; }
    }
}
