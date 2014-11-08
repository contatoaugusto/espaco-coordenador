using System;
using System.Data;
using System.IO;
using System.Web;
using EC.Common;
using EC.Modelo;
using EC.Negocio;

namespace UI.Web.EC
{
    /// <summary>
    /// Summary description for Utils
    /// </summary>
    public sealed class Utils
    {
        public static string Password = "&*(%$$EA@#)";

        public static string PortalUrl = "http://www.uniceub.com.br";

        public static System.Collections.Generic.List<string> GetTemplates()
        {
            System.Collections.Generic.List<string> tmp = new System.Collections.Generic.List<string>();
            string[] templates = Directory.GetDirectories(HttpContext.Current.Server.MapPath("~/Templates"));

            foreach (string template in templates)
                tmp.Add(template.Split('\\')[template.Split('\\').Length - 1]);

            return tmp;
        }

        public static object FormatNumeroProtocolo(object value)
        {
            if (Library.ToInteger(value) < 100000000)
            {
                return string.Format("0{0}", Library.ToInteger(value));
            }
            else
            {
                return value;
            }
        }

        public static string FormatNumeroProtocolo(string value)
        {
            return ((string)FormatNumeroProtocolo(((object)value)));

        }

        //public static string GetUrlMasterPage(string nameMasterPage)
        //{
        //    if (Session.idAluno > 0)
        //        return string.Format("~/Templates/{0}/MasterPage.master", Session.Template);
        //    else
        //        return string.Format("~/Templates/{0}/MasterPage.master", Utils.GetTemplates()[0]);
        //}

        public static string GetPeriodo(int nuPeriodo)
        {
            if (nuPeriodo == 1)
                return "Primeiro Semestre";
            else if (nuPeriodo == 2)
                return "Segundo Semestre";
            else if (nuPeriodo == 3)
                return "Terceiro Semestre";
            else if (nuPeriodo == 4)
                return "Quarto Semestre";
            else if (nuPeriodo == 5)
                return "Quinto Semestre";
            else if (nuPeriodo == 6)
                return "Sexto Semestre";
            else if (nuPeriodo == 7)
                return "Sétimo Semestre";
            else if (nuPeriodo == 8)
                return "Oitavo Semestre";
            else if (nuPeriodo == 9)
                return "Nono Semestre";
            else if (nuPeriodo == 10)
                return "Décimo Semestre";
            else if (nuPeriodo == 11)
                return "Décimo Primeiro Semestre";
            else if (nuPeriodo == 12)
                return "Décimo Segundo Semestre";
            return "";
        }

        public static string GetPeriodoLetivo(int nuPeriodoLetivo)
        {
            if (nuPeriodoLetivo == 1)
                return "Primeiro Semestre";
            else if (nuPeriodoLetivo == 2)
                return "Segundo Semestre";
            return "";
        }

        public static string GetUrlImageTheme(string image)
        {
            return Library.ResolveClientUrl("~/App_Themes/" + Session.Template + "/Images/" + image);
        }

        public static string GetPathThemesImage()
        {
            return Library.ResolveClientUrl("~/App_Themes/" + Session.Template + "/Images/");
        }

        public static string GetPathRelativeImageTheme(string image)
        {
            return string.Format("App_Themes\\{0}\\Images\\{1}", Session.Template, image);
        }

        public static string MaxLenghtConcate(string value, int maxLenght)
        {
            if (value.Length > maxLenght)
                return value.Substring(0, maxLenght) + "...";
            return value;
        }

        /// <summary>
        /// Valida o tamanho de um nome. Ex: Stiven Fabiano da Câmara irá mostrar Stiven Câmara
        /// </summary>
        /// <param name="nmAluno"></param>
        /// <returns></returns>
        public static string ValidateName(object nmAluno)
        {
            if (nmAluno == null)
                return "Não informado";

            nmAluno = nmAluno.ToString().Replace("'", "");

            string[] partes = nmAluno.ToString().Split(' ');
            string[] naoUtilizar = new string[] { "da", "de", "do", "dos", "e", "a", "o", "i", "das", "um", "uma", "uns", "umas" };
            bool useParte2 = true;

            if (partes.Length == 1)
            {
                return partes[0];
            }
            else if (partes.Length == 2)
            {
                foreach (string w in naoUtilizar)
                {
                    if (w.ToLower() == partes[1])
                    {
                        useParte2 = false;
                        break;
                    }
                }

                if (useParte2)
                    return string.Format("{0} {1}", partes[0], partes[1]);
                else
                    return string.Format("{0}", partes[0]);
            }
            else if (partes.Length >= 3)
            {
                foreach (string w in naoUtilizar)
                {
                    if (w.ToLower() == partes[1])
                    {
                        useParte2 = false;
                        break;
                    }
                }

                if (useParte2)
                    return string.Format("{0} {1}", partes[0], partes[1]);
                else
                    return string.Format("{0} {1}", partes[0], partes[2]);
            }

            return "";
        }

