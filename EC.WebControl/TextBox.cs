using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using EC.Common;
using System.Text;

[assembly: TagPrefix("SGI.UI.WebControls", "sgi")]
[assembly: WebResource("SGI.UI.WebControls.TextBox.js", "text/javascript", PerformSubstitution = true)]
//[assembly: WebResource("SGI.UI.WebControls.TextBox.jQuery.jQuery.js", "text/javascript", PerformSubstitution = true)]
[assembly: WebResource("SGI.UI.WebControls.TextBox.Money.autoNumeric.js", "text/javascript", PerformSubstitution = true)]
[assembly: WebResource("SGI.UI.WebControls.TextBox.Money.metadata.js", "text/javascript", PerformSubstitution = true)]
//Datepicker scripts
[assembly: WebResource("SGI.UI.WebControls.Datepicker.jquery.ui.core.js", "text/javascript", PerformSubstitution = true)]
[assembly: WebResource("SGI.UI.WebControls.Datepicker.jquery.ui.datepicker.js", "text/javascript", PerformSubstitution = true)]

namespace EC.UI.WebControls
{
    [ToolboxData("<{0}:TextBox runat=\"server\"></{0}:TextBox>")]
    public class TextBox : System.Web.UI.WebControls.TextBox
    {
        #region Variables
        private RequiredFieldValidator _requeridFieldValidator = new RequiredFieldValidator();
        private string _cssFocus = "textboxfocus";
        private string _cssBlur = "textboxblur";
        private bool _allowNegative = false;
        private int _size = 0;
        private bool _selectOnEnter = false;
        private TextBoxType _textBoxType = TextBoxType.Text;
        public string _clientScript = "true";
        private bool _setFocusOnError = true;
        private bool _validateTextBoxTypeClientScript = true;
        private bool _innerLabel = false;
        private string _label = "";
        private int _decimalPlaces = 2;
        private bool _showSimbol = false;
        private DateTime? _dtInicial = null;
        private DateTime? _dtFinal = null;
        #endregion

        public TextBox()
        {
            this.CssClass = "textbox";
        }

        #region Properties
        public DateTime Date
        {
            get
            {
                if (TextBoxType != TextBoxType.Date)
                    throw new Exception("Date value invalid for " + TextBoxType.ToString());
                else
                {
                    return Library.ToDate(Text);
                }
            }
        }
        public int Int
        {
            get
            {
                return Library.ToInteger(Text);
            }
        }
        [Category("Custom"), DefaultValue("0.0")]
        public double Double
        {
            get
            {
                if (TextBoxType != TextBoxType.Money & TextBoxType != TextBoxType.Number)
                    throw new Exception("Double value invalid for " + TextBoxType.ToString());
                else
                    return Library.ToDouble(Text.Replace("$", "").Replace("R", ""));
            }
        }
        [Category("Custom"), DefaultValue("0.0")]
        public decimal Decimal
        {
            get
            {
                if (TextBoxType != TextBoxType.Money & TextBoxType != TextBoxType.Number)
                    throw new Exception("Decimal value invalid for " + TextBoxType.ToString());
                else
                    return Library.ToDecimal(Text.Replace("$", "").Replace("R", ""));
            }
        }
        [Category("SGI"), DefaultValue("")]
        public string Label
        {
            get { return _label; }
            set
            {
                _label = value;
                if (Text.Trim().Length == 0)
                    Text = Label;
            }
        }

        [Category("SGI"), DefaultValue(false)]
        public bool InnerLabel
        {
            get { return _innerLabel; }
            set { _innerLabel = value; }
        }
        [Category("SGI"), DefaultValue(false)]
        public bool RequiredField
        {
            get
            {
                if (ViewState["RequiredField"] == null)
                {
                    return false;
                }
                else
                {
                    return (bool)ViewState["RequiredField"];
                }
            }
            set { ViewState["RequiredField"] = value; }
        }

        [Category("SGI"), DefaultValue("")]
        public string MaskFormat
        {
            get { return Library.ToString(ViewState["MaskFormat"]); }
            set { ViewState["MaskFormat"] = value; }
        }

