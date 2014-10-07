using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace EC.Common
{
    [Serializable()]
    public class Error
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
    [Serializable()]
    public sealed class ErrorList
    {
        private static IList<Error> _instance = null;
        private ErrorList() { }

        public static IList<Error> Instance
        {
            get { return _instance ?? (_instance = Load()); }
        }

        public static Error Get(int id)
        {
            return Instance.First(e => e.Id.ToInt32() == id);
        }

        private static IList<Error> Load()
        {
            var xml = ReadXml();

            var alerts = (from error in xml.Descendants("Error")
                          select new Error
                          {
                              Id = error.Attribute("Id").Value.ToInt32(),
                              Description = error.Value.Trim()
                          }).ToList();

            return alerts;
        }
        private static XDocument ReadXml()
        {
            var pathFileAlert = Library.GetPathFileAlert();
            var nameFileAlert = Library.GetNameFileAlert();

            if (string.IsNullOrEmpty(pathFileAlert))
                throw new Exception("The PathFileAlert item was not found in web.config.");

            if (string.IsNullOrEmpty(nameFileAlert))
                throw new Exception("The NameFileAlert item was not found in web.config.");

            return XDocument.Load(Path.Combine(pathFileAlert, nameFileAlert));
        }
    }
}
