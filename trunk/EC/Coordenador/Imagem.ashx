<%@ WebHandler Language="C#" Class="Aluno_Imagem" %>

using System;
using System.Web;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.SessionState;
using SGI.Common;

public class Aluno_Imagem : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        System.Drawing.Image img = null;

        int idFotoUsuario = SGI.Common.Library.ToInteger(context.Request.Url.Query.Replace("?", ""));

        if (idFotoUsuario > 0)
        {
            context.Response.ClearContent();
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);

            var fotoUsuario = new SGI.DataContext.Controller.Coorporativo.FotoUsuario().Bind(idFotoUsuario);
            if (fotoUsuario != null)
            {
                try
                {
                    MemoryStream ms = new MemoryStream(fotoUsuario.imFotoUsuario);
                    img = System.Drawing.Image.FromStream(ms);
                }
                catch
                {
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