        /// <summary>
        /// Parses the BBCode into XHTML in a safe non-breaking manor.]
        /// Sample:
        /// BBCode(ref body, "b", "strong");
        /// BBCode(ref body, "i", "em");
        /// BBCode(ref body, "cite", "cite");
        /// </summary>
        public static void BBCode(ref string body, string code, string tag)
        {
            int start = body.IndexOf("[" + code + "]", StringComparison.Ordinal);
            if (start > -1)
            {
                if (body.Contains("[/" + code + "]"))
                {
                    body = body.Remove(start, code.Length + 2);
                    body = body.Insert(start, "<" + tag + ">");

                    int end = body.IndexOf("[/" + code + "]", start, StringComparison.Ordinal);

                    body = body.Remove(end, code.Length + 3);
                    body = body.Insert(end, "</" + tag + ">");

                    BBCode(ref body, code, tag);
                }
            }
        }

        public static object ConvertBooleanToString(object value)
        {
            if (Convert.ToBoolean(value))
                return "Sim";
            else
                return "Não";
        }

        public static string FormatTextGridView(object text, int maxlength)
        {
            return FormatTextGridView(text.ToString(), maxlength);
        }

        public static string FormatTextGridView(string text)
        {
            return FormatTextGridView(text.ToString(), text.Trim().Length);
        }

        public static string FormatTextGridView(string text, int maxlength)
        {

            if (text.Trim().Length == 0)
                text = "-";

            if (text.Trim().Length > maxlength)
                text = text.Substring(0, maxlength) + "...";



            return text;
        }

        //public static void RenderReportToPFD(HttpContext context, Microsoft.Reporting.WebForms.ReportViewer rViewer)
        //{
            //Microsoft.Reporting.WebForms.Warning[] warnings;
            //string[] streamids;
            //string mimeType = "";
            //string encoding = "";
            //string extension = "";
            //// string deviceInfo;

            //byte[] bytes = rViewer.LocalReport.Render(
            //    "PDF", null, out mimeType, out encoding, out extension,
            //    out streamids, out warnings);

            //context.Response.Buffer = true;
            //context.Response.Clear();
            //context.Response.ContentType = mimeType;
            //context.Response.AddHeader("content-disposition", "outline; filename=20840718." + extension);
            //context.Response.BinaryWrite(bytes);
            //context.Response.Flush();
        //}

        //public static void SaveConfiguration(Configuration configuration)
        //{
        //    Library.SaveBinaryData(PathConfiguration, configuration);
        //}

        //public static string PathConfiguration
        //{
        //    get { return string.Format("{0}/App_Data/Users/{1}.bin", HttpContext.Current.Server.MapPath("."), Session.idAluno); }
        //}

        //public static Configuration ReadConfiguration()
        //{
        //    Configuration o = new Configuration();

        //    if (File.Exists(PathConfiguration))
        //        o = (Configuration)Library.LoadBinaryData(Utils.PathConfiguration);

        //    if (o == null)
        //        o = new Configuration();

        //    Session.Template = o.Template;

        //    return o;
        //}