        [Category("SGI"), DefaultValue("")]
        public string NoMaskText
        {
            get { return ClearMask(Text); }
        }

        [Category("SGI"), DefaultValue(TextBoxType.Text)]
        public TextBoxType TextBoxType
        {
            get { return _textBoxType; }
            set { _textBoxType = value; }
        }

        [Category("SGI"), DefaultValue("true")]
        public bool IsEmpty
        {
            get { return (Text.Trim() == string.Empty ? true : false); }
        }

        [Category("SGI"), DefaultValue("")]
        public string CssBlur
        {
            get { return _cssBlur; }
            set { _cssBlur = value; }
        }

        [Category("SGI"), DefaultValue(true)]
        public bool SetFocusOnError
        {
            get { return _setFocusOnError; }
            set { _setFocusOnError = value; }
        }

        [Category("SGI"), DefaultValue(true)]
        public bool ValidateTextBoxTypeClientScript
        {
            get { return _validateTextBoxTypeClientScript; }
            set { _validateTextBoxTypeClientScript = value; }
        }

        [Category("SGI"), DefaultValue("")]
        public string CssFocus
        {
            get { return _cssFocus; }
            set { _cssFocus = value; }
        }

        [Category("SGI"), DefaultValue(false)]
        private RequiredFieldValidator RequiredFieldValidator
        {
            get { return _requeridFieldValidator; }
            set { _requeridFieldValidator = value; }
        }

        public override bool Enabled
        {
            get { return base.Enabled; }
            set { base.Enabled = RequiredFieldValidator.Enabled = value; }
        }

        [Category("SGI"), DefaultValue("")]
        public string ErrorMessage
        {
            get
            {
                object o = ViewState["ErrorMessage"];
                if (o != null)
                    return o.ToString();
                else
                    return string.Empty;

            }
            set { ViewState["ErrorMessage"] = value; }
        }

        [Category("SGI"), DefaultValue(true)]
        public string ClientScript
        {
            get { return _clientScript; }
            set { _clientScript = value; }
        }

        [Category("SGI"), DefaultValue(false)]
        public bool AllowNegative
        {
            get { return _allowNegative; }
            set { _allowNegative = value; }
        }

        [Category("SGI"), DefaultValue(0)]
        public int Size
        {
            get { return _size; }
            set { _size = value; }
        }

        [Category("SGI"), DefaultValue(false)]
        public bool SelectOnEnter
        {
            get { return _selectOnEnter; }
            set { _selectOnEnter = value; }
        }

        [Category("SGI")]
        public string Mask
        {
            set { Text = FormatMake(value); }
        }
        [Category("Custom"), DefaultValue("0.00")]
        public int DecimalPlaces
        {
            get { return _decimalPlaces; }
            set { _decimalPlaces = value; }
        }

        [Category("Money"), DefaultValue("")]
        public bool ShowMoneySimbol
        {
            get
            {

                if (_textBoxType != WebControls.TextBoxType.Money)
                    throw new Exception("O símbolo só é permitido para o TextBox do tipo Money.");
                return _showSimbol;
            }
            set { _showSimbol = value; }
        }

        [Category("SGI"), DefaultValue(null)]
        public DateTime? DtInicial
        {
            get { return _dtInicial; }
            set { _dtInicial = value; }
        }
        [Category("SGI"), DefaultValue(null)]
        public DateTime? DtFinal
        {
            get { return _dtFinal; }
            set { _dtFinal = value; }
        }
        #endregion

        private string ClearMask(string value)
        {
            if (TextBoxType != TextBoxType.Date)
            {
                value = value.Replace(".", "");
                value = value.Replace("/", "");
                value = value.Replace(":", "");
                if (!((TextBoxType == TextBoxType.Money) && AllowNegative))
                {
                    value = value.Replace("-", "");
                }
            }
            return value;
        }

