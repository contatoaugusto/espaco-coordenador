using System.Web.UI;
using System.ComponentModel;

namespace EC.UI.WebControls
{
    [ToolboxData("<{0}:RadioButton runat=\"server\"></{0}:RadioButton>")]
    public class RadioButton : System.Web.UI.WebControls.RadioButton
    {
        public RadioButton()
        {
            CssClass = "RadioButton";
        }

        [Bindable(true)]
        public object Tag
        {
            get { return ViewState["Tag"]; }
            set { ViewState["Tag"] = value; }
        }

        [Bindable(true),
        Category("Data"),
        DefaultValue("-1"),
        Description("Sets the RadioButton value that is returned to your application.")]
        public string Value
        {
            get { return (string)ViewState["value"]; }
            set { ViewState["value"] = value; }
        }
    }
}
