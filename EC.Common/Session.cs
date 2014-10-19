using System;
using System.Web;
using System.Web.SessionState;
//using EC.Modelo;

namespace EC.Common
{
    public class Session 
    {
        #region Syncronization Properties
        public const string SESSION_NAME = "mainThreadState";
        public const string REQUEST_NAME = "RequestThreadState";

        public static HttpContext Context { get; set; }

        private static HttpSessionState SessionState { get; set; }

        #endregion

        #region Generic Util Methods
        private static T GetSessionData<T>(string sessionDataKeyName)
        {
            T result;

            if (HttpContext.Current == null)
            {
                SessionState = Context.Application[SESSION_NAME] as HttpSessionState;
                if (typeof(T) == typeof(int))
                {
                    result = (T)(Library.ToInteger(SessionState[sessionDataKeyName]) as object);
                    return result;
                }
                else if (typeof(T) == typeof(long))
                {
                    result = (T)(Library.ToLong(SessionState[sessionDataKeyName]) as object);
                    return result;
                }
                else if (typeof(T) == typeof(bool))
                {
                    result = (T)(Library.ToBoolean(SessionState[sessionDataKeyName]) as object);
                    return result;
                }
                else if (typeof(T) == typeof(Parameters))
                {
                    result = (T)(SessionState[sessionDataKeyName] as object);
                    if (result == null)
                    {
                        SessionState[sessionDataKeyName] = new EC.Common.Parameters();
                        result = (T)(SessionState[sessionDataKeyName] as object);
                    }
                    return result;
                }
                else if (typeof(T) == typeof(RequestPage))
                {
                    result = (T)(SessionState[sessionDataKeyName] as object);
                    if (result == null)
                    {
                        SessionState[sessionDataKeyName] = new EC.Common.RequestPage();
                        result = (T)(SessionState[sessionDataKeyName] as object);
                    }
                    return result;
                }
                else if (typeof(T) == typeof(string[]))
                {
                    result = (T)((SessionState[sessionDataKeyName] as string[]) as object);
                    return result;
                }
                result = (T)(Library.ToString(SessionState[sessionDataKeyName]) as object);
                return result;
            }
            else if (HttpContext.Current.Session == null)
            {
                SessionState = Context.Application[SESSION_NAME] as HttpSessionState;
                if (typeof(T) == typeof(int))
                {
                    result = (T)(Library.ToInteger(SessionState[sessionDataKeyName]) as object);
                    return result;
                }
                else if (typeof(T) == typeof(long))
                {
                    result = (T)(Library.ToLong(SessionState[sessionDataKeyName]) as object);
                    return result;
                }
                else if (typeof(T) == typeof(bool))
                {
                    result = (T)(Library.ToBoolean(SessionState[sessionDataKeyName]) as object);
                    return result;
                }
                else if (typeof(T) == typeof(Parameters))
                {
                    result = (T)(SessionState[sessionDataKeyName] as object);
                    if (result == null)
                    {
                        SessionState[sessionDataKeyName] = new EC.Common.Parameters();
                        result = (T)(SessionState[sessionDataKeyName] as object);
                    }
                    return result;
                }
                else if (typeof(T) == typeof(RequestPage))
                {
                    result = (T)(SessionState[sessionDataKeyName] as object);
                    if (result == null)
                    {
                        SessionState[sessionDataKeyName] = new EC.Common.RequestPage();
                        result = (T)(SessionState[sessionDataKeyName] as object);
                    }
                    return result;
                }
                else if (typeof(T) == typeof(string[]))
                {
                    result = (T)((SessionState[sessionDataKeyName] as string[]) as object);
                    return result;
                }
                result = (T)(Library.ToString(SessionState[sessionDataKeyName]) as object);
                return result;
            }
            else
            {
                if (typeof(T) == typeof(int))
                {
                    result = (T)(Library.ToInteger(HttpContext.Current.Session[sessionDataKeyName]) as object);
                    return result;
                }
                else if (typeof(T) == typeof(long))
                {
                    result = (T)(Library.ToLong(HttpContext.Current.Session[sessionDataKeyName]) as object);
                    return result;
                }
                else if (typeof(T) == typeof(bool))
                {
                    result = (T)(Library.ToBoolean(HttpContext.Current.Session[sessionDataKeyName]) as object);
                    return result;
                }
                else if (typeof(T) == typeof(Parameters))
                {
                    result = (T)(HttpContext.Current.Session[sessionDataKeyName] as object);
                    if (result == null)
                    {
                        HttpContext.Current.Session[sessionDataKeyName] = new EC.Common.Parameters();
                        result = (T)(HttpContext.Current.Session[sessionDataKeyName] as object);
                    }
                    return result;
                }
                else if (typeof(T) == typeof(RequestPage))
                {
                    result = (T)(HttpContext.Current.Session[sessionDataKeyName] as object);
                    if (result == null)
                    {
                        HttpContext.Current.Session[sessionDataKeyName] = new EC.Common.RequestPage();
                        result = (T)(HttpContext.Current.Session[sessionDataKeyName] as object);
                    }
                    return result;
                }
                else if (typeof(T) == typeof(RequestPageCollection))
                {
                    result = (T)(HttpContext.Current.Session[sessionDataKeyName] as object);
                    if (result == null)
                    {
                        HttpContext.Current.Session[sessionDataKeyName] = new EC.Common.RequestPageCollection();
                        result = (T)(HttpContext.Current.Session[sessionDataKeyName] as object);
                    }
                    return result;
                }
                else if (typeof(T) == typeof(string[]))
                {
                    result = (T)((HttpContext.Current.Session[sessionDataKeyName] as string[]) as object);
                    return result;
                }
                result = (T)(Library.ToString(HttpContext.Current.Session[sessionDataKeyName]) as object);
                return result;
            }
        }
        #endregion
        public static long idPessoa
        {
            get
            {
                return GetSessionData<long>("ID_PESSOA");
            }
            set
            {
                HttpContext.Current.Session["ID_PESSOA"] = value;
            }
        }

