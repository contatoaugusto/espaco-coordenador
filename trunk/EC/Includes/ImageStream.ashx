<%@ WebHandler Language="C#" Class="ImageStream" %>
using System;
using System.Web;
using System.Web.SessionState;

//SGI
using SGI.Common;

public class ImageStream : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "APPLICATION/OCTET-STREAM";
        
        if (context.Session == null)
            return;
        
        if (context.Session[Const.BYTE_CACHE] == null || context.Session[Const.FILENAME_CACHE] == null)
            return;
        
        byte[] streamSource = (byte[])context.Session[Const.BYTE_CACHE];
        string fileName = Library.ToString(context.Session[Const.FILENAME_CACHE]);
        context.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");
        context.Response.BinaryWrite(streamSource);
        context.Response.Flush();
        context.Session.Remove(Const.BYTE_CACHE);
        context.Session.Remove(Const.FILENAME_CACHE);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}