using EC.Common;

namespace EC.UI.WebControls
{
    public class DropDownList : System.Web.UI.WebControls.DropDownList
    {
        public DropDownList()
        {
            Initialize();
        }

        protected void Initialize()
        {
            CssClass = "dropdownlist";
        }

        public bool RequiredField
        {
            get { return Library.ToBoolean(ViewState["RequiredField"]); }
            set { ViewState["RequiredField"] = value; }
        }

        public int Int32
        {
            get { return Library.ToInteger(SelectedValue); }       
                        

        }
        protected override void OnDataBinding(System.EventArgs e)
        {
            base.OnDataBinding(e);
        }
        protected override void OnDataBound(System.EventArgs e)
        {       
            base.OnDataBound(e);
        }
    }
}