        //public static USUARIO usuario
        //{
        //    get
        //    {
        //        return GetSessionData<USUARIO>("USUARIO");
        //    }
        //    set
        //    {
        //        HttpContext.Current.Session["USUARIO"] = value;
        //    }
        //}

        public static int idCurso
        {
            get
            {
                try
                {
                    return GetSessionData<int>("ID_CURSO");
                }
                catch
                {
                    return 0;
                }
            }
            set { HttpContext.Current.Session["ID_CURSO"] = value; }
        }

        public static string nmCurso
        {
            get
            {
                try
                {
                    return GetSessionData<string>("NMCURSO");
                }
                catch
                {
                    return "";
                }
            }
            set { HttpContext.Current.Session["NMCURSO"] = value; }
        }


        public static string IPUsuario
        {
            get 
            {
                var context = HttpContext.Current;
                HttpRequest request;

                if (context == null)
                {
                    request = Context.Application[REQUEST_NAME] as HttpRequest;
                }
                else
                {
                    request = context.Request;
                }

                return Library.ToString(request.ServerVariables["REMOTE_ADDR"]); 
            }
        }
        public static string IPFWDUsuario
        {
            get
            {
                var context = HttpContext.Current;
                HttpRequest request;

                if (context == null)
                {
                    request = Context.Application[REQUEST_NAME] as HttpRequest;
                }
                else
                {
                    request = context.Request;
                }

                return Library.ToString(request.ServerVariables["HTTP_X_FORWARDED_FOR"]);
            }
        }
        //public static String idNivelAcessoItemMenu
        //{
        //    get
        //    {
        //        return GetSessionData<String>("idNivelAcessoItemMenu:Cache");
        //    }
        //    set
        //    {
        //        HttpContext.Current.Session["idNivelAcessoItemMenu:Cache"] = value;
        //    }
        //}
        //public static String idNivelFuncaoSistema
        //{
        //    get
        //    {
        //        return GetSessionData<String>(Const.IDNIVELFUNCAOSISTEMA);
        //    }
        //    set
        //    {
        //        HttpContext.Current.Session[Const.IDNIVELFUNCAOSISTEMA] = value;
        //    }
        //}
        //public static String idItemMenuPerfil
        //{
        //    get
        //    {
        //        return GetSessionData<String>(Const.IDITEMMENUPERFIL);
        //    }
        //    set
        //    {
        //        HttpContext.Current.Session[Const.IDITEMMENUPERFIL] = value;
        //    }
        //}
        //public static String idPerfilFuncaoSistema
        //{
        //    get
        //    {

        //        return GetSessionData<String>(Const.IDPERFILFUNCAOSISTEMA_CACHE);
        //    }
        //    set
        //    {
        //        HttpContext.Current.Session[Const.IDPERFILFUNCAOSISTEMA_CACHE] = value;
        //    }
        //}
        //public static String idItemMenu
        //{
        //    get
        //    {
        //        return GetSessionData<String>(Const.IDITEMMENU_CACHE);
        //    }
        //    set
        //    {
        //        HttpContext.Current.Session[Const.IDITEMMENU_CACHE] = value;
        //    }
        //}
        public static int idUsuario
        {
            get
            {
                try
                {
                    return GetSessionData<int>("ID_USUARIO");
                }
                catch
                {
                    return 0;
                }
            }
            set { HttpContext.Current.Session["ID_USUARIO"] = value; }
        }
        //public static int idSistema
        //{
        //    get
        //    {
        //        return GetSessionData<int>(Const.IDSISTEMA);
        //    }
        //    set
        //    {
        //        HttpContext.Current.Session[Const.IDSISTEMA] = value;
        //    }
        //}
        //public static int idPerfil
        //{
        //    get
        //    {
        //        return GetSessionData<int>(Const.IDPERFIL);
        //    }
        //    set
        //    {
        //        HttpContext.Current.Session[Const.IDPERFIL] = value;
        //    }
        //}
        ////public static int idFuncionario
        ////{
        ////    get { return Library.ToInteger(HttpContext.Current.Session[Const.IDFUNCIONARIO_CACHE]); }
        ////    //set { HttpContext.Current.Session[Const.IDFUNCIONARIO_CACHE] = value; }
        ////}
        //public static int idFuncaoSistema
        //{
        //    get
        //    {
        //        return GetSessionData<int>(Const.IDFUNCAOSISTEMA);
        //    }
        //    set { HttpContext.Current.Session[Const.IDFUNCAOSISTEMA] = value; }
        //}
        //public static int idQuestionario
        //{
        //    get
        //    {
        //        return GetSessionData<int>(Const.IDQUESTIONARIO_CACHE);
        //    }
        //    set { HttpContext.Current.Session[Const.IDQUESTIONARIO_CACHE] = value; }
        //}
        public static EC.Common.Parameters Params
        {
            get
            {
                return GetSessionData<Parameters>("Parameters");
            }
            set
            {
                HttpContext.Current.Session["Parameters"] = value;
            }
        }
        
