using System.Web.UI;
using System;
using System.Text;
using System.Collections;

namespace EC.UI.WebControls
{
    [ToolboxData("<{0}:Button runat=\"server\"></{0}:Button>")]
    public class Button : System.Web.UI.WebControls.Button
    {
        private bool _disableOnClick = false;

        public Button()
        {
            CssClass = "button";
        }

        public bool DisableOnClick
        {
            get { return _disableOnClick; }
            set { _disableOnClick = value; }
        }

        protected override void OnPreRender(EventArgs e)
        {   
            if (DisableOnClick)
                this.Attributes.Add("onclick", GetDisableButtonJavaScript());

            if (CausesValidation ==false)
                this.Attributes.Add("onclick", "Page_ValidationActive = false;");
        }

        public string GetDisableButtonJavaScript()
        {
            string disabledText = this.Text == "" ? "Processing..." : Text; 
            disabledText = disabledText.Replace("'", "");

            StringBuilder sb = new StringBuilder();
            //Eventos de PostBack
            PostBackOptions opt = new PostBackOptions(this, "", "", false, true, true, true, this.CausesValidation, this.ValidationGroup);
            sb.Append(this.Page.ClientScript.GetPostBackEventReference(opt));

            sb.Append(";");

            if(this.DisableOnClick)
                sb.Append("this.disabled='true';this.value='" + disabledText + "';");

            if (this.CausesValidation && this.Page.Validators.Count > 0 && this != null)
            {
                //Insiro a validação da página
                sb.Insert(0,"if (typeof(Page_ClientValidate) == 'function') { if (Page_ClientValidate('" + this.ValidationGroup + "') == false) { return false; } else {");
                //Fechamento do else com os eventos de postback e disableClick, e fechamento da validação de página.
                sb.Append("} } ");
            }
            
            return sb.ToString();
        }


        protected override void Render(HtmlTextWriter writer)
        {
            this.Attributes.Add("onmouseover", "this.className='buttonfocus';");
            this.Attributes.Add("onmouseout", string.Format("this.className='{0}';", CssClass));

            #region -*- show validator's -*-
            //if (base.CausesValidation)
            //{
            //    this.Attributes.Add("onclick", "return validateSubmitButton();" + Attributes["onclick"]);

            //    // Apresenta mensagem dos Control's Validator's.
            //    System.Text.StringBuilder oClientScript = new System.Text.StringBuilder();

            //    oClientScript.Append("<script language='javascript' defer>" + "\n");
            //    oClientScript.Append("function showErrorSGI(errDesc)" + "\n");
            //    oClientScript.Append("{" + "\n");
            //    oClientScript.Append("	try" + "\n");
            //    oClientScript.Append("	{ " + "\n");
            //    oClientScript.Append("		window.parent.frames['Tree'].montaErro(errDesc,15);" + "\n");
            //    oClientScript.Append("	} " + "\n");
            //    oClientScript.Append("	catch(e) " + "\n");
            //    oClientScript.Append("	{ " + "\n");
            //    oClientScript.Append("		alert(errDesc);" + "\n");
            //    oClientScript.Append("	}" + "\n");
            //    oClientScript.Append("	return false;" + "\n");
            //    oClientScript.Append("}" + "\n");
            //    oClientScript.Append("function validateSubmitButton()" + "\n");
            //    oClientScript.Append("{" + "\n");
            //    oClientScript.Append("	try" + "\n");
            //    oClientScript.Append("	{" + "\n");
            //    oClientScript.Append("		if (typeof(Page_ClientValidate) == 'function')" + "\n");
            //    oClientScript.Append("		{" + "\n");
            //    oClientScript.Append("			if(!Page_ClientValidate()){showErrorSGI('" + "Informe os campos obrigatórios." + "');return false;}else{return true;}");
            //    oClientScript.Append("		}" + "\n");
            //    oClientScript.Append("	}" + "\n");
            //    oClientScript.Append("	catch(e) " + "\n");
            //    oClientScript.Append("	{ " + "\n");
            //    oClientScript.Append("	" + "\n");
            //    oClientScript.Append("	}" + "\n");
            //    oClientScript.Append("}" + "\n");
            //    oClientScript.Append("</script>" + "\n");

            //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "scriptButtonValidate", oClientScript.ToString());
            //}
            #endregion

            base.Render(writer);
        }
         

    }
}
