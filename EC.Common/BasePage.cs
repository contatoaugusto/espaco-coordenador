using System;
using System.Configuration;
using System.Web;
using System.ComponentModel;
using EC.Common;
using System.Reflection;
using System.Collections.Generic;
using UniCEUB.Core.Log;

namespace EC.Common
{

    [Serializable()]
    public class BasePage : System.Web.UI.Page
    {
        #region -*- Variables -*-
        //-- variaveis boleanas
        private bool _ValidClient = true;
        private bool _openWindowScreenCenter = false;
        private bool _showError = true;
        private bool _showValidators = false;
        private bool _saveRequestPage = true;
        private bool _CacheClient = true;
        //-- variaveis string
        private string _UrlRequestPage = string.Empty;
        private string _NamePage = string.Empty;
        private string _IPServer = string.Empty;
        //-- variaveis object
        private RequestPageCollection _oReqPge;
        private Parameters _Params = new Parameters();
        #endregion

        #region -*- Constructors -*-
        public BasePage() { }
        #endregion

        #region -*- Properties -*-
        public string PathImages
        {
            get { return GetUrl() + "/Images/"; }
        }

        public string PathIncludes
        {
            get { return GetUrl() + "/Includes/"; }
        }

        [Description("Request page, order of pages stores last the 5."), Category("SGI")]
        public RequestPageCollection RequestPageCollection
        {
            get { return _oReqPge; }
            set { _oReqPge = value; }
        }

        [Description("It does not enable the cache in the machine client"), Category("SGI")]
        public bool CacheClient
        {
            get { return _CacheClient; }
            set { _CacheClient = value; }
        }

        [Description("PageMode."), Category("SGI")]
        public BasePageMode PageMode
        {
            get
            {
                object o = ViewState[Const.PAGEMODE_CACHE];
                if (o != null)
                    return (BasePageMode)o;
                return BasePageMode.None;
            }
            set { ViewState[Const.PAGEMODE_CACHE] = value; }
        }

        [Description("Alert Maintenance"), Category("SGI"), DefaultValue(true)]
        public bool AlertMaintenance
        {
            get
            {
                object o = ViewState["AlertMaintenance:Cache"];
                if (o != null)
                    return (bool)o;
                return true;
            }
            set { ViewState["AlertMaintenance:Cache"] = value; }
        }

        [Description("NameFileSerializable by User."), Category("SGI")]
        public string NameFileSerializable
        {
            get { return Common.Session.idUsuario.ToString() + ".dat"; }
        }

        [Description("Save request page"), Category("SGI"), DefaultValue(true)]
        public bool SaveRequestPage
        {
            get { return _saveRequestPage; }
            set { _saveRequestPage = value; }
        }

        [Description("Valid Client"), Category("SGI"), DefaultValue(true)]
        public bool ValidClient
        {
            get { return _ValidClient; }
            set { _ValidClient = value; }
        }

        [Description("Show validators in page ASP.Net"), Category("SGI"), DefaultValue(true)]
        public bool ShowValidators
        {
            get { return _showValidators; }
            set { _showValidators = value; }
        }

        [Description("Gera o javascript para abrir um popup centgralizado"), Category("SGI"), DefaultValue(true)]
        public bool OpenWindowScreenCenter
        {
            get { return _openWindowScreenCenter; }
            set { _openWindowScreenCenter = value; }
        }

        [Description("Usado no SGI 2003, para mostrar o erro no frame"), Category("SGI"), DefaultValue(true)]
        public bool ShowError
        {
            get { return _showError; }
            set { _showError = value; }
        }
        #endregion

        /// <summary>
        /// Add the script to the page header
        /// </summary>
        /// <param name="page"></param>
        /// <param name="script"></param>
        /// <param name="defer"></param>
        public void AddJavaScriptToPageHeader(string script, bool defer)
        {
            string aux = "";
            if (defer)
                aux = "defer=\"defer\" ";

            string tag = string.Format("<script {1}src=\"{0}\" type=\"text/javascript\"></script>", script, aux);
            System.Web.UI.LiteralControl include = new System.Web.UI.LiteralControl(tag);
            this.Header.Controls.Add(include);
        }

        public void AddJavaScriptToPageHeader(string script)
        {
            AddJavaScriptToPageHeader(script, false);
        }

