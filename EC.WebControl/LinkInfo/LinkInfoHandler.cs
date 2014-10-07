using System.Text;
using System.Web;
using System.Web.SessionState;

namespace EC.UI.WebControls
{
    /// <summary>
    /// Descrição resumida para LinkInfoHandlercs
    /// </summary>
    public class LinkInfoHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            string url = context.Request.QueryString["url"];

            if (!string.IsNullOrEmpty(url))
            {
                ILinkInfo linkInfo = LinkInfo.Get(url);

                StringBuilder sb = new StringBuilder();

                sb.Append("[");
                sb.Append("{");
                sb.AppendFormat("url : '{0}',", linkInfo.Url);
                sb.AppendFormat("title : '{0}',", linkInfo.Title);
                sb.AppendFormat("subtitle : '{0}',", linkInfo.Subtitle);
                sb.AppendFormat("summary : '{0}',", linkInfo.Summary);
                sb.AppendFormat("images : null", linkInfo.Subtitle);
                sb.Append("}");
                sb.Append("]");

                context.Response.Write(sb.ToString());
            }
        }

        public bool IsReusable
        {
            get { return false; }
        }


    }
}