        public static string GetUsuarioPerfil(object idUsuarioOwner)
        {
            //var us = new SGI.DataContext.Controller.Sistema.Usuario().Bind(Library.ToInteger(idUsuarioOwner));
            //if (us != null)
            //{
            //    if (us.idPerfilComunicacao == 0)
            //    {
            //        //nome                
            //        string[] s = us.nmPessoa.Split(' ');
            //        return (s[0] + "&nbsp;" + s[s.Length - 1]);
            //    }
            //    var pc = new SGI.DataContext.Controller.Coorporativo.PerfilComunicacao().Bind(us.idPerfilComunicacao);
            //    if (pc != null)
            //    {
            //        if (pc.icUsaDescricaoPerfil)
            //        {
            //            //perfil
            //            return (pc.dePerfilComunicacao);
            //        }
            //        else
            //        {
            //            if (pc.idPerfilComunicacao == 7)
            //            {
            //                //prof.: + nome                
            //                string[] s = us.nmPessoa.Split(' ');
            //                return ("Prof.&nbsp;" + s[0] + "&nbsp;" + s[s.Length - 1]);
            //            }
            //            else if (pc.idPerfilComunicacao == 6)//Coordenador
            //            {
            //                //O Cobbe pediu para alterar de cood -> prof              
            //                string[] s = us.nmPessoa.Split(' ');
            //                return ("Prof.&nbsp;" + s[0] + "&nbsp;" + s[s.Length - 1]);
            //            }
            //            else
            //            {
            //                //nome                
            //                string[] s = us.nmPessoa.Split(' ');
            //                return (s[0] + "&nbsp;" + s[s.Length - 1]);
            //            }

            //        }
            //    }
            //}
            return "";
        }

        /// <summary>
        /// Usado para verificar os alunos participantes do ENADE.
        /// O arquivo ENADE.xml é enviado pelo Fabiano Raimundo
        /// e disponibilizado na pasta App_Data/ENADE.xml.
        /// A validação da participação do aluno é verificada na montagem do menu (MenuHandler.cs) do respectivo.
        /// </summary>
        /// <returns>bool</returns>
        public static bool AlunoParticipanteENADE()
        {
            string path = AppSettings.PathRoot + @"App_Data\ENADE.xml";

            if (System.IO.File.Exists(path) & Session.coAcesso.Length > 0)
            {
                DataSet data = null;

                if (HttpContext.Current.Cache["DATAENADE"] != null)
                    data = HttpContext.Current.Cache["DATAENADE"] as DataSet;

                if (data == null)
                {
                    data = new DataSet();
                    data.ReadXml(path, XmlReadMode.InferSchema);

                    if (data != null)
                        if (data.Tables[0].Rows.Count > 0)
                            HttpContext.Current.Cache.Insert("DATAENADE", data);
                }

                DataView view = data.Tables[0].DefaultView;
                view.RowFilter = "ra = " + Session.coAcesso;

                DataTable tmp = Library.ConvertDataViewToDataTable(view);
                return (tmp.Rows.Count > 0);
            }

            return false;
        }

        public static void NoCache()
        {
            System.Web.HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            System.Web.HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            System.Web.HttpContext.Current.Response.Cache.SetNoStore();

            //Response.ClearHeaders();
            //Response.AppendHeader("Cache-Control", "no-cache"); //HTTP 1.1
            //Response.AppendHeader("Cache-Control", "private"); // HTTP 1.1
            //Response.AppendHeader("Cache-Control", "no-store"); // HTTP 1.1
            //Response.AppendHeader("Cache-Control", "must-revalidate"); // HTTP 1.1
            //Response.AppendHeader("Cache-Control", "max-stale=0"); // HTTP 1.1 
            //Response.AppendHeader("Cache-Control", "post-check=0"); // HTTP 1.1 
            //Response.AppendHeader("Cache-Control", "pre-check=0"); // HTTP 1.1 
            //Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.1 
            //Response.AppendHeader("Keep-Alive", "timeout=3, max=993"); // HTTP 1.1 
            //Response.AppendHeader("Expires", "Mon, 26 Jul 1997 05:00:00 GMT"); // HTTP 1.1 
        }

        public static string ValidaUrlSGI()
        {
            if (HttpContext.Current.Request.Url.AbsoluteUri.IndexOf("hml.espacoaluno.") > -1)
                return "http://hml.sgi.uniceub.br";
            else if (HttpContext.Current.Request.Url.AbsoluteUri.IndexOf("dsv.espacoaluno.") > -1 | HttpContext.Current.Request.Url.AbsoluteUri.IndexOf("localhost") > -1)
                return "http://dsv.sgi.uniceub.br";
            else if (HttpContext.Current.Request.Url.AbsoluteUri.IndexOf("hmlu.espacoaluno.") > -1)
                return "http://hmlu.sgi.uniceub.br";
            else
                return "https://www.sgi.uniceub.br";
        }

