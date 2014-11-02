using System;
using System.Data;
using System.Web;
using System.Web.SessionState;
using EC.Common;
using EC.Modelo;
using EC.Negocio;

namespace EC.Common
{
    public sealed class ControleAcesso
    {

        public static bool verificaAcesso(int idCargo)
        {
            
            string []itemsPerfilProfessor = {""};
            string[] itemsPerfilCoordenador = { "" };
            string[] itemsPerfilDiretor = { "" };

            return true;
        }

        public static string verificaAcesso(int []idCargoComAcesso)
        {
            HttpSessionState sessaoServidor = HttpContext.Current.Session;
            SessionUsuario sessionUsuario = ((SessionUsuario)sessaoServidor[Const.USUARIO]);
            
            string retorno = "Sem acesso ao recurso selecionado!";

            foreach (var i in idCargoComAcesso)
            {
                if (i == sessionUsuario.USUARIO.FUNCIONARIO.ID_CARGO)
                {
                    retorno = "";
                    break;
                }
            }
            
            return retorno;
        }
        

    }
}
