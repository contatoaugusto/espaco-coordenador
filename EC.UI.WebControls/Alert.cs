using System;
using System.Drawing;
using System.Web.UI;
using System.ComponentModel;
using EC.Common;

[assembly: TagPrefix("EC.UI.WebControls", "sgi")]
[assembly: WebResourceAttribute("EC.UI.WebControls.Alert.css", "text/css", PerformSubstitution = true)]

namespace EC.UI.WebControls
{
    [ToolboxData("<{0}:Alert Runat=\"server\" Visible=\"false\"></{0}:Alert>")]
    [Serializable()]
    [Designer(typeof(AlertDesigner))]
    public class Alert : Control, IPostBackDataHandler, IPostBackEventHandler
    {
        #region Variables
        private StyleAlert _styleAlert = StyleAlert.SGI2005;
        private int _time = 5000;
        private AlertList _alertList;
        private EC.Common.Alert _alert;
        private bool _addTagTopBottom = false;
        private string _text = string.Empty;
        private string _buttonYesText = "Yes";
        private string _buttonNoText = "No";
        private string _buttonCancelText = "Cancel";
        private string _width = "50%";
        private bool _anchor = true;
        private bool _showConfirm = false;
        private Align _align = Align.Left;
        private bool _showButtomCancel = false;
        #endregion Variables

        #region Events
        public event EventHandler YesClick;
        public event EventHandler NoClick;
        public event EventHandler CancelClick;
        #endregion

        #region Constructs
        public Alert()
        {
            Text = "[" + ID + "]";
            Visible = false;
        }

        public Alert(string key)
        {
            ItemAlert = new EC.Common.Alert(key);
        }
        #endregion Constructs

        #region Properties
        [Description("Enable the anchor post of the page.")]
        public bool Anchor
        {
            get{return _anchor;}
            set { _anchor = value; }
        }
        [Description("Display icon in message.")]
        public bool AutoHide
        {
            get
            {
                object o = ViewState["AutoHide:Cache"];
                if(o == null)
                {
                    ViewState["AutoHide:Cache"] = true;
                    return true;
                }
                else
                    return Library.ToBoolean(o);
            }
            set { ViewState["AutoHide:Cache"] = value; }
        }
        [Description("Display icon in message.")]
        public bool ShowIcon
        {
            get { return Library.ToBoolean(ViewState["ShowIcon:Cache"]); }
            set { ViewState["ShowIcon:Cache"] = value; }
        }
        public string CommandArgument
        {
            get { return Library.ToString(ViewState["CommandArgument:Alert:Cache"]); }
            set { ViewState["CommandArgument:Alert:Cache"] = value; }
        }
        [Description("Type style control.")]
        public AlertType AlertType
        {
            get
            {
                object o = ViewState["AlertType:Cache"];
                if (o != null)
                    return (AlertType)o;
                else
                    return AlertType.Info;
            }
            set { ViewState["AlertType:Cache"] = value; }
        }
        [Description("Add tags <br /> in top e bottom.")]
        public bool AddTagTopBottom
        {
            get { return _addTagTopBottom; }
            set { _addTagTopBottom = value; }
        }
        public AlertList AlertList
        {
            get { return _alertList; }
            set { _alertList = value; }
        }

        public EC.Common.Alert ItemAlert
        {
            get { return _alert; }
            set { _alert = value; }
        }

        public StyleAlert StyleAlert
        {
            get { return _styleAlert; }
            set { _styleAlert = value; }
        }

        public Align Align
        {
            get { return _align; }
            set { _align = value; }
        }

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public bool ShowConfirm
        {
            get { return _showConfirm; }
            set { _showConfirm = value; }
        }

        public bool ShowButtomCancel
        {
            get { return _showButtomCancel; }
            set { _showButtomCancel = value; }
        }

        public string ButtonYesText
        {
            get { return _buttonYesText; }
            set { _buttonYesText = value; }
        }

        public string ButtonCancelText
        {
            get { return _buttonCancelText; }
            set { _buttonCancelText = value; }
        }

        public string ButtonNoText
        {
            get { return _buttonNoText; }
            set { _buttonNoText = value; }
        }
        public string CssText
        {
            get
            {
                object o = ViewState["CssText:Cache"];
                if (o != null)
                    return (string)o;
                else
                    return string.Empty;
            }
            set { ViewState["CssText:Cache"] = value; }
        }

        public string Width
        {
            get { return _width; }
            set { _width = value; }
        }
        #endregion Properties

        #region Methods
        public void RaisePostDataChangedEvent()
        {

        }

        public bool LoadPostData(string postDataKey, System.Collections.Specialized.NameValueCollection postCollection)
        {
            return true;
        }

        public void Bind()
        {
            Bind(null, ItemAlert, null, null);
        }

        //public void Bind(AlertList alertList)
        //{
        //    Bind(alertList, null, null, null);
        //}

        public void Bind(AlertList alertList, params string[] args)
        {
            Bind(alertList, null, null, args);
        }

