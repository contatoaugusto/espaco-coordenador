using System;
using System.Globalization;
using System.Web.UI;

namespace EC.UI.WebControls
{
    public class LookupPage : Page
    {
        protected object Result
        {
            get
            {
                string key;
                key = Request.QueryString["Parent"] + "_Result";
                return Session[key];
            }
            set
            {
                string key;
                key = Request.QueryString["Parent"] + "_Result";
                Session[key] = value;
            }
        }

        protected void RefreshParentDialog()
        {
            //string format = "<script>window.close();window.opener.execScript('__doPostBack(\"{0}\",\"Selected\");','JScript');</script>";
            // Cross-browser script thanks to Janus
            string format = "<script>window.close();window.opener.location.href='javascript:void(__doPostBack(\"{0}\",\"Selected\"))';</script>";
            Page.ClientScript.RegisterClientScriptBlock(GetType(), "Close",
                String.Format(CultureInfo.CurrentCulture, format, Request.QueryString["Parent"]));
        }
    }
}
