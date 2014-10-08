using System.Web.UI;
using System.ComponentModel;

namespace EC.UI.WebControls
{
    [ToolboxData("<{0}:CheckBox runat=\"server\"></{0}:CheckBox>")]
    public class CheckBox : System.Web.UI.WebControls.CheckBox
    {
        public CheckBox()
        {
            CssClass = "checkbox";
        }

        [Bindable(true)]
        public object Tag
        {
            get { return ViewState["0"]; }
            set { ViewState["0"] = value; }
        }

        [Bindable(true),
        Category("Data"),
        DefaultValue("-1"),
        Description("Sets the checkbox value that is returned to your application.")]
        public string Value
        {
            get { return (string)ViewState["1"]; }
            set { ViewState["1"] = value; } 
        }
    }
}
