
namespace EC.Common
{
    public interface IUserContext
    {
        int idUsuario { get; set; }
        bool icLogDados { get; set; }
        int idItemMenuPerfil { get; set; }
        string IPUsuario { get; set; }
        string IPFWDUsuario { get; set; }
    }
}