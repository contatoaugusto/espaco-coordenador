using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using EC.Common;

namespace EC.Common
{
    /// <summary>
    /// Summary description for AlertList.
    /// </summary>
    [Serializable()]
    public class AlertList : CollectionBase
    {
        private TypeAlert _type = TypeAlert.Error;

        #region Constructs
        public AlertList(string key, string description)
        {
            this.Add(key, description);
        }

        public AlertList(string key)
        {
            Alert alert = new Alert(key);
            this.Add(alert);
        }

        public AlertList(Alert alert)
        {
            this.Add(alert);
        }

        public AlertList(Alert alert, TypeAlert type)
        {
            this.Type = type;
            this.Add(alert);
        }

        public AlertList(string key, TypeAlert type)
        {
            Alert alert = new Alert(key);
            this.Type = type;
            this.Add(alert);
        }

        public AlertList(string description, params object[] args)
        {
            Alert alert = new Alert(description, args);
            this.Add(alert);
        }

        public AlertList(string key, string[] args)
        {
            Alert alert = new Alert(key, args);
            this.Add(alert);
        }

        public AlertList() { }
        #endregion Constructs

        #region Properties
        public TypeAlert Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public Alert this[int index]
        {
            get
            {
                return (Alert)List[index];
            }
            set
            {
                List[index] = value;
            }
        }
        #endregion Properties

        #region Methods
        public void Add(string key, string description, params string[] args)
        {
            Alert alert = new Alert();

            alert.Key = key;
            alert.Description = description;
            alert.Args = args;

            List.Add(alert);
        }

        public void Add(string description, params object[] args)
        {
            Alert alert = new Alert(description, args);

            List.Add(alert);
        }

        public void Append(AlertList list)
        {
            if (list.HasAlert())
            {
                for (int i = 0; i < list.Count; i++)
                {
                    this.Add(list[i].Key, list[i].Description, list[i].Args);
                }
            }
        }

        public void Add(string key)
        {
            Alert alert = new Alert();
            alert.Key = key;
            List.Add(alert);
        }

        public void Add(Alert alert)
        {
            List.Add(alert);
        }

        public bool HasAlert()
        {
            return List.Count > 0;
        }

        public AlertList GetAlertListSuccess()
        {
            this.Type = TypeAlert.Info;
            return this;
        }

        public void BindDescription()
        {

            foreach (Alert alert in this)
            {
                alert.Description = ErrorList.Get(alert.Key.ToInt32()).Description;
            }
        }

        public string ToXml()
        {
            try
            {
                string xml = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\n";
                xml += "<alertlist>\n";

                for (int i = 0; i < this.Count; i++)
                {
                    xml += "<alert>\n";
                    xml += "	<key>" + this[i].Key + "</key>\n";
                    xml += "	<description>" + this[i].Description + "</description>\n";
                    xml += "	<control>" + this[i].Control + "</control>\n";
                    xml += "</alert>\n";
                }

                xml += "</alertlist>";
                return xml;
            }
            catch
            {
                string xml = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\n";
                xml += "<alertlist />";
                return xml;
            }
        }

        #endregion Methods

    }
}