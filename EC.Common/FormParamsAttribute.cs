using System.Reflection;
using System.Collections.Generic;
using System;
using System.Web;
namespace EC.Common
{
    [System.AttributeUsage(AttributeTargets.Property)]
    public sealed class FormParamsAttribute : Attribute
    {
        public static bool ContainsAttribute(MemberInfo member)
        {
            FormParamsAttribute[] attributes = GetAttributes(member);
            return ((attributes != null) && (attributes.Length > 0));
        }
        public static FormParamsAttribute[] GetAttributes(MemberInfo obj)
        {
            List<FormParamsAttribute> attributes = new List<FormParamsAttribute>();
            foreach (object attribute in obj.GetCustomAttributes(typeof(FormParamsAttribute), true))
                attributes.Add((FormParamsAttribute)attribute);

            return attributes.ToArray();
        }

        public static MemberInfo[] GetFormParams(object form)
        {
            List<MemberInfo> members = new List<MemberInfo>();

            foreach (MemberInfo member in form.GetType().GetProperties())
                if (ContainsAttribute(member))
                    members.Add(member);

            return members.ToArray();
        }

        public static object GetValue(object form, MemberInfo formMember)
        {
            if (formMember.MemberType == MemberTypes.Field)
                return formMember.DeclaringType.GetField(formMember.Name).GetValue(form);
            else if (formMember.MemberType == MemberTypes.Property)
                return formMember.DeclaringType.GetProperty(formMember.Name).GetValue(form, null);
            else
                return null;
        }

        public static MemberInfo Find(object form, string nameFormParam)
        {
            foreach (MemberInfo member in FormParamsAttribute.GetFormParams(form))
                if (member.Name == nameFormParam)
                    return member;
            return null;
        }

        public static void SetValue(object form, string nameFormParam, object value)
        {
            MemberInfo member = FormParamsAttribute.Find(form, nameFormParam);
            if (member != null)
            {
                PropertyInfo prop = member.DeclaringType.GetProperty(member.Name);
				if (!prop.CanWrite)
					return;
                else if (prop.PropertyType.FullName.Equals("System.Int32"))
                    member.DeclaringType.GetProperty(member.Name).SetValue(form, Library.ToInteger(value.ToString()), null);
                else if (prop.PropertyType.FullName.Equals("System.String"))
                    member.DeclaringType.GetProperty(member.Name).SetValue(form, value.ToString(), null);
                else if (prop.PropertyType.FullName.Equals("System.Double"))
                    member.DeclaringType.GetProperty(member.Name).SetValue(form, Library.ToDouble(value.ToString()), null);
                else if (prop.PropertyType.FullName.Equals("System.Boolean"))
                    member.DeclaringType.GetProperty(member.Name).SetValue(form, Library.ToBoolean(value.ToString()), null);
                else if (prop.PropertyType.FullName.Equals("System.Decimal"))
                    member.DeclaringType.GetProperty(member.Name).SetValue(form, Library.ToDecimal(value.ToString()), null);
                else if (prop.PropertyType.FullName.Equals("System.Int64"))
                    member.DeclaringType.GetProperty(member.Name).SetValue(form, Library.ToLong(value.ToString()), null);
                else if (prop.PropertyType.FullName.Equals("System.DateTime")) 
                    member.DeclaringType.GetProperty(member.Name).SetValue(form, Library.ToDate(value.ToString()), null);
            }
        }

        public static object GetType(MemberInfo formMember)
        {
            if (formMember.MemberType == MemberTypes.Field)
                return formMember.DeclaringType.GetField(formMember.Name).FieldType;
            else if (formMember.MemberType == MemberTypes.Property)
                return formMember.DeclaringType.GetProperty(formMember.Name).PropertyType;

            return null;
        }

        public static void Redirect(object control,string url)
        {
            string query = string.Empty;
            foreach (MemberInfo member in FormParamsAttribute.GetFormParams(control))
                query += string.Format("{0}={1}&", member.Name, FormParamsAttribute.GetValue(control, member));

            if (query.Length > 0)
            {
                query = query.Substring(0, query.Length - 1);
                query = HttpUtility.UrlEncode(new SGI.Safe.Cryptography("FP:SGI").EncryptData(query));
                url = string.Format((url.IndexOf("aspx?") > -1? "{0}&fprs{1}": "{0}?fprs{1}"), url, query);
            }

            HttpContext.Current.Response.Redirect(url);
        }

        public static void ReadFormParams(object control)
        {
           //-- HttpContext.Current.Response.Write("function ReadFormParams(object control)<br>");
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.Url.Query))
            {
                string query = HttpContext.Current.Request.Url.Query;

                try
                {
                    query = HttpContext.Current.Request.Url.Query;//.Replace("?", "");

                    if (query.Substring(0, 5).ToLower() != "?fprs" &&
                        query.IndexOf("&fprs") == -1) return;


                    query = (query.IndexOf("&fprs") > -1 ? query.Substring(query.IndexOf("&fprs"), (query.Length - query.IndexOf("&fprs"))) : query);

                    query = query.Replace("?fprs", "").Replace("&fprs", "");

                    string[] prs = new SGI.Safe.Cryptography("FP:SGI").DecryptData(HttpUtility.UrlDecode(query)).Split('&');
                    
                    //--HttpContext.Current.Response.Write(prs.ToString() + "<br>");
                    
                    foreach (string p in prs)
                    {
                        //--HttpContext.Current.Response.Write(p + "<br>");
                        string[] p1 = p.Split('=');
                        FormParamsAttribute.SetValue(control, p1[0], p1[1]);
                    }

                    foreach (MemberInfo member in FormParamsAttribute.GetFormParams(control))
                        query += string.Format("{0}={1}&", member.Name, FormParamsAttribute.GetValue(control, member));
                }
                catch(Exception ex)
                {
                    HttpContext.Current.Response.Write(ex.Message);            
                }
            }
        }
    }
}
