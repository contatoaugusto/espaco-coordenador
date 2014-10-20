//SGI

namespace UI.Web.EC.Coordenador
{
    public partial class Aluno_DadosPessoais : UI.Web.EC.Page
    {
        public Aluno_DadosPessoais()
            : base("Content") 
        {
        }

        public bool RedirectToCongresso { get; set; }
        public bool RedirectToMatricula { get; set; }

        public int idCongresso { get; set; }

        public int idCongressista { get; set; }

    }
}