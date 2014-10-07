using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.Design;
using System.Drawing.Design;

#region Styles
[assembly: WebResourceAttribute("SGI.UI.WebControls.TextArea.TextArea.css", "text/css", PerformSubstitution = true)]
#endregion Styles

#region Images
[assembly: WebResource("SGI.UI.WebControls.TextArea.grippie.png", "image/png")]
#endregion Images

#region JavaScript
//[assembly: WebResourceAttribute("SGI.UI.WebControls.jQuery.jQuery.js", "text/javascript", PerformSubstitution = true)]
[assembly: WebResourceAttribute("SGI.UI.WebControls.jQuery.jquery.textarearesizer.js", "text/javascript", PerformSubstitution = true)]
[assembly: WebResourceAttribute("SGI.UI.WebControls.jQuery.jquery.textcounting.js", "text/javascript", PerformSubstitution = true)]
#endregion JavaScript

namespace EC.UI.WebControls
{
    [ToolboxData("<{0}:TextArea Runat=\"server\"></{0}:TextArea>")]
    [Serializable()]
    [Designer(typeof(ControlDesigner))]
    [ControlValueProperty("Text")]
    public class TextArea : System.Web.UI.WebControls.TextBox
    {
        public TextArea()
        {
            this.TextMode = System.Web.UI.WebControls.TextBoxMode.MultiLine;
            CssClass = "resizable";
        }

