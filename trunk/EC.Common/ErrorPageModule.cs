using System;
using System.Web;
using System.Web.SessionState;

namespace EC.Common
{
    public class ErrorPageModule : System.Web.IHttpModule, IRequiresSessionState
    {
        public void Init(HttpApplication context)
        {
            context.Error += new EventHandler(SendError);
        }

        private void SendError(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;
            HttpContext context = application.Context;
            Exception exception = context.Error;

            // unwrap exception if top exception is an HttpUnhandledException
            if (exception is HttpUnhandledException && exception.InnerException != null)
                exception = exception.InnerException;

            HttpException httpException = exception as HttpException;

            //// don't log file not found exceptions
            //if (httpException != null && httpException.GetHttpCode() == 404)
            //    return;

            //// viewstate exceptions raise a WebViewStateFailureAuditEvent, not an exception event
            //if (httpException != null && httpException.InnerException is ViewStateException)
            //    return;

            if (AppSettings.EnableLog)
            {
                new EC.Common.Mail().SendMailToWebAdministrator(AppSettings.SystemMail, string.Format("Error in site {0}", AppSettings.UrlSite),
                             EC.Common.Mail.HtmlErrorPage(HttpContext.Current,
                             new MailParameter("@Application", AppSettings.NameApplication),
                             new MailParameter("@ErrorIn", context.Request.Url.ToString()),
                             new MailParameter("@IP", Library.GetIP()),
                             new MailParameter("@ErrorMessage", exception.Message),
                             new MailParameter("@ErrorTrace", exception.StackTrace.ToString()),
                             new MailParameter("@Source", exception.Source.ToString()),
                             new MailParameter("@LocalPath", context.Request.Url.LocalPath),
                             new MailParameter("@DateTime", System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"))));
            }
        }

        public void Dispose()
        {
        }
    }
}
