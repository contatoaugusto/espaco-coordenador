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
            this.REUNIAO_ASSUNTO_TRATADO = new HashSet<REUNIAO_ASSUNTO_TRATADO>();
            this.REUNIAO_ATA = new HashSet<REUNIAO_ATA>();
            this.REUNIAO_COMPROMISSO = new HashSet<REUNIAO_COMPROMISSO>();
            this.REUNIAO_PARTICIPANTE = new HashSet<REUNIAO_PARTICIPANTE>();
            this.REUNIAO_PAUTA = new HashSet<REUNIAO_PAUTA>();
        }
    
        public int ID_REUNIAO { get; set; }
        public int ID_TIPOREUNIAO { get; set; }
        public string TITULO { get; set; }
        public string LOCAL { get; set; }
        public Nullable<System.DateTime> DATAHORA { get; set; }
        public Nullable<int> ID_SEMESTRE { get; set; }
        public Nullable<int> SEQUENCIA { get; set; }
    
        public virtual ICollection<ACAO_REUNIAO> ACAO_REUNIAO { get; set; }
        public virtual ICollection<REUNIAO_ASSUNTO_TRATADO> REUNIAO_ASSUNTO_TRATADO { get; set; }
        public virtual ICollection<REUNIAO_ATA> REUNIAO_ATA { get; set; }
        public virtual ICollection<REUNIAO_COMPROMISSO> REUNIAO_COMPROMISSO { get; set; }
        public virtual ICollection<REUNIAO_PARTICIPANTE> REUNIAO_PARTICIPANTE { get; set; }
        public virtual ICollection<REUNIAO_PAUTA> REUNIAO_PAUTA { get; set; }
        public virtual SEMESTRE SEMESTRE { get; set; }
        public virtual TIPO_REUNIAO TIPO_REUNIAO { get; set; }
    }
}