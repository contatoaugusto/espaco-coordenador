<%@ WebHandler Language="C#" Class="CEP" %>

using System;
using System.Web;
using System.Text;
using System.Data;
using SGI.Common;
using System.Web.SessionState;

public class CEP : IHttpHandler, IRequiresSessionState {

    public void ProcessRequest(HttpContext context)
    {
        context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        context.Response.ContentType = "text/json";

        if (!string.IsNullOrEmpty(context.Request.Url.Query))
        {
            string nuCEP = context.Request.Url.Query.Replace("?", "");
            var searchCep = new SGI.DataContext.Controller.Coorporativo.CEP().GetByCEP(nuCEP);

            if (!searchCep.IsNull())
                context.Response.Write(GetResultDataJSON(searchCep));
        }
    }

    public string GetResultDataJSON(DataTable data)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("[");

        int index = 0;
        int count = data.Rows.Count;

        foreach (DataRow row in data.Rows)
        {
            index++;

            builder.Append("{");
            builder.AppendFormat("{0}:'{1}',", "nmBairro", row["nmBairro"]);
            builder.AppendFormat("{0}:'{1}',", "edLogradouro", row["edLogradouro"]);
            builder.AppendFormat("{0}:'{1}',", "nmCidade", row["nmCidade"]);
            builder.AppendFormat("{0}:'{1}'", "idUF", row["idUF"]); 
            builder.Append("}");

            if (index < count)
                builder.Append(",");
        }

        builder.Append("]");

        return builder.ToString();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}