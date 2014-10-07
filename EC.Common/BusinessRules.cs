using System;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

namespace EC.Common
{
    [Serializable]
    public sealed class BusinessRules
    {
        public static string GetRule(int number)
        {
            string key = "PathFileAlert";

            string value = ConfigurationManager.AppSettings[key];
            if (value != null)
            {
                string path = System.IO.Path.Combine(value, "BusinessRules.xml");

                if (!System.IO.File.Exists(path))
                    throw new Exception(string.Format("The file \"{0}\" is not found", path));

                XDocument rules = XDocument.Load(path);

                var rule = rules.Descendants("Rule").Where(r => (int)r.Attribute("Number") == number).Single();

                return rule.Value;
            }
            else
                throw new Exception("The key \"BusinessRulesPath\" not found in web.config");
        }
    }
}
