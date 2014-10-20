<%@ WebHandler Language="C#" Class="Avatar" %>

using System;
using System.Web;
using EC.Common;
using System.IO;
using System.Drawing.Imaging;

public class Avatar : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {

        if (Url.HasParams)
        {
            string[] prs = Url.GetUrlParams();
            
            string fileName = prs[0];
          
            string pathUpload = Path.Combine(AppSettings.PathUpload, @"temp\imageupload\");

            var img = System.Drawing.Image.FromFile(pathUpload);

            ImageFormat imageFormat = ImageFormat.Gif;

            switch (fileName.Split('.')[1])
            { 
                case "gif":
                    imageFormat = ImageFormat.Gif;
                    break;
                case "jpeg":
                    imageFormat = ImageFormat.Jpeg;
                    break;
                case "png":
                    imageFormat = ImageFormat.Png;
                    break;
                case "bmp":
                    imageFormat = ImageFormat.Bmp;
                    break;
            }

            img = img.GetThumbnailImage(54, 65, null, new IntPtr());
            img.Save(context.Response.OutputStream, imageFormat);
            img.Dispose();

            context.Response.End();
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}