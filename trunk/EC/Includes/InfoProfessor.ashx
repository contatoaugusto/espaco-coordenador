<%@ WebHandler Language="C#" Class="InfoProfessor" %>

using System;
using System.Web;
using System.Data;
using System.Text;
using SGI.Common;
using System.Web.SessionState;
using UI.Web.EA;

public class InfoProfessor : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/html";

        if (!string.IsNullOrEmpty(context.Request.Url.Query))
        {
            int idProfessor = context.Request.Url.Query.Replace("?", "").ToInt32();

            if (idProfessor > 0)
            {
                DataTable data = new SGI.DataContext.Controller.Administrativo.Funcionario().GetListInformacoesAcademicasProfessor(idProfessor);

                int idPessoa = 0;
                string nmPessoa = "";
                string deTipoTituloProfessor = "";
                string deCurso = "";
                string deAreaConhecimento = "";
                string edCurriculoLattes = "";
                
                foreach (DataRow row in data.Rows)
                {
                    idPessoa = row["idPessoa"].ToInt32();
                    nmPessoa = row["nmPessoa"].ToString();
                    deTipoTituloProfessor = row["deTipoTituloProfessor"].ToString();
                    deCurso = row["deCurso"].ToString();
                    deAreaConhecimento = row["deAreaConhecimento"].ToString();
                    edCurriculoLattes = row["edCurriculoLattes"].ToString();
                    
                    
                    context.Response.Write(CreateItem(idPessoa, nmPessoa, deTipoTituloProfessor, deCurso, deAreaConhecimento, edCurriculoLattes));
                }
            }
            else
                context.Response.Write("Informações não disponíveis para esse professor");

        }
    }
    
        private string CreateItem(long idPessoa, string nmPessoa, string deTipoTituloProfessor, string deCurso, string deAreaConhecimento, string edCurriculoLattes)
    {
        StringBuilder html = new StringBuilder();
        
        if(deTipoTituloProfessor.Trim().Length == 0)
            deTipoTituloProfessor = "Currículo";
        
                if(deCurso.Trim().Length == 0)
            deCurso = "Não Cadastrado";
        
        html.Append("<div class=\"tooltip-professor\">");
        html.Append("    <div class=\"foto\">");
        html.AppendFormat("    <img src=\"{0}?{1}\" style=\"width:50px;padding-right:5px;\" alt=\"Foto do Professor\" />", Library.ResolveClientUrl("~/includes/fotopessoa.ashx"), idPessoa);
        html.Append("    </div>");
        html.Append("    <div class=\"info\">");
        html.AppendFormat("        <h3>{0}</h3>", nmPessoa);
        html.AppendFormat("        <strong>{0}</strong>", deCurso);

        
        html.Append("        <br />");
        html.Append("        <br />");
        html.AppendFormat("        {0}", deAreaConhecimento.ToString().Replace(";", "<br/>"));

        if (!string.IsNullOrEmpty(edCurriculoLattes))
        {
            html.Append("        <br />");
            html.Append("        <br />");
            html.AppendFormat("        <img src=\"{0}\" complete=\"complete\"/>&nbsp;<a href=\"{1}\" target=\"_blank\">Currículo Lattes</a>", Utils.GetUrlImageTheme("link.png"), edCurriculoLattes.ToString().Replace(";", "<br/>"));
        }
        
        html.Append("    </div>");
        html.Append("    <div class=\"cb\"></div>");
        html.Append("</div>");

        return html.ToString();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}