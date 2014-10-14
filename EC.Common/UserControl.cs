using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EC.Common
{
    public abstract class UserControl : System.Web.UI.UserControl
    {
        protected UserControl()
        { }

       

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            FormParamsAttribute.ReadFormParams(this);
        }

        protected void Redirect(string url)
        {
            FormParamsAttribute.Redirect(this, url);
        }
    }
}