        public static string nmPessoa
        {
            get
            {
                return GetSessionData<string>("NMPESSOA");
            }
            set { HttpContext.Current.Session["NMPESSOA"] = value; }
        }
        
        //public static string edEmailProfissional
        //{
        //    get
        //    {
        //        return GetSessionData<string>("edEmailProfissional:Cache");
        //    }
        //    set { HttpContext.Current.Session["edEmailProfissional:Cache"] = value; }
        //}
        //public static string edEmailPessoal
        //{
        //    get
        //    {
        //        return GetSessionData<string>("edEmailPessoal:Cache");
        //    }
        //    set { HttpContext.Current.Session["edEmailPessoal:Cache"] = value; }
        //}
        public static string nmUsuario
        {
            get
            {
                return GetSessionData<string>("nmUsuario");
            }
            set { HttpContext.Current.Session["nmUsuario"] = value; }
        }
        public static RequestPageCollection RequestPageCollection
        {
            get
            {
                return GetSessionData<RequestPageCollection>("RequestPage");
            }
            set
            {
                HttpContext.Current.Session["RequestPage"] = value;
            }
        }

        public static bool IsValid()
        {
            try
            {
                if (Session.idUsuario == 0) //|| Session.idAlunoMatricula == 0)// Utilizado somente para a Matrícula
                //if( Session.idUsuario == 0 )
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        //public static bool icLogDados
        //{
        //    get
        //    {
        //        try
        //        {
        //            return GetSessionData<bool>(Const.ICLOGDADOS_CACHE);
        //        }
        //        catch
        //        {
        //            return true;
        //        }
        //    }
        //    set { HttpContext.Current.Session[Const.ICLOGDADOS_CACHE] = value; }
        //}
        //public static bool icLogAcesso
        //{
        //    get
        //    {
        //        return GetSessionData<bool>(Const.ICLOGACESSO_CACHE);
        //    }
        //    set { HttpContext.Current.Session[Const.ICLOGACESSO_CACHE] = value; }
        //}
        public static string[] CollectionPageAccess
        {
            get
            {
                return GetSessionData<string[]>(Const.COLLECTIONPAGEACCESS_CACHE);
            }
            set { HttpContext.Current.Session[Const.COLLECTIONPAGEACCESS_CACHE] = value; }
        }
        public static String Template
        {
            get
            {
                return GetSessionData<String>("Template");
            }
            set { HttpContext.Current.Session["Template"] = value; }
        }
        //public static String coRelatorio
        //{
        //    get
        //    {
        //        return GetSessionData<String>(Const.CORELATORIO_CACHE);
        //    }
        //    set { HttpContext.Current.Session[Const.CORELATORIO_CACHE] = value; }
        //}
        //public static String coTipoFormulario
        //{
        //    get
        //    {
        //        return GetSessionData<String>(Const.COTIPOFORMULARIO_CACHE);
        //    }
        //    set { HttpContext.Current.Session[Const.COTIPOFORMULARIO_CACHE] = value; }
        //}
        //public static int nuNivelItemMenuPerfil
        //{
        //    get
        //    {
        //        return GetSessionData<int>(Const.NUNIVELITEMMENUPERFIL_CACHE);
        //    }
        //    set { HttpContext.Current.Session[Const.NUNIVELITEMMENUPERFIL_CACHE] = value; }
        //}
        public static int idFuncionario
        {
            get
            {
                return GetSessionData<int>("ID_FUNCIONARIO");
            }
            set { HttpContext.Current.Session["ID_FUNCIONARIO"] = value; }
        }
        public static bool icCoordenador
        {
            get { return Library.ToBoolean(HttpContext.Current.Session["ICCOORDENADOR"]); }
            set { HttpContext.Current.Session["ICCOORDENADOR"] = value; }
        }


        public static string coAcesso
        {
            get
            {
                return GetSessionData<string>(Const.COACESSO);
            }
            set { HttpContext.Current.Session[Const.COACESSO] = value; }
        }
        
    }

}