        public void Bind(EC.Common.Alert alert)
        {
            Bind(null, alert, null, alert.Args);
        }

        //public void Bind(string key)
        //{
        //    Bind(null, null, key, null);
        //}
        public void Bind(string key, params string[] args)
        {
            Bind(null, null, key, args);
        }

        public void Bind(AlertList alertList, EC.Common.Alert alert, string key, params string[] args)
        {
            Visible = true;

            _alertList = alertList;

            if(_alertList == null) 
                _alertList = new AlertList();

            //if (alertList != null && alert != null && !String.IsNullOrEmpty(key))
            //    Text = string.Empty;

            if (alertList == null && alert == null && !String.IsNullOrEmpty(key))
            {
                alert = new EC.Common.Alert(key);
                if (args == null)
                {
                    if (StyleAlert == StyleAlert.SGI2005)
                            Text = "<span class=\"" + CssText + "\">" + alert.GetDescription() + "</span>";
                    else
                        Text = alert.Key + " - " + alert.Description;
                }
                else
                {
                    if (StyleAlert == StyleAlert.SGI2005)
                            Text = "<span class=\"" + CssText + "\">" + alert.Key + " - " + string.Format(alert.GetDescription(), args) +
                                   "</span>";
                    else
                        Text = alert.Key + " - " + string.Format(alert.GetDescription(), args);
                }
            }

            if (alertList == null && alert != null && String.IsNullOrEmpty(key))
            {
                if (args == null)
                {
                    if (StyleAlert == StyleAlert.SGI2005)
                            Text = "<span class=\"" + CssText + "\">" + alert.GetDescription() + "</span>";
                    else
                        Text = alert.Key + " - " + alert.GetDescription();
                }
                else
                {
                    if (StyleAlert == StyleAlert.SGI2005)
                            Text = "<span class=\"" + CssText + "\">" + string.Format(alert.GetDescription(), args) + "</span>";
                    else
                        Text = alert.Key + " - " + string.Format(alert.GetDescription(), args);
                }
            }

            if (alertList != null && alert == null && String.IsNullOrEmpty(key))
            {
                if (alertList.Count == 1)
                {
                    alert = alertList[0];
                    //if (alert.Args.Length > 0)
                    //    args = alert.Args;

                    if (args == null)
                    {
                        if (StyleAlert == StyleAlert.SGI2005)
                            Text = alert.Key + "&nbsp;-&nbsp;<span class=\"" + CssText + "\">&nbsp;" + alert.GetDescription() + "</span>";
                        else
                            Text = alert.Key + " - " + alert.GetDescription();
                    }
                    else
                    {
                        if (StyleAlert == StyleAlert.SGI2005)
                            Text = alert.Key + "&nbsp;-&nbsp;<span class=\"" + CssText + "\">&nbsp;" + string.Format(alert.GetDescription(), args) + "</span>";
                        else
                            Text = alert.Key + " - " + string.Format(alert.GetDescription(), args);
                    }
                }
                else
                {
                    for (int i = 0; i < alertList.Count; i++)
                    {
                        alert = alertList[i];

                        if (StyleAlert == StyleAlert.SGI2005)
                        {
                            if (alert.Args == null)
                                Text += alert.Key + "&nbsp;-&nbsp;<span class=\"" + CssText + "\">&nbsp;" + alert.GetDescription() + "</span><br />";
                            else
                                Text += alert.Key + "&nbsp;-&nbsp;<span class=\"" + CssText + "\">&nbsp;" + string.Format(alert.GetDescription(), alert.Args) + "</span><br />";
                        }
                        else
                        {
                            if (args != null)
                                Text = alert.Key + " - " + string.Format(alert.GetDescription(), args);
                            else
                                Text = alert.Key + " - " + alert.GetDescription();
                        }
                    }
                }
            }

            Show();
        }

        public void Hide()
        {
            Visible = false;
        }

        public void Show()
        {
            Visible = true;
        }

        public void RaisePostBackEvent(string eventArgument)
        {
            if (eventArgument.Equals("YesClick"))
                YesClick(this, EventArgs.Empty);
            else if (eventArgument.Equals("NoClick"))
                NoClick(this, EventArgs.Empty);
            else if (eventArgument.Equals("CancelClick"))
                CancelClick(this, EventArgs.Empty);
        }
        #endregion Methods

