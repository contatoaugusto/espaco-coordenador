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
    
    public partial class REUNIAO_ASSUNTO_TRATADO
    {
        public int ID_ASSTRAT { get; set; }
        public Nullable<int> ID_REUNIAO { get; set; }
        public string DESCRICAO { get; set; }
        public Nullable<int> ITEM { get; set; }
        public Nullable<int> ID_TIPOASSTRATADO { get; set; }
    
        public virtual REUNIAO REUNIAO { get; set; }
        public virtual TIPO_ASSUNTO_TRATADO TIPO_ASSUNTO_TRATADO { get; set; }
    }
}