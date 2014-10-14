using System;
using System.Net;
using System.IO;
using System.Configuration;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mail;
using UniCEUB.Core.Log;
using MailMessage = System.Net.Mail.MailMessage;
using MailPriority = System.Net.Mail.MailPriority;

namespace EC.Common
{

    public enum TypeMail
    {
        AccessPageDenied = 1,
        Vestibular = 2,
        ErrorSite = 3,
        PasswordUser = 4,
        Reprovacao_SolCHex = 5,
        EsqueceuSenha = 6,
        ProcessoSeletivo = 7,

        /* Portal */
        Graduado = 8,
        PosGraduacao = 9,
        Transferencia = 10,
        Mestrado = 11,
        Aperfeicoamento = 12,
        Extensao = 13,
        Doutorado = 14,
        Mudanca = 15,
        EspecializacaoDistancia = 16

    }
    public struct MailParameter
    {
        public string Key;
        public string Value;

        public MailParameter(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
    public struct EMail
    {
        public string deAssunto;
        public string deHtmlEmail;
    }


    public class Mail
    {
        public struct Address
        {
            public string Email;
            public string Name;

            public Address(string sEmail, string sName)
            {
                Email = sEmail;
                Name = sName;
            }
        }

        private string _smtpServer = null;
        private string _mailServer = String.Empty;
        private string _errorMessage = String.Empty;
        public string SmtpServer
        {
            get { return _smtpServer; }
            set { _smtpServer = value; }
        }

        public string MailServer
        {
            get { return _mailServer; }
            set { _mailServer = value; }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        public bool Send(string[] to, string from, System.Net.Mail.MailPriority priority, string subject, string body)
        {
            string emails = string.Empty;
            foreach (string email in to)
                emails += email + ";";

            if (!string.IsNullOrEmpty(emails))
                emails = emails.Substring(0, emails.Length - 1);

            return Send(null, emails, from, priority, subject, body);
        }

        public bool Send(string smtpserver, string to, string from, System.Net.Mail.MailPriority priority, string subject, string body)
        {
            try
            {
                MailMessage mail = new MailMessage(from, to);
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.Priority = priority;
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                SmtpClient client = new SmtpClient();

                if (smtpserver != null)
                    client.Host = smtpserver;
                else
                    client.Host = Library.GetSmtpServer();

                client.Credentials = new NetworkCredential("sgi", "987321654", "DINFOR");

                client.Send(mail);
                return true;
            }
            catch (Exception e)
            {
                _errorMessage = e.Message;
                return false;
            }
        }

        public bool Send(string to, string from, System.Net.Mail.MailPriority priority, string subject, string body)
        {
            try
            {
                string systemMail = Library.GetSystemMail();

                if (string.IsNullOrEmpty(systemMail))
                    systemMail = from;

                MailMessage mail = new MailMessage(systemMail, to);

                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.Priority = priority;

                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                SmtpClient client = new SmtpClient();

                if (Library.GetSmtpServer() != null)
                    client.Host = Library.GetSmtpServer();
                else
                    throw new Exception("It is necessary to inside inform the SmtpServer in web.config of the \"AppSettings\" session.");

                client.Send(mail);
                return true;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                _errorMessage = e.Message;
                return false;
            }
        }

        public bool SendMailToWebAdministrator(string from, string subject, string message)
        {
            try
            {
                return Send(Library.GetWebAdministrators(), from, MailPriority.High, subject, message);
            }
            catch (Exception e)
            {
                Logger.Error(e);
                _errorMessage = e.Message;
                return false;
            }
        }

        public bool SendMailAccessPageDenied(string NamePage)
        {
            try
            {
                string[] email = GetHtmlEmail(TypeMail.AccessPageDenied);
                string Html = email[1];
                Html = Html.Replace("@Usuario", Session.idUsuario + " - " + Session.nmPessoa);
                //Html = Html.Replace("@idMenuPerfil", Session.idItemMenuPerfil.ToString());
                Html = Html.Replace("@NamePage", NamePage);
                Html = Html.Replace("@Date", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                Html = Html.Replace("@IPFWD", Session.IPFWDUsuario);
                Html = Html.Replace("@IP", Session.IPUsuario);
                return SendMailToWebAdministrator(Library.GetNameApplication(), email[0] + " - " + Library.GetPathPageUrl(), Html);
            }
            catch (Exception e)
            {
                Logger.Error(e); ;
                _errorMessage = e.Message;
                return false;
            }
        }

        public bool SendMailPasswordUser(string NamePage)
        {
            try
            {
                string[] email = GetHtmlEmail(TypeMail.AccessPageDenied);
                string Html = email[1];
                Html = Html.Replace("@Usuario", Session.idUsuario + " - " + Session.nmPessoa);
                //Html = Html.Replace("@idMenuPerfil", Session.idItemMenuPerfil.ToString());
                //Html = Html.Replace("@idNivelAcessoItemMenu", Session.idNivelAcessoItemMenu.ToString());
                Html = Html.Replace("@NamePage", NamePage);
                Html = Html.Replace("@Date", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                Html = Html.Replace("@IPFWD", Session.IPFWDUsuario);
                Html = Html.Replace("@IP", Session.IPUsuario);
                return SendMailToWebAdministrator(Library.GetNameApplication(), email[0] + " - " + Library.GetPathPageUrl(), Html);
            }
            catch (Exception e)
            {
                Logger.Error(e);
                _errorMessage = e.Message;
                return false;
            }
        }

        /// <summary>
        /// Retorna os parametros para o email
        /// </summary>
        /// <param name="type">Retorna um array de string.</param>
        /// <returns>Retorna um array de 3 posições. 0 - Assunto; 1 - Descrição do html.</returns>
        private string[] GetHtmlEmail(TypeMail type)
        {
            try
            {
                var oDts = new System.Data.DataSet();

                oDts.ReadXml(Library.PathFileDataXml() + "SISXML_Email.xml");

                System.Data.DataTable oDtt = Library.FilterDataTable(oDts.Tables[0], "idEmail = '" + GetCodeType(type) + "'");

                string[] sReturn = new string[2];

                sReturn[0] = oDtt.Rows[0][1].ToString();
                sReturn[1] = oDtt.Rows[0][2].ToString();

                return sReturn;


            }
            catch (Exception e)
            {
                Logger.Error(e);
                return new string[3];
            }
        }

        private string GetCodeType(TypeMail type)
        {
            switch (type)
            {
                case TypeMail.AccessPageDenied:
                    return "1";
                case TypeMail.Vestibular:
                    return "2";
                case TypeMail.ErrorSite:
                    return "3";
                case TypeMail.PasswordUser:
                    return "4";
                case TypeMail.Reprovacao_SolCHex:
                    return "5";
                case TypeMail.Graduado:
                    return "8";
                case TypeMail.PosGraduacao:
                    return "9";
                case TypeMail.Transferencia:
                    return "10";
                case TypeMail.Mestrado:
                    return "11";
                case TypeMail.Aperfeicoamento:
                    return "12";
                case TypeMail.Extensao:
                    return "13";
                case TypeMail.Doutorado:
                    return "14";
                case TypeMail.Mudanca:
                    return "15";
                case TypeMail.EspecializacaoDistancia:
                    return "16";
            }

            return "0";
        }

        public static string HtmlErrorPage(HttpContext context, params MailParameter[] mailParameteres)
        {
            string html = ClientScript.HtmlErrorPage();

            foreach (MailParameter mp in mailParameteres)
                html = html.Replace(mp.Key, mp.Value);

            string aux = string.Empty;
            //Form
            foreach (string form in context.Request.Form)
                aux += string.Format("{0}: {1}<br />", form, context.Request.Form[form]);

            if (string.IsNullOrEmpty(aux))
                aux = "Undefined";

            html = html.Replace("@HttpForm", aux);

            //QueryString
            aux = string.Empty;
            foreach (string form in context.Request.QueryString)
                aux += string.Format("{0}: {1}<br />", form, context.Request.QueryString[form]);

            if (string.IsNullOrEmpty(aux))
                aux = "Undefined";

            html = html.Replace("@HttpQueryString", aux);

            //Cookies
            aux = string.Empty;
            foreach (string cookie in context.Request.Cookies)
            {
                aux += string.Format("{0}: {1}<br />", cookie, context.Request.Cookies[cookie]);

                foreach (string key in context.Request.Cookies[cookie].Values.AllKeys)
                    aux += string.Format("&nbsp;&nbsp;&nbsp;&nbsp;{0}: {1}<br />", key, context.Request.Cookies[cookie].Values[key]);
            }

            if (string.IsNullOrEmpty(aux))
                aux = "Undefined";

            html = html.Replace("@Cookies", aux);

            //Sessions
            aux = string.Empty;
            if (context.Session != null)
            {
                foreach (string session in context.Session.Keys)
                    aux += string.Format("{0}: {1}<br />", session, context.Session[session]);
            }

            if (string.IsNullOrEmpty(aux))
                aux = "Undefined";

            html = html.Replace("@Sessions", aux);

            //Server Variables
            aux = string.Empty;
            foreach (string variable in context.Request.ServerVariables)
                aux += string.Format("{0}: {1}<br />", variable, context.Request.ServerVariables[variable]);

            html = html.Replace("@ServerVariables", aux);

            html = html.Replace("@MachineName", Environment.MachineName);

            html = html.Replace("@User", Environment.UserName);
            html = html.Replace("@User", "Undefined");

            if (context.Request.UrlReferrer != null)
                html = html.Replace("@UrlReferrer", context.Request.UrlReferrer.ToString());

            html = html.Replace("@UrlReferrer", "Undefined");

            return html;
        }

        public string GetHtmlEmail(TypeMail type, params MailParameter[] oParams)
        {
            try
            {
                string[] sReturn = GetHtmlEmail(type);
                string Html = sReturn[1];

                foreach (MailParameter oParam in oParams)
                    Html = Html.Replace(oParam.Key, oParam.Value);

                return Html;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return string.Empty;
            }
        }

        public EMail GetEmail(TypeMail type, params MailParameter[] oParams)
        {
            try
            {
                string[] sReturn = GetHtmlEmail(type);
                EMail em = new EMail();
                em.deAssunto = sReturn[0];
                em.deHtmlEmail = sReturn[1];


                foreach (MailParameter oParam in oParams)
                    em.deHtmlEmail = em.deHtmlEmail.Replace(oParam.Key, oParam.Value);


                return em;


            }
            catch (Exception e)
            {
                Logger.Error(e);
                return new EMail();
            }
        }
    }
}