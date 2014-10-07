using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.Design;
using System.Drawing.Design;
using EC.Common;

[assembly: WebResourceAttribute("SGI.UI.WebControls.AjaxUpdating.AjaxUpdating.css", "text/css", PerformSubstitution = true)]
[assembly: WebResourceAttribute("SGI.UI.WebControls.AjaxUpdating.AjaxUpdating.js", "text/javascript", PerformSubstitution = true)]
[assembly: WebResource("SGI.UI.WebControls.AjaxUpdating.Default.gif", "image/gif")]

namespace EC.UI.WebControls
{
    [ToolboxData("<{0}:AjaxUpdating Runat=\"server\"></{0}:AjaxUpdating>")]
    [Designer(typeof(ControlDesigner))]
    public class AjaxUpdating : Control
    {
        [UrlProperty, Editor(typeof(ImageUrlEditor), typeof(UITypeEditor)), Bindable(true), DefaultValue("")]
        public string ImageUrl
        {
            get
            {
                string o = (string)ViewState["ImageUrl"];
                if (o != null)
                    return o;
                return String.Empty;
            }
            set
            {
                ViewState["ImageUrl"] = value;
            }
        }

        public string Text
        {
            get
            {
                string o = (string)ViewState["Text"];
                if (o != null)
                    return o;
                return "Atualizando informações...";
            }
            set
            {
                ViewState["Text"] = value;
            }
        }

        public bool ShowTimer
        {
            get { return EC.Common.Library.ToBoolean(ViewState["ShowTimer"]); }
            set { ViewState["ShowTimer"] = value; }
        }

        public string CssClass
        {
            get
            {
                string o = (string)ViewState["CssClass"];
                if (o != null)
                    return o;
                return "ajax-default";
            }
            set
            {
                ViewState["CssClass"] = value;
            }
        }

        public string CssTimer
        {
            get
            {
                string o = (string)ViewState["CssTimer"];
                if (o != null)
                    return o;
                return "ajax-updating-timer";
            }
            set
            {
                ViewState["CssTimer"] = value;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            RegisterCss("SGI.UI.WebControls.AjaxUpdating.AjaxUpdating.css");

            if (!Page.ClientScript.IsClientScriptIncludeRegistered("WCAU"))
                Page.ClientScript.RegisterClientScriptInclude("WCAU", Page.ClientScript.GetWebResourceUrl(GetType(), "SGI.UI.WebControls.AjaxUpdating.AjaxUpdating.js"));

        }

        protected override void Render(HtmlTextWriter writer)
        {
            string urlImage = Page.ClientScript.GetWebResourceUrl(GetType(), "SGI.UI.WebControls.AjaxUpdating.Default.gif");

            if (!string.IsNullOrEmpty(ImageUrl))
                urlImage = ImageUrl;

            writer.Write("<div id=\"ajax-updating\" class=\"" + CssClass + "\" style=\"display:none\">");

            writer.Write("<img src=\"" + urlImage.Replace("~/", "") + "\" alt=\"" + Text + "\" /><br />");
            writer.Write(Text);
            //Timer
            writer.Write("<div id=\"ajax-updating-timer\" class=\"" + CssTimer + "\" style=\"display:" + (ShowTimer ? "''" : "none") + ";\"></div>");

            writer.Write("</div>");
        }
    }
}
