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
    
    public partial class CURSO_COORDENADOR
    {
        public int ID_CURSO_COORDENADOR { get; set; }
        public int ID_FUNCIONARIO { get; set; }
        public int ID_CURSO { get; set; }
    
        public virtual CURSO CURSO { get; set; }
        public virtual FUNCIONARIO FUNCIONARIO { get; set; }
    }
}