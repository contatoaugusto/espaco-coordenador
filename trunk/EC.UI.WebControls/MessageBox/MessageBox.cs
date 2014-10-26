using System;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.ComponentModel;
using System.Text;
using EC.Common;

#region Styles
[assembly: WebResourceAttribute("EC.UI.WebControls.MessageBox.Styles.Outlook.css", "text/css")]
[assembly: WebResourceAttribute("EC.UI.WebControls.MessageBox.Styles.UniCEUB.css", "text/css")]
#endregion Styles

#region Images
[assembly: WebResource("EC.UI.WebControls.MessageBox.MessageBox-Close.gif", "image/gif")]
[assembly: WebResource("EC.UI.WebControls.MessageBox.MessageBox-Information.gif", "image/gif")]
[assembly: WebResource("EC.UI.WebControls.MessageBox.MessageBox-Question.gif", "image/gif")]
[assembly: WebResource("EC.UI.WebControls.MessageBox.MessageBox-Warning.gif", "image/gif")]
[assembly: WebResource("EC.UI.WebControls.MessageBox.MessageBox-Error.gif", "image/gif")]
#endregion Images

#region JavaScript
//[assembly: WebResourceAttribute("EC.UI.WebControls.jQuery.jQuery.js", "text/javascript")]
[assembly: WebResourceAttribute("EC.UI.WebControls.jQuery.jquery.blockUI.js", "text/javascript")]
#endregion JavaScript

namespace EC.UI.WebControls
{
    [ToolboxData("<{0}:MessageBox Runat=\"server\"></{0}:MessageBox>")]
    [Serializable()]
    [Designer(typeof(MessageBoxDesigner))]
    public class MessageBox : Control, IPostBackEventHandler
    {
        #region Variables
        private short _timeout = 5000;
        private string _cssClass = "mb-main";
        private string _cssClassHeader = "mb-header";
        private string _cssClassHeaderCaption = "mb-header-caption";
        private string _cssClassHeaderIcon = "mb-header-icon";
        private string _cssClassContent = "mb-content";
        private string _cssClassContentText = "mb-content-text";
        private string _cssClassContentIcon = "mb-content-icon";
        private string _cssClassContentButtons = "mb-content-buttons";
        private string _cssClassContentButton = "mb-content-button";
        private string _onConfirmClientClick = "";
        private string _onCancelClientClick = "";
        private string _onCustomClientClick = "";
        private string _onCloseClientClick = "";
        private bool _cancel = false;
        private bool _buttonConfirmDisableOnClick = true;

        public event EventHandler ConfirmClick;
        public event EventHandler CancelClick;
        public event EventHandler CustomClick;
        public event EventHandler CloseClick;
        #endregion Variables

        public MessageBox()
        {
            CssClass = "mb-main";
            Visible = false;
        }