        private string FormatMake(string value)
        {
            try
            {
                if (value.Trim() == "" | value.Length == 0)
                {
                    return value;
                }

                switch (TextBoxType)
                {
                    case TextBoxType.CEP:
                        return string.Format("{0}.{1}-{2}", value.Substring(0, 2), value.Substring(2, 3), value.Substring(5, 3));
                    case TextBoxType.CPF:
                        return string.Format("{0}.{1}.{2}-{3}", value.Substring(0, 3), value.Substring(3, 3), value.Substring(6, 3), value.Substring(9, 2));
                    case TextBoxType.CNPJ:
                        return string.Format("{0}.{1}.{2}/{3}-{4}", value.Substring(0, 2), value.Substring(2, 3), value.Substring(5, 3), value.Substring(8, 4), value.Substring(12, 2));
                    case TextBoxType.Date:
                        return FormatDate(value);
                    case TextBoxType.MonthYear:
                        return string.Format("{0}/{1}", value.Substring(0, 2), value.Substring(2, 4));
                    case TextBoxType.Money:
                        return Convert.ToDouble(value).ToString("#,##0.00");
                    case TextBoxType.Hour:
                        return string.Format("{0}:{1}", value.Substring(0, 2), value.Substring(2, 2));
                    case TextBoxType.Year:
                        return (value == "0" ? string.Empty : value);
                    case TextBoxType.Phone:
                        return string.Format("{0}-{1}", value.Substring(0, 4), value.Substring(4, 4));
                    case TextBoxType.IE:
                        return string.Format("{0}.{1}.{2}.{3}", value.Substring(0, 3), value.Substring(3, 3), value.Substring(6, 3), value.Substring(9, 3));
                    case TextBoxType.RA:
                        return string.Format("{0}/{1}", value.Substring(0, 7), value.Substring(7, 1));
                    case TextBoxType.DRT:
                        return string.Format("{0}/{1}", value.Substring(0, 4), value.Substring(4, 1));
                    case TextBoxType.ContratoFIES_BB:
                        return string.Format("{0}.{1}.{2}", value.Substring(0, 3), value.Substring(3, 3), value.Substring(6, 3));
                    case TextBoxType.ContratoFIES_CEF:
                        return string.Format("{0}.{1}.{2}.{3}-{4}", value.Substring(0, 2), value.Substring(2, 4), value.Substring(6, 3), value.Substring(9, 7), value.Substring(16, 2));
                    case TextBoxType.Pis:
                        return string.Format("{0}.{1}.{2}-{3}", value.Substring(0, 3), value.Substring(3, 3), value.Substring(6, 4), value.Substring(10, 1));
                    case TextBoxType.Time:
                        return string.Format("{0}:{1}", value.Substring(0, 2), value.Substring(2, 2));
                    default:
                        return value;
                }
            }
            catch
            {
                return value;
            }
        }

        private string FormatDate(string value)
        {
            try
            {
                return Convert.ToDateTime(value).ToString("dd/MM/yyyy");
            }
            catch
            {
                return "";
            }
        }

        #region Methods Override
        protected override void OnInit(EventArgs e)
        {
            //if (RequiredField)
            //{
            //    RequiredFieldValidator = new RequiredFieldValidator();

            //    RequiredFieldValidator.ControlToValidate = ID;
            //    RequiredFieldValidator.ErrorMessage = ErrorMessage;
            //    RequiredFieldValidator.EnableClientScript = (ClientScript.ToLower() != "false");
            //    RequiredFieldValidator.SetFocusOnError = SetFocusOnError;
            //    RequiredFieldValidator.ValidationGroup = ValidationGroup;

            //    IEnumerator keys = Style.Keys.GetEnumerator();

            //    while (keys.MoveNext())
            //    {
            //        String key = (String)keys.Current;
            //        RequiredFieldValidator.Style.Add("position", "absolute");

            //        switch (key.ToUpper())
            //        {
            //            case "Z-INDEX":
            //                int value = Convert.ToInt32(Style[key]);
            //                value += Convert.ToInt32(Width.Value) + 5;
            //                RequiredFieldValidator.Style.Add(key, value.ToString());
            //                break;
            //            case "LEFT":
            //                int value1 = Convert.ToInt32(Style[key].Substring(0, Style[key].Length - 2)) + 5;
            //                value1 += Convert.ToInt32(Width.Value);
            //                RequiredFieldValidator.Style.Add(key, value1.ToString());
            //                break;
            //            case "TOP":
            //                RequiredFieldValidator.Style.Add(key, Convert.ToString(Convert.ToInt32(Style[key].Substring(0, Style[key].Length - 2)) + 1));
            //                break;

            //        }

            //    }

            //    Controls.Add(RequiredFieldValidator);
            //}
        }