        public static bool IsMobileUser()
        {
            HttpContext context = HttpContext.Current;

            if (context.Request.Browser.IsMobileDevice)
                return true;

            if (context.Request.ServerVariables["HTTP_X_WAP_PROFILE"] != null)
                return true;

            if (context.Request.ServerVariables["HTTP_ACCEPT"] != null && context.Request.ServerVariables["HTTP_ACCEPT"].ToLower().Contains("wap"))
                return true;

            if (context.Request.ServerVariables["HTTP_USER_AGENT"] != null)
            {
                string[] mobiles = new[] { 
                    "nokia", "windows ce", "blackberry", 
                    "symbian", "samsung", "HTC", 
                    "sony", "alcatel", "philips", 
                    "panasonic", "sharp", "benq", 
                    "iphone", "android", "mobile", 
                    "mini", "opera mini", "opera mobile", 
                    "ericsson", "sonyericsson"
                };

                foreach (string s in mobiles)
                    if (context.Request.ServerVariables["HTTP_USER_AGENT"].ToLower().Contains(s.ToLower()))
                        return true;
            }
            return false;
        }

        public static int LimiteSoliciticaoProtocoloPorAluno
        {
            get { return Library.ToInteger(System.Configuration.ConfigurationManager.AppSettings["LimiteSoliciticaoProtocoloPorAluno"]); }
        }

        public static void RedirectToPrint(string title, string url, string mapTitle)
        {
            HttpContext.Current.Response.Redirect(string.Format("~/impressao.aspx{0}", Url.CreateUrlParams(title, url, mapTitle, ButtomPrintAction.ShowHtmlButtons)));
        }

        public static void RedirectToPrint(string title, string url, string mapTitle, ButtomPrintAction action)
        {
            HttpContext.Current.Response.Redirect(string.Format("~/impressao.aspx{0}", Url.CreateUrlParams(title, url, mapTitle, action)));
        }

        public static string GetUrlRedirectToPrint(string title, string url, string mapTitle)
        {
            return Library.ResolveClientUrl(string.Format("~/impressao.aspx{0}", Url.CreateUrlParams(title, url, mapTitle, ButtomPrintAction.ShowHtmlButtons)));
        }

        public static string GetUrlRedirectToPrint(string title, string url, string mapTitle, ButtomPrintAction action)
        {
            return Library.ResolveClientUrl(string.Format("~/impressao.aspx{0}", Url.CreateUrlParams(title, url, mapTitle, action)));
        }

        private static void RenderReportToFormat(HttpContext context, Microsoft.Reporting.WebForms.ReportViewer reportViewer, string format)
        {
            RenderReportToFormat(context, reportViewer, format, string.Empty);
        }

        private static void RenderReportToFormat(HttpContext context, Microsoft.Reporting.WebForms.ReportViewer reportViewer, string format, string reportName)
        {
            Microsoft.Reporting.WebForms.Warning[] warnings;
            string[] streamids;
            string mimeType = "";
            string encoding = "";
            string extension = "";
            // string deviceInfo;

            byte[] bytes = reportViewer.LocalReport.Render(
                format, null, out mimeType, out encoding, out extension,
                out streamids, out warnings);

            context.Response.Buffer = true;
            context.Response.Clear();
            context.Response.ContentType = mimeType;
            if (reportName == string.Empty)
            {
                context.Response.AddHeader("content-disposition", string.Format("outline; filename={0}.{1}", Guid.NewGuid().ToString(), extension));
            }
            else
            {
                context.Response.AddHeader("content-disposition", string.Format("outline; filename={0}.{1}", reportName, extension));
            }
            context.Response.BinaryWrite(bytes);
            context.Response.Flush();
        }

        public static void RenderReportToPDF(HttpContext context, Microsoft.Reporting.WebForms.ReportViewer reportViewer)
        {
            RenderReportToFormat(context, reportViewer, "PDF");
        }

        public static void RenderReportToPDF(HttpContext context, Microsoft.Reporting.WebForms.ReportViewer reportViewer, string reportName)
        {
            RenderReportToFormat(context, reportViewer, "PDF", reportName);
        }

