using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace EC.Common
{
    public static class Extensions 
    {
        public static short ToInt16(this object value)
        {
            return Library.ToShort(value);
        }
        public static int ToInt32(this object value)
        {
            return Library.ToInteger(value);
        }
        public static long ToInt64(this object value)
        {
            return Library.ToLong(value);
        }
        public static bool ToBoolean(this object value)
        {
            return Library.ToBoolean(value);
        }
        public static byte ToByte(this object value)
        {
            return Library.ToByte(value);
        }
        public static DateTime ToDate(this object value)
        {
            return Library.ToDate(value);
        }
        public static double ToDouble(this object value)
        {
            return Library.ToDouble(value);
        }
        /// <summary>
        /// Format date in dd/MM/yyyy (pt-BR)
        /// </summary>
        /// <param name="value">Date</param>
        /// <returns>Formated date</returns>
        public static string FormatPT(this DateTime value)
        {
            return ((DateTime)value).ToString("dd/MM/yyyy");
        }

        public static string ToDateFormatedPT(this object value)
        {
            if (string.IsNullOrEmpty(value.ToString()))
                return string.Empty;
            return ((DateTime)value).ToString("dd/MM/yyyy");
        }

        public static bool IsNull(this object value)
        {
            return value == null;
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
        
        public static string Reverse(this string value)
        {
            char[] arr = value.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

        public static string RenderControl(this Control control)
        {
            StringBuilder html = new StringBuilder();
            StringWriter sw = new StringWriter(html);
            HtmlTextWriter htmlText = new HtmlTextWriter(sw);
            control.RenderControl(htmlText);
            return html.ToString();
        }

        public static List<string> ToTextArray(this ListItemCollection items)
        {
            List<string> list = new List<string>();

            foreach (ListItem item in items)
                list.Add(item.Text);

            return list;
        }

        public static List<string> ToValueArray(this ListItemCollection items)
        {
            List<string> list = new List<string>();

            foreach (ListItem item in items)
                list.Add(item.Value);

            return list;
        }


        public static List<ListItem> ToList(this ListItemCollection items)
        {
            var list = new List<ListItem>();

            foreach (ListItem item in items)
                list.Add(item);

            return list;
        }
        public static Dictionary<string, string> ToArray(this ListItemCollection items)
        {
            Dictionary<string, string> list = new Dictionary<string, string>();

            foreach (ListItem item in items)
                list.Add(item.Text, item.Value);

            return list;
        }
        public static DateTime ParseDateTime(this string date)
        {
            string dayOfWeek = date.Substring(0, 3).Trim();
            string month = date.Substring(4, 3).Trim();
            string dayInMonth = date.Substring(8, 2).Trim();
            string time = date.Substring(11, 9).Trim();
            string offset = date.Substring(20, 5).Trim();
            string year = date.Substring(25, 5).Trim();
            string dateTime = string.Format("{0}-{1}-{2} {3}", dayInMonth, month, year, time);
            DateTime ret = DateTime.Parse(dateTime);
            return ret;
        }

        public static List<string> SelectedValues(this CheckBoxList checkBoxList)
        {
            return checkBoxList.Items.ToList()
                               .Where(item => item.Selected)
                               .Select(item => item.Value)
                               .ToList();
        }
    }
}
