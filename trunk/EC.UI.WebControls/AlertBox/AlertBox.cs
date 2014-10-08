using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.Design;
using EC.Common;

[assembly: WebResourceAttribute("EC.UI.WebControls.AlertBox.AlertBox.js", "text/javascript", PerformSubstitution = true)]
[assembly: WebResourceAttribute("EC.UI.WebControls.AlertBox.AlertBox.css", "text/css", PerformSubstitution = true)]

namespace EC.UI.WebControls
{
    [ToolboxData("<{0}:Alert Runat=\"server\"></{0}:Alert>")]
    [Serializable()]
    [Designer(typeof(MessageBoxDesigner))]
    public class AlertBox : Control
    {
        #region Variables
        private short _timeout = 30;
        private bool _showed = false;
        #endregion Variables

        public AlertBox()
        {
            CssClass = "alertBox";
        }

        #region Properties
        /// <summary>
        /// Close alert on click
        /// </summary>
        [Category("Behavior")]
        public bool CloseOnClick
        {
            get
            {
                object o = ViewState["CloseOnClick"];
                if (o == null)
                    ViewState["CloseOnClick"] = true;
                return Convert.ToBoolean(ViewState["CloseOnClick"]);
            }
            set { ViewState["CloseOnClick"] = value; }
        }

        /// <summary>
        /// Auto close alert
        /// </summary>
        [Category("Behavior")]
        public bool AutoClose
        {
            get
            {
                object o = ViewState["AutoClose"];
                if (o == null)
                    ViewState["AutoClose"] = true;
                return Convert.ToBoolean(ViewState["AutoClose"]);
            }
            set { ViewState["AutoClose"] = value; }
        }

        /// <summary>
        /// Text of alert
        /// </summary>
        public string Text
        {
            get { return Convert.ToString(ViewState["Text"]); }
            set { ViewState["Text"] = value; }
        }

        /// <summary>
        /// Css Class 
        /// </summary>
        public string CssClass
        {
            get
            {
                object o = (string)ViewState["CssClass"];
                if (o != null)
                    return o.ToString();
                return "alertbox";
            }
            set { ViewState["CssClass"] = value; }
        }

        /// <summary>
        /// Timeout for close alert (in seconds)
        /// </summary>
        [Category("Behavior")]
        public short Timeout
        {
            get
            {
                return _timeout;
            }
            set { _timeout = value; }
        }

        //
        // Summary:
        //     Gets or sets a value that indicates whether a server control is rendered
        //     as UI on the page.
        //
        // Returns:
        //     true if the control is visible on the page; otherwise false.
        [DefaultValue(false)]
        [Bindable(true)]
        public override bool Visible
        {
            get
            {
                return base.Visible;
            }
            set
            {
                base.Visible = value;
            }
        }
        #endregion Properties
        
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if(!_showed)
                Visible = false;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            RegisterCss("EC.UI.WebControls.AlertBox.AlertBox.css");

            if (!Page.ClientScript.IsClientScriptIncludeRegistered("WCAB"))
                Page.ClientScript.RegisterClientScriptInclude("WCAB", Page.ClientScript.GetWebResourceUrl(GetType(), "EC.UI.WebControls.AlertBox.AlertBox.js"));

        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (AutoClose)
                RegisterScript("ca_" + ClientID, ScriptCloseAlert());
        }

        protected override void Render(HtmlTextWriter writer)
        {
            string eventCloseOnClick = " onclick=\"__sgiCAB('" + ClientID + "');\"";
            if (!CloseOnClick)
                eventCloseOnClick = string.Empty;

            string cssClass = string.Empty;
            if (!string.IsNullOrEmpty(CssClass))
                cssClass = string.Format(" class=\"{0}\"", CssClass);

            writer.Write(string.Format("<div id=\"{0}\"{1}{2}>", ClientID, cssClass, eventCloseOnClick));
            writer.Write(Text);
            writer.Write("</div>\n");
        }

        private string ScriptCloseAlert()
        {
            StringBuilder writer = new StringBuilder();

            writer.Append("function __sgiCAB" + ClientID + "() {\n");
            writer.Append("    __sgiCAB('"+ ClientID +"');\n");
            writer.Append("}\n");

            if (AutoClose)
                writer.Append("setTimeout(\"__sgiCAB" + ClientID + "()\", " + Timeout.ToString() + "000);\n");

            return writer.ToString();
        }

        public void Show()
        {
            Show(Text, null);
        }

        public void Show(string text)
        {
            Show(text, null);
        }

        public void Show(EC.Common.Alert alert)
        {
            Show(alert.GetDescription());
        }

        public void Show(int idAlert)
        {
            Show(EC.Common.Alert.GetDescription(Library.FormatIntWithZero(idAlert, 3)));
        }

        public void Show(AlertList alertList)
        {
            Text = string.Empty;
            foreach (EC.Common.Alert a in alertList)
                Text += a.GetDescription() + "<br />";

            if (!string.IsNullOrEmpty(Text))
                Text = Text.Substring(0, Text.Length - 6);

            Visible = true;
        }

        public void Show(string text, params object[] args)
        {
            if (!Library.isNumeric(text))
                Text = text;
            else
            {
                if (args != null && args.Length > 0)
                    Text = EC.Common.Alert.GetDescription(text, args);
                else
                    Text = EC.Common.Alert.GetDescription(text);
            }

            Visible = _showed = true;
        }

    }
}
