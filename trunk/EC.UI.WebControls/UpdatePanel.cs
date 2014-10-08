using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.IO;

namespace EC.UI.WebControls
{
    [ToolboxData("<{0}:UpdatePanel Runat=\"server\"></{0}:UpdatePanel>")]
    public class UpdatePanel : System.Web.UI.UpdatePanel
    {
        protected override void RenderChildren(HtmlTextWriter writer)
        {
            // IsInPartialRendering is a reliable way of
            // telling it's a callback
            if (!this.IsInPartialRendering && this.RenderMode == UpdatePanelRenderMode.Block)
            {
                StringBuilder sb = new StringBuilder();
                base.RenderChildren(new HtmlTextWriter(new StringWriter(sb)));


                string renderedHtml = sb.ToString();


                if (!renderedHtml.StartsWith("<div "))
                    // This should never happen; better safe than sorry
                    throw new Exception("An UpdatePanel with a RenderMode of Block isn't rendering as a <div>");


                writer.Write("<div style=\"display:inline\"");
                writer.Write(renderedHtml.Substring(4));
            }
            else
                base.RenderChildren(writer);
        }
    }
}