        public static void RenderReportToExcel(HttpContext context, Microsoft.Reporting.WebForms.ReportViewer reportViewer)
        {
            RenderReportToFormat(context, reportViewer, "Excel");
        }

        public static void RenderReportToImage(HttpContext context, Microsoft.Reporting.WebForms.ReportViewer reportViewer)
        {
            RenderReportToFormat(context, reportViewer, "Image");
        }

        public static void RenderReportToExcel(HttpContext context, Microsoft.Reporting.WebForms.ReportViewer reportViewer, string reportName)
        {
            RenderReportToFormat(context, reportViewer, "Excel", reportName);
        }

        /// <summary>
        /// Verifica se o acesso a uma determinada página está correto.
        /// Ou seja, para acessar uma página o usuário só pode ter vindo determinadas páginas.
        /// </summary>
        /// <param name="referrerPages">Páginas de referências</param>
        /// <returns>true|false</returns>
        public static bool IsValidPageAccess(string[] referrerPages)
        {
            try
            {
                bool redirect = false;

                string url = HttpContext.Current.Request.ServerVariables["HTTP_REFERER"].ToLower();

                foreach (string page in referrerPages)

                    if (url.IndexOf(page.ToLower()) > -1)
                        return true;

                return redirect;
            }
            catch
            {
                return false;
            }
        }

        public static void ValidaUrl()
        {
            if (Url.HasParams)
            {
                if (HttpContext.Current.Session.SessionID != Url.GetSessionID())
                {
                    HttpContext.Current.Response.Redirect(Library.ResolveClientUrl("~/erros/urlinvalida.aspx"), false);
                    return;
                }
            }
        }

        public static bool ValidaFaseMatricula()
        {
            //if (Library.IsDateEmpty(SessionMatricula.dtInicioFaseMatricula)) return false;
            //if (Library.IsDateEmpty(SessionMatricula.dtFimFaseMatricula)) return false;
            //if (SessionMatricula.dtInicioFaseMatricula > DateTime.Now) return false;
            //if (SessionMatricula.dtFimFaseMatricula < DateTime.Now) return false;


            //var fase = new SGI.DataContext.Controller.Academico.FaseMatricula().BindByAlunoMatricula(SessionAluno.idAlunoMatricula);

            //if (fase == null)
            //    return false;

            return true;
        }