        [Bindable(true, BindingDirection.TwoWay),
        PersistenceMode(PersistenceMode.EncodedInnerDefaultProperty),
        Editor("System.ComponentModel.Design.MultilineStringEditor,System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)),
        Localizable(true), Category("Appearance"), DefaultValue(""), Description("TextBox_Text")]
        public override string Text
        {
            get
            {
                string str = (string)this.ViewState["Text"];
                if (str != null)
                {

                    if (str.Length > MaxLength)
                    {
                        int lenght = MaxLength;
                        if(MaxLength == 0)
                            return str;
                        else
                        {
                            str = str.Replace("\n", "").Replace("\r", "");
                            lenght = str.Length;
                            return str.Substring(0, lenght);
                        }
                    }
                    else
                        return str;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["Text"] = value;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            RegisterCss("SGI.UI.WebControls.TextArea.TextArea.css");

            string image = Page.ClientScript.GetWebResourceUrl(GetType(), "SGI.UI.WebControls.TextArea.grippie.png");
            string style = "<style type=\"text/css\">.grippie-image{background: #EEEEEE url(" + image + ") no-repeat scroll center 1px;}</style>";

            LiteralControl include = new LiteralControl(style);
            Page.Header.Controls.Add(include);

            //RegisterClientScriptInclude("JQUERY", "SGI.UI.WebControls.jQuery.jQuery.js");

            RegisterClientScriptInclude("WCTATAA", "SGI.UI.WebControls.jQuery.jquery.textarearesizer.js");
            RegisterClientScriptInclude("WCTATC", "SGI.UI.WebControls.jQuery.jquery.textcounting.js");

            RegisterClientScriptBlock("TA" + ClientID, ScriptShowTextArea());
        }

         void RegisterCss(string resourceName)
        {
            string cssLink = "<link rel=\"stylesheet\" type=\"text/css\" href=\"" + Page.ClientScript.GetWebResourceUrl(GetType(), resourceName) + "\" />";

            LiteralControl include = new LiteralControl(cssLink);
            Page.Header.Controls.Add(include);
        }

        void RegisterClientScriptBlock(string key, string script)
        {
            //var sm = ScriptManager.GetCurrent(Page);

            //if (sm == null)
            //{
            if (!Page.ClientScript.IsClientScriptBlockRegistered(key))
                Page.ClientScript.RegisterClientScriptBlock(GetType(), key, script, true);
            //}
            //else
            //    ScriptManager.RegisterClientScriptInclude(this, GetType(), key, script);
        }

        void RegisterClientScriptInclude(string key, string resourceName)
        {
            //var sm = ScriptManager.GetCurrent(Page);

            //if (sm == null)
            //{
            if (!Page.ClientScript.IsClientScriptIncludeRegistered(key))
                Page.ClientScript.RegisterClientScriptInclude(key, Page.ClientScript.GetWebResourceUrl(GetType(), resourceName));
            //}
            //else
            //    ScriptManager.RegisterClientScriptInclude(this, GetType(), key, Page.ClientScript.GetWebResourceUrl(GetType(), resourceName));
        }

        #region Register
        //private void RegisterScriptInHeader(string script)
        //{
        //    string tag = string.Format("<script src=\"{0}\" type=\"text/javascript\"></script>", Page.ClientScript.GetWebResourceUrl(GetType(), script));
        //    LiteralControl include = new LiteralControl(tag);
        //    Page.Header.Controls.Add(include);
        //}

        //private void RegisterCss(string alias)
        //{
        //    string cssLink = "<link rel=\"stylesheet\" type=\"text/css\" href=\"" + Page.ClientScript.GetWebResourceUrl(this.GetType(), alias) + "\" />";

        //    LiteralControl include = new LiteralControl(cssLink);
        //    Page.Header.Controls.Add(include);
        //}

        #endregion Register

        private string ScriptShowTextArea()
        {
            StringBuilder script = new StringBuilder();

            string image = Page.ClientScript.GetWebResourceUrl(GetType(), "SGI.UI.WebControls.TextArea.grippie.png");

            script.Append("$(document).ready(function() {\n");
            //script.Append("     $.fn.textCounting.defaults.lengthExceededClass= \"lengthExceededClass\";\n");

            //script.Append("     var otherSettings= {\n");
            //script.Append("	    	maxLengthSource: 'list',\n");
            //script.Append("	    	maxLengthList: '5',\n");
            //script.Append("	    	countWhat: 'words', \n");
            //script.Append("	    	targetModifierType:'id', \n");
            //script.Append("	    	countDirection:'up,down', \n");
            //script.Append("	    	targetModifier:'wordCount,wordsLeft'\n");
            //script.Append("		}\n");

            //script.Append("     var otherSettings= {\n");
            //script.Append("	    	lengthExceededClass: 'lengthExceededClass',\n");
            //script.Append("		}\n");

            //script.Append("    $('#" + ClientID + "').textCounting(otherSettings);\n");
            script.Append("    $('#" + ClientID + "').textCounting();\n");
            script.Append("    $('textarea.resizable:not(.processed)').TextAreaResizer();\n");
            script.Append("});\n");


            script.Append("function sgi_" + this.ClientID + "_vad() {\n");
            script.AppendFormat("   return $(\"#{0}\").val().length <= {1};\n", this.ClientID, this.MaxLength);
            script.Append("}\n\n");

            script.Append("function sgi_" + this.ClientID + "_vadr() {\n");

            script.AppendFormat("var o = $(\"#{0}\");\n", this.ClientID);
            script.Append("     if (o.val().length > "+ this.MaxLength + ") {\n");
            script.AppendFormat("        var t = o.val().substr(0, {0});\n", this.MaxLength - 1);
            script.AppendFormat("        o.val(t);\n");
            script.AppendFormat("       $(\"span#{0}\").text(\"0\");\n", this.ClientID);
            script.Append("     }\n");  

            script.Append("}");

            return script.ToString();
        }

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);

            if (MaxLength > 0)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Maxlength, MaxLength.ToString());
                writer.AddAttribute("onkeypress", string.Format("return sgi_{0}_vad()", this.ClientID));
                writer.AddAttribute("onblur", string.Format("return sgi_{0}_vadr()", this.ClientID));
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);

            if (MaxLength > 0)
            {
                if (!System.Threading.Thread.CurrentThread.CurrentUICulture.IetfLanguageTag.Equals("pt-BR"))
                    writer.Write("<i>(Characters left: <span id=\"" + ClientID + "Down\"></span>)</i>");
                else
                    writer.Write("<i>(Caracteres restantes: <span id=\"" + ClientID + "Down\"></span>)</i>");
            }
        }

    }
}