        //protected override void OnUnload(EventArgs e)
        //{
        //    if(TextBoxType == TextBoxType.CPF)
        //        Text = FormatMake(Text);
        //    base.OnUnload(e);
        //}

        protected override void OnLoad(EventArgs e)
        {
            //string js = "Callas.WebControls.TextBox.js";

            //if (!Page.ClientScript.IsClientScriptIncludeRegistered(GetType(), js))
            //    Page.ClientScript.RegisterClientScriptInclude(GetType(), js, UrlScript + "TextBox.js");

            if (RequiredField)
            {
                RequiredFieldValidator = new RequiredFieldValidator();

                if (string.IsNullOrEmpty(ErrorMessage))
                    ErrorMessage = "<img src='" + ResolveClientUrl("~/images/obrigatorio.gif") + "' alt='Campo obrigatório' />";

                RequiredFieldValidator.ControlToValidate = ID;
                RequiredFieldValidator.ErrorMessage = ErrorMessage;
                RequiredFieldValidator.EnableClientScript = (ClientScript.ToLower() != "false");
                RequiredFieldValidator.SetFocusOnError = SetFocusOnError;
                RequiredFieldValidator.ValidationGroup = ValidationGroup;

                IEnumerator keys = Style.Keys.GetEnumerator();

                while (keys.MoveNext())
                {
                    String key = (String)keys.Current;
                    RequiredFieldValidator.Style.Add("position", "absolute");

                    switch (key.ToUpper())
                    {
                        case "Z-INDEX":
                            int value = Convert.ToInt32(Style[key]);
                            value += Convert.ToInt32(Width.Value) + 5;
                            RequiredFieldValidator.Style.Add(key, value.ToString());
                            break;
                        case "LEFT":
                            int value1 = Convert.ToInt32(Style[key].Substring(0, Style[key].Length - 2)) + 5;
                            value1 += Convert.ToInt32(Width.Value);
                            RequiredFieldValidator.Style.Add(key, value1.ToString());
                            break;
                        case "TOP":
                            RequiredFieldValidator.Style.Add(key, Convert.ToString(Convert.ToInt32(Style[key].Substring(0, Style[key].Length - 2)) + 1));
                            break;

                    }

                }

                Controls.Add(RequiredFieldValidator);
            }

            base.OnLoad(e);
        }

        protected override void OnPreRender(EventArgs e)
        {
            //if (TextBoxType == WebControls.TextBoxType.Money)
            //    RegisterClientScriptInclude("JQUERY", "SGI.UI.WebControls.jQuery.jQuery.js");

            if (TextBoxType == WebControls.TextBoxType.Money)
            {
                RegisterClientScriptResource("SGI.UI.WebControls.TextBox.Money.metadata.js");
                RegisterClientScriptResource("SGI.UI.WebControls.TextBox.Money.autoNumeric.js");
            }
            else
            {
                RegisterClientScriptResource("SGI.UI.WebControls.TextBox.js");
                RegisterClientScriptResource("SGI.UI.WebControls.Datepicker.jquery.ui.core.js");
                RegisterClientScriptResource("SGI.UI.WebControls.Datepicker.jquery.ui.datepicker.js");
            }


            base.OnPreRender(e);
        }

        void RegisterClientScriptResource(string resourceName)
        {
            var sm = ScriptManager.GetCurrent(Page);

            if (sm == null)
                Page.ClientScript.RegisterClientScriptResource(GetType(), resourceName);
            else
                ScriptManager.RegisterClientScriptResource(this, GetType(), resourceName);
        }