        public static AlertList VerificaPendenciasAluno(int idAlunoMatricula)
        {
            AlertList alertList = new AlertList();

            //var faseMatricula = new SGI.DataContext.Controller.Academico.FaseMatricula().BindByAlunoMatricula(idAlunoMatricula);

            //var alunoMatricula = new SGI.DataContext.Controller.Academico.Matricula().BindAlunoMatricula(idAlunoMatricula);



            ////-- Aluno ou Fase matricula não instanciados
            //if (alunoMatricula == null) alertList.Add("010");

            //if (faseMatricula == null) alertList.Add("560");

            ////******************* DESCOMENTAR *************************
            ////System.Web.HttpContext.Current.Response.Write("<strong>*********** VALIDAÇÃO Conferir Dados Pessoas esta COMENTADA ****************</strong><br>");
            ////--> Validação Confirmação dos Dados Pessoais
            //if (alunoMatricula.icConferiuDadosPessoais != "S")// && Common.Session.IPUsuario.IndexOf("172.") == -1)  // remover posteriormente - clever
            //    alertList.Add("1035");

            ////--> Não validar para alunos dos cursos sequenciais inferiores a 2005/2
            //int nuPeriodoIngresso = Library.ToInteger(alunoMatricula.aaIngresso.ToString() + "0" + alunoMatricula.nuPeriodoIngresso.ToString());
            ////******************* DESCOMENTAR *************************
            ////System.Web.HttpContext.Current.Response.Write("<strong>*********** VALIDAÇÃO LEITURA DO CONTRATO COMENTADA ****************</strong><br>");
            ////--> Validação da Leitura do Contrato
            //if ((alunoMatricula.idTipoCurso != 3) && (alunoMatricula.idTipoCurso != 5))
            //{
            //    if (alunoMatricula.icAceitouContrato != "S")//  && Common.Session.IPUsuario.IndexOf("172.") == -1) // remover posteriormente - clever
            //        alertList.Add("1036");
            //}

            ////-->> Validações primárias
            //if (alertList.HasAlert()) return alertList;

            ////-->> Somente verificar a validação financeira para alunos não matriculados
            //if (alunoMatricula.idSituacaoAcademica != 1)
            //{
            //    if (alunoMatricula.idTipoCurso != 5 || (alunoMatricula.idTipoCurso == 5 && nuPeriodoIngresso >= 200502))
            //    {
            //        //-- verifica o pagamento da primeira parcela
            //        if (!SessionMatricula.icPrimeiraParcelaEmDia && Library.StringToBool(faseMatricula.icPrimeiraParcela))
            //        {
            //            if (!SessionMatricula.icLiberaMatricula)
            //                alertList.Add("529");
            //        }//-- verifica pagamento da 1a. parcela
            //        else if (!SessionMatricula.icLiberaMatricula)
            //        {
            //            if (Library.StringToBool(faseMatricula.icPrimeiraParcela) && (!SessionMatricula.icPrimeiraParcelaEmDia))
            //            {
            //                alertList.Add("529");
            //            }
            //        }//-- verifica se o pagamento está em dia

            //        if (Library.StringToBool(faseMatricula.icPagamentoEmDia) && (!SessionMatricula.icPagamentoEmDia))
            //        {
            //            alertList.Add("1037");
            //        }
            //    }
            //}

            //if (alunoMatricula.idSituacaoAcademica != 1 && alunoMatricula.idSituacaoAcademica != 8 && alunoMatricula.idSituacaoAcademica != 9)
            //    alertList.Add("612", alunoMatricula.deSituacaoAcademica);

            ////-- não permitir a entrada de alunos matriculado ou pré-matriculado
            //if (faseMatricula.icInclusaoDisciplina != "S")
            //{
            //    if (alunoMatricula.idSituacaoAcademica == 1 || alunoMatricula.idSituacaoAcademica == 9)
            //        alertList.Add("562");
            //}

            ////-- não permitir a entrada de alunos matriculado ou pré-matriculado
            //if (faseMatricula.icMatriculaInternet != "S")
            //{
            //    if (alunoMatricula.idSituacaoAcademica != 1 && alunoMatricula.idSituacaoAcademica != 9)
            //        alertList.Add("1039", alunoMatricula.deSituacaoAcademica);
            //}

            //if (Library.StringToBool(faseMatricula.icFaseSugestao) &&
            //    !Library.StringToBool(alunoMatricula.icMatriculaSugerida))
            //    alertList.Add("574");


            ////-- verifica se libera para retorno / reabertura
            //if (alunoMatricula.idSituacaoAcademica == 2)
            //{
            //    if (!Library.StringToBool(faseMatricula.icReabertura))
            //        alertList.Add("1039", "Trancado");
            //    else if (!Library.StringToBool(alunoMatricula.icRetornoReabertura))
            //        alertList.Add("1042");
            //}
            //else if (alunoMatricula.idSituacaoAcademica == 3)
            //{
            //    if (!Library.StringToBool(faseMatricula.icRetorno))
            //        alertList.Add("1039", "Abandono");
            //    else if (!Library.StringToBool(alunoMatricula.icRetornoReabertura))
            //        alertList.Add("1043");

            //}

            ////-->> Parâmetro que controla acesso de calouro no sistema.
            //if (alunoMatricula.aaIngresso == alunoMatricula.aaPeriodoLetivo &&
            //    alunoMatricula.nuPeriodoIngresso == alunoMatricula.nuPeriodoLetivo)
            //{
            //    //-->> Parâmetro não é validado para alunos da pós-graduação
            //    if (faseMatricula.icMatriculaCalouro != "S" && alunoMatricula.idTipoCurso != 3)
            //        alertList.Add("1041");

            //}

            ////-- verifica se é reabertura
            //if ((!Library.StringToBool(faseMatricula.icReabertura)) && alunoMatricula.idSituacaoAcademica == 2 && (!Library.StringToBool(alunoMatricula.icRetornoReabertura)))
            //    alertList.Add("1042");
            ////-- verifica se habilita retorno
            //if ((!Library.StringToBool(faseMatricula.icRetorno)) && alunoMatricula.idSituacaoAcademica == 3 && (!Library.StringToBool(alunoMatricula.icRetornoReabertura)))
            //    alertList.Add("1043");
            ////-- verifica se é permitida para alunos não matriculados
            ////--if(faseMatricula.icInclusaoDisciplina == "N" && alunoMatricula.idSituacaoAcademica != 1) alertList.Add("");

            ////-->> Validação somente para os alunos do curso de mestrado.
            //if (alunoMatricula.idTipoCurso == 4)
            //{

            //    var pesquisaAluno = new SGI.DataContext.Controller.Academico.PesquisaAluno().BindByPeriodoLetivoAluno(alunoMatricula.idPeriodoLetivo.ToInt32(), alunoMatricula.idAluno.ToInt32());

            //    if (pesquisaAluno == null)
            //        alertList.Add("1044");

            //}
            return alertList;
        }

