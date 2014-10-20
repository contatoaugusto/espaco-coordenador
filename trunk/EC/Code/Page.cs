using System;
using System.Web.UI;
using EC.Common;

namespace UI.Web.EC
{
    /// <summary>
    /// Summary description for Page
    /// </summary>
    public class Page : System.Web.UI.Page
    {
        #region Properties
        private string NameMasterPage
        {
            get { return ViewState["000"] as string; }
            set { ViewState["000"] = value; }
        }

        public bool IncludePrintScript
        {
            get;
            private set;
        }
        #endregion Properties

        public Page()
        {
            if (!UI.Web.EC.Utils.IsMobileUser())
                NameMasterPage = "Content";
            else
                NameMasterPage = "Mobile";
        }

        public Page(string nameMasterPage)
        {
            NameMasterPage = nameMasterPage;
        }

        public Page(string nameMasterPage, bool includePrintScript)
        {
            NameMasterPage = nameMasterPage;
            IncludePrintScript = includePrintScript;
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            string template = EC.Utils.GetTemplates()[0];

            //if (SessionAluno.idAlunoMatricula > 0)
            //    template = SessionAluno.Template;
            //else
            //    Response.Redirect("Logout.ashx");

            //if (EC.Utils.IsMobileUser())
            //{
            //    if (Session["Vc"] != null && (Session["Vc"]).ToString().Equals("s"))
            //    {
            //        if (NameMasterPage.Equals("Mobile"))
            //        {
            //            MasterPageFile = string.Format("~/Mobile/templates/{0}/{1}.master", template, NameMasterPage);
            //            Theme = string.Format("Mobile_{0}", template);
            //        }
            //        else
            //        {
            //            MasterPageFile = string.Format("~/Templates/{0}/{1}.master", template, NameMasterPage);
            //            Theme = template;
            //        }
            //    }
            //    else
            //    {
            //        if (NameMasterPage.Equals("Main") || NameMasterPage.Equals("Content"))
            //        {
            //            MasterPageFile = string.Format("~/Templates/{0}/{1}.master", template, NameMasterPage);
            //            Theme = template;
            //        }
            //        else
            //        {
            //            MasterPageFile = string.Format("~/Mobile/templates/{0}/{1}.master", template, NameMasterPage);
            //            Theme = string.Format("Mobile_{0}", template);
            //        }
            //    }
            //}
            //else
            //{
            //    MasterPageFile = string.Format("~/Templates/{0}/{1}.master", template, NameMasterPage);
            //    Theme = template;
            //}

            EC.Utils.ValidaUrl();

            string url = Library.GetPathPageUrl();

            //if (SessionMatricula.icUsuarioAcompanhamentoMatricula)
            //{
            //    if (!url.Contains("/matricula/") && !url.Contains("/matriculaestagio/") &&
            //        !url.Contains("gradehoraria.aspx") && !url.Contains("2ViaFatura.aspx"))
            //    {
            //        Response.Redirect("~/matricula/Matricula.aspx" + Url.CreateUrlParams("error", "1038|"));
            //    }
            //}
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (IncludePrintScript)
                Page.ClientScript.RegisterClientScriptInclude("PrintScript", ResolveClientUrl("~/Includes/jquery.printElement.min.js"));
        }

        protected void RegisterScriptInHeader(string script)
        {
            string tag = string.Format("<script src=\"{0}\" type=\"text/javascript\"></script>", ResolveClientUrl(script));
            LiteralControl include = new LiteralControl(tag);
            this.Header.Controls.Add(include);
        }
    }
}
