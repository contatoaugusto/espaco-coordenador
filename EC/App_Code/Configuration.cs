using System;
using System.Collections;
using System.Collections.Generic;

namespace UI.Web.EC
{
    /// <summary>
    /// Summary description for Config
    /// </summary>
    [Serializable()]
    public class Configuration
    {
        private string _template;
        private ArrayList _rss = new ArrayList();
        private List<int> _shortCut = new List<int>();

        public Configuration()
        {
            _template = "Default";
        }

        public string Template
        {
            get { return _template; }
            set { _template = value; }
        }

        public ArrayList Rss
        {
            get { return _rss; }
            set { _rss = value; }
        }

        public List<int> ShortCut
        {
            get { return _shortCut; }
            set { _shortCut = value; }
        }
    }
}
