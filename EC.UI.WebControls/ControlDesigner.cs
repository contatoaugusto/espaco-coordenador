using System;
using System.ComponentModel;
namespace EC.UI.WebControls
{
    internal class ControlDesigner : System.Web.UI.Design.ControlDesigner
    {
        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
        }

        public override bool AllowResize
        {
            get
            {
                return true;
            }
        }

        public override string GetDesignTimeHtml()
        {
            return CreatePlaceHolderDesignTimeHtml("EC.UI.WebControls.Control");
        }


        protected override string GetErrorDesignTimeHtml(Exception e)
        {
            string pattern = "Control while creating design time HTML:<br/>{0}";
            return String.Format(pattern, e.Message);
        }
    }
}
