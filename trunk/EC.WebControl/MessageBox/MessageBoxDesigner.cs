using System;
using System.ComponentModel;
using System.Web.UI.Design;

namespace EC.UI.WebControls
{
    internal class MessageBoxDesigner : ControlDesigner
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
            return CreatePlaceHolderDesignTimeHtml("SGI.UI.WebControls.MessageBox");
        }


        protected override string GetErrorDesignTimeHtml(Exception e)
        {
            string pattern = "MessageBox while creating design time HTML:<br/>{0}";
            return String.Format(pattern, e.Message);
        }
    }
}
