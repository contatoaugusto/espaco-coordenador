using System;
using System.ComponentModel;
using System.Web.UI;
using System.Drawing.Design;
using System.Web.UI.Design;
using EC.Common;

namespace EC.UI.WebControls
{
    [ToolboxData("<{0}:RangeValidator runat=\"server\"></{0}:RangeValidator>")]
    public class RangeValidator : System.Web.UI.WebControls.RangeValidator
    {
        // Fields
        private string _errorImageUrl = string.Empty;

        // Methods
        protected override void OnInit(EventArgs e)
        {
            if (string.IsNullOrEmpty(ErrorImageUrl))
                ErrorImageUrl = "~/images/obrigatorio.gif";

            if (Library.ToInteger(MinimumValue) == 0)
                MinimumValue = "1";

            if (Library.ToInteger(MaximumValue) == 0)
                MaximumValue = "99999";

            ErrorMessage = "<img src=\"" + ResolveClientUrl(this.ErrorImageUrl) + "\" class=\"" + this.CssErrorImageUrl + "\" alt=\"\" />";

            base.OnInit(e);
        }

        // Properties
        [DefaultValue(""), Category("Custom")]
        public string CssErrorImageUrl
        {
            get
            {
                if (this.ViewState["CssErrorImageUrl"] == null)
                {
                    return "errorimageurl";
                }
                return this.ViewState["CssErrorImageUrl"].ToString();
            }
            set
            {
                this.ViewState["CssErrorImageUrl"] = value;
            }
        }

        [DefaultValue(""), Category("Custom"), UrlProperty, Bindable(true), Editor(typeof(ImageUrlEditor), typeof(UITypeEditor))]
        public string ErrorImageUrl
        {
            get
            {
                string str = (string)this.ViewState["ErrorImageUrl"];
                return ((str == null) ? string.Empty : str);
            }
            set
            {
                if (value == null)
                {
                    this.ViewState.Remove("ErrorImageUrl");
                }
                else
                {
                    this.ViewState["ErrorImageUrl"] = value;
                }
            }
        }
    }
}