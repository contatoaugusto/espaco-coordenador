using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EC.Common
{
    public class JsonError
    {
        public string ID { get; set; }

        public string Description { get; set; }

        public JsonError(string id, string description)
        {
            this.ID = id;
            this.Description = description;
        }

        public JsonError(int id, string description)
        {
            this.ID = id.ToString();
            this.Description = description;
        }
    }
}
