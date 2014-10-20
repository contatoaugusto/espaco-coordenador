<%@ WebHandler Language="C#" Class="NoCacheAvatar" %>

using System;
using System.Web;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.SessionState;
using EC.Negocio;

public class NoCacheAvatar : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        System.Drawing.Image img = null;
        
        int idUsuario = EC.Common.Library.ToInteger(context.Request.Url.Query.Replace("?", ""));

        if (idUsuario > 0)
        {
            context.Response.ClearContent();
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);

            byte[] imgByteArray = NUsuario.ConsultarById(idUsuario).FOTO; //new SGI.DataContext.Controller.Coorporativo.FotoUsuario().GetAvatar(idUsuario);
            if (imgByteArray.Length > 0)
            {
                try
                {
                    MemoryStream ms = new MemoryStream(imgByteArray);
                    img = System.Drawing.Image.FromStream(ms);
                }
                catch
                {
                }
            }
        }


        if (img == null)
            img = Image.FromFile(string.Format("{0}images\\avatar.gif", EC.Common.AppSettings.PathRoot));

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