        protected override void OnInit(EventArgs e)
        {
            try
            {
            GetRequestPageCollection();


            //--idusuario
            //switch (Environment.MachineName.ToUpper())
            //{
            //    case "CEUB-055492": //Clei
            //    case "CEUB-055490": //Joel
            //    case "CEUB-055497": //Stiven
            //    case "CEUB-055495": // Willy
            //    case "CEUB-055496": //Helton
            //        if(SGI.Common.Session.idUsuario == 0)
            //            SGI.Common.Session.idUsuario = -1;
            //        break;
            //}

            _NamePage = Library.GetNamePage();
            _IPServer = Request.ServerVariables["LOCAL_ADDR"];


            _UrlRequestPage = System.IO.Path.GetFileName(Request.Path);
            _Params = Common.Session.Params;

            // Configurada no Web.Config AppSettings, o valor default é false
            if (AppSettings.ValidSession)
                if (!Common.Session.IsValid())
                {
                    Common.Session.Params.Clear();
                    Common.Session.Params.Add(Const.URL, "index.aspx");
                    Common.Session.Params.Add(Const.ALERTLIST_CACHE, new AlertList("535"));
                    Response.Redirect("~/InvalidSession.aspx");
                }

            if (ShowValidators)
                    Page.ClientScript.RegisterStartupScript(GetType(), "__showValidators",
                        Common.ClientScript.ShowValidators());

            if (OpenWindowScreenCenter)
                    Page.ClientScript.RegisterStartupScript(GetType(), "OpenWindowScreenCenter",
                        Common.ClientScript.OpenWindowScreenCenter());

            if (ShowError)
                Page.ClientScript.RegisterStartupScript(GetType(), "ShowError", Common.ClientScript.ShowError());

            //-- Codigo adicionado para não deixar cache na maquina do cliente. (André)
            if (!_CacheClient)
            {
                Response.Cache.SetExpires(DateTime.Now.Subtract(new TimeSpan(1)));
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.CacheControl = "private";
                Response.Expires = 0;
                Response.ExpiresAbsolute = DateTime.Now.Subtract(new TimeSpan(1));
                Response.AddHeader("pragma", "no-cache");
            }

            base.OnInit(e);

            ReadQueryString();

            ReadFormParams();

            //if( SGI.Common.Session.coAcesso == "078903")
            //    foreach (string o in Session.Keys)
            //        Response.Write(o + ": " + Session[o].ToString() + "<br>");
            }
            catch (Exception)
            {
                string s = "";
            }

        }

        private void ReadQueryString()
        {
            try
            {
                //-->> Bind parametros do menu
                string idIM = new SGI.Safe.Cryptography("SGI:UniCEUB").DecryptData(Library.ToString(Request.QueryString["idim"]));
                //EC.Common.Session.idItemMenu = idIM;

                string idPFS = new SGI.Safe.Cryptography("SGI:UniCEUB").DecryptData(Library.ToString(Request.QueryString["idpfs"]));
                //EC.Common.Session.idPerfilFuncaoSistema = EC.Common.Cryptography.Encryption(idPFS, Const.KEY_CACHE);

                string idNIM = new SGI.Safe.Cryptography("SGI:UniCEUB").DecryptData(Library.ToString(Request.QueryString["idnim"]));
                //EC.Common.Session.idNivelAcessoItemMenu = idNIM;

                string idNFS = new SGI.Safe.Cryptography("SGI:UniCEUB").DecryptData(Library.ToString(Request.QueryString["idnfs"]));
                //EC.Common.Session.idNivelFuncaoSistema = idNFS;

                string idIMP = new SGI.Safe.Cryptography("SGI:UniCEUB").DecryptData(Library.ToString(Request.QueryString["idimp"]));
                //EC.Common.Session.idItemMenuPerfil = idIMP;

            }
            catch
            {

            }
        }

        protected override void OnError(EventArgs e)
        {
            base.OnError(e);

            Logger.Error(Server.GetLastError());
        }

        private void GetRequestPageCollection()
        {
            _oReqPge = Common.Session.RequestPageCollection;
        }

        public string GetPathFileName()
        {
            return ToString().Replace("SGI.UI", "").Replace(".", "/") + ".aspx";
        }

        public string GetFileName()
        {
            try
            {
                string fullPath = Request.ServerVariables["PATH_TRANSLATED"].ToString();
                string[] arFullPath = fullPath.Split('\\');
                return arFullPath[arFullPath.Length - 1];
            }
            catch
            {
                return string.Empty;
            }
        }

        protected RequestPage GetRequestPage(string nameFile)
        {
            for (int i = 0; i < _oReqPge.Count; i++)
            {
                if (System.IO.Path.GetFileName(nameFile) == _oReqPge[i].Key)
                {
                    return _oReqPge[i];
                }
            }
            return new RequestPage();
        }

        protected string GetUrl()
        {
            string url;
            int index = Request.Url.OriginalString.IndexOf("?");
            if (index > -1)
                url = Request.Url.OriginalString.Substring(0, index);
            else
                url = Request.Url.OriginalString;
            return url.Replace(Request.AppRelativeCurrentExecutionFilePath.Replace("~", ""), "");
        }

        protected Parameters GetParametersByNamePage(string nameFile)
        {
            for (int i = 0; i < _oReqPge.Count; i++)
            {
                if (System.IO.Path.GetFileName(nameFile) == _oReqPge[i].Key)
                {
                    return _oReqPge[i].Params;
                }
            }
            return new Parameters();
        }

