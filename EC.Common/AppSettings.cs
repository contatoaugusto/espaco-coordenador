using System.Configuration;

namespace EC.Common
{
    public abstract class AppSettings
    {
        public static string ScriptVersion
        {
            get { return Library.ToString(ConfigurationManager.AppSettings["ScriptVersion"]); }
        }
        public static bool ValidSession
        {
            get { return Library.ToBoolean(ConfigurationManager.AppSettings["ValidSession"]); }
        }

        public static bool IsNotConnectionStringRegistry
        {
            get { return Library.ToBoolean(ConfigurationManager.AppSettings["IsNotConnectionStringRegistry"]); }
        }

        public static bool IsCryptographyConnectionString
        {
            get { return Library.ToBoolean(ConfigurationManager.AppSettings["IsCryptographyConnectionString"]); }
        }

        public static string UrlSite
        {
            get { return ConfigurationManager.AppSettings["UrlSite"]; }
        }

        public static string UrlImagemHeader
        {
            get { return ConfigurationManager.AppSettings["UrlImagemHeader"]; }
        }

        public static string PathReport
        {
            get { return ConfigurationManager.AppSettings["PathReport"]; }
        }

        public static string PathFileAlert
        {
            get { return ConfigurationManager.AppSettings["PathFileAlert"]; }
        }

        public static string PathUpload
        {
            get { return ConfigurationManager.AppSettings["PathUpload"]; }
        }

        public static string PathUploadEspacoAluno
        {
            get { return ConfigurationManager.AppSettings["PathUploadEspacoAluno"]; }
        }

        public static string PathUploadFichaPratica
        {
            get { return ConfigurationManager.AppSettings["PathUploadFichaPratica"]; }
        }

        public static string PathRoot
        {
            get { return ConfigurationManager.AppSettings["PathRoot"]; }
        }

        public static string NameFileAlert
        {
            get { return ConfigurationManager.AppSettings["NameFileAlert"]; }
        }

        public static string WebAdministrators
        {
            get { return ConfigurationManager.AppSettings["WebAdministrators"]; }
        }

        public static string SmtpServer
        {
            get { return ConfigurationManager.AppSettings["SmtpServer"]; }
        }

        public static string SystemMail
        {
            get { return ConfigurationManager.AppSettings["SystemMail"]; }
        }


        public static string NameApplication
        {
            get { return ConfigurationManager.AppSettings["NameApplication"]; }
        }

        public static bool EnableLog
        {
            get { return Library.ToBoolean(ConfigurationManager.AppSettings["EnableLog"]); }
        }

        public static string StaticUrl
        {
            get { return ConfigurationManager.AppSettings["StaticUrl"]; }
        }

        public static string DigitalLibraryServiceUrl
        {
            get { return ConfigurationManager.AppSettings["DigitalLibrary_ServiceUrl"]; }
        }

        public static string DigitalLibraryApiKey
        {
            get { return ConfigurationManager.AppSettings["DigitalLibrary_ApiKey"]; }
        }
    }
}