        #region Properties
        /// <summary>
        /// BackgroundColor of MessageBox. 
        /// Cor de Fundo do MessageBox. 
        /// </summary>
        public string BackgroundColor
        {
            get
            {
                if (ViewState["BackgroundColor"] == null)
                    ViewState["BackgroundColor"] = "#ffffff";

                return (string)ViewState["BackgroundColor"];
            }
            set
            {
                ViewState["BackgroundColor"] = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool ButtonConfirmDisableOnClick
        {
            get { return _buttonConfirmDisableOnClick; }
            set { _buttonConfirmDisableOnClick = value; }
        }

        /// <summary>
        /// Opacity of MessageBox. Value: 10% of 100%
        /// Transparência do MessageBox. Valor: 10% a 100%
        /// </summary>
        public double Opacity
        {
            get
            {
                if (ViewState["Opacity"] == null)
                    ViewState["Opacity"] = 70;

                return Convert.ToDouble(ViewState["Opacity"]);
            }
            set
            {
                ViewState["Opacity"] = value;
            }
        }
        /// <summary>
        /// Styles of MessageBox
        /// Estilos do MessageBox
        /// </summary>
        public MessageBoxStyle Skin
        {
            get
            {
                if (ViewState["MessageBoxSkin"] == null)
                    ViewState["MessageBoxSkin"] = MessageBoxStyle.Outlook;
                return (MessageBoxStyle)ViewState["MessageBoxSkin"];
            }
            set { ViewState["MessageBoxSkin"] = value; }
        }
        /// <summary>
        /// Styles of MessageBox
        /// Estilos do MessageBox
        /// </summary>
        [Obsolete("A propriedade Style está absoleta. Utilize o Skin.")]
        public MessageBoxStyle Style
        {
            get
            {
                if (ViewState["MessageBoxStyle"] == null)
                    ViewState["MessageBoxStyle"] = MessageBoxStyle.Outlook;
                return (MessageBoxStyle)ViewState["MessageBoxStyle"];
            }
            set { ViewState["MessageBoxSkin"] = value; }
        }
        /// <summary>
        /// Cancels the closing
        /// Cancela o fechamento
        /// </summary>
        public bool Cancel
        {
            get
            {
                return _cancel;
            }
            set { _cancel = value; }
        }
        /// <summary>
        /// Shows the custom button
        /// Mostra o botão customizado
        /// </summary>
        public bool ShowCustomButton
        {
            get
            {
                return Convert.ToBoolean(ViewState["ShowCustomButton"]);
            }
            set { ViewState["ShowCustomButton"] = value; }
        }
        ///// <summary>
        ///// Close by clicking
        ///// Fecha ao clicar
        ///// </summary>
        //public bool CloseOnClick
        //{
        //    get
        //    {
        //        return Convert.ToBoolean(ViewState["CloseOnClick"]);
        //    }
        //    set { ViewState["CloseOnClick"] = value; }
        //}

        /// <summary>
        /// Type of MessageBox
        /// Tipo do MessageBox
        /// </summary>
        public MessageBoxType Type
        {
            get
            {
                if (ViewState["Type"] == null)
                    ViewState["Type"] = MessageBoxType.Information;

                return (MessageBoxType)ViewState["Type"];
            }
            set
            {
                ViewState["Type"] = value;
            }
        }
        /// <summary>
        /// The MessageBox is closed in accordance with the timeout
        /// O MessageBox é fechado de acordo com o tempo especificado
        /// </summary>
        public bool AutoClose
        {
            get
            {
                return Convert.ToBoolean(ViewState["AutoClose"]);
            }
            set { ViewState["AutoClose"] = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Text
        {
            get
            {
                return Convert.ToString(ViewState["Text"]);
            }
            set { ViewState["Text"] = value; }
        }
        public string Caption
        {
            get
            {
                return Convert.ToString(ViewState["Caption"]);
            }
            set { ViewState["Caption"] = value; }
        }
        [Category("Behavior")]
        public short Timeout
        {
            get
            {
                return _timeout;
            }
            set { _timeout = value; }
        }
        [Category("Behavior")]
        public string OnCancelClientClick
        {
            get
            {
                return _onCancelClientClick;
            }
            set { _onCancelClientClick = value; }
        }
        [Category("Behavior")]
        public string OnConfirmClientClick
        {
            get
            {
                return _onConfirmClientClick;
            }
            set { _onConfirmClientClick = value; }
        }
        [Category("Behavior")]
        public string OnCustomClientClick
        {
            get
            {
                return _onCustomClientClick;
            }
            set { _onCustomClientClick = value; }
        }
        [Category("Behavior")]
        public string OnCloseClientClick
        {
            get
            {
                return _onCloseClientClick;
            }
            set { _onCloseClientClick = value; }
        }

        #region Css
        [Category("Style")]
        public string CssClass
        {
            get
            {
                return _cssClass;
            }
            set { _cssClass = value; }
        }
        [Category("Style")]
        public string CssClassHeader
        {
            get
            {
                return _cssClassHeader;
            }
            set { _cssClassHeader = value; }
        }
        [Category("Style")]
        public string CssClassHeaderIcon
        {
            get
            {
                return _cssClassHeaderIcon;
            }
            set { _cssClassHeaderIcon = value; }
        }
        [Category("Style")]
        public string CssClassHeaderCaption
        {
            get
            {
                return _cssClassHeaderCaption;
            }
            set { _cssClassHeaderCaption = value; }
        }
        [Category("Style")]
        public string CssClassContentButtons
        {
            get
            {
                return _cssClassContentButtons;
            }
            set { _cssClassContentButtons = value; }
        }
        [Category("Style")]
        public string CssClassContent
        {
            get
            {
                return _cssClassContent;
            }
            set { _cssClassContent = value; }
        }
        [Category("Style")]
        public string CssClassContentButton
        {
            get
            {
                return _cssClassContentButton;
            }
            set { _cssClassContentButton = value; }
        }
        [Category("Style")]
        public string CssClassContentIcon
        {
            get
            {
                return _cssClassContentIcon;
            }
            set { _cssClassContentIcon = value; }
        }
        [Category("Style")]
        public string CssClassContentText
        {
            get
            {
                return _cssClassContentText;
            }
            set { _cssClassContentText = value; }
        }
        #endregion Css

        #region Button Text
        public string ButtonConfirmText
        {
            get
            {
                if (ViewState["ButtonConfirmText"] == null)
                    //ViewState["ButtonConfirmText"] = "Ok";
                    ViewState["ButtonConfirmText"] = "Sim";
                return Convert.ToString(ViewState["ButtonConfirmText"]);
            }
            set { ViewState["ButtonConfirmText"] = value; }
        }
        public string ButtonCancelText
        {
            get
            {
                if (ViewState["ButtonCancelText"] == null)
                    ViewState["ButtonCancelText"] = "Não";
                    //ViewState["ButtonCancelText"] = "Cancelar";
                return Convert.ToString(ViewState["ButtonCancelText"]);
            }
            set { ViewState["ButtonCancelText"] = value; }
        }
        public string ButtonCustomText
        {
            get
            {
                if (ViewState["ButtonCustomText"] == null)
                    ViewState["ButtonCustomText"] = "Custom";
                return Convert.ToString(ViewState["ButtonCustomText"]);
            }
            set { ViewState["ButtonCustomText"] = value; }
        }
        #endregion Button Text

        #endregion Properties

        public void RaisePostBackEvent(string eventArgument)
        {
            if (eventArgument.Equals("ConfirmClick"))
            {
                if (ConfirmClick != null)
                    ConfirmClick.Invoke(this, EventArgs.Empty);
            }
            else if (eventArgument.Equals("CancelClick"))
            {
                if (CancelClick != null)
                    CancelClick.Invoke(this, EventArgs.Empty);
            }
            else if (eventArgument.Equals("CustomClick"))
            {
                if (CustomClick != null)
                    CustomClick.Invoke(this, EventArgs.Empty);
            }
            else if (eventArgument.Equals("CloseClick"))
            {
                if (CloseClick != null)
                    CloseClick.Invoke(this, EventArgs.Empty);
            }

            if (!Cancel)
                Close();

        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (string.IsNullOrEmpty(OnCloseClientClick))
                OnCloseClientClick = "__sgiMBC();return false;";

            writer.Write("    <table id=\"" + ClientID + "\" class=\"" + CssClass + "\" align=\"center\">\n");
            writer.Write("        <tr class=\"" + CssClassHeader + "\">\n");
            writer.Write("            <td class=\"" + CssClassHeaderCaption + "\">\n");
            writer.Write(Caption.Trim());
            writer.Write("            </td>\n");
            writer.Write("            <td class=\"" + CssClassHeaderIcon + "\">\n");

            string onClick = string.Empty;
            string closeImage = Page.ClientScript.GetWebResourceUrl(GetType(), "../EC.UI.WebControls/MessageBox/MessageBox-Close.gif");
            onClick = (!string.IsNullOrEmpty(OnCloseClientClick) ? OnCloseClientClick : Page.ClientScript.GetPostBackEventReference(this, "CloseClick"));

            writer.Write("                <img style=\"cursor:hand;\" onclick=\"" + onClick + "\" src=\"" + closeImage.Replace("~/", "") + "\" alt=\"\" />\n");

            writer.Write("            </td>\n");
            writer.Write("        </tr>\n");
            writer.Write("        <tr>\n");
            writer.Write("            <td colspan=\"2\" style=\"padding: 3px;\">\n");
            writer.Write("                <table class=\"" + CssClassContent + "\" width=\"100%\">\n");
            writer.Write("                    <tr>\n");
            writer.Write("                        <td class=\"" + CssClassContentIcon + "\">\n");

            string iconImage = Page.ClientScript.GetWebResourceUrl(GetType(), "../EC.UI.WebControls/MessageBox/MessageBox-" + Type.ToString() + ".gif");

            writer.Write("                            <img src=\"" + iconImage.Replace("~/", "") + "\" alt=\"" + Type.ToString() + "\" />\n");

            writer.Write("                        </td>\n");
            writer.Write("                        <td class=\"" + CssClassContentText + "\">\n");
            writer.Write(Text.Trim());
            writer.Write("                        </td>\n");
            writer.Write("                    </tr>\n");
            writer.Write("                    <tr>\n");
            writer.Write("                        <td class=\"mb-content-buttons\" colspan=\"2\">\n");

            string buttonID = string.Empty;
            string buttonEvents = "onmouseover=\"this.className='mb-content-button-over'\" onmousedown=\"this.className='mb-content-button-down'\" onmouseout=\"this.className='mb-content-button'\" ";

            if (ShowCustomButton)
            {
                onClick = (!string.IsNullOrEmpty(OnCustomClientClick) ? OnCustomClientClick : Page.ClientScript.GetPostBackEventReference(this, "CustomClick"));
                onClick = EnsureEndsWithSemicolon(onClick);
                buttonID = string.Format("{0}_ButtonCustomText", ClientID);
                writer.Write("<input type=\"submit\" name=\"" + buttonID + "\" value=\"" + ButtonCustomText +
                             "\" id=\"" + buttonID + "\" class=\"mb-content-button\" onclick=\"" + onClick + "\" " + buttonEvents + "/>&nbsp;&nbsp;");
            }

            onClick = (!string.IsNullOrEmpty(OnConfirmClientClick) ? OnConfirmClientClick : GetDisableButtonJavaScript());
            onClick = EnsureEndsWithSemicolon(onClick);
            buttonID = string.Format("{0}_ButtonConfirmText", ClientID);
            writer.Write("<input type=\"submit\" name=\"" + buttonID + "\" value=\"" + ButtonConfirmText +
                "\" id=\"" + buttonID + "\" class=\"mb-content-button\" onclick=\"" + onClick + "\" " + buttonEvents + "/>");

            if (Type == MessageBoxType.Question)
            {
                onClick = (!string.IsNullOrEmpty(OnCancelClientClick) ? OnCancelClientClick : Page.ClientScript.GetPostBackEventReference(this, "CancelClick"));
                onClick = EnsureEndsWithSemicolon(onClick);
                buttonID = string.Format("{0}_ButtonCancelText", ClientID);
                writer.Write("&nbsp;&nbsp;<input type=\"submit\" name=\"" + buttonID + "\" value=\"" + ButtonCancelText +
                             "\" id=\"" + buttonID + "\" class=\"mb-content-button\" onclick=\"" + onClick + "\" " + buttonEvents + "/>");
            }

            writer.Write("                        </td>\n");
            writer.Write("                    </tr>\n");
            writer.Write("                </table>\n");
            writer.Write("            </td>\n");
            writer.Write("        </tr>\n");
            writer.Write("    </table>\n");
            //writer.Write("</div>\n");
        }

        public string GetDisableButtonJavaScript()
        {
            string disabledText = "Processando...";
            disabledText = disabledText.Replace("'", "");

            StringBuilder sb = new StringBuilder();
            sb.Append("this.disabled='true';this.value='" + disabledText + "';");

            sb.Append(Page.ClientScript.GetPostBackEventReference(this, "ConfirmClick"));

            sb.Append(";");


            return sb.ToString();
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            RegisterScript("WCMBC", ScriptCloseMessageBox());

            if (Visible)
                RegisterScript("WCMBS", ScriptShowMessageBox());
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            RegisterCss(string.Format("EC.UI.WebControls.MessageBox.Styles.{0}.css", Skin.ToString()));
            
            //if (!Page.ClientScript.IsClientScriptIncludeRegistered("JQUERY"))
            //    Page.ClientScript.RegisterClientScriptInclude("JQUERY", Page.ClientScript.GetWebResourceUrl(GetType(), "EC.UI.WebControls.jQuery.jquery-1.2.6.js"));

            if (!Page.ClientScript.IsClientScriptIncludeRegistered("BLOCKUI"))
                Page.ClientScript.RegisterClientScriptInclude("BLOCKUI", Page.ClientScript.GetWebResourceUrl(GetType(), "EC.UI.WebControls.jQuery.jquery.blockUI.js"));
        }

        private string ScriptCloseMessageBox()
        {
            StringBuilder writer = new StringBuilder();

            writer.Append("function __sgiMBC() {\n");
            writer.Append("    $(document).ready(function() { \n");
            writer.Append("        $.unblockUI();  \n");
            writer.Append("        $('#" + ClientID + "').hide();  \n");
            writer.Append("    }); \n");

            writer.Append("}\n");

            writer.Append("function CloseMessageBox() {\n");
            writer.Append("     __sgiMBC();\n");
            writer.Append("}\n");

            if (AutoClose)
                writer.Append("    setTimeout(\"__sgiMBC()\", " + Timeout.ToString() + ");\n");

            return writer.ToString();
        }

        private string ScriptShowMessageBox()
        {
            StringBuilder writer = new StringBuilder();

            writer.Append("$(document).ready(function() { \n");

            writer.Append("    $.blockUI.defaults.fadeOut = 0; \n");
            writer.Append("    $.blockUI.defaults.css.border = 0; \n");
            writer.Append("    $.blockUI.defaults.css.width = 'auto'; \n");
            writer.Append("    $.blockUI.defaults.overlayCSS.backgroundColor = '" + BackgroundColor + "';\n");

            string opacity = string.Format("{0:0.0}", (Opacity / 100)).Replace(",", ".");

            writer.Append("    $.blockUI.defaults.overlayCSS.opacity = '" + opacity + "';\n");

            writer.Append("    $.blockUI.defaults.allowBodyStretch = true;\n");
            writer.Append("    $.blockUI.defaults.applyPlatformOpacityRules = false;\n");

            writer.Append("    $.blockUI({ message: $(\"#" + ClientID + "\") });\n");

            writer.Append("}); \n");

            return writer.ToString();
        }

        #region Show

        public void ShowInformation(int rule, params string[] arguments)
        {
            Show(BusinessRules.GetRule(rule), "Informação", MessageBoxType.Information, arguments);
        }

        public void ShowInformation(int rule, string buttonConfirmText, params string[] arguments)
        {
            Show(BusinessRules.GetRule(rule), buttonConfirmText, "Informação", MessageBoxType.Information, arguments);
        }

        public void ShowError(int rule, params string[] arguments)
        {
            Show(BusinessRules.GetRule(rule), "Erro", MessageBoxType.Error);
        }

        public void ShowWarning(int rule, params string[] arguments)
        {
            Show(BusinessRules.GetRule(rule), "Atenção", MessageBoxType.Warning);
        }

        public void ShowQuestion(int rule, params string[] arguments)
        {
            Show(BusinessRules.GetRule(rule), "Confirmação", MessageBoxType.Question, "Sim", "Não");
        }

        public void ShowQuestion(int rule, string buttonConfirmText, string buttonCancelText, params string[] arguments)
        {
            Show(BusinessRules.GetRule(rule), "Confirmação", MessageBoxType.Question, buttonConfirmText, buttonCancelText, arguments);
        }

        public void Show()
        {
            Visible = true;
        }

        public void Show(string text, params object[] argsOfText)
        {
            Show(text, Caption, Type, ButtonConfirmText, ButtonCancelText, ButtonCustomText, argsOfText);
        }

        public void Show(string text)
        {
            Show(text, Caption);
        }

        public void Show(string text, string caption)
        {
            Show(text, caption, Type);
        }

        public void Show(string text, string caption, params object[] argsOfText)
        {
            Show(text, caption, Type, ButtonConfirmText, ButtonCancelText, ButtonCustomText, argsOfText);
        }

        public void Show(string text, string caption, MessageBoxType type)
        {
            Show(text, caption, type, ButtonConfirmText);
        }

        public void Show(string text, string caption, MessageBoxType type, params object[] argsOfText)
        {
            Show(text, caption, type, ButtonConfirmText, ButtonCancelText, ButtonCustomText, argsOfText);
        }

        public void Show(string text, string caption, MessageBoxType type, string buttonConfirmText)
        {
            Show(text, caption, type, buttonConfirmText, ButtonCancelText, ButtonCustomText);
        }

        public void Show(string text, string caption, MessageBoxType type, string buttonConfirmText, params object[] argsOfText)
        {
            Show(text, caption, type, buttonConfirmText, ButtonCustomText, argsOfText);
        }

        public void Show(string text, string caption, MessageBoxType type, string buttonConfirmText, string buttonCancelText)
        {
            Show(text, caption, type, buttonConfirmText, buttonCancelText, ButtonCustomText);
        }

        public void Show(string text, string caption, MessageBoxType type, string buttonConfirmText, string buttonCancelText, params object[] argsOfText)
        {
            Show(text, caption, type, buttonConfirmText, buttonCancelText, ButtonCustomText, argsOfText);
        }

        public void Show(string text, string caption, MessageBoxType type, string buttonConfirmText, string buttonCancelText, string buttonCustomText, params object[] argsOfText)
        {
            if (Library.isNumeric(text))
            {
                if (argsOfText != null && argsOfText.Length > 0)
                    Text = EC.Common.Alert.GetDescription(text, argsOfText);
                else
                    Text = EC.Common.Alert.GetDescription(text);
            }
            else
            {
                if (argsOfText != null && argsOfText.Length > 0)
                {
                    Text = String.Format(text, argsOfText);
                }
                else
                    Text = text;
            }

            Caption = caption;
            Type = type;
            ButtonConfirmText = buttonConfirmText;
            ButtonCancelText = buttonCancelText;
            ButtonCustomText = buttonCustomText;
            Visible = true;
        }
        #endregion Show

        public void Close()
        {
            Visible = false;
        }
    }
}
