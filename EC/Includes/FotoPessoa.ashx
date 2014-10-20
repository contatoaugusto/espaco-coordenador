<%@ WebHandler Language="C#" Class="FotoPessoa" %>

using System;
using System.Web;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.SessionState;
using EC.Common;
using UI.Web.EC;

public class FotoPessoa : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {

        bool checkSession = true;
        int idPessoa = 0;

        if (!string.IsNullOrEmpty(context.Request.Url.Query))
        {
            idPessoa = EC.Common.Library.ToInteger(context.Request.Url.Query.Replace("?", ""));
            checkSession = false;
        }
        
        if (checkSession)
            idPessoa = ((EC.Modelo.USUARIO)context.Session["USUARIO"]).ID_USUARIO;// SessionAluno.idPessoa.ToInt32();

        System.Drawing.Image img = null;

        if (idPessoa > 0)
        {
            context.Response.ClearContent();
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);

            var o = EC.Negocio.NUsuario.ConsultarById(idPessoa);// new SGI.DataContext.Controller.Coorporativo.FotoPessoa().Bind(idPessoa);
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