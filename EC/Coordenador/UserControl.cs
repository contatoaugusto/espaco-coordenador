using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UI.Web.EC.Coordenador
{
    public abstract class UserControl : System.Web.UI.UserControl
    {
        public UserControl()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        public abstract void BindControl();
    }
}
