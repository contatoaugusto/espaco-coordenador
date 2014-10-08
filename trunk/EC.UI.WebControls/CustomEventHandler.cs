using System;
using System.ComponentModel;
using System.Web.UI;

namespace EC.UI.WebControls
{
    [DefaultEvent("CustomEvent"),
    ToolboxData("<{0}:CustomEventHandler runat=\"server\"></{0}:CustomEventHandler>")]
    [Serializable()]
    public class CustomEventHandler : System.Web.UI.Control, IPostBackDataHandler, IPostBackEventHandler
    {
        public event EventHandler CustomEvent;
        public string EventArgument = string.Empty;

        public CustomEventHandler()
        {

        }

        public bool LoadPostData(string postDataKey, System.Collections.Specialized.NameValueCollection nameCol)
        {
            return true;
        }


        public void RaisePostDataChangedEvent()
        {

        }

        public void RaisePostBackEvent(string EventArgument)
        {
            try
            {
                this.EventArgument = EventArgument;
                CustomEvent(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
            }
        }

        protected override void Render(HtmlTextWriter output)
        {
            // TODO:  Add EventHandle.Render implementation
            string script = Page.ClientScript.GetPostBackEventReference(this, "CustomEvent");
            //Page.RegisterRequiresPostBack(this);
            //output.Write(script);
            base.Render(output);
        }
    }
}
