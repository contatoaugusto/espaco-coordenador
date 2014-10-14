using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace EC.UI.WebControls
{
    public class Control : System.Web.UI.Control
    {

        protected void RegisterScriptInHeader(string script)
        {
            RegisterScriptInHeader(script, "");
        }

        protected void RegisterScriptInHeader(string script, string parameters)
        {
            string tag = string.Format("<script src=\"{0}\" type=\"text/javascript\"></script>", Page.ClientScript.GetWebResourceUrl(GetType(), script) + (parameters.Length > 0 ? parameters : ""));
            LiteralControl include = new LiteralControl(tag);
            Page.Header.Controls.Add(include);
        }

        protected void RegisterScriptInHeader(string script, bool defer)
        {
            string aux = "";
            if (defer)
                aux = "defer=\"defer\" ";

            string tag = string.Format("<script {1}src=\"{0}\" type=\"text/javascript\"></script>", Page.ClientScript.GetWebResourceUrl(GetType(), script), aux);
            LiteralControl include = new LiteralControl(tag);
            Page.Header.Controls.Add(include);
        }

        protected void RegisterCss(string alias)
        {
            string cssLink = "<link rel=\"stylesheet\" type=\"text/css\" href=\"" + Page.ClientScript.GetWebResourceUrl(this.GetType(), alias) + "\" />";

            LiteralControl include = new LiteralControl(cssLink);
            Page.Header.Controls.Add(include);
        }

        protected void RegisterScript(string key, string script)
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), key, script, true);
        }

        protected string EnsureEndsWithSemicolon(string value)
        {
            if (value != null && value.Length > 0 && !value.EndsWith(";"))
                return value += ";";
            return value;
        }
    }
}
