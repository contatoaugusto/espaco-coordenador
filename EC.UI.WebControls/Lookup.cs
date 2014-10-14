using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EC.UI.WebControls
{
    [ToolboxBitmap(typeof(HyperLink))]
    public class Lookup : HyperLink
    {
        public Lookup()
        {
            this.Text = "Lookup";
        }

        private ButtonType buttonType = ButtonType.Link;

        public event EventHandler Selected;

        #region Properties
        [Browsable(false)]
        public object Result
        {
            get
            {
                string key;
                key = this.ClientID + "_Result";
                return Page.Session[key];
            }
        }

        // TODO: Implement this shit
        [DefaultValue(ButtonType.Link)]
        public ButtonType ButtonType
        {
            get { return buttonType; }
            set
            {
                buttonType = value;
            }
        }

        [DefaultValue(false)]
        public bool DialogLocation
        {
            get
            {
                if (ViewState["DialogLocation"] != null)
                {
                    return (bool)ViewState["DialogLocation"];
                }
                else
                {
                    return false;
                }
            }
            set { ViewState["DialogLocation"] = value; }
        }

        public Unit DialogWidth
        {
            get
            {
                if (ViewState["DialogWidth"] != null)
                {
                    return (Unit)ViewState["DialogWidth"];
                }
                else
                {
                    return Unit.Empty;
                }
            }
            set { ViewState["DialogWidth"] = value; }
        }
        public Unit DialogHeight
        {
            get
            {
                if (ViewState["DialogHeight"] != null)
                {
                    return (Unit)ViewState["DialogHeight"];
                }
                else
                {
                    return Unit.Empty;
                }
            }
            set { ViewState["DialogHeight"] = value; }
        }
        public Unit DialogLeft
        {
            get
            {
                if (ViewState["DialogLeft"] != null)
                {
                    return (Unit)ViewState["DialogLeft"];
                }
                else
                {
                    return Unit.Empty;
                }
            }
            set { ViewState["DialogLeft"] = value; }
        }
        public Unit DialogTop
        {
            get
            {
                if (ViewState["DialogTop"] != null)
                {
                    return (Unit)ViewState["DialogTop"];
                }
                else
                {
                    return Unit.Empty;
                }
            }
            set { ViewState["DialogTop"] = value; }
        }

        //[UrlProperty, Editor(typeof(WebFormUrlEditor), typeof(UITypeEditor)), Bindable(true), DefaultValue("")]
        public string DialogNavigateUrl
        {
            get
            {
                string url = (string)this.ViewState["DialogNavigateUrl"];
                if (url != null)
                {
                    return url;
                }
                return String.Empty;
            }
            set
            {
                this.ViewState["DialogNavigateUrl"] = value;
            }
        }
        #endregion

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);
            writer.AddAttribute(HtmlTextWriterAttribute.Href, "javascript:void(0)");
        }

        protected override void OnPreRender(EventArgs e)
        {
            string separator;
            if (DialogNavigateUrl.ToString().IndexOf("?") != -1)
            {
                separator = "&";
            }
            else
            {
                separator = "?";
            }
            string format;
            format = "window.open('{0}{5}Parent={4}',null,'height={1},width={2},status=1,toolbar=0,menubar=0,location={3},resizable=1,scrollbars=1');";
            Attributes.Add("onclick",
                String.Format(CultureInfo.CurrentCulture, format, DialogNavigateUrl.ToString().Replace("~/", ""), DialogHeight.Value, DialogWidth.Value,
                Convert.ToByte(DialogLocation), this.ClientID, separator));

            base.OnPreRender(e);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            if (!DesignMode)
            {
                RenderPostBackScript(writer, this.Page.Form.UniqueID);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            if (this.Context.Request["__EVENTTARGET"] == this.ClientID &&
                this.Context.Request["__EVENTARGUMENT"] == "Selected")
            {
                // Raise the selected event
                if (Selected != null)
                    Selected(this, EventArgs.Empty);
            }
            base.OnLoad(e);
        }

        private void RenderPostBackScript(HtmlTextWriter writer, string formUniqueID)
        {
            Type t = typeof(Page);

            FieldInfo fi;
            fi = t.GetField("_fPostBackScriptRendered", BindingFlags.Instance | BindingFlags.NonPublic);

            bool rendered;
            rendered = (bool)fi.GetValue(this.Page);

            if (!rendered)
            {
                MethodInfo mi;
                mi = t.GetMethod("RegisterPostBackScript", BindingFlags.Instance | BindingFlags.NonPublic);
                mi.Invoke(this.Page, null);

                mi = t.GetMethod("RenderPostBackScript", BindingFlags.Instance | BindingFlags.NonPublic);
                mi.Invoke(this.Page, new object[] { writer, formUniqueID });
            }
        }

    }
}
