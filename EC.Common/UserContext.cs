
namespace EC.Common
{
    public class UserContext : IUserContext
    {
        public int idUsuario { get; set; }

        public bool icLogDados { get; set; }

        public int idItemMenuPerfil { get; set; }

        public string IPUsuario { get; set; }

        public string IPFWDUsuario { get; set; }

        public static IUserContext UserContextFactory()
        {
            return new UserContext
            {
                idUsuario = Session.idUsuario,
                //icLogDados = Session.icLogDados,
                //idItemMenuPerfil = Session.idItemMenuPerfil == null? 0 : Session.idItemMenuPerfil.ToInt32(),
                IPUsuario = Session.IPUsuario,
                IPFWDUsuario = Session.IPFWDUsuario,
            };
        }
    }
}
