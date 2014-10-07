using System;
using System.Web;

namespace EC.Common
{
    /// <summary>
    /// Summary description for SGIException.
    /// </summary>
    public class SGIException1 : Exception
    {
        public SGIException1()
        { }

        public SGIException1(string message)
            : base(message)
        {
            Publish(this);
        }

        public SGIException1(System.Exception innerException)
        {
            Publish(innerException);
        }

        public SGIException1(int number, string message)
            : base(message)
        {
            Number = number;

            Publish(this);
        }

        public SGIException1(string message, System.Exception innerException)
            : base(message, innerException)
        {
            Publish(this);
        }

        public int Number
        {
            get;
            set;
        }

        private void Publish(Exception e)
        {
            if (AppSettings.EnableLog)
            {
                if (HttpContext.Current != null)
                {
                    HttpContext context = HttpContext.Current;
                    new EC.Common.Mail().SendMailToWebAdministrator(AppSettings.WebAdministrators, string.Format("Error in site {0}", AppSettings.UrlSite),
                                 EC.Common.Mail.HtmlErrorPage(context,
                                 new MailParameter("@Application", AppSettings.NameApplication),
                                 new MailParameter("@ErrorIn", context.Request.Url.ToString()),
                                 new MailParameter("@IP", Library.GetIP()),
                                 new MailParameter("@ErrorMessage", e.Message),
                                 new MailParameter("@InnerExceptionErrorMessage", (e.InnerException == null? "": e.InnerException.Message)),
                                 new MailParameter("@ErrorTrace", e.StackTrace.ToString()),
                                 new MailParameter("@Source", e.Source.ToString()),
                                 new MailParameter("@LocalPath", context.Request.Url.LocalPath),
                                 new MailParameter("@DateTime", System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"))));
                }
            }
        }
    }
}
