<%@ WebHandler Language="C#" Class="FotoUsuario" %>

using System;
using System.Web;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.SessionState;
using EC.Negocio;
using EC.Modelo;

public class FotoUsuario : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        int idUsuario = EC.Common.Library.ToInteger(context.Request.Url.Query.Replace("?", ""));

        System.Drawing.Image img = null;

        if (idUsuario > 0)
        {
            context.Response.ClearContent();
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);



            //var o = new SGI.DataContext.Controller.Coorporativo.FotoPessoa().BindByUsuario(idUsuario);

            var o = NUsuario.ConsultarById(idUsuario);
            if (o != null)
            {
                if (o.FOTO != null)
                {
                    try
                    {
                        MemoryStream ms = new MemoryStream(o.FOTO);
                        img = System.Drawing.Image.FromStream(ms);
                    }
                    catch
                    {
                    }
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