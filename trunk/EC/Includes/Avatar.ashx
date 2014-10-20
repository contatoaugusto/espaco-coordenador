<%@ WebHandler Language="C#" Class="Avatar" %>

using System;
using System.Web;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.SessionState;
using EC.Common;
using System.Collections.Specialized;
using UI.Web.EC;
using EC.Negocio;

public class Avatar : IHttpHandler, IRequiresSessionState
{
    private const int DAYS_TO_CACHE = 2;

    public void ProcessRequest(HttpContext context)
    {
        context.Response.Clear();
        context.Response.ContentType = "image/jpg";

        System.Drawing.Image img = null;

        int idUsuario = Library.ToInteger(context.Request.Url.Query.Replace("?", ""));

        context.Response.Cache.SetExpires(DateTime.Now.AddDays(DAYS_TO_CACHE));
        context.Response.Cache.SetCacheability(HttpCacheability.Public);
        context.Response.Cache.SetValidUntilExpires(false);

        img = GetImage(idUsuario, context);

        //context.Response.Flush();

        img.Save(context.Response.OutputStream, ImageFormat.Jpeg);
        img.Dispose();

        
        context.Response.End();
    }

    public bool IsReusable
    {
        get
        {
            return true;
        }
    }

    private Image GetImage(int idUsuario, HttpContext context)
    {
        string pathImages = string.Format(@"{0}\images\", EC.Common.AppSettings.PathRoot);
        
        System.Drawing.Image img = null;

        string imageCache = "";
        
        if (idUsuario > 0)
        {
            imageCache = string.Format(@"{0}{1}.tmp", UI.Web.EC.Utils.PathImagesCache, idUsuario);

            if (!Directory.Exists(UI.Web.EC.Utils.PathImagesCache))
                Directory.CreateDirectory(UI.Web.EC.Utils.PathImagesCache);

            if (File.Exists(imageCache))
            {
                var fileInfo = new FileInfo(imageCache);

                if (fileInfo.CreationTime.AddDays(DAYS_TO_CACHE) < DateTime.Now)
                {
                    try
                    {
                        File.Delete(imageCache);
                    }
                    catch { }
                }
            }

            if (!File.Exists(imageCache))
            {
                byte[] imageBytes = NUsuario.ConsultarById(idUsuario).FOTO; //new SGI.DataContext.Controller.Coorporativo.FotoUsuario().GetAvatar(idUsuario);
                
                if (imageBytes.Length > 0)
                {
                    try
                    {
                        using (MemoryStream stream = new MemoryStream(imageBytes))
                        {
                            stream.Position = 0;
                            img = System.Drawing.Image.FromStream(stream, false, true);
                            img = img.GetThumbnailImage(54, 65, null, new IntPtr());
                        }
                    }
                    catch 
                    { 
                    }
                    
                    if (img != null)
                    {
                        byte[] bytes = new ImageConverter().ConvertTo(img, typeof(byte[])) as byte[];

                        if (!BytesToFile(imageCache, bytes))
                            img = null;
                    }
                }
            }
            else
            {
                FileStream fs = null;
                
                try
                {
                    fs = new FileStream(imageCache, FileMode.Open, FileAccess.Read);
                    img = Image.FromStream(fs);  
                }
                catch
                {
                }
                finally
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
        }

        if (img == null)
        {
            imageCache = string.Format("{0}avatar.gif", pathImages);
            
            img = Image.FromFile(imageCache);
            img = img.GetThumbnailImage(54, 65, null, new IntPtr());
        }

        //string fileName = imageCache.Substring(imageCache.LastIndexOf('\\') + 1);
        context.Response.AddHeader("content-disposition", string.Format("inline; filename=avatar.ashx?{0}", idUsuario));

        return img;
    }

    public bool BytesToFile(string fileName, byte[] bytes)
    {
        try
        {
            //System.IO.FileStream _FileStream = new System.IO.FileStream(fileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            System.IO.FileStream fileStream = new System.IO.FileStream(fileName, System.IO.FileMode.Append);
            fileStream.Write(bytes, 0, bytes.Length);
            fileStream.Close();
            return true;
        }
        catch (Exception e)        
        {
            Console.WriteLine("Exception caught in process: {0}", e.ToString());
        }

        return false;
    } 
}


//public class Avatar : ImageHandler, IRequiresSessionState
//{
//    public Avatar()
//    {
//        this.ImageTransforms.Add(new ImageResizeTransform { Width = 54, Mode = ImageResizeMode.Fit });

//        this.EnableServerCache = true;
//        this.EnableClientCache = true;
//    }

//    public override ImageInfo GenerateImage(NameValueCollection parameters)
//    {
//        int idUsuario = SGI.Common.Library.ToInteger(HttpContext.Current.Request.Url.Query.Replace("?", ""));

//        Image img = null;

//        if (idUsuario > 0)
//        {
//            byte[] imgByteArray = new SGI.DataContext.Controller.Coorporativo.FotoUsuario().GetAvatar(idUsuario);
//            if (imgByteArray.Length > 0)
//            {
//                try
//                {
//                    MemoryStream ms = new MemoryStream(imgByteArray);
//                    img = System.Drawing.Image.FromStream(ms);
//                }
//                catch
//                {
//                }
//            }
//        }


//        if (img == null)
//            img = Image.FromFile(string.Format("{0}images\\avatar.gif", SGI.Common.AppSettings.PathRoot));

//        img = img.GetThumbnailImage(54, 65, null, new IntPtr());
//        //img.Dispose();

//        return new ImageInfo(img);
//    }
//}