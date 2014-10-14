<%@ WebHandler Language="C#" Class="MaterialDidatico" %>

using System;
using System.Web;
using System.Web.SessionState;
using SGI.Common;

public class MaterialDidatico : IHttpHandler, IRequiresSessionState {
    
    public void ProcessRequest (HttpContext context) 
    {
        if (Url.HasParams)
        {
            string fileName = Url.GetUrlParams()[0];
            
            System.IO.FileInfo file = new System.IO.FileInfo(fileName);
            if (file != null)
            {
                context.Response.Clear();
                context.Response.AddHeader("Content-Disposition", "attachment;filename=" + file.Name);
                context.Response.AddHeader("Content-Length", file.Length.ToString());
                context.Response.ContentType = "application/octet-stream";
                context.Response.WriteFile(file.FullName);
                context.Response.End();
            }
        }
    }
 
    public bool IsReusable {
        get {
            return true;
        }
    }

}