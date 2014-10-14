using System;
using System.Resources;
using System.Text;
namespace EC.Common
{
}

namespace EC.Common
{

    sealed public class ClientScript
    {
        public static string __doPostBack()
        {
            try
            {
                return GetScript("__doPostBack");
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        public static string ValidClient()
        {
            try
            {
                return GetScript("ValidClient");
                //return String.Empty;

            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        public static string ContratoAceito()
        {
            try
            {
                return GetScript("ContratoAceito");

            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }


        public static string CountControl()
        {
            try
            {
                return GetScript("CountControl");
                //return String.Empty;

            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        /// <summary>
        /// Script para somar um array de objetos. Parametros: nome do array e objeto que vai mostrar o total
        /// </summary>
        /// <returns>Script para somar um array de objetos</returns>
        public static string SumArrayControl()
        {
            try
            {
                return GetScript("SumArrayControl");
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        public static string ShowValidators()
        {
            try
            {
                return GetScript("ShowValidators");
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        public static string OpenWindowScreenCenter()
        {
                return GetScript("OpenWindowScreenCenter");
        }

        /// <summary>
        /// Abre uma popup a partir dos parametros informados.
        /// </summary>
        /// <param name="Url">Url da página a ser aberta</param>
        /// <param name="Width">Tamanho da propriedade width do objeto window em pixels</param>
        /// <param name="Heigth">Tamanho da propriedade heigth do objeto window em pixels</param>
        /// <param name="NameWindow">Nome do objeto que vai ser criado</param>
        /// <returns>Retorna o script para ser renderizar na página.</returns>
        public static string OpenWindowScreenCenter(string url, int width, int heigth, string nameWindow)
        {
            string script = GetScript("OpenWindowScreenCenter");
            script += "<script defer language=\"javascript\" type=\"text/javascript\">\n";
            script +=  "//<![CDATA[\n";
            script += "OpenWindowScreenCenter('" + url + "', " + heigth + ", " + width + ", '" + nameWindow + "');\n";
            script += "//]]>";
            script += "</script>\n";
            return script;
        }

        /// <summary>
        /// Abre uma popup a partir da url informada. A popup vai ser aberta 650 x 500 e no nome de sgipopup.
        /// </summary>
        /// <param name="Url">Url da página a ser aberta</param>
        /// <returns>Retorna o script para ser renderizar na página.</returns>
        public static string OpenWindowScreenCenter(string url)
        {
            return ClientScript.OpenWindowScreenCenter(url, 650, 500, Const.SGIPOPUP);
        }

        public static string PularCampoByIndex()
        {
            return GetScript("PularCampoByIndex");
        }

        public static string VerificReprovacaoFalta()
        {
            return GetScript("VerificReprovacaoFalta");
        }

        public static string VerificMencaoReprovacao()
        {
            return GetScript("VerificMencaoReprovacao");
        }

        public static string HideTable()
        {
            return GetScript("HideTable");
        }

        public static string ShowError()
        {
            return GetScript("ShowError");
        }

        public static string IsNumeric()
        {
            return GetScript("IsNumeric");
        }

        public static string MaskFormat()
        {
            return GetScript("MaskFormat");
        }

        public static string CalcMedia()
        {
            return GetScript("CalcMedia");
        }

        public static string CopyValue()
        {
            return GetScript("CopyValue");
        }

        public static string ValidMaxValue()
        {
            return GetScript("ValidMaxValue");
        }


        public static string HabDesCamposCheck()
        {
            return GetScript("HabDesCamposCheck");
        }

        public static string FormataMoeda()
        {
            return GetScript("FormataMoeda");
        }

        public static string FormataNota()
        {
            return GetScript("FormataNota");
        }

        public static string ReturnMencaoByLegenda()
        {
            return GetScript("ReturnMencaoByLegenda");
        }

        public static string ValidNotaMencao()
        {
            return GetScript("ValidNotaMencao");
        }

        public static string FormatNotaRedacao()
        {
            return GetScript("FormatNotaRedacao");
        }

        public static string ValidMencao()
        {
            return GetScript("ValidMencao");
        }

        public static string ShowHide()
        {
            return GetScript("ShowHide");
        }

        public static string copyValueSelect()
        {
            return GetScript("copyValueSelect");
        }

        public static string NumMaxFaltas()
        {
            return GetScript("NumMaxFaltas");
        }

        public static string Alfanumeric()
        {
            return GetScript("Alfanumeric");
        }

        public static string ShowHideTag()
        {
            return GetScript("ShowHideTag");
        }

        public static string DesformataInteiro()
        {
            return GetScript("DesformataInteiro");
        }
        public static string FormataValorInteiro()
        {
            return GetScript("FormataValorInteiro");
        }
        public static string NumFormat()
        {
            return GetScript("NumFormat");
        }
        public static string CalculaValorAditado()
        {
            return GetScript("CalculaValorAditado");
        }
        public static string ChangeParent()
        {
            return GetScript("ChangeParent");
        }


        private static string GetScript(string Key)
        {
            ResourceManager _ResourceManager = new ResourceManager("SGI.Common.ClientScript", typeof(ClientScript).Module.Assembly);
            return _ResourceManager.GetString(Key, null);
        }
        public static string CloseWindow()
        {
            return GetScript("CloseWindow");
        }


        /// <summary>
        /// Script para chamar evento na página pai e fechar popup.
        /// </summary>
        /// <param name="IDControl">ID do control que vai ser chamado a partir da função __doPostBack(eventTarget, eventArgument)</param>
        /// <returns>Retorna o script para ser renderizar na página.</returns>
        public static string CloseWindow(string IDControl)
        {
            string Script = string.Empty;
            if (IDControl.Trim().Length > 0)
                Script = ClientScript.CalldoPostBackOpener(IDControl);

            Script += GetScript("CloseWindow");
            return Script;
        }

        /// <summary>
        /// Chama o evento na página pai (window.opener).
        /// </summary>
        /// <param name="IDControl">ID do controle a ser chamado</param>
        /// <returns>Script para ser renderizado na pagina aberta.</returns>
        public static string CalldoPostBackOpener(string IDControl)
        {
            string Script = GetScript("CalldoPostBackOpener");
            Script = Script.Replace("@IDControl", IDControl);
            return Script;
        }

        public static string ClosePopupSetFocusInButton()
        {
            return GetScript("ClosePopupSetFocusInButton");
        }

        public static string ModelFileHTML()
        {
            return GetScript("ModelFileHTML");
        }

        public static string OpenModalDialogReturnCallEvent(string Url, string IDControl)
        {
            Url = "'" + Url + "'";
            IDControl = "'" + IDControl + "'";
            string Return = GetScript("OpenModalDialogReturnCallEvent");

            return Return.Replace("@url", Url).Replace("@IDControl", IDControl);
        }

        public static string ToolTipCSS()
        {

            return GetScript("ToolTipCSS");
        }

        public static string ToolTipJS()
        {
            return GetScript("ToolTipJS");
        }

        public static string Manutencao()
        {
            return GetScript("Manutencao");
        }

        public static string DoToolTip()
        {
            return GetScript("DoToolTip");
        }

        public static string TabulacaoByIndex()
        {
            return GetScript("TabulacaoByIndex");
        }

        public static string keyEvent()
        {
            return GetScript("keyEvent");
        }

        public static string IsPressedKey()
        {
            return GetScript("IsPressedKey");
        }

        public static string HtmlErrorPage()
        {
            return GetScript("HtmlErrorPage");
        }

        public static string ShowModalDialog(string selector, string closeSelector)
        {
            var str = string.Format(@"$('{0}').lightbox_me({{centered: true, overlaySpeed: 'fast', lightboxSpeed:'fast', closeSelector: '{1}' }});", selector, closeSelector);
            return str;
        }
    }
}
