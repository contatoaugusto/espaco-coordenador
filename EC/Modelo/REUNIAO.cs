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
    
    public partial class REUNIAO
    {
        public REUNIAO()
        {
            this.ACAO_REUNIAO = new HashSet<ACAO_REUNIAO>();
            this.ASSUNTO_TRATADO = new HashSet<ASSUNTO_TRATADO>();
            this.COMPROMISSO = new HashSet<COMPROMISSO>();
            this.PAUTA_REUNIAO = new HashSet<PAUTA_REUNIAO>();
            this.PARTICIPANTE = new HashSet<PARTICIPANTE>();
        }
    
        public int ID_REUNIAO { get; set; }
        public int SEMESTRE { get; set; }
        public int ID_TIPOREUNIAO { get; set; }
        public string LOCAL { get; set; }
        public Nullable<System.DateTime> DATAHORA { get; set; }
        public string TITULO { get; set; }
    
        public virtual ICollection<ACAO_REUNIAO> ACAO_REUNIAO { get; set; }
        public virtual ICollection<ASSUNTO_TRATADO> ASSUNTO_TRATADO { get; set; }
        public virtual ICollection<COMPROMISSO> COMPROMISSO { get; set; }
        public virtual ICollection<PAUTA_REUNIAO> PAUTA_REUNIAO { get; set; }
        public virtual ICollection<PARTICIPANTE> PARTICIPANTE { get; set; }
        public virtual TIPO_REUNIAO TIPO_REUNIAO { get; set; }
    }
}
