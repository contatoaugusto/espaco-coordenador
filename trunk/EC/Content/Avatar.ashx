<%@ WebHandler Language="C#" Class="UI.Web.EP.Handlers.Avatar" %>

using System;
using System.Web;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.SessionState;
using SGI.Core;
using System.Collections.Specialized;
using SGI.Web;

namespace UI.Web.EP.Handlers
{
    public class Avatar : CacheBase, IHttpHandler, IRequiresSessionState
    {
        private SGI.UI.Process.Sistema.IUsuarioProcess _usuarioProcess;
        private const int DAYS_TO_CACHE = 2;
        private string _imageCache = "";
        private string _imageName = "";
        private FileInfo _fileInfo;

        public Avatar()
        {
            _usuarioProcess = new SGI.UI.Process.Sistema.UsuarioProcess();
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Clear();
            context.Response.ContentType = "image/jpg";

            Image img = null;

            int idUsuario = context.Request.Url.Query.Replace("?", "").ToInt32();

            img = GetImage(idUsuario, context);

            bool isAvatarDefault = false;

            if (img == null)
            {
                _imageName = "avatar.gif";
                _imageCache = Path.Combine(AppSettings.PathImages, _imageName);

                img = Image.FromFile(_imageCache);
                img = img.GetThumbnailImage(54, 65, null, new IntPtr());
                isAvatarDefault = true;
            }

            context.Response.AddHeader("content-disposition", string.Format("inline; filename={0}", _imageName));
            
            if(!isAvatarDefault)
                SetCaching(context);

            img.Save(context.Response.OutputStream, ImageFormat.Jpeg);
            img.Dispose();
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
        
        private void SetCaching(HttpContext context)
        {
            DateTime updated = DateTime.Parse("01/10/2009");

            var modifyDate = new DateTime();

            if (!DateTime.TryParse(context.Request.Headers["If-Modified-Since"], out modifyDate))
            {
                modifyDate = _fileInfo != null ? _fileInfo.CreationTime : DateTime.Now;
            }

            string eTag = context.Request.Headers["If-None-Match"];

            if (string.IsNullOrEmpty(eTag))
            {
                eTag = GetFileETag(_imageCache, updated);
            }

            context.Response.Cache.SetExpires(DateTime.Now.AddDays(DAYS_TO_CACHE));

            if (!IsFileModified(_imageCache, modifyDate, eTag))
            {
                context.Response.StatusCode = 304;
                context.Response.StatusDescription = "Not Modified";
                context.Response.AddHeader("Content-Length", "0");
                context.Response.Cache.SetLastModified(modifyDate);
                context.Response.Cache.SetETag(eTag);
                context.Response.End();
                return;
            }

            context.Response.Cache.SetAllowResponseInBrowserHistory(true);
            context.Response.Cache.SetLastModified(modifyDate);
            context.Response.Cache.SetETag(eTag);
        }

        private Image GetImage(int idUsuario, HttpContext context)
        {
            Image img = null;

            string pathImagesCache = Path.Combine(AppSettings.PathImages, "Cache\\");
            
            if (idUsuario > 0)
            {
                _imageName = string.Format("{0}.jpg", idUsuario);
                _imageCache = string.Format(@"{0}{1}", pathImagesCache, _imageName).Replace("\\\\", @"\").Replace("\\", @"\");

                if (!Directory.Exists(pathImagesCache))
                    Directory.CreateDirectory(pathImagesCache);

                if (File.Exists(_imageCache))
                {
                    _fileInfo = new FileInfo(_imageCache);
                    try
                    {
                        if (_fileInfo.CreationTime.AddDays(DAYS_TO_CACHE) < DateTime.Now)
                        {
                            File.Delete(_imageCache);

                        }
                        else
                        {
                            img = Image.FromFile(_imageCache);
                            img = img.GetThumbnailImage(54, 65, null, new IntPtr());
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.Error(e);
                    }
                }
                else
                {
                    byte[] imageBytes = _usuarioProcess.GetAvatar(idUsuario);

                    if (imageBytes != null && imageBytes.Length > 0)
                    {
                        try
                        {
                            using (var stream = new MemoryStream(imageBytes))
                            {
                                stream.Position = 0;
                                img = Image.FromStream(stream, false, true);
                                img = img.GetThumbnailImage(54, 65, null, new IntPtr());
                            }
                        }
                        catch(Exception e)
                        {
                            Logger.Error(e);
                        }

                        if (img != null)
                        {
                            if (!BytesToFile(_imageCache, imageBytes))
                                img = null;
                        }
                    }
                }
            }

            return img;
        }

        public void SaveFile(Byte[] fileBytes, string fileName)
        {
            var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
            fileStream.Write(fileBytes, 0, fileBytes.Length);
            fileStream.Close();
        }

        public bool BytesToFile(string fileName, byte[] bytes)
        {
            try
            {
                var fileStream = new FileStream(fileName, System.IO.FileMode.Append);
                fileStream.Write(bytes, 0, bytes.Length);
                fileStream.Close();
                return true;
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }

            return false;
        }
       
    }

}