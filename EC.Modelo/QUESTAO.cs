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
    
    public partial class QUESTAO
    {
        public QUESTAO()
        {
            this.RESPOSTA = new HashSet<RESPOSTA>();
        }
    
        public int ID_QUESTAO { get; set; }
        public Nullable<int> ID_DISCIPLINA { get; set; }
        public Nullable<int> ID_AMC { get; set; }
        public Nullable<int> ID_FUNCIONARIO { get; set; }
        public string DESCRICAO { get; set; }
        public byte[] IMAGEM { get; set; }
        public Nullable<int> ID_PROVA { get; set; }
    
        public virtual AMC AMC { get; set; }
        public virtual DISCIPLINA DISCIPLINA { get; set; }
        public virtual FUNCIONARIO FUNCIONARIO { get; set; }
        public virtual PROVA PROVA { get; set; }
        public virtual ICollection<RESPOSTA> RESPOSTA { get; set; }
    }
}