        protected Parameters GetParametersPage()
        {
            return GetParametersByNamePage(Library.GetNamePage());
        }

        protected bool BindParameters()
        {
            return Library.ToBoolean(GetParametersLastRequestPage()[Const.BINDPARAMETERS_CACHE]);
        }

        protected AlertList LastAlertList()
        {
            try
            {
                return (AlertList)(GetParametersLastRequestPage()[Const.ALERTLIST_CACHE]);
            }
            catch
            {
                return new AlertList();
            }
        }

        protected Parameters GetParametersPage(bool ClearParams)
        {
            Parameters tempParams = GetParametersByNamePage(Library.GetNamePage());
            if (ClearParams)
            {
                Common.Session.Params.Clear();
            }
            return tempParams;
        }

        protected Parameters GetParametersLastRequestPage()
        {
            return GetParametersLastRequestPage(false);
        }

        protected Parameters GetParametersLastRequestPage(bool clearParams)
        {
            try
            {
                Parameters param;
                if (_oReqPge[_oReqPge.Count - 1].Key == Library.GetNamePage())
                    param = _oReqPge[_oReqPge.Count - 2].Params;
                else
                    param = _oReqPge[_oReqPge.Count - 1].Params;

                if (clearParams)
                    Common.Session.Params.Clear();

                return param;
            }
            catch
            {
                return new Parameters();
            }
        }

        protected string GetLastPageName()
        {
            try
            {
                string name = _oReqPge[_oReqPge.Count - 1].Key;
                if (name.Trim() == Library.GetNamePage())
                {
                    name = _oReqPge[_oReqPge.Count - 2].Key;
                }
                return name;
            }
            catch
            {
                return "";
            }
        }

        protected override void OnUnload(EventArgs e)
        {
            try
            {
                if (_saveRequestPage)
                {
                    var o = new Parameters();
                    _Params.CopyTo(ref o);
                    _oReqPge.Add(_UrlRequestPage, o);
                    Common.Session.RequestPageCollection = _oReqPge;
                }
            }
            catch (Exception x)
            {
                Logger.Error(x);
            }

            base.OnUnload(e);
        }

        protected override void OnPreRender(EventArgs e)
        {
            // TODO:  Add BasePage.OnPreRender implementation

            base.OnPreRender(e);
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            // TODO:  Add BasePage.Render implementation
            base.Render(writer);

            if (_IPServer == "172.16.1.115")
            {
                if (_NamePage != "sgi.aspx" &&
                    _NamePage != "Header.aspx" &&
                    _NamePage != "SIS100_MenuLeft.aspx" &&
                    _NamePage != "modulo.aspx")
                {

                    Response.Write("</td> ");
                    Response.Write("</tr>");
                    Response.Write("</TABLE><br>");
                }
            }
        }

        protected void Redirect(string url)
        {
            FormParamsAttribute.Redirect(this, url);
        }

        /// <summary>
        /// Retorna uma querystring formada pelas propriedades com o atributo FormParams
        /// </summary>
        /// <returns>QueryString</returns>
        protected string CreateQueryStringByFormParams()
        {
            string query = string.Empty;
            string url = string.Empty;

            foreach (MemberInfo member in FormParamsAttribute.GetFormParams(this))
                query += string.Format("{0}={1}&", member.Name, FormParamsAttribute.GetValue(this, member));

            if (query.Length > 0)
            {
                query = query.Substring(0, query.Length - 1);
                query = HttpUtility.UrlEncode(new SGI.Safe.Cryptography("FP:SGI").EncryptData(query));
                url = string.Format("?fprs{0}", query);
            }

            return url;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formParams"></param>
        /// <returns>QueryString</returns>
        protected string CreateQueryStringByFormParams(Dictionary<string, object> formParams)
        {
            return GenerateQueryStringByFormParams(formParams);
        }

        public static string GenerateQueryStringByFormParams(Dictionary<string, object> formParams)
        {
            string query = string.Empty;
            string url = string.Empty;

            foreach (var formParam in formParams)
                query += string.Format("{0}={1}&", formParam.Key, formParam.Value);

            if (query.Length > 0)
            {
                query = query.Substring(0, query.Length - 1);
                query = HttpUtility.UrlEncode(new SGI.Safe.Cryptography("FP:SGI").EncryptData(query));
                url = string.Format("?fprs{0}", query);
            }

            return url;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="formParams"></param>
        /// <returns>QueryString</returns>
        protected string CreateQueryStringByFormParams(string pageUrl, Dictionary<string, object> formParams)
        {
            return String.Concat(pageUrl, CreateQueryStringByFormParams(formParams));
        }

        private void ReadFormParams()
        {
            FormParamsAttribute.ReadFormParams(this);
        }
    }
}
