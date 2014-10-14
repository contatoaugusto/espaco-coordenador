using System.Web;
using System.Web.SessionState;

namespace EC.Common
{
    public sealed class Url
    {
        #region Syncronization Properties
        public const string SESSION_NAME = "mainThreadState";
        private static HttpContext _contexto;
        public static HttpContext Contexto
        {
            get
            {
                return _contexto;
            }
            set
            {
                _contexto = value;
            }
        }
        private static HttpSessionState Sessao { get; set; }

        #endregion

        public static readonly string Key = ")*(&¨%4!@";

        /// <summary>
        /// Verifica se tem parâmetros existentes na url
        /// </summary>
        public static bool HasParams
        {
            get { return GetUrlParams().Length > 0; }
        }

        //--->> SGI.Common
        public static string CreateUrlParams(params object[] urlParam)
        {

            string query = string.Empty;

            foreach (object param in urlParam)
                query += param.ToString() + "$";

            // Verificação se o Contexto ou a Sessão são nulas
            if (HttpContext.Current == null)
            {
                Sessao = Contexto.Application[SESSION_NAME] as HttpSessionState;
                if (HttpContext.Current.Session != null)
                    query += HttpContext.Current.Session.SessionID;
                else
                {
                    if (!string.IsNullOrEmpty(query))
                        query = query.Substring(0, query.Length - 1);
                }

                return "?" + HttpUtility.UrlEncode(new SGI.Safe.Cryptography(Url.Key).EncryptData(query));
            }
            else if (HttpContext.Current.Session == null)
            {
                Sessao = Contexto.Application[SESSION_NAME] as HttpSessionState;
                if (HttpContext.Current.Session != null)
                    query += HttpContext.Current.Session.SessionID;
                else
                {
                    if (!string.IsNullOrEmpty(query))
                        query = query.Substring(0, query.Length - 1);
                }

                return "?" + HttpUtility.UrlEncode(new SGI.Safe.Cryptography(Url.Key).EncryptData(query));
            }

            // Verificação de Sessão
            if (HttpContext.Current.Session != null)
                query += HttpContext.Current.Session.SessionID;
            else
            {
                if (!string.IsNullOrEmpty(query))
                    query = query.Substring(0, query.Length - 1);
            }

            return "?" + HttpUtility.UrlEncode(new SGI.Safe.Cryptography(Url.Key).EncryptData(query));
        }

        /// <summary>
        /// Retorna todos os parâmetros passados pela url
        /// </summary>
        /// <returns></returns>
        public static string[] GetUrlParams()
        {
            string query = HttpContext.Current.Request.Url.Query;
            string[] prs = new string[0];

            try
            {
                if (!string.IsNullOrEmpty(query))
                {
                    query = query.Replace("?", "");
                    prs = new SGI.Safe.Cryptography(Url.Key).DecryptData(HttpUtility.UrlDecode(query)).Split('$');
                }
                return prs;
            }
            catch
            {
                return new string[0];
            }
        }

        public static string GetSessionID()
        {
            string[] prs = GetUrlParams();
            return prs[prs.Length - 1];
        }

        public static bool IsValidQueryStringBySessionID(string[] prs)
        {
            if(prs == null || prs.Length == 0)
                return false;

            return prs[prs.Length -1] == HttpContext.Current.Session.SessionID;
        }
    }
}
