using System;
using System.ComponentModel;


namespace EC.UI.WebControls
{
    /// <summary>
    /// Designer for the AlertDesigner
    /// </summary>
    class AlertDesigner : ControlDesigner
    {
        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
        }

        // Make this control resizeable on the design surface
        public override bool AllowResize
        {
            get
            {
                return true;
            }
        }

        public override string GetDesignTimeHtml()
        {
            return CreatePlaceHolderDesignTimeHtml("SGI.Components.Alert");
        }


        protected override string GetErrorDesignTimeHtml(Exception e)
        {
            string pattern = "Alert while creating design time HTML:<br/>{0}";
            return String.Format(pattern, e.Message);
        }
    }
}