        void RegisterStartupScript(string key, string script)
        {
            var sm = ScriptManager.GetCurrent(Page);

            if (sm == null)
            {
                if (!Page.ClientScript.IsStartupScriptRegistered(key))
                    Page.ClientScript.RegisterStartupScript(typeof(Page), key, script, true);
            }
            else
                ScriptManager.RegisterStartupScript(this, typeof(Page), key, script, true);
        }

        void RegisterClientScriptBlock(string key, string script)
        {
            var sm = ScriptManager.GetCurrent(Page);

            if (sm == null)
            {
                if (!Page.ClientScript.IsClientScriptBlockRegistered(key))
                    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), key, script, true);
            }
            else
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), key, script, true);
        }

        protected override void Render(HtmlTextWriter output)
        {
            string aux;
            StringBuilder script = new StringBuilder();
            string options = "";

            if (SelectOnEnter)
            {
                aux = Attributes["onclick"];
                Attributes.Add("onclick", "this.select();" + Attributes["onclick"]);
            }

            Attributes.Add("onFocus", "this.className='" + CssFocus + "';" + Attributes["onFocus"]);
            Attributes.Add("onBlur", "this.className='" + CssBlur + "';" + Attributes["onBlur"]);

            if (InnerLabel)
            {
                Attributes.Add("onFocus", "ChangeInnerLabelFocus(this, '" + Label + "');" + Attributes["onFocus"]);
                Attributes.Add("onBlur", "ChangeInnerLabelBlur(this, '" + Label + "');" + Attributes["onBlur"]);
                if (Text.Length == 0)
                    Text = Label;
            }

            switch (TextBoxType)
            {
                case TextBoxType.IE:

                    throw new NotImplementedException("O tipo IE não foi implementado");

                case TextBoxType.CEP:

                    script.Append("jQuery(function ($) {");
                    script.Append("$('#" + ClientID + "').maskCEP();");
                    script.Append("});");

                    RegisterStartupScript("TB" + ClientID, script.ToString());

                    MaxLength = 10;
                    Size = MaxLength;
                    break;
                case TextBoxType.CPF:

                    MaxLength = 14;
                    Size = MaxLength;

                    script.Append("jQuery(function ($) {");
                    script.Append("$('#" + ClientID + "').maskCPF();");
                    script.Append("});");

                    RegisterStartupScript("TB" + ClientID, script.ToString());

                    break;
                case TextBoxType.CNPJ:
                    MaxLength = 18;
                    Size = MaxLength;

                    script.Append("jQuery(function ($) {");
                    script.Append("$('#" + ClientID + "').maskCNPJ();");
                    script.Append("});");

                    RegisterStartupScript("TB" + ClientID, script.ToString());

                    break;
                case TextBoxType.Alfanumeric:

                    MaxLength = 100;
                    //Width = 110;

                    script.Append("jQuery(function ($) {");
                    script.Append("$('#" + ClientID + "').alfaNumeric({length:" + MaxLength + "});");
                    script.Append("});");

                    RegisterStartupScript("TB" + ClientID, script.ToString());

                    break;
                case TextBoxType.Money:

                    options = "{ aSep: '.', aDec: ','";

                    if (AllowNegative)
                        options += ", aNeg: '-'";

                    if (ShowMoneySimbol)
                        options += ", aSign: 'R$'";

                    options += string.Format(", mDec: '{0}'", DecimalPlaces);

                    options += "}";

                    script.Append("jQuery(function ($) {");
                    script.Append("$('#" + ClientID + "').autoNumeric(" + options + ");");
                    script.Append("});");

                    RegisterStartupScript("TB" + ClientID, script.ToString());

                    Style.Add("text-align", "Right");
                    Style.Add("float", "left");

                    Size = 15;
                    break;
                case TextBoxType.Number:
                case TextBoxType.DDD:
                case TextBoxType.Year:
                case TextBoxType.RA:
                case TextBoxType.DRT:

                    if (MaxLength == 0)
                        MaxLength = 10;

                    if (TextBoxType == WebControls.TextBoxType.DDD)
                        MaxLength = 3;
                    else if (TextBoxType == WebControls.TextBoxType.Year)
                        MaxLength = 4;
                    else if (TextBoxType == WebControls.TextBoxType.RA)
                        MaxLength = 8;
                    else if (TextBoxType == WebControls.TextBoxType.DRT)
                        MaxLength = 6;

                    script.Append("jQuery(function ($) {");
                    script.Append("$('#" + ClientID + "').onlyNumbers({length:" + MaxLength + "});");
                    script.Append("});");

                    RegisterStartupScript("TB" + ClientID, script.ToString());

                    Size = MaxLength;
                    break;
                case TextBoxType.Date:
                   
                  
                    script.Append("jQuery(function ($) {");

                    if (DtInicial.HasValue || DtFinal.HasValue)
                    {
                        if (DtInicial != null)
                            script.Append("$('#" + ClientID + "').attr('dayIn','" + DtInicial.Value.ToShortDateString() + "');");
                        if (DtFinal != null)
                            script.Append("$('#" + ClientID + "').attr('dayFn','" + DtFinal.Value.ToShortDateString() + "');");
                    }

                    script.Append("$('#" + ClientID + "').maskDate();");

                    script.Append("});");

                    RegisterStartupScript("TB" + ClientID, script.ToString());
                    
                    break;
                case TextBoxType.Email:

                    if (ValidateTextBoxTypeClientScript)
                    {
                        script.Append("jQuery(function ($) {");
                        script.Append("$('#" + ClientID + "').validMail();");
                        script.Append("});");

                        RegisterStartupScript("TB" + ClientID, script.ToString());
                    }

                    break;

                case TextBoxType.Phone:

                    script.Append("jQuery(function ($) {");
                    script.Append("$('#" + ClientID + "').maskPhone();");
                    script.Append("});");

                    RegisterStartupScript("TB" + ClientID, script.ToString());

                    break;
                case TextBoxType.Hour:

                    script.Append("jQuery(function ($) {");
                    script.Append("$('#" + ClientID + "').maskHour();");
                    script.Append("});");

                    RegisterStartupScript("TB" + ClientID, script.ToString());

                    break;
                case TextBoxType.MonthYear:

                    script.Append("jQuery(function ($) {");
                    script.Append("$('#" + ClientID + "').maskMonthYear();");
                    script.Append("});");

                    RegisterStartupScript("TB" + ClientID, script.ToString());

                    break;
                case TextBoxType.ContratoFIES_CEF:

                    script.Append("jQuery(function ($) {");
                    script.Append("$('#" + ClientID + "').maskNumeroContrato();");
                    script.Append("});");

                    RegisterStartupScript("TB" + ClientID, script.ToString());

                    break;
                case TextBoxType.ContratoFIES_BB:

                    script.Append("jQuery(function ($) {");
                    script.Append("$('#" + ClientID + "').maskNumeroContratoBB();");
                    script.Append("});");

                    RegisterStartupScript("TB" + ClientID, script.ToString());

                    break;
                case TextBoxType.Pis:

                    MaxLength = 14;
                    Size = MaxLength;

                    script.Append("jQuery(function ($) {");
                    script.Append("$('#" + ClientID + "').maskPis();");
                    script.Append("});");

                    RegisterStartupScript("TB" + ClientID, script.ToString());

                    break;
                case TextBoxType.Time:

                    script.Append("jQuery(function ($) {");
                    script.Append("$('#" + ClientID + "').maskTime();");
                    script.Append("});");

                    RegisterStartupScript("TB" + ClientID, script.ToString());

                    break;
            }

            //Attributes.Add("onblur", "if (typeof(ShowValidators) != \"undefined\")ShowValidators();" + Attributes["onblur"]);
            //Attributes.Add("","");

            if (Size > 0)
                Attributes.Add("size", Size.ToString());

            if (TextMode == TextBoxMode.MultiLine && this.MaxLength != 0)
                Attributes.Add("maxLength", MaxLength.ToString());

            base.Render(output);

            if (RequiredField)
                RequiredFieldValidator.RenderControl(output);
        }
        #endregion Methods Override
    }
}