        public static string ElapsedTime(DateTime startTime)
        {
            TimeSpan elapsedTime = DateTime.Now.Subtract(startTime);

            //Verifico quantos anos se passaram
            {
                int years = elapsedTime.Days / 365;
                if (years > 0)
                    return years + (years == 1 ? " ano" : " anos");
            }
            //Verifico quantos meses se passaram
            {
                int months = elapsedTime.Days / 30;
                if (months > 0)
                    return months + (months == 1 ? " mês" : " meses");
            }
            //Verifico quantos dias se passaram
            {
                if (elapsedTime.Days > 0)
                    return elapsedTime.Days + (elapsedTime.Days == 1 ? " dia" : " dias");
            }
            //Verifico quantas horas se passaram
            {
                if (elapsedTime.Hours > 0)
                    return elapsedTime.Hours + (elapsedTime.Hours == 1 ? " hora" : " horas");
            }
            //Verifico quantos minutos se passaram
            {
                if (elapsedTime.Minutes > 0)
                    return elapsedTime.Minutes + (elapsedTime.Minutes == 1 ? " minuto" : " minutos");
            }
            //Verifico quantos segundos se passaram
            {
                if (elapsedTime.Seconds > 0)
                    return elapsedTime.Seconds + (elapsedTime.Seconds == 1 ? " segundo" : " segundos");
                else
                    return "1 segundo";
            }
        }

        public static string ValidateTypeOfJson(object value)
        {
            if (value == null)
                return "";
            else if (value.GetType() == typeof(bool))
                return value.ToString().ToLower();
            else if (value.GetType() == typeof(int))
                return value.ToString();
            else if (value.GetType() == typeof(long))
                return value.ToString();
            else if (value.GetType() == typeof(DateTime))
                return string.Format("'{0}'", Library.ToDate(value).ToString("dd/MM/yyyy"));
            else if (value.GetType() == typeof(string))
                return string.Format("'{0}'", value.ToString().Replace("'", "").Replace("\n", "<br />").Replace("\r", "").Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;"));

            return value.ToString();
        }

        public static string PathImagesCache
        {
            get
            {

                return string.Format(@"{0}_imagecache\", AppSettings.PathUpload);
                
            }
        }

        public static string GetScriptVersion()
        {
            return string.Concat("?v=", AppSettings.ScriptVersion);
        }

        public static object FormatCNPJCPF(object value)
        {
            switch (Library.ToString(value).Length)
            {
                case 14:
                    return Library.MaskCNPJ(value.ToString());
                case 11:
                    return Library.MaskCPF(value.ToString());
                default:
                    return string.Empty;
            }

        }

        public static bool CoordenadorNaoLogado()
        {
            HttpContext context = HttpContext.Current;
            return (((SessionUsuario)context.Session["USUARIO"]).USUARIO.ID_USUARIO == 0);
        }

        public static SessionUsuario GetUsuarioLogado()
        {
            HttpContext context = HttpContext.Current;
            return (((SessionUsuario)context.Session["USUARIO"]));
        }

        public static string GetCargos(int idPessoa)
        {
            string cargo = "";
            var cargoList = NCargo.ConsultarByPessoa(idPessoa);

            foreach (var obj in cargoList)
            {
                cargo += obj.DESCRICAO + "\n";
            }

            return cargo;
        }
    }
}
