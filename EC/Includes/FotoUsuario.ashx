<%@ WebHandler Language="C#" Class="FotoUsuario" %>

using System;
using System.Web;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.SessionState;

public class FotoUsuario : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        int idUsuario = SGI.Common.Library.ToInteger(context.Request.Url.Query.Replace("?", ""));

        System.Drawing.Image img = null;

        if (idUsuario > 0)
        {
            context.Response.ClearContent();
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);



            var o = new SGI.DataContext.Controller.Coorporativo.FotoPessoa().BindByUsuario(idUsuario);
            if (o != null)
            {
                if (o.imFotoPessoa != null)
                {
                    try
                    {
                        MemoryStream ms = new MemoryStream(o.imFotoPessoa);
                        img = System.Drawing.Image.FromStream(ms);
                    }
                    catch
                    {
                    }
                }
            }
        }
        
        if (img == null)
            img = Image.FromFile(string.Format("{0}images\\avatar.gif", SGI.Common.AppSettings.PathRoot));

        img = img.GetThumbnailImage(54, 65, null, new IntPtr());
        img.Save(context.Response.OutputStream, ImageFormat.Jpeg);
        img.Dispose();

        context.Response.End();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}