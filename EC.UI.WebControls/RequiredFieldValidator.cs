using System;
using System.ComponentModel;
using System.Web.UI;
using EC.Common;
using System.Web.UI.Design;
using System.Drawing.Design;

namespace EC.UI.WebControls
{
        [ToolboxData("<{0}:RangeValidator runat=\"server\"></{0}:RangeValidator>")]
    public class RequiredFieldValidator : System.Web.UI.WebControls.RequiredFieldValidator
    {
        // Fields
        private string _errorImageUrl = string.Empty;

        // Methods
        protected override void OnInit(EventArgs e)
        {
            if (string.IsNullOrEmpty(ErrorImageUrl) & string.IsNullOrEmpty(ErrorMessage))
            {
                ErrorImageUrl = "~/images/obrigatorio.gif";
                ErrorMessage = "<img src=\"" + ResolveClientUrl(this.ErrorImageUrl) + "\" class=\"" + this.CssErrorImageUrl + "\" alt=\"\" />";
            }

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