        protected override void Render(HtmlTextWriter writer)
        {
            string id = "alert" + AlertType.ToString().ToLower();
            if (StyleAlert == StyleAlert.SGI2005)
            {
                writer.Write("<a name=\"alert\"></a>\n");
                writer.Write("\t<div id=\"" + ClientID + "\" onclick=\"HideAlert();\">\n");

                if (AddTagTopBottom)
                    writer.Write("<br />\n");

                writer.Write(string.Format("<div id=\"{0}\" style=\"width:{1}\">\n", id, Width));
                writer.Write("\t<div class=\"text\">\n");
                writer.Write(Text.Trim());
                writer.Write("\t</div>\n");

                #region ShowConfirm

                if (ShowConfirm)
                {
                    writer.Write("\t<div class=\"separator\"></div>\n");
                    writer.Write("\t<div class=\"buttons\">\n");

                    //writer.Write("<input type=\"submit\" class=\"button\" name=\"ButtonYes\" value=\"" + ButtonYesText +
                    //             "\" id=\"ButtonYes\" onclick=\"" +
                    //             Page.ClientScript.GetPostBackEventReference(this, "YesClick") + "\"/>&nbsp;&nbsp;");
                    //writer.Write("<input type=\"submit\" class=\"button\" name=\"ButtonNo\" value=\"" + ButtonNoText +
                    //             "\" id=\"ButtonNo\" onclick=\"" +
                    //             Page.ClientScript.GetPostBackEventReference(this, "NoClick") + "\"/>");

                    writer.Write("<a id=\"lnkYes\" class=\"alertlink\" href=\"javascript:" +
                                 Page.ClientScript.GetPostBackEventReference(this, "YesClick") + ";\">" + ButtonYesText +
                                 "</a>&nbsp;&nbsp;&nbsp;&nbsp;");

                    writer.Write("<a id=\"lnkNo\" class=\"alertlink\" href=\"javascript:" +
                                 Page.ClientScript.GetPostBackEventReference(this, "NoClick") + ";\">" + ButtonNoText +
                                 "</a>");


                    if (ShowButtomCancel)
                        //writer.Write("&nbsp;&nbsp;<input type=\"submit\" class=\"button\" name=\"ButtonCancel\" value=\"" +
                        //             ButtonCancelText + "\" id=\"ButtonCancel\" onclick=\"" +
                        //             Page.ClientScript.GetPostBackEventReference(this, "CancelClick") + "\"/>");
                        writer.Write(
                            "&nbsp;&nbsp;&nbsp;&nbsp;<a id=\"lnkCancel\" class=\"alertlink\" href=\"javascript:" +
                            Page.ClientScript.GetPostBackEventReference(this, "CancelClick") + "\"/>" + ButtonCancelText +
                            "</a>");

                    writer.Write("\t</div>\n");
                }


                writer.Write("</div>");

                if (AddTagTopBottom)
                    writer.Write("<br />\n");

                #endregion ShowConfirm

                writer.Write("\t</div>\n");


                writer.Write("<script defer>\n");


                writer.Write("function HideAlert()\n");
                writer.Write("{\n");

                if (!ShowConfirm)
                {
                    writer.Write("\tdocument.getElementById(\"" + ClientID + "\").style.visibility='hidden';\n");
                    //writer.Write("\tdocument.getElementById(\"" + id + "\").style.top='0px';\n");
                    writer.Write("\tdocument.getElementById(\"" + ClientID + "\").style.position='absolute';\n");
                    writer.Write("\tdocument.getElementById(\"" + ClientID + "\").style.top='10px';\n");
                    writer.Write("\tdocument.getElementById(\"" + ClientID + "\").style.left='200px';\n");
                }
                writer.Write("}\n");

                if (Text.Trim().Length > 100)
                    _time = 10000;

                if (AutoHide)
                    writer.Write("setTimeout(\"HideAlert()\", " + _time.ToString() + ");\n");


                if (_anchor)
                    writer.Write("\twindow.location.hash='alert';\n");

                writer.Write("\n");
                writer.Write("</script>\n");
            }
            else
            {
                writer.Write("<script defer>\n");
                writer.Write("	function showError()\n");
                writer.Write("	{	try { \n");
                writer.Write("			window.parent.frames['Tree'].showConfirm('" + Text + "'," + _time.ToString() + ");");
                writer.Write("		} catch(e){\n");
                writer.Write("			alert('" + Text + "');\n");
                writer.Write("		}\n");
                writer.Write("	} \n");
                writer.Write("	showError();\n");
                writer.Write("	</script>\n");
            }
        }


        protected override void OnPreRender(EventArgs e)
        {
            //if (!Page.ClientScript.IsClientScriptIncludeRegistered("WebControlsAlert"))
            //    Page.ClientScript.RegisterClientScriptInclude("WebControlsAlert", Page.ClientScript.GetWebResourceUrl(GetType(), "EC.UI.WebControls.Alert.js"));

            if (StyleAlert == StyleAlert.SGI2005)
            {
                string alias = "EC.UI.WebControls.Alert.css";
                string csslink = "<link rel=\"stylesheet\" type=\"text/css\" href=\"" +
                                 Page.ClientScript.GetWebResourceUrl(this.GetType(), alias) + "\" />";

                LiteralControl include = new LiteralControl(csslink);
                this.Page.Header.Controls.Add(include);
            }

            base.OnPreRender(e);
        }
    }
    
    public enum StyleAlert
    {
        SGI2003,
        SGI2005
    }
}

