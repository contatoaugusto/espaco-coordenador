using System;
using System.Web.UI;

namespace EC.UI.WebControls
{
    [ToolboxData("<{0}:AlertRequiredFields runat=\"server\"></{0}:AlertRequiredFields>")]
    public class AlertRequiredFields : System.Web.UI.Control
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            string key1 = "sgi_showrequiredfields";
            string script = "function sgi_showrequiredfields() { try { if (!Page_IsValid) $('.alertrequiredfields').show(); else $('.alertrequiredfields').hide(); } catch(e){} } $('.alertrequiredfields').bind('click', function(){$(this).hide();})";

            if (!Page.ClientScript.IsStartupScriptRegistered(this.GetType(), key1))
                Page.ClientScript.RegisterStartupScript(this.GetType(), key1, script, true);

            string key = "OnSubmitScript";

            if (!Page.ClientScript.IsOnSubmitStatementRegistered(Page.GetType(), key))
                Page.ClientScript.RegisterOnSubmitStatement(Page.GetType(), key, "if (typeof (ValidatorOnSubmit) == \"function\" && ValidatorOnSubmit() == false){ if(Page_ValidationActive){sgi_showrequiredfields();} return false;}\n");
        }

        protected override void Render(HtmlTextWriter writer)
        {            
            writer.Write("<div class=\"alertrequiredfields\">\n");
            writer.Write("Existem campos obrigatórios a serem preenchidos.");
            writer.Write("</div>");
        }  
    }
}
