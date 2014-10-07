<%@ WebHandler Language="C#" Class="UI.Web.EP.Handlers.ImageNotice" %>

using System;
using System.Web;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.SessionState;
using SGI.Core;
using System.Collections.Specialized;
using SGI.Business.Entities.Portal;
using SGI.UI.Process.Coorporativo;

namespace UI.Web.EP.Handlers
{
    public class ImageNotice : IHttpHandler, IRequiresSessionState
    {
        ITimelineProcess _timelineProcess;

        public ImageNotice()
        {
            _timelineProcess = new SGI.UI.Process.Coorporativo.TimelineProcess();
        }

        public void ProcessRequest(HttpContext context)
        {
            int idImagemPublicacao = context.Request.Url.Query.Replace("?", "").ToInt32();
            
            if (idImagemPublicacao > 0)
            {
                ImagemPublicacao imagem = _timelineProcess.GetImagemPublicacao(idImagemPublicacao);

                if (imagem != null)
                {
                    MemoryStream ms = new MemoryStream(imagem.imImagem);
                    System.Drawing.Image img = System.Drawing.Image.FromStream(ms);

                    //HttpContext.Current.Response.Clear();
                    context.Response.ClearContent();
                    context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    //HttpContext.Current.Response.ContentType = "image/jpeg";
                    context.Response.ContentType = imagem.deContentType;

                    //bmp.Save(HttpContext.Current.Response.OutputStream, ImageFormat.Gif);
                    if (imagem.deContentType.IndexOf("gif") > -1)
                        img.Save(context.Response.OutputStream, ImageFormat.Gif);
                    else if (imagem.deContentType.IndexOf("png") > -1)
                        img.Save(context.Response.OutputStream, ImageFormat.Png);
                    else
                        img.Save(context.Response.OutputStream, ImageFormat.Jpeg);

                    img.Dispose();

                    context.Response.StatusCode = 200;
                    context.Response.End();
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }

    }

}