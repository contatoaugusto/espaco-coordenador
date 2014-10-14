
using System.Web;
namespace EC.Common
{
    public class JsonItem
    {
        public string Key { get; set; }

        public object Value { get; set; }

        public bool IsArray { get; set; }

        public JsonItem(string key, object value)
        {
            
            Key = key;
            if (value is string)
            {
                value = HttpUtility.HtmlEncode((string)value);
            }
            Value = value;
        }

        public JsonItem(string key, object value, bool isArray)
        {
            Key = key;
            if (value is string && !isArray)
            {
                value = HttpUtility.HtmlEncode((string)value);
            }
            Value = value;
            IsArray = isArray;
        }
    }
}
