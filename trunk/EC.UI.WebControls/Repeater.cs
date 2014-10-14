using System;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace EC.UI.WebControls
{
    [ToolboxData("<{0}:Repeater Runat=\"server\"></{0}:Repeater>")]
    public class Repeater : System.Web.UI.WebControls.Repeater
    {
        private ITemplate _emptyTemplate;

        [Browsable(false)]
        [DefaultValue(null)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateContainer(typeof(RepeaterItem))]
        public ITemplate EmptyTemplate
        {
            get { return _emptyTemplate; }
            set { _emptyTemplate = value; }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);
            if (Items.Count == 0)
            {
                Controls.Clear();
                if (EmptyTemplate != null)
                    EmptyTemplate.InstantiateIn(this);
            }
        }
    }
}
