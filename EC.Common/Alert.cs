using System;
using System.Linq;
using System.Xml;
using UniCEUB.Core.Log;

namespace EC.Common
{
    [Serializable()]
    public class Alert
    {
        #region Variables
        private string _key = "";
        private string _description = "";
        private string _control = "";
        private string[] _args = new string[0];
        #endregion Variables

        #region Constructs
        public Alert()
        {
        }
        public Alert(string key)
        {
            Key = key;
            GetDescription();
        }

        public Alert(string key, string description)
        {
            Key = key;
            Description = description;
        }
        public Alert(string description, params object[] args)
        {
            Key = description;
            Args = new string[args.Length];

            for (int i = 0; i < args.Length; i++) 
            {
                if (args[i] != null)
                    args[i].ToString();
                else
                    return;
            }

        }
        public Alert(string key, string[] args)
        {
            Key = key;
            Args = args;
        }
        #endregion Constructs

        #region Properties
        public string[] Args
        {
            get { return _args; }
            set { _args = value; }
        }

        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        public string Control
        {
            get { return _control; }
            set { _control = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        #endregion Properties

        #region Methods

        public string GetDescription()
        {
            if (Key.Trim().Length == 0)
                return Description;

            var error = ErrorList.Get(_key.ToInt32());

            if (Description.Trim().Length == 0)
            {
                if (Control.Trim().Length == 0)
                {
                    Description = Args.Length == 0 ? error.Description : string.Format(error.Description, Args);
                }
                else
                    Description = error.Description + ": " + Control;
            }

            return Description;
        }

        public override string ToString()
        {
            string aux = string.Empty;
            aux += Key + ";";
            aux += Description + ";";
            return aux;
        }

        public static string GetDescription(string key)
        {
            return GetDescription(key, null);
        }

        public static string GetDescription(string key, params object[] args)
        {
            var alerts = ErrorList.Instance;

            if (!alerts.Any())
                return "Mensagem não cadastrada.";

            var alert = alerts.FirstOrDefault(a => a.Id == key.ToInt32());

            if (alert == null)
                return "Mensagem não cadastrada.";

            if (args != null && args.Length > 0)
                return string.Format(alert.Description, args);

            return alert.Description;
        }

        #endregion Methods
    }
}