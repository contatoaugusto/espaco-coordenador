using System;
using System.Collections.Generic;
using System.Text;

namespace EC.Common
{
    public class JsonSerializer
    {
        List<List<JsonItem>> _json = new List<List<JsonItem>>();

        public int Count
        {
            get { return _json.Count; }
        }

        public void Add(params JsonItem[] items)
        {
            List<JsonItem> item = new List<JsonItem>();

            foreach (JsonItem jsonItem in items)
                item.Add(jsonItem);

            _json.Add(item);
        }

        public void Clear()
        {
            _json.Clear();
        }

        public string Serialize()
        {
            return Serialize(false);
        }

        public string Serialize(bool convertToArray)
        {
            StringBuilder json = new StringBuilder();
            int i = 0;

            if (_json.Count > 1 || convertToArray)
                json.Append("[");

            foreach (List<JsonItem> jsonItems in _json)
            {
                i++;

                json.Append("{");

                int j = 0;

                foreach (JsonItem jsonItem in jsonItems)
                {
                    j++;

                    json.Append(Validate(jsonItem));

                    if (j < jsonItems.Count)
                        json.Append(",");


                }
                json.Append("}");


                if (i < _json.Count)
                    json.Append(",");
            }

            if (_json.Count > 1 || convertToArray)
                json.Append("]");

            return json.ToString();
        }

        private string Validate(JsonItem jsonItem)
        {

            if (jsonItem.Value == null)
                jsonItem.Value = "";

            string type = jsonItem.Value.GetType().ToString();

            switch (type)
            {
                case "System.Byte":
                case "System.Int32":
                case "System.UInt32":
                case "System.Int16":
                case "System.Int64":
                    return string.Format(@"""{0}"":{1}", jsonItem.Key, jsonItem.Value.ToString());

                case "System.Boolean":
                    return string.Format(@"""{0}"":{1}", jsonItem.Key, jsonItem.Value.ToString().ToLower());

                case "System.DBNull":
                case "System.String":

                    if (jsonItem.IsArray)
                        return string.Format(@"""{0}"":{1}", jsonItem.Key, QuoteString(jsonItem.Value.ToString()));
                    else
                        return string.Format(@"""{0}"":""{1}""", jsonItem.Key, QuoteString(jsonItem.Value.ToString()));

                case "System.DateTime":
                    return string.Format(@"""{0}"":""{1}""", jsonItem.Key, Convert.ToDateTime(jsonItem.Value).ToString("dd/MM/yyyy HH:mm:ss"));

                case "System.Double":
                    return string.Format(@"""{0}"":""{1}""", jsonItem.Key, Convert.ToInt32(jsonItem.Value).ToString()); 
                default:
                    throw new Exception("The type of value of JsonItem not implemented");
            }

        }

        private string QuoteString(string s)
        {
            StringBuilder sb = new StringBuilder();

            char c;
            int len = s.Length;

            sb.EnsureCapacity(sb.Length + s.Length + 2);

            for (int i = 0; i < len; i++)
            {
                c = s[i];
                switch (c)
                {
                    case '\\':
                        sb.Append("\\\\");
                        break;
                    case '\b':
                        sb.Append("\\b");
                        break;
                    case '\t':
                        sb.Append("\\t");
                        break;
                    case '\r':
                        sb.Append("\\r");
                        break;
                    case '\n':
                        sb.Append("\\n");
                        break;
                    case '\f':
                        sb.Append("\\f");
                        break;
                    default:
                        if (c < ' ')
                        {
                            sb.Append("\\u");
                            sb.Append(((int)c).ToString("x4", System.Globalization.CultureInfo.InvariantCulture));

                        }
                        else
                        {
                            sb.Append(c);
                        }
                        break;
                }
            }

            return sb.ToString();
        }

    }
}
