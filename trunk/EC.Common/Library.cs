using System;
using System.Net.Mail;
using System.Collections;
using System.IO;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Configuration;
using System.Reflection;
using System.ComponentModel;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;
using UniCEUB.Core.Log;
using Encoder = System.Drawing.Imaging.Encoder;
using Image = System.Drawing.Image;
using System.Resources;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Configuration;
using System.Linq;
using OfficeOpenXml;
//using CrystalDecisions.CrystalReports.Engine;


namespace EC.Common
{
    public class Library
    {
        public static string GetPathUpload(int idUsuario)
        {
            return string.Format("{0}{1}\\", ConfigurationManager.AppSettings["PathUpload"], idUsuario);
        }

        public static string GetPathUploadEspacoAluno(int idUsuario)
        {
            return string.Format("{0}{1}\\", ConfigurationManager.AppSettings["PathUploadEspacoAluno"], idUsuario);
        }

        public static int GetMaxRequestLengthInWebConfig()
        {
            try
            {
                var section = ConfigurationManager.GetSection("system.web/httpRuntime") as HttpRuntimeSection;
                return section.MaxRequestLength * 1024;
            }
            catch
            {
                return 0;
            }
        }

        public static string CreateJSONObject(Dictionary<string, object> objects)
        {
            StringBuilder sb = new StringBuilder();
            int index = 0;
            sb.Append("{");

            foreach (KeyValuePair<string, object> pair in objects)
            {
                sb.AppendFormat("'{0}':{1}", pair.Key, ValidateTypeOfJson(pair.Value));


                if (index < objects.Count - 1)
                    sb.Append(", ");

                index++;
            }

            sb.Append("}");

            return sb.ToString();
        }

        public static T TransferProperties<T>(T source, T target)
        {
            if (source.GetType() != target.GetType())
                throw new Exception("Os objetos são diferentes");

            PropertyInfo[] sourceProperties = source.GetType().GetProperties();
            PropertyInfo[] targetProperties = target.GetType().GetProperties();

            foreach (PropertyInfo sourcePropertyInfo in sourceProperties)
            {
                foreach (PropertyInfo targetPropertyInfo in targetProperties)
                {
                    if (sourcePropertyInfo.Name.ToLower() == targetPropertyInfo.Name.ToLower())
                    {
                        object sourceValue = sourcePropertyInfo.GetValue(source, null);
                        if (targetPropertyInfo.CanWrite)
                        {
                            sourcePropertyInfo.SetValue(target, sourceValue, null);
                            break;
                        }
                    }
                }
            }

            return target;
        }

        public static T TransferProperties<T>(DataTable source, T target)
        {
            if (source.Rows.Count > 1)
                throw new Exception("A tabela não pode ter mais de um registro");

            var row = source.Rows[0];

            DataColumnCollection sourceColumns = source.Columns;
            PropertyInfo[] targetProperties = target.GetType().GetProperties();

            foreach (DataColumn sourceColumn in sourceColumns)
            {
                foreach (PropertyInfo targetPropertyInfo in targetProperties)
                {
                    if (sourceColumn.ColumnName.ToLower() == targetPropertyInfo.Name.ToLower())
                    {
                        object sourceValue = row[sourceColumn.ColumnName];
                        if (targetPropertyInfo.CanWrite)
                        {
                            targetPropertyInfo.SetValue(target, sourceValue, null);
                            break;
                        }
                    }
                }
            }

            return target;
        }

        public static string CreateStringArray(ICollection<int> list, string separator)
        {
            string stringArray = "";

            foreach (object item in list)
                stringArray += string.Format("{0}{1}", item, separator);

            if (list.Count > 0)
                stringArray.Substring(0, stringArray.Length - 1);

            return stringArray;
        }

        private static bool ColumnEqual(object A, object B)
        {
            if (A == DBNull.Value && B == DBNull.Value)
                return true;
            if (A == DBNull.Value || B == DBNull.Value)
                return false;
            return (A.Equals(B));
        }

        public static string HighlightText(object value, string search)
        {
            string aux2 = string.Empty;
            bool replace = false;

            foreach (string word1 in value.ToString().Split(' '))
            {
                replace = false;

                foreach (string word in search.Split(new char['%'], StringSplitOptions.RemoveEmptyEntries))
                {
                    if (word1.ToLower() == word.ToLower())
                        replace = true;
                }

                if (replace)
                    aux2 += string.Format(" <b>{0}</b>", word1);
                else
                    aux2 += string.Format(" {0}", word1);
            }

            return aux2.Trim();
        }

        public static DataTable SelectDistinct(DataTable dataTable, string column)
        {
            DataTable dt = dataTable.Clone();
            dt.Rows.Clear();

            object lastValue = null;
            foreach (DataRow dr in dataTable.Select("", column))
            {
                if (lastValue == null || !(ColumnEqual(lastValue, dr[column])))
                {
                    lastValue = dr[column];
                    dt.Rows.Add(dr.ItemArray);
                }
            }

            return dt;
        }

        public static bool IsFile(string fileName)
        {
            return !IsDirectory(fileName);
        }

        public static bool IsDirectory(string path)
        {
            FileAttributes attr = File.GetAttributes(path);

            return (attr & FileAttributes.Directory) == FileAttributes.Directory;
        }

        #region Web.Config
        public static string GetConnectionString(string nameConnectionString)
        {
            return ConfigurationManager.ConnectionStrings[nameConnectionString].ToString();
        }
        public static string GetEmpresaAX()
        {
            return ConfigurationManager.AppSettings["coEmpresaAX"].ToString();
        }
        public static string GetServidorAX()
        {
            return ConfigurationManager.AppSettings["nmServerAX"].ToString();
        }
        #endregion Web.Config

        public static String RemoveAccents(String value)
        {
            String normalizedString = value.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < normalizedString.Length; i++)
            {
                Char c = normalizedString[i];
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(c);
            }

            return stringBuilder.ToString();
        }

        public static System.Web.UI.Control FindControlRecursive(System.Web.UI.Control root, string id)
        {
            if (root.ID == id)
            {
                return root;
            }

            foreach (System.Web.UI.Control c in root.Controls)
            {
                System.Web.UI.Control t = FindControlRecursive(c, id);
                if (t != null)
                {
                    return t;
                }
            }

            return null;
        }

        public static string GetMonthForInt(int monthInt)
        {
            string monthName = "";
            switch (monthInt)
            {
                case 1:
                    monthName = "Janeiro";
                    break;
                case 2:
                    monthName = "Fevereiro";
                    break;
                case 3:
                    monthName = "Março";
                    break;
                case 4:
                    monthName = "Abril";
                    break;
                case 5:
                    monthName = "Maio";
                    break;
                case 6:
                    monthName = "Junho";
                    break;
                case 7:
                    monthName = "Julho";
                    break;
                case 8:
                    monthName = "Agosto";
                    break;
                case 9:
                    monthName = "Setembro";
                    break;
                case 10:
                    monthName = "Outubro";
                    break;
                case 11:
                    monthName = "Novembro";
                    break;
                case 12:
                    monthName = "Dezembro";
                    break;
            }
            return monthName;
        }

        public static string GetPublishVersion()
        {
            string manifestFilePath = string.Concat(System.Reflection.Assembly.GetExecutingAssembly().Location, ".manifest");
            Microsoft.Build.Tasks.Deployment.ManifestUtilities.AssemblyIdentity ident = Microsoft.Build.Tasks.Deployment.ManifestUtilities.AssemblyIdentity.FromManifest(manifestFilePath);
            if (ident != null)
                return ident.Version;
            else
                return "0.0.0.0";
        }

        public static string GetDeploymentVersion()
        {
            if (System.Deployment.Application.ApplicationDeployment.CurrentDeployment != null)
            {
                Version vers = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion;
                return vers.ToString();
            }
            else
                return "0.0.0.0";
        }

        public static string GetIP()
        {
            return System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName())[0].ToString();
        }

        public static bool EnableLog()
        {
            if (ConfigurationManager.AppSettings["EnableLog"] != null)
                return ToBoolean(ConfigurationManager.AppSettings["EnableLog"]);
            else
                return false;
        }

        public static string GetContentPage(string url)
        {
            try
            {
                string strURL = url;
                System.Net.WebClient objWebClient = new System.Net.WebClient();
                Byte[] aRequestedHTML;

                aRequestedHTML = objWebClient.DownloadData(strURL);
                System.Text.UTF8Encoding objUTF8 = new System.Text.UTF8Encoding();

                string strRequestedHTML;
                strRequestedHTML = objUTF8.GetString(aRequestedHTML);

                return strRequestedHTML;
            }
            catch (Exception ex)
            {
                return "Exception: Problemas ao fazer download da página solicitada: " + ex.Message;
            }
        }

        #region Util
        public static string GetRandomPassword(int Lenght)
        {
            Random oRnd = new Random();

            System.Text.StringBuilder sPassWord = new System.Text.StringBuilder(Lenght);

            for (int i = 1; i <= Lenght; i++)
            {
                int iCharIndex;
                do
                {
                    iCharIndex = oRnd.Next(43, 123);
                }
                while (
                    (iCharIndex <= 48 && iCharIndex >= 57) ||
                    (iCharIndex <= 65 && iCharIndex >= 90) ||
                    (iCharIndex <= 97 && iCharIndex >= 122));
                sPassWord.Append(Convert.ToChar(iCharIndex));
            }

            return sPassWord.ToString();
        }
        public static string GetAlfaNumberRandomPassword(int Lenght)
        {
            Random oRnd = new Random();

            System.Text.StringBuilder sPassWord = new System.Text.StringBuilder(Lenght);

            for (int i = 1; i <= Lenght; i++)
            {
                int iCharIndex;
                do
                {
                    iCharIndex = oRnd.Next(43, 123);
                }
                while (
                    (iCharIndex <= 48 && iCharIndex >= 57) ||
                    (iCharIndex <= 65 && iCharIndex >= 90));
                sPassWord.Append(Convert.ToChar(iCharIndex));
            }

            return sPassWord.ToString();
        }
        public static string FormatIntWithZero(int value, int length)
        {
            string mask = string.Empty;
            if (value.ToString().Length > length)
                return value.ToString().Substring(0, length);

            for (int i = 0; i < length - value.ToString().Length; i++)
                mask += "0";

            return mask + value.ToString();
        }

        public static string CompleteString(string value, string stringComplete, int length)
        {
            return CompleteString(value, stringComplete, length, DirectionCompleteString.Left);
        }

        public static string CompleteString(string value, string stringComplete, int length, DirectionCompleteString direction)
        {
            string mask = string.Empty;

            for (int i = 0; i < length - value.ToString().Length; i++)
                mask += stringComplete;

            if (direction == DirectionCompleteString.Left)
                return mask + value.ToString();
            else
                return value.ToString() + mask;
        }

        public static string CompleteString(string value, string stringComplete, int length, DirectionCompleteString direction, bool cutString)
        {
            string mask = string.Empty;

            if (cutString)
                if (value.Length > length)
                    value = value.Substring(0, length);

            for (int i = 0; i < length - value.ToString().Length; i++)
                mask += stringComplete;

            if (direction == DirectionCompleteString.Left)
                return mask + value.ToString();
            else
                return value.ToString() + mask;
        }
        /// <summary>
        /// Altera todas as letras da todas as palavras para maiúsculo.
        /// Ex.: STIVEN FABIANO DA CAMARA
        ///	     Stiven Fabiano da Camara
        /// </summary>
        /// <param name="value">string</param>
        /// <returns></returns>
        public static string ChangeWordFirstUpper(string value)
        {
            try
            {
                value = value.Replace("  ", " ");
                string aux = string.Empty;
                string[] words = value.Trim().Split(' ');
                string[] wordsnotchange = new string[]{"da", "de", "do", "dos", "e", "a", "o", "i", "das", "um", "uma", "uns", "umas", "I", "II", "III",
														  "IV", "V", "VI", "VII", "VIII", "IX"};
                foreach (string word in words)
                {
                    bool valid = true;
                    foreach (string notchange in wordsnotchange)
                        if (notchange == word)
                        {
                            aux += word + " ";
                            valid = false;
                            break;
                        }
                        else
                        {
                            if (value.IndexOf(word) != 0)
                                if (notchange == word.ToLower())
                                {
                                    aux += word.ToLower() + " ";
                                    valid = false;
                                    break;
                                }
                        }

                    if (valid)
                        aux += word.ToUpper().Substring(0, 1) + word.ToLower().Substring(1, word.Length - 1) + " ";
                }
                return aux;
            }
            catch
            {
                return string.Empty;
            }
        }
        #endregion Util

        public static string FormatLogin(string coAcesso)
        {
            try
            {

                if (Library.isNumeric(coAcesso))
                {
                    if (coAcesso.Trim().Length < 6)
                    {
                        coAcesso = ((string)("000000")).Substring(0, 6 - coAcesso.Length) + coAcesso.Trim();
                    }
                }


                return coAcesso;
            }
            catch
            {
                return coAcesso;
            }
        }

        /// <summary>
        /// Select value of object DropDownList.
        /// </summary>
        /// <param name="dropDownList"></param>
        /// <param name="value"></param>
        public static void SelectDropDownList(DropDownList dropDownList, string value)
        {
            ListItem itm = dropDownList.Items.FindByValue(value);
            if (itm != null)
                itm.Selected = true;
        }

        /// <summary>
        /// Get ID dynamic format by year, month, day, hour, minute, second and millisecond.
        /// </summary>
        /// <returns></returns>
        public static string GetIDDynamic()
        {
            string id = DateTime.Now.Year.ToString();
            id += String.Format("{0:00}", DateTime.Now.Month);
            id += String.Format("{0:00}", DateTime.Now.Day);
            id += DateTime.Now.Hour.ToString();
            id += DateTime.Now.Minute.ToString();
            id += DateTime.Now.Second.ToString();
            id += DateTime.Now.Millisecond.ToString();
            return id;
        }

        /// <summary>
        /// Retorna o CPF com a máscara. Ex.: 000.000.000-00
        /// </summary>
        /// <returns>string</returns>
        public static string MaskCPF(string cpf)
        {
            try
            {
                return string.Format("{0}.{1}.{2}-{3}", cpf.Substring(0, 3), cpf.Substring(3, 3), cpf.Substring(6, 3), cpf.Substring(9, 2));
            }
            catch
            {
                return string.Empty;
            }
        }

        //public static string MaskPis(string pis)
        //{
        //    try
        //    {
        //        return string.Format("{0}.{1}.{2}-{3}", pis.Substring(0, 3), pis.Substring(3, 3), pis.Substring(6, 4), pis.Substring(10, 1));
        //    }
        //    catch
        //    {
        //        return string.Empty;
        //    }
        //}

        /// <summary>
        /// Retorna o CPF com a máscara. Ex.: 000.000.000-00
        /// </summary>
        /// <returns>string</returns>
        public static string MaskCEP(string cep)
        {
            try
            {
                return string.Format("{0}.{1}-{2}", cep.Substring(0, 2), cep.Substring(2, 3), cep.Substring(5, 3));
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Retorna o RG com a máscara. Ex.: 000.000.000
        /// </summary>
        /// <returns>string</returns>
        public static string MaskRG(string nuRG)
        {
            nuRG.Replace(".", "");

            for (int i = nuRG.Length - 3; i > 0; i -= 3)
                nuRG = nuRG.Insert(i, ".");

            return nuRG;
        }

        /// <summary>
        /// Retorna o RA com a máscara. Ex.: 0000000/0
        /// </summary>
        /// <returns>string</returns>
        public static string MaskRA(string nuRA)
        {
            return nuRA.Insert(nuRA.Length - 1, "/");
        }

        /// <summary>
        /// Retorna o Titulo de Eleitor com a máscara. Ex.: 0000 0000 0000
        /// </summary>
        /// <returns>string</returns>
        public static string MaskTituloEleitor(string nuTituloEleitor)
        {
            nuTituloEleitor.Replace(" ", "").Replace(".", "").Replace("-", "");

            for (int i = nuTituloEleitor.Length - 4; i > 0; i -= 4)
                nuTituloEleitor = nuTituloEleitor.Insert(i, " ");

            return nuTituloEleitor;
        }

        /// <summary>
        /// Retorna o Telefone com a máscara. Ex.: 0000-0000
        /// </summary>
        /// <returns>string</returns>
        public static string MaskTelefone(string nuTelefone)
        {
            //nuTelefone.Replace("-", "").Replace(" ", "");

            //for (int i = nuTelefone.Length - 4; i > 0; i -= 4)
            //    nuTelefone = nuTelefone.Insert(i, "-");

            //return nuTelefone;

            switch (nuTelefone.Length)
            {
                case 8:
                    return string.Format("{0}-{1}", Mid(nuTelefone, 1, 4), Mid(nuTelefone, 5, 4));
                case 10:
                    return string.Format("({0}) {1}-{2}", Mid(nuTelefone, 1, 2), Mid(nuTelefone, 3, 4), Mid(nuTelefone, 7, 4));
                default:
                    return nuTelefone;
            }
        }

        /// <summary>
        /// Valida CPF
        /// </summary>
        /// <param name="value"></param>
        /// <returns>bool</returns>
        public static bool ValidCPF(string value)
        {

            int d1, d2;
            int soma = 0;
            string digitado;
            string calculado;

            // Pega a string passada no parametro
            string cpf;

            // Pesos para calcular o primeiro digito
            int[] peso1 = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            // Pesos para calcular o segundo digito
            int[] peso2 = new int[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            int[] n = new int[11];

            bool retorno;

            // Limpa a string
            cpf = value.Replace(".", "").Replace("-", "").Replace("/", "").Replace("\\", "");

            // Se o tamanho for < 11 entao retorna como inválido
            if (cpf.Length != 11)
            {

                return false;
            }

            if (cpf == "11122233355")
            {
                return true;
            }

            // Caso coloque todos os numeros iguais
            switch (cpf)
            {

                case "11111111111":
                    return false;
                case "00000000000":
                    return false;
                case "22222222222":
                    return false;
                case "33333333333":
                    return false;
                case "44444444444":
                    return false;
                case "55555555555":
                    return false;
                case "66666666666":
                    return false;
                case "77777777777":
                    return false;
                case "88888888888":
                    return false;
                case "99999999999":
                    return false;
            }

            try
            {

                // Quebra cada digito do CPF
                n[0] = Convert.ToInt32(cpf.Substring(0, 1));
                n[1] = Convert.ToInt32(cpf.Substring(1, 1));
                n[2] = Convert.ToInt32(cpf.Substring(2, 1));
                n[3] = Convert.ToInt32(cpf.Substring(3, 1));
                n[4] = Convert.ToInt32(cpf.Substring(4, 1));
                n[5] = Convert.ToInt32(cpf.Substring(5, 1));
                n[6] = Convert.ToInt32(cpf.Substring(6, 1));
                n[7] = Convert.ToInt32(cpf.Substring(7, 1));
                n[8] = Convert.ToInt32(cpf.Substring(8, 1));
                n[9] = Convert.ToInt32(cpf.Substring(9, 1));
                n[10] = Convert.ToInt32(cpf.Substring(10, 1));

            }
            catch
            {

                return false;
            }


            // Calcula cada digito com seu respectivo peso
            for (int i = 0; i <= peso1.GetUpperBound(0); i++)
            {

                soma += (peso1[i] * Convert.ToInt32(n[i]));
            }

            // Pega o resto da divisao
            int resto = soma % 11;

            if (resto == 1 || resto == 0)
            {

                d1 = 0;

            }
            else
            {

                d1 = 11 - resto;
            }

            soma = 0;

            // Calcula cada digito com seu respectivo peso
            for (int i = 0; i <= peso2.GetUpperBound(0); i++)
            {

                soma += (peso2[i] * Convert.ToInt32(n[i]));
            }

            // Pega o resto da divisao
            resto = soma % 11;

            if (resto == 1 || resto == 0)
            {

                d2 = 0;

            }
            else
            {

                d2 = 11 - resto;
            }

            calculado = d1.ToString() + d2.ToString();
            digitado = n[9].ToString() + n[10].ToString();

            // Se os ultimos dois digitos calculados bater com
            // os dois ultimos digitos do cpf entao é válido
            if (calculado == digitado)
            {
                retorno = true;
            }
            else
            {
                retorno = false;
            }

            return retorno;

        }

        public static bool Pis(string value)
        {
            //Valores fixo que multiplica os 10 primeiros digitos do pis
            int[] multiplicador = new int[10] { 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string pis;
            bool retorno;

            if (value.Trim().Length == 0)
                return false;

            value = value.Trim();
            pis = value.Replace("-", "").Replace(".", "").PadLeft(11, '0');

            //Pega o ultimo digite "Digito de verificação"
            char ultimo = value[10];

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(pis[i].ToString()) * multiplicador[i];

            //Pega o valor do resto (caso tenha) da divisão da soma por 11
            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            //Comparando o digito veirficador(ultimo) e o resto que foi calculado pelo sistema, se forem iguais, o título é válido.
            if (ultimo.ToInt32() == resto)
            {
                retorno = true;
            }

            else
            {
                retorno = false;
            }

            return retorno;
        }

        public static string MaskCNPJ(string value)
        {
            try
            {
                return string.Format("{0}.{1}.{2}/{3}-{4}", value.Substring(0, 2), value.Substring(2, 3), value.Substring(5, 3), value.Substring(8, 4), value.Substring(12, 2));
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Retorna o Cheque CMC7 com a máscara. Ex.: 12345678 1234567890 123456789012
        /// </summary>
        /// <returns>string</returns>
        public static string MaskChequeCMC7(string cheque)
        {
            try
            {
                return string.Format("{0} {1} {2}", cheque.Substring(0, 8), cheque.Substring(8, 10), cheque.Substring(18, 12));
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Valida Cheque CMC7
        /// </summary>
        /// <param name="value"></param>
        /// <returns>bool</returns>
        public static bool ValidChequeCMC7(string value)
        {
            string _concat1 = "";
            string _concat2 = "";
            string _concat3 = "";
            int _mult1 = 0;
            int _mult2 = 0;
            int _mult3 = 0;
            int _val = 0;
            int _digCalc1 = 0;
            int _digCalc2 = 0;
            int _digCalc3 = 0;

            value = ClearMask(value);

            if (value.Length != 30) return false;

            for (int x = 0; x < value.Length; x++)
            {
                _val = Convert.ToInt32(value.Substring(x, 1));

                //Concatena Digitos para cálculo do DV da 1a. Banda
                if (x > 7 && x < 18)
                {
                    //_mult1 = (x % 2) == 0 ? 1 : 2;
                    _mult1 = (_mult1 == 0 || _mult1 == 2) ? 1 : 2;
                    _concat1 = _concat1 + (_val * _mult1).ToString();
                }

                //Concatena Digitos para cálculo do primeiro DV da 3a. Banda
                if (x > -1 && x < 7)
                {
                    //_mult2 = (x % 2) == 0 ? 2 : 1;
                    _mult2 = (_mult2 == 0 || _mult2 == 1) ? 2 : 1;
                    _concat2 = _concat2 + (_val * _mult2).ToString();
                }

                //Concatena Digitos para cálculo do segundo DV da 3a. Banda
                if (x > 18 && x < 29)
                {
                    //_mult3 = (x % 2) == 0 ? 1 : 2;
                    _mult3 = (_mult3 == 0 || _mult3 == 2) ? 1 : 2;
                    _concat3 = _concat3 + (_val * _mult3).ToString();
                }
            }

            //Cálculo do DV da 1a. Banda, a partir dos dígitos da 2a. Banda
            int _soma = 0;
            for (int x = 0; x < _concat1.Length; x++)
            {
                _soma = _soma + Convert.ToInt32(_concat1.Substring(x, 1));
            }
            _digCalc1 = ((_soma % 10) == 0) ? 0 : 10 - (_soma % 10);

            //Cálculo do primeiro DV da 3a. Banda, a partir dos dígitos da 1a. Banda
            _soma = 0;
            for (int x = 0; x < _concat2.Length; x++)
            {
                _soma = _soma + Convert.ToInt32(_concat2.Substring(x, 1));
            }
            _digCalc2 = ((_soma % 10) == 0) ? 0 : 10 - (_soma % 10);

            //Cálculo do segundo DV da 3a. Banda, a partir dos dígitos da 3a. Banda
            _soma = 0;
            for (int x = 0; x < _concat3.Length; x++)
            {
                _soma = _soma + Convert.ToInt32(_concat3.Substring(x, 1));
            }
            _digCalc3 = ((_soma % 10) == 0) ? 0 : 10 - (_soma % 10);


            if (value.Substring(7, 1) == _digCalc1.ToString() &&
                value.Substring(18, 1) == _digCalc2.ToString() &&
                value.Substring(29, 1) == _digCalc3.ToString())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static String GetCodigoBancoCMC7(String valor)
        {
            valor = valor.Substring(0, 3);
            return valor;
        }

        public static String GetCodigoAgenciaCMC7(String valor)
        {
            valor = valor.Substring(3, 4);
            return valor;
        }
        public static String GetNumChequeCMC7(String valor)
        {
            valor = valor.Substring(11, 6);
            return valor;
        }

        public static String GetTipoChequeCMC7(String valor)
        {
            valor = valor.Substring(17, 1);
            return valor;
        }

        public static String GetNumContCorrenteCMC7(String valor)
        {
            //001 - BB
            //033 - Santander
            //070 - BRB
            //104 - CEF
            //237 - Bradesco
            //341 - Itau
            //353 - Santander do Brasil
            //356 - Banco ABN Amro Real
            //399 - HSBC
            //409 - Unibanco

            var codBanco = GetCodigoBancoCMC7(valor);
            switch (codBanco)
            {
                case "104":
                    valor = valor.Substring(20, 9);
                    break;
                case "409":
                    valor = valor.Substring(21, 8);
                    break;
                case "356":
                case "237":
                    valor = valor.Substring(22, 7);
                    break;
                case "001":
                case "033":
                case "341":
                    valor = valor.Substring(23, 6);
                    break;
                case "353":
                    valor = valor.Substring(22, 8);
                    break;
                case "070":
                    valor = valor.Substring(19, 10);
                    break;
                default:
                    valor = valor.Substring(19, 10);
                    break;
            }
            return valor;
        }

        /// <summary>
        /// It compares the value between two dates, respectively.
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <param name="compare"></param>
        /// <returns>bool</returns>
        [Description("It compares the value between two dates, respectively.")]
        public static bool CompareDate(DateTime date1, DateTime date2, DateTimeCompare compare)
        {
            DateTime dateCompare1 = new DateTime(date1.Year, date1.Month, date1.Day);
            DateTime dateCompare2 = new DateTime(date2.Year, date2.Month, date2.Day);

            switch (compare)
            {
                case DateTimeCompare.Greater:
                    return (dateCompare1 > dateCompare2);
                case DateTimeCompare.Minus:
                    return (dateCompare1 < dateCompare2);
                case DateTimeCompare.GreaterEqual:
                    return (dateCompare1 >= dateCompare2);
                case DateTimeCompare.MinusEqual:
                    return (dateCompare1 <= dateCompare2);
                case DateTimeCompare.Equal:
                    return (dateCompare1 == dateCompare2);
                case DateTimeCompare.Different:
                    return (dateCompare1 != dateCompare2);
            }
            return false;
        }


        public static bool CompareString(string str1, string str2, bool casesensitive, bool accentsensitive)
        {


            if (!casesensitive)
            {
                str1 = str1.ToLower();
                str2 = str2.ToLower();
            }
            if (!accentsensitive)
            {
                str1 = str1.Replace("á", "a").Replace("à", "a").Replace("Á", "A").Replace("À", "A");
                str1 = str1.Replace("é", "e").Replace("è", "e").Replace("É", "E").Replace("È", "E");
                str1 = str1.Replace("í", "i").Replace("ì", "i").Replace("Í", "I").Replace("Ì", "I");
                str1 = str1.Replace("ó", "o").Replace("ò", "o").Replace("Ó", "O").Replace("Ò", "O");
                str1 = str1.Replace("ú", "u").Replace("ù", "u").Replace("Ú", "U").Replace("Ù", "U");

                str2 = str2.Replace("á", "a").Replace("à", "a").Replace("Á", "A").Replace("À", "A");
                str2 = str2.Replace("é", "e").Replace("è", "e").Replace("É", "E").Replace("È", "E");
                str2 = str2.Replace("í", "i").Replace("ì", "i").Replace("Í", "I").Replace("Ì", "I");
                str2 = str2.Replace("ó", "o").Replace("ò", "o").Replace("Ó", "O").Replace("Ò", "O");
                str2 = str2.Replace("ú", "u").Replace("ù", "u").Replace("Ú", "U").Replace("Ù", "U");
            }

            if (str1.Trim() == str2.Trim())
                return true;
            else
                return false;
        }

        [Description("It compares the value between two hours, respectively.")]
        public static bool CompareHour(string hour1, string hour2, HourFormatCompare compare)
        {
            try
            {
                string[] arrHour1 = hour1.Split(':');
                string[] arrHour2 = hour2.Split(':');

                int hourCompare1 = ToInteger(arrHour1[0]) * 60 + ToInteger(arrHour1[1]);
                int hourCompare2 = ToInteger(arrHour2[0]) * 60 + ToInteger(arrHour2[1]);

                switch (compare)
                {
                    case HourFormatCompare.Greater:
                        return (hourCompare1 > hourCompare2);
                    case HourFormatCompare.Minus:
                        return (hourCompare1 < hourCompare2);
                    case HourFormatCompare.GreaterEqual:
                        return (hourCompare1 >= hourCompare2);
                    case HourFormatCompare.MinusEqual:
                        return (hourCompare1 <= hourCompare2);
                    case HourFormatCompare.Equal:
                        return (hourCompare1 == hourCompare2);
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static string[] ReadTextFile(string strFileName)
        {

            StreamReader o = File.OpenText(strFileName);

            ArrayList oReturn = new ArrayList();

            while (o.Peek() != -1)
            {
                oReturn.Add(o.ReadLine());
            }

            string[] aReturn = (string[])oReturn.ToArray(typeof(string));

            return aReturn;

        }

        public static bool AddDataColumn(DataTable myTable, string NameColumn, string DataType)
        {
            return AddDataColumn(myTable, NameColumn, DataType, DBNull.Value);
        }

        public static bool AddDataColumn(DataTable myTable, string NameColumn, string DataType, object DefaultValue)
        {
            try
            {
                if (myTable.Columns.IndexOf(NameColumn) >= 0)
                    return false;

                DataColumn myColumn = new DataColumn();
                myColumn.DataType = Type.GetType(DataType);
                myColumn.AllowDBNull = true;
                myColumn.ColumnName = NameColumn;
                myColumn.DefaultValue = DefaultValue;

                myTable.Columns.Add(myColumn);

                //DataRow myRow; 

                //for(int i = 0; i < 10; i++)
                //{ 
                if (myTable.Rows.Count > 0)
                    myTable.Rows[0][NameColumn] = DefaultValue;
                //}
                return true;

            }
            catch (Exception e)
            {
                Logger.Error(e);
                return false;
            }
        }

        public static double DateDiff(string HowToCompare, DateTime StartDate, DateTime EndDate)
        {
            double diff;

            try
            {
                TimeSpan TS = new TimeSpan(EndDate.Ticks - StartDate.Ticks);
                switch (HowToCompare.ToLower())
                {
                    case "m":
                        diff = Convert.ToDouble(TS.TotalMinutes);
                        break;
                    case "s":
                        diff = Convert.ToDouble(TS.TotalSeconds);
                        break;
                    case "t":
                        diff = Convert.ToDouble(TS.Ticks);
                        break;
                    case "mm":
                        diff = Convert.ToDouble(TS.TotalMilliseconds);
                        break;
                    case "yyyy":
                        diff = Convert.ToDouble(TS.TotalDays / 365);
                        break;
                    case "q":
                        diff = Convert.ToDouble((TS.TotalDays / 365) / 4);
                        break;
                    default:
                        //d
                        diff = Convert.ToDouble(TS.TotalDays);
                        break;
                }
            }
            catch (Exception e)
            {
                diff = -1;
                Logger.Error(e);
            }
            return diff;
        }

        public static bool IsValidUrl(string Url)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(Url, "(http|ftp|https)://([\\w-]+\\.)+(/[\\w- ./?%&=]*)?");
        }

        public static bool CreateFile(string Path, string Text)
        {
            try
            {
                if (File.Exists(Path))
                {
                    File.Delete(Path);
                }

                using (FileStream oFs = File.Create(Path))
                {
                    Byte[] b = new System.Text.UTF8Encoding(true).GetBytes(Text);
                    oFs.Write(b, 0, b.Length);
                }
                return true;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return false;
            }
        }

        [Description("Convert a format hour 'HH:MM' in int.")]
        public static int ConvertHourToMinutes(string value)
        {

            try
            {
                if (value.IndexOf(":") == -1)
                    throw new Exception("Invalid format hour 'hh:mm'.");

                string[] time = value.Split(':');
                int hour = ToInteger(time[0]);
                int minutes = ToInteger(time[1]);
                return (hour * 60) + minutes;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return 0;
            }
        }

        public static string HtmlDecode(string value)
        {
            try
            {
                return HttpContext.Current.Server.HtmlDecode(value);
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return value;
            }
        }

        public static string HtmlEncode(string value)
        {
            try
            {
                return HttpContext.Current.Server.HtmlEncode(value);
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return value;
            }
        }

        public static string GetPathUrl()
        {
            string sReturn;
            string[] sArray;
            char[] sBarra = new char[] { '/' };

            sReturn = "http://" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"].ToString() + "/";

            sArray = HttpContext.Current.Request.ServerVariables["PATH_INFO"].ToString().Split(sBarra);

            for (int i = 0; i < sArray.Length - 1; i++)
            {
                if (sArray[i].Length > 0)
                    sReturn += sArray[i].ToString() + "/";
            }

            return sReturn;

        }

        public static string GetPathPageUrl()
        {
            string sReturn;
            //			string[] sArray;
            //			char[] sBarra = new char[]{'/'};

            sReturn = "http://" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"].ToString() + HttpContext.Current.Request.ServerVariables["PATH_INFO"].ToString();

            //			sArray = HttpContext.Current.Request.ServerVariables["PATH_INFO"].ToString().Split(sBarra);
            //
            //			for(int i = 0; i < sArray.Length; i++)
            //			{
            //				if (sArray[i].Length > 0)
            //					sReturn += sArray[i].ToString() + "/";	
            //			}

            return sReturn;

        }
        public static string GetPathPhysical()
        {
            string sReturn = "";
            string[] sArray;
            char[] sBarra = new char[] { '/', '\\' };

            sArray = HttpContext.Current.Request.PhysicalPath.Split(sBarra);

            for (int i = 0; i < sArray.Length - 1; i++)
            {
                if (sArray[i].Length > 0)
                    sReturn += sArray[i].ToString() + "/";
            }

            return sReturn;

        }

        public static int GetidTipoProcessoSeletivo()
        {
            return ToInteger(ConfigurationManager.AppSettings["idTipoProcessoSeletivo"]);
        }

        public static string GetPathReport()
        {
            return GetPathReport("");
        }

        public static string GetPathReport(string FileName)
        {
            return ConfigurationManager.AppSettings["PathFileReport"].ToString() + FileName;
        }

        public static string GetPathFileLog()
        {
            return ConfigurationManager.AppSettings["PathFileLog"].ToString();
        }

        public static string PathFileDataXml()
        {
            return ConfigurationManager.AppSettings["PathFileDataXml"].ToString();
        }

        public static string GetPathReportTemp()
        {
            return ConfigurationManager.AppSettings["PathFileReport"].ToString() + @"temp\";
        }

        public static string GetPathSerializable()
        {
            return ConfigurationManager.AppSettings["PathSerializable"].ToString();
        }

        public static string GetPathUpLoad()
        {
            return ConfigurationManager.AppSettings["PathUpLoad"].ToString();
        }

        public static string GetPathAfm()
        {
            return ConfigurationManager.AppSettings["PatheAfm"].ToString();
        }

        public static string GetPathRoot()
        {
            return ConfigurationManager.AppSettings["PathRoot"].ToString();
        }

        public static string GetUrlServer()
        {
            return ConfigurationManager.AppSettings["UrlServer"].ToString();
        }
        public static string GetUrlSite()
        {
            return ConfigurationManager.AppSettings["UrlSite"].ToString();
        }
        public static string GetSmtpServer()
        {
            return ConfigurationManager.AppSettings["SmtpServer"].ToString();
        }
        public static string GetSystemMail()
        {
            try
            {
                return ConfigurationManager.AppSettings["SystemMail"].ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        public static string[] GetWebAdministrators()
        {
            return ConfigurationManager.AppSettings["WebAdministrators"].ToString().Split(';');
        }
        public static string[] GetAllowedUsers()
        {
            return ConfigurationManager.AppSettings["AllowedUsers"].ToString().Split(';');
        }
        public static string GetDataSource()
        {
            return ConfigurationManager.AppSettings["DataSource"].ToString();
        }

        public static string GetNamePage()
        {
            string sReturn = string.Empty;
            string[] sArray;

            var context = HttpContext.Current;
            HttpRequest request;

            try
            {
                if (context == null)
                {
                    request = HttpContext.Current.Application[Session.REQUEST_NAME] as HttpRequest;
                }
                else
                {
                    request = context.Request;
                }

                sArray = request.ServerVariables["PATH_INFO"].ToString().Split(new char[] { '/' });

                if (sArray != null && sArray.Length > 0)
                    sReturn += sArray[sArray.Length - 1].ToString();
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return "";
            }

            return sReturn;
        }

        public static string Mid(string Str, int Start, int Length)
        {

            if (Length < 0)
                throw new ArgumentException("Argument 'Length' must be greater or equal to zero.", "Length");
            if (Start <= 0)
                throw new ArgumentException("Argument 'Start' must be greater than zero.", "Start");
            if ((Str == null) || (Str.Length == 0))
                return String.Empty; // VB.net does this.

            if ((Length == 0) || (Start > Str.Length))
                return String.Empty;

            if (Length > (Str.Length - Start))
                Length = (Str.Length - Start) + 1;

            return Str.Substring(Start - 1, Length);

        }


        public static bool IsValidYear(int value)
        {
            if (value <= 1900 | value >= 2051)
            {
                return false;
            }
            return true;
        }


        public static string GetDayOfWeek(DayOfWeek value)
        {
            string sDayOfWeek = string.Empty;
            switch (value)
            {
                case DayOfWeek.Sunday:
                    sDayOfWeek = "Domingo";
                    break;
                case DayOfWeek.Monday:
                    sDayOfWeek = "Segunda";
                    break;
                case DayOfWeek.Tuesday:
                    sDayOfWeek = "Terça";
                    break;
                case DayOfWeek.Wednesday:
                    sDayOfWeek = "Quarta";
                    break;
                case DayOfWeek.Thursday:
                    sDayOfWeek = "Quinta";
                    break;
                case DayOfWeek.Friday:
                    sDayOfWeek = "Sexta";
                    break;
                case DayOfWeek.Saturday:
                    sDayOfWeek = "Sábado";
                    break;
            }
            return sDayOfWeek;
        }

        public static int GetDaysBetweenDate(DateTime dateInitial, DateTime dateFinal)
        {
            try
            {
                return dateFinal.Subtract(dateInitial).Days;
            }
            catch
            {
                return 0;
            }
        }

        public static DateTime GetLastDateOfTheMonth(DateTime value)
        {
            try
            {
                DateTime dNextMonth = value.AddMonths(1);
                DateTime dTemp;
                int iDays = 0;
                do
                {
                    iDays++;
                    dTemp = value.AddDays(iDays);
                }
                while (dTemp < dNextMonth);

                return dTemp.AddDays(-1);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        public static string GetFile(string sFile)
        {
            TextReader oTr;
            string sReturn;
            try
            {
                oTr = new StreamReader(HttpContext.Current.Server.MapPath(sFile));
                sReturn = oTr.ReadToEnd();
                oTr.Close();
                return sReturn;
            }
            catch
            {
                string sErrorMessage = string.Format("The HTML fragment file '{0}' cannot be found", sFile);
                throw new ApplicationException(sErrorMessage);
            }
        }


        /*\
         *	Remove os itens selecionados de um ListBox / Dropdownlist.
        \*/
        public static bool RemoveSelectedItems(ListControl oList)
        {
            try
            {
                ArrayList oArray = new ArrayList();

                foreach (ListItem oItem in oList.Items)
                {
                    if (!oItem.Selected)
                        continue;
                    oArray.Add(oItem);
                }

                foreach (object oItem in oArray)
                {
                    oList.Items.Remove(((ListItem)oItem));
                }

                oList.ClearSelection();

                return true;
            }
            catch
            {
                return false;
            }
        }



        public static bool MoveAllItems(ListControl from, ListControl to)
        {
            try
            {
                ArrayList oArray = new ArrayList();

                foreach (ListItem oItem in from.Items)
                {
                    oArray.Add(oItem);
                }

                foreach (object oItem in oArray)
                {
                    to.Items.Add(((ListItem)oItem));
                    from.Items.Remove(((ListItem)oItem));
                }

                to.ClearSelection();
                from.ClearSelection();

                return true;
            }
            catch
            {
                return false;
            }
        }


        /*\
         *	Método transporta os Item(s) selecionados no ListBox / DropDownList para outro.
        \*/

        public static void MoveSelectedItems(ListControl from, ListControl to)
        {
            ArrayList selectedItems = new ArrayList();

            foreach (ListItem item in from.Items)
            {
                if (!item.Selected)
                    continue;
                selectedItems.Add(item);
            }

            foreach (object item in selectedItems)
            {
                to.Items.Add(((ListItem)item));
                from.Items.Remove(((ListItem)item));
            }

            to.ClearSelection();
            from.ClearSelection();
        }


        //Replace string function. 
        public static String Replace(String oText, String oFind, String oReplace)
        {
            int iPos = oText.IndexOf(oFind);
            String strReturn = "";
            while (iPos != -1)
            {
                strReturn += oText.Substring(0, iPos) + oReplace;
                oText = oText.Substring(iPos + oFind.Length);
                iPos = oText.IndexOf(oFind);
            }
            if (oText.Length > 0)
                strReturn += oText;
            return strReturn;
        }


        public static void ValidSession()
        {

        }


        public static void ReadProperties(object obj, DataRow row)
        {
            PropertyInfo[] properties = obj.GetType().GetProperties();

            foreach (DataColumn column in row.Table.Columns)
            {
                for (int i = 0; i < properties.Length; i++)
                {
                    PropertyInfo property = properties[i];
                    if (property.Name.ToLower() == column.ColumnName.ToLower())
                    {
                        if (property.CanWrite)
                        {
                            property.SetValue(obj, ValidTypeProperty(property, row), null);
                            break;
                        }
                    }
                }
            }
        }


        public static void ReadProperties(object obj, System.Xml.XmlNodeList xmlNodeList)
        {
            PropertyInfo[] properties = obj.GetType().GetProperties();

            for (int i = 0; i < properties.Length; i++)
            {
                PropertyInfo property = properties[i];
                if (property.CanWrite)
                    foreach (System.Xml.XmlNode node in xmlNodeList)
                        if (node.Name == property.Name)
                            property.SetValue(obj, ValidTypeProperty(node.InnerText, node.Attributes["type"].InnerText), null);
            }
        }

        public static void ResponseWrite(string s)
        {
            HttpContext.Current.Response.Write(s);
        }
        public static object ValidTypeProperty(object value, string type)
        {
            switch (type)
            {
                case "System.Int32":
                    return Convert.ToInt32(value);

                case "System.String":
                    return Convert.ToString(value);

                case "System.Long":
                    return Convert.ToInt64(value);

                case "System.Int64":
                    return Convert.ToInt64(value);

                case "System.Double":
                    return Convert.ToDouble(value);

                case "System.DateTime":
                    try
                    {
                        return DateTime.Parse(value.ToString(), new CultureInfo("pt-BR", false).DateTimeFormat);
                    }
                    catch
                    {
                        string[] dateFormat = value.ToString().Split(' ');
                        string[] date = dateFormat[0].Split('/');
                        string[] hour = dateFormat[1].Split(':');
                        return new DateTime(Library.ToInteger(date[2]), Library.ToInteger(date[0]), Library.ToInteger(date[1]),
                            Library.ToInteger(hour[0]), Library.ToInteger(hour[1]), Library.ToInteger(hour[2]));
                    }
                //return Convert.ToDateTime(value);

                case "System.Decimal":
                    return Convert.ToDecimal(value);

                case "System.Drawing.Bitmap":
                    return (System.Drawing.Bitmap)(value);

                case "System.Data.DataTable":
                    return null;

                case "System.Char":
                    return Convert.ToChar(value);

                case "System.Boolean":
                    return Convert.ToBoolean(value);

                default:
                    return null;
            }
        }

        public static object ValidTypeProperty(PropertyInfo oProperty, DataRow oRow)
        {
            switch (oProperty.PropertyType.ToString())
            {
                case "System.Int32":
                    return GetInt32(oRow, oProperty.Name);

                case "System.String":
                    return GetString(oRow, oProperty.Name);

                case "System.Int64":
                    return GetInt64(oRow, oProperty.Name);

                case "System.Double":
                    return GetDouble(oRow, oProperty.Name);

                case "System.DateTime":
                    return GetDateTime(oRow, oProperty.Name);

                case "System.Decimal":
                    return GetDecimal(oRow, oProperty.Name);

                case "System.Drawing.Bitmap":
                    return GetBitmap(oRow, oProperty.Name);

                case "System.Data.DataTable":
                    return oRow.Table;

                case "System.Char":
                    return GetChar(oRow, oProperty.Name);

                case "System.Byte[]":
                    return GetByteArray(oRow, oProperty.Name);

                case "System.Boolean":
                    return GetBoolean(oRow, oProperty.Name);

                case "System.Guid":
                    return GetGuid(oRow, oProperty.Name);

                default:
                    if (oProperty.PropertyType.BaseType.ToString() == "System.Enum")
                        return GetInt32(oRow, oProperty.Name);
                    return null;
            }
        }


        public static DataTable GetObjectDataTable(object oObj)
        {
            try
            {
                DataTable oDtt = new DataTable(oObj.GetType().Name);

                PropertyInfo[] aProperties = oObj.GetType().GetProperties();
                //"<" + oProperty.Name + ">" + oProperty.GetValue(oObj, null) + "</" + oProperty.Name + ">\n";
                for (int i = 0; i < aProperties.Length; i++)
                {
                    PropertyInfo oProperty = aProperties[i];
                    DataColumn oCol = new DataColumn(oProperty.Name);
                    oCol.DataType = oProperty.PropertyType;
                    oCol.DefaultValue = oProperty.GetValue(oObj, null);
                    oDtt.Columns.Add(oCol);
                }
                DataRow oRow = oDtt.NewRow();
                oDtt.Rows.Add(oRow);

                return oDtt;
            }
            catch
            {
                return new DataTable();
            }
        }

        public static DataRow GetObjectDataRow(object oObj)
        {
            try
            {
                DataTable oDtt = new DataTable(oObj.GetType().Name);
                PropertyInfo[] aProperties = oObj.GetType().GetProperties();
                DataColumn oCol;

                // Add Column Id in Table
                oCol = new DataColumn("Id");
                oCol.DataType = Type.GetType("System.Int32");
                oCol.AutoIncrement = true;
                oCol.AutoIncrementStep = 1;
                oDtt.Columns.Add(oCol);

                for (int i = 0; i < aProperties.Length; i++)
                {
                    PropertyInfo oProperty = aProperties[i];
                    if (oProperty.Name.ToLower() != "parent")
                    {
                        oCol = new DataColumn(oProperty.Name);
                        oCol.DataType = oProperty.PropertyType;
                        oCol.DefaultValue = oProperty.GetValue(oObj, null);
                        oDtt.Columns.Add(oCol);
                    }
                }

                // Add Column NumRegister in Table
                oCol = new DataColumn("NumRegister");
                oCol.DataType = Type.GetType("System.Int32");
                oCol.DefaultValue = 1;
                oDtt.Columns.Add(oCol);

                DataRow oRow = oDtt.NewRow();
                oDtt.Rows.Add(oRow);

                return oDtt.Rows[0];
            }
            catch
            {
                throw new ApplicationException();
            }
        }

        public static string GetObjectXml(object oObj)
        {
            try
            {
                string sXml = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\n";
                sXml += "<object name=\"" + oObj.GetType().Name + "\">\n";

                PropertyInfo[] aProperties = oObj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

                for (int i = 0; i < aProperties.Length; i++)
                {
                    PropertyInfo oProperty = aProperties[i];
                    if (oProperty.Name.ToLower() != "datasource")
                        if (oProperty.GetValue(oObj, null) != null)
                            sXml += "<" + oProperty.Name + " type=\"" + oProperty.GetValue(oObj, null).GetType().ToString() + "\"><![CDATA[" + oProperty.GetValue(oObj, null).ToString() + "]]></" + oProperty.Name + ">\n";
                }

                sXml += "</object>";
                return sXml;
            }
            catch
            {
                return null;
            }
        }


        public static string GetObjectNameString(object oObj)
        {
            try
            {
                string sAux = string.Empty;
                PropertyInfo[] aProperties = oObj.GetType().GetProperties();

                for (int i = 0; i < aProperties.Length; i++)
                {
                    PropertyInfo oProperty = aProperties[i];
                    sAux += oProperty.Name + "=" + oProperty.GetValue(oObj, null) + "§";
                }
                return sAux;
            }
            catch
            {
                return "";
            }
        }

        public static string GetObjectNodeXML(object oObj)
        {
            try
            {
                string sAux = string.Empty;
                PropertyInfo[] aProperties = oObj.GetType().GetProperties();

                for (int i = 0; i < aProperties.Length; i++)
                {
                    PropertyInfo oProperty = aProperties[i];
                    sAux += "<property name='" + oProperty.Name + "'>" + oProperty.GetValue(oObj, null) + "</property>";
                }
                return sAux;
            }
            catch
            {
                return "";
            }
        }

        public static string GetObjectString(object oObj)
        {
            try
            {
                string sAux = string.Empty;
                PropertyInfo[] aProperties = oObj.GetType().GetProperties();

                for (int i = 0; i < aProperties.Length; i++)
                {
                    PropertyInfo oProperty = aProperties[i];
                    sAux += oProperty.GetValue(oObj, null) + "§";
                }
                return sAux;
            }
            catch
            {
                return "";
            }
        }


        public static bool isNumeric(string value)
        {
            try
            {
                Convert.ToInt64(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string ClearMask(string value)
        {
            try
            {
                return value.Replace(".", "").Replace("-", "").Replace("/", "").Replace("<", "").Replace(">", "").Replace(":", "");
            }
            catch
            {
                return value;
            }
        }

        public static bool IsDateNotEmpty(object value)
        {
            try   //inclusão 01/01/1900 01/01/0001 02/01/1900
            {
                DateTime validDate = Convert.ToDateTime(value);

                //Data inválida
                return validDate.Year > 1900;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsDateEmpty(object value)
        {
            try
            {
                DateTime validDate = Convert.ToDateTime(value);
                return validDate.Year <= 1900;
            }
            catch
            {
                return true;
            }
        }

        public static bool IsDate(object value)
        {
            try
            {
                //Data inválida
                if (ToDate(value) == Convert.ToDateTime("1/1/1900"))
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static Int32 GetInt32(DataRow oRow, string Field)
        {
            return oRow.IsNull(Field) ? 0 : Convert.ToInt32(oRow[Field]);
        }


        public static Bitmap GetBitmap(DataRow oRow, string Field)
        {
            if (oRow.IsNull(Field))
            {
                return null;
            }
            else
            {
                return (Bitmap)oRow[Field];

            }
        }

        public static Image GetImage(DataRow oRow, string field)
        {
            if (oRow.IsNull(field))
            {
                return null;
            }
            else
            {
                return ConvertByteToImage(oRow[field] as byte[]);

            }
        }

        /// <summary>
        /// Ajusta a imagem para a taxa de proporção desejada, eliminando as áreas sobresalentes da imagem.
        /// </summary>
        /// <param name="imagemOriginal">Imagem à ser ajustada.</param>
        /// <param name="taxaProporcao">Taxa de proporção desejada.Exemplo: (3x4).</param>
        /// <param name="tamanhoSaida">Tamanho de saída em pixels desejado.</param>
        /// <param name="resolucaoSaida">Resolução em xDpi desejada.</param>
        /// <returns>Imagem ajustada à proporção, tamanho e resolução desejada.</returns>
        /// <example>System.Drawing.Image imagemResultante = AjustarImagem(imagemOriginal, ProportionRates._3x4, new Size(300,400), 300f);</example>
        public static Image AdjustImage(Image originalImage, ProportionRate proportionRate, Size outputSize, float outputResolution)
        {
            Bitmap resultImage;
            int expectedWidth;
            int expectedHeight;

            expectedWidth = ((int)(((float)originalImage.Height) * proportionRate.PropRate));
            expectedHeight = ((int)(((float)originalImage.Width) / proportionRate.PropRate));

            if (originalImage.Width > expectedWidth)
            {
                int x = (originalImage.Width - expectedWidth) / 2;
                resultImage = new Bitmap(expectedWidth, originalImage.Height);
                Graphics graphic = System.Drawing.Graphics.FromImage(resultImage);
                graphic.DrawImage(originalImage, new Rectangle(0, 0, expectedWidth, originalImage.Height), new Rectangle(x, 0, expectedWidth, originalImage.Height), GraphicsUnit.Pixel);
                graphic.Dispose();
            }
            else if (originalImage.Height > expectedHeight)
            {
                int y = (originalImage.Height - expectedHeight) / 2;
                resultImage = new Bitmap(originalImage.Width, expectedHeight);
                Graphics graphic = System.Drawing.Graphics.FromImage(resultImage);
                graphic.DrawImage(originalImage, new Rectangle(0, 0, originalImage.Width, expectedHeight), new Rectangle(0, y, originalImage.Width, expectedHeight), GraphicsUnit.Pixel);
                graphic.Dispose();
            }
            else
            {
                resultImage = new Bitmap(originalImage.Width, originalImage.Height);
                Graphics graphic = System.Drawing.Graphics.FromImage(resultImage);
                graphic.DrawImage(originalImage, 0, 0, originalImage.Width, originalImage.Height);
                graphic.Dispose();
            }

            resultImage = new Bitmap(resultImage, outputSize);
            resultImage.SetResolution(outputResolution, outputResolution);

            return resultImage as Image;
        }

        public static Int64 GetInt64(DataRow oRow, string Field)
        {
            return oRow.IsNull(Field) ? 0 : Convert.ToInt64(oRow[Field]);
        }


        public static string GetString(DataRow oRow, string Field)
        {
            return oRow.IsNull(Field) ? string.Empty : Convert.ToString(oRow[Field]);
        }

        public static char GetChar(DataRow oRow, string Field)
        {
            return oRow.IsNull(Field) ? new char() : Convert.ToChar(oRow[Field]);
        }

        public static bool GetBoolean(DataRow oRow, string Field)
        {
            return oRow.IsNull(Field) ? false : Convert.ToBoolean(oRow[Field]);
        }

        public static Guid GetGuid(DataRow row, string field)
        {
            try
            {
                return row.IsNull(field) ? Guid.Empty : (Guid)row[field];
            }
            catch
            {
                return Guid.Empty;
            }
        }

        public static long GetLong(DataRow oRow, string Field)
        {
            return oRow.IsNull(Field) ? 0 : Convert.ToInt64(oRow[Field]);
        }

        public static byte[] GetByteArray(DataRow oRow, string Field)
        {
            try
            {
                return oRow.IsNull(Field) ? new byte[0] : (Byte[])(oRow[Field]);
            }
            catch
            {
                return new byte[0];
            }
        }

        public static string GetSexo(string coSexo)
        {
            try
            {
                if (coSexo.Trim().ToUpper() == "M")
                {
                    return "Masculino";
                }
                else if (coSexo.Trim().ToUpper() == "F")
                {
                    return "Feminino";
                }

                return String.Empty;

            }
            catch
            {
                return string.Empty;
            }
        }


        public static DateTime GetDateTime(DataRow oRow, string Field)
        {
            try
            {
                return oRow.IsNull(Field) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(oRow[Field]);
            }
            catch
            {
                return new DateTime(1900, 1, 1);
            }
        }


        public static decimal GetDecimal(DataRow oRow, string Field)
        {
            try
            {
                return oRow.IsNull(Field) ? 0 : Convert.ToDecimal(oRow[Field]);
            }
            catch
            {
                return 0;
            }
        }


        public static double GetDouble(DataRow oRow, string Field)
        {
            try
            {
                return oRow.IsNull(Field) ? 0 : Convert.ToDouble(oRow[Field]);
            }
            catch
            {
                return 0;
            }
        }


        public static bool ClearControls(Control oControl)
        {
            try
            {
                for (int i = 0; i < oControl.Controls.Count; i++)
                {
                    if (oControl.Controls[i] is TextBox)
                    {
                        TextBox _TextBox = (TextBox)oControl.Controls[i];
                        _TextBox.Text = "";
                    }
                    else if (oControl.Controls[i] is DropDownList)
                    {
                        DropDownList _DropDownList = (DropDownList)oControl.Controls[i];
                        _DropDownList.ClearSelection();
                    }
                    else if (oControl.Controls[i] is ListBox)
                    {
                        ListBox _ListBox = (ListBox)oControl.Controls[i];
                        _ListBox.ClearSelection();
                    }
                    else if (oControl.Controls[i] is CheckBox)
                    {
                        CheckBox _CheckBox = (CheckBox)oControl.Controls[i];
                        _CheckBox.Checked = false;
                    }
                    else if (oControl.Controls[i] is RadioButton)
                    {
                        RadioButton _RadioButton = (RadioButton)oControl.Controls[i];
                        _RadioButton.Checked = false;
                    }
                    if (oControl.Controls[i].Controls.Count > 0)
                    {
                        ClearControls(oControl.Controls[i]);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool ClearDropDownList(Control oControl)
        {
            try
            {
                for (int i = 0; i < oControl.Controls.Count; i++)
                {
                    if (oControl.Controls[i] is DropDownList)
                    {
                        DropDownList _DropDownList = (DropDownList)oControl.Controls[i];
                        _DropDownList.ClearSelection();
                    }

                    if (oControl.Controls[i].Controls.Count > 0)
                    {
                        ClearDropDownList(oControl.Controls[i]);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool ConfigDropDownList(Control oControl)
        {
            return ConfigDropDownList(oControl, "Selecione");
        }

        public static bool ConfigDropDownList(Control oControl, string valueInit)
        {
            try
            {
                for (int i = 0; i < oControl.Controls.Count; i++)
                {
                    if (oControl.Controls[i] is DropDownList)
                    {
                        DropDownList _DropDownList = (DropDownList)oControl.Controls[i];

                        if (_DropDownList.Items.Count == 1)
                        {
                            if (_DropDownList.Items[0].Value == "0")
                            {
                                _DropDownList.Items[0].Text = valueInit;
                                _DropDownList.Width = Unit.Pixel(150);
                            }
                        }
                        else if (_DropDownList.Items.Count == 0)
                        {
                            _DropDownList.Items.Add(new ListItem(valueInit, "0"));
                            _DropDownList.Width = Unit.Pixel(150);
                        }
                        else
                        {
                            _DropDownList.Width = Unit.Empty;
                        }
                    }

                    if (oControl.Controls[i].Controls.Count > 0)
                    {
                        ConfigDropDownList(oControl.Controls[i], valueInit);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool EnabledControls(Control control)
        {
            return DisabledControls(control, false, false);
        }

        public static bool DisabledControls(Control control)
        {
            return DisabledControls(control, true, true);
        }

        public static bool DisabledControls(Control control, bool bStatus)
        {
            return DisabledControls(control, true, true);
        }

        public static bool DisabledControls(Control oControl, bool bStatus, bool disabledButtons)
        {
            try
            {
                for (int i = 0; i < oControl.Controls.Count; i++)
                {
                    if (oControl.Controls[i] is TextBox)
                    {

                        TextBox _TextBox = (TextBox)oControl.Controls[i];
                        _TextBox.ReadOnly = bStatus;
                        _TextBox.Enabled = !bStatus;
                    }
                    else if (oControl.Controls[i] is DropDownList)
                    {
                        DropDownList _DropDownList = (DropDownList)oControl.Controls[i];
                        _DropDownList.Enabled = !bStatus;
                    }
                    else if (oControl.Controls[i] is ListBox)
                    {
                        ListBox _ListBox = (ListBox)oControl.Controls[i];
                        _ListBox.Enabled = !bStatus;
                    }
                    else if (oControl.Controls[i] is CheckBox)
                    {
                        CheckBox _CheckBox = (CheckBox)oControl.Controls[i];
                        _CheckBox.Enabled = !bStatus;
                    }
                    else if (oControl.Controls[i] is RadioButton)
                    {
                        RadioButton _RadioButton = (RadioButton)oControl.Controls[i];
                        _RadioButton.Enabled = !bStatus;
                    }
                    else if (oControl.Controls[i] is Button)
                    {
                        if (disabledButtons)
                        {
                            Button _Button = (Button)oControl.Controls[i];
                            _Button.Enabled = !bStatus;
                        }
                    }

                    if (oControl.Controls[i].Controls.Count > 0)
                    {
                        DisabledControls(oControl.Controls[i], bStatus, disabledButtons);
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }


        public static string FormatString(string Value, TypeString tType)
        {
            try
            {
                switch (tType)
                {
                    case TypeString.CNPJ:
                        return string.Format("{0}.{1}.{2}/{3}-{4}", Value.Substring(0, 2), Value.Substring(2, 3), Value.Substring(5, 3), Value.Substring(8, 4), Value.Substring(12, 2));
                    case TypeString.CPF:
                        return string.Format("{0}.{1}.{2}-{3}", Value.Substring(0, 3), Value.Substring(3, 3), Value.Substring(6, 3), Value.Substring(9, 2));
                    case TypeString.Date:
                        if (Convert.ToDateTime(Value) == Convert.ToDateTime("1/1/1900"))
                            return string.Empty;
                        else if (Convert.ToDateTime(Value) == Convert.ToDateTime("2/1/1900"))
                            return string.Empty;
                        else if (Convert.ToDateTime(Value) == Convert.ToDateTime("1/1/0001"))
                            return string.Empty;
                        else
                            return Convert.ToDateTime(Value).ToString("dd/MM/yyyy");
                    case TypeString.Numeric:
                        return Convert.ToDouble(Value).ToString("N2");
                    case TypeString.RA:
                        if (ToInteger(Value) == 0)
                            return string.Empty;
                        return string.Format("{0}/{1}", Value.Substring(0, Value.Length - 1), Value.Substring(Value.Length - 1, 1));
                    case TypeString.Int:
                        return Convert.ToInt64(Value).ToString("#,##0");
                    case TypeString.Text:
                        return Value;
                    case TypeString.CEP:
                        return string.Format("{0}.{1}-{2}", Value.Substring(0, 2), Value.Substring(2, 3), Value.Substring(5, 3));
                    case TypeString.Telephone:
                        Value = Value.Replace("-", "").Replace(" ", "").Replace(".", "");
                        return string.Format("{0}-{1}", Value.Substring(0, Value.Length - 4), Value.Substring(Value.Length - 4, 4));
                    case TypeString.Currency:
                        //System.Globalization.NumberFormatInfo oNumberFormatInfo = System.Globalization.NumberFormatInfo.CurrentInfo;
                        //return Convert.ToDouble(Value).ToString(oNumberFormatInfo.CurrencySymbol + "#,##0.00", System.Globalization.NumberFormatInfo.CurrentInfo);
                        return Convert.ToDouble(Value).ToString("C");
                    default:
                        return Value;
                }

            }
            catch
            {
                return Value;
            }
        }


        public static string FormatString(object Value, TypeString tType)
        {
            try
            {
                return FormatString(Value.ToString(), tType);

            }
            catch
            {
                return Value.ToString();
            }
        }

        public static string FormatString(string sFormat, DataRow oRow)
        {
            try
            {
                if (sFormat.IndexOf("{") >= 0)
                {
                    bool bInRegion = false;
                    int i;
                    int iPositionInitial = 0;
                    ;
                    char[] c = new char[sFormat.Length];
                    string s = string.Empty;
                    int iTam = 0;

                    for (i = 0; i < sFormat.Length; i++)
                    {
                        c[i] = Convert.ToChar(sFormat.Substring(i, 1));

                        switch (c[i])
                        {
                            case '{':
                                bInRegion = true;
                                iPositionInitial = i + 2;
                                break;

                            case '}':
                                if (bInRegion)
                                {
                                    string sField = Mid(sFormat, iPositionInitial, iTam);
                                    s += oRow[sField].ToString();
                                    bInRegion = false;
                                    iPositionInitial = 0;
                                    iTam = 0;
                                }
                                else
                                {
                                    throw new Exception("Format invalid.");
                                }

                                break;

                            default:
                                if (!bInRegion)
                                {
                                    s += c[i].ToString();
                                }
                                else
                                {
                                    iTam++;
                                }
                                break;
                        }
                    }

                    return s;
                }
                else
                {
                    return oRow[sFormat].ToString().Trim();
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw;
            }
        }

        public static void BindDropDownList(DropDownList dropDownList, DataTable dataTable, string dataValueField, string dataTextField)
        {
            BindDropDownList(dropDownList, dataTable, dataValueField, dataTextField, true, null);
        }
        public static void BindDropDownList(DropDownList dropDownList, DataTable dataTable, string dataValueField, string dataTextField, bool createListItem)
        {
            BindDropDownList(dropDownList, dataTable, dataValueField, dataTextField, createListItem, null);
        }
        public static void BindDropDownList(DropDownList dropDownList, DataTable dataTable, string dataValueField, string dataTextField, bool createListItem, string listItemName)
        {
            if (createListItem)
            {
                if (string.IsNullOrEmpty(listItemName))
                    listItemName = "Selecione";

                BindDropDownList(dropDownList, dataTable, dataValueField, dataTextField, listItemName);
            }
            else
                BindDropDownList(dropDownList, dataTable, dataValueField, dataTextField, null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dropDownList"></param>
        /// <param name="dataTable"></param>
        /// <param name="dataValueField"></param>
        /// <param name="dataTextField"></param>
        /// <param name="periodoEspecial">qualquer inteiro positivo indicando que existe um periodo especial</param>
        public static void BindDropDownList(DropDownList dropDownList, DataTable dataTable, string dataValueField, string dataTextField, int periodoEspecial)
        {
            if (periodoEspecial >= 0)
                BindDropDownList(dropDownList, dataTable, dataValueField, dataTextField, "Selecione");
            else
                BindDropDownList(dropDownList, dataTable, dataValueField, dataTextField, null);
        }
        public static void BindDropDownList(DropDownList dropDownList, DataTable dataTable, string dataValueField, string dataTextField, string startValue)
        {
            try
            {
                //if (dropDownList.ID.ToString() != "ddlidPeriodoLetivo")
                //dataTable = Library.SortDataTable(dataTable, dataTextField);

                dropDownList.Items.Clear();

                if (startValue != null)
                    dropDownList.Items.Add(new ListItem("  ", "0"));

                if (dataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        //oDropDownList.Items.Add(new ListItem(oDtt.Rows[i][DataTextField].ToString(), oDtt.Rows[i][DataValueField].ToString()));
                        dropDownList.Items.Add(new ListItem(FormatString(dataTextField, dataTable.Rows[i]), FormatString(dataValueField, dataTable.Rows[i])));
                    }

                }

                if (startValue != null)
                {
                    dropDownList.Items[0].Value = "0";
                    dropDownList.Items[0].Text = startValue;
                }

                dropDownList.ClearSelection();
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw;
            }
        }
        public static void BindListItem(ListItemCollection Items, DataTable dataTable, string dataValueField, string dataTextField)
        {
            BindListItem(Items, dataTable, dataValueField, dataTextField, null);
        }
        public static void BindListItem(ListItemCollection Items, DataTable dataTable, string dataValueField, string dataTextField, string startValue)
        {
            try
            {

                Items.Clear();

                if (startValue != null)
                    Items.Add(new ListItem("  ", "0"));

                if (dataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        //oDropDownList.Items.Add(new ListItem(oDtt.Rows[i][DataTextField].ToString(), oDtt.Rows[i][DataValueField].ToString()));
                        Items.Add(new ListItem(FormatString(dataTextField, dataTable.Rows[i]), FormatString(dataValueField, dataTable.Rows[i])));
                    }

                }

                if (startValue != null)
                {
                    Items[0].Value = "0";
                    Items[0].Text = startValue;
                }

            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw;
            }
        }

        public static void BindCheckBoxList(CheckBoxList checkBoxList, DataTable dataTable, string dataValueField, string dataTextField)
        {
            try
            {
                checkBoxList.Items.Clear();

                if (dataTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                        checkBoxList.Items.Add(new ListItem(FormatString(dataTextField, dataTable.Rows[i]), FormatString(dataValueField, dataTable.Rows[i])));
                }

                checkBoxList.ClearSelection();
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw;
            }
        }

        public static void AppendItemsDropDownList(DropDownList oDropDownList, DataTable oDtt, string DataValueField, string DataTextField, bool Selecione)
        {
            try
            {
                if (oDtt.Rows.Count > 0)
                {
                    for (int i = 0; i < oDtt.Rows.Count; i++)
                    {
                        //oDropDownList.Items.Add(new ListItem(oDtt.Rows[i][DataTextField].ToString(), oDtt.Rows[i][DataValueField].ToString()));
                        oDropDownList.Items.Add(new ListItem(FormatString(DataTextField, oDtt.Rows[i]), oDtt.Rows[i][DataValueField].ToString()));
                    }

                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw;
            }
        }


        public static void BindListBox(ListBox listBox, DataTable dataTable, string dataValueField, string dataTextField, bool clear)
        {
            try
            {
                if (clear)
                    listBox.Items.Clear();

                for (int i = 0; i < dataTable.Rows.Count; i++)
                    listBox.Items.Add(new ListItem(FormatString(dataTextField, dataTable.Rows[i]), dataTable.Rows[i][dataValueField].ToString()));
                /*
                if(clear)
                    listBox.Items.Clear();

                listBox.DataTextField = dataTextField;
                listBox.DataValueField = dataValueField;
                listBox.DataSource = dataTable;
                listBox.DataBind();
                */
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw;
            }
        }
        public static void BindListBox(ListBox listBox, DataTable dataTable, string dataValueField, string dataTextField)
        {
            BindListBox(listBox, dataTable, dataValueField, dataTextField, true);
        }


        public static int ToInteger(object value)
        {
            int id = 0;

            if (value == null)
                return id;

            int.TryParse(value.ToString(), out id);
            return id;
        }

        public static string ToRA(object value)
        {
            try
            {
                return string.Format("{0}/{1}", value.ToString().Substring(0, value.ToString().Length - 1), value.ToString().Substring(value.ToString().Length - 1, 1));
            }
            catch
            {
                return string.Empty;
            }
        }

        public static int RAToInteger(object value)
        {
            try
            {
                if (ToString(value).Length > 1)
                {
                    string[] anuRA = value.ToString().Trim().Split('/');
                    return Convert.ToInt32((anuRA[0] + anuRA[1]));
                }
                else
                {
                    return ToInteger(value.ToString().Trim());
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return 0;
            }
        }

        public static string ToString(object value)
        {
            try
            {
                return Convert.ToString(value).Trim();
            }
            catch
            {
                return string.Empty;
            }
        }

        public static Parameters ToParameters(object value)
        {
            try
            {
                Parameters param = (Parameters)value;
                if (param != null)
                    return (Parameters)value;
                else
                    return new Parameters();
            }
            catch
            {
                return new Parameters();
            }
        }

        public static byte ToByte(object value)
        {
            try
            {
                return Convert.ToByte(value);
            }
            catch
            {
                return 0;
            }
        }

        public static Int16 ToShort(object value)
        {
            try
            {
                return Convert.ToInt16(value);
            }
            catch
            {
                return 0;
            }
        }

        public static bool ToBoolean(object value)
        {
            try
            {
                if (value == null)
                    return false;

                if (value.ToString().Equals("N"))
                    return false;
                else if (value.ToString().Equals("S"))
                    return true;
                else
                    return Convert.ToBoolean(value);
            }
            catch
            {
                return false;
            }
        }


        public static string BooleanToString(bool value)
        {
            try
            {
                if (value)
                {
                    return "S";
                }
                else
                {
                    return "N";
                }

            }
            catch
            {
                return string.Empty;
            }
        }


        public static bool StringToBool(string Value)
        {
            try
            {
                if (Value.Trim().ToUpper() == "S")
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch
            {
                return false;
            }
        }


        public static long ToLong(object Value)
        {
            try
            {
                return Convert.ToInt64(Value);
            }
            catch
            {
                return 0;
            }
        }


        public static DateTime ToDate(object Value)
        {
            try
            {
                if (Value.ToString().Length == 0)
                {// Foi informado um valor vazio
                    return new DateTime(1900, 1, 2);
                }

                return Convert.ToDateTime(Value, new CultureInfo("pt-BR"));
            }
            catch
            {
                return new DateTime(1900, 1, 1);
            }
        }

        public static Guid? ToGuid(string value)
        {
            try
            {
                return new Guid(value);
            }
            catch
            {
                return null;
            }
        }


        public static object ToDBDate(object Value)
        {
            try
            {

                DateTime DateCompare = new DateTime(Convert.ToDateTime(Value).Year, Convert.ToDateTime(Value).Month, Convert.ToDateTime(Value).Day);


                if (Convert.ToDateTime(DateCompare) == Convert.ToDateTime("01/01/1900"))
                {
                    return DBNull.Value;
                }

                if (Convert.ToDateTime(DateCompare) == Convert.ToDateTime("01/01/0001"))
                {
                    return DBNull.Value;
                }

                if (Convert.ToDateTime(DateCompare) == Convert.ToDateTime("02/01/1900"))
                {// Foi informado um valor vazio
                    return DBNull.Value;
                }

                if (Convert.ToDateTime(DateCompare) < Convert.ToDateTime("01/01/1900"))
                {
                    return DBNull.Value;
                }

                return Convert.ToDateTime(Value).ToString("yyyy-MM-dd HH:mm:ss");
            }
            catch
            {
                return DBNull.Value;
            }
        }


        public static char ToChar(object value)
        {
            try
            {
                return Convert.ToChar(value);
            }
            catch
            {
                return new char();
            }
        }


        public static ArrayList ToArrayList(object value)
        {
            try
            {
                return (ArrayList)(value);
            }
            catch
            {
                return new ArrayList();
            }
        }


        public static double ToDouble(object Value)
        {
            try
            {
                return Convert.ToDouble(Value);
            }
            catch
            {
                return 0;
            }
        }

        public static object ToDBInteger(object Value)
        {
            try
            {
                if (Convert.ToInt32(Value) == 0)
                {
                    return DBNull.Value;
                }
                else
                {
                    return Convert.ToInt32(Value);
                }
            }
            catch
            {
                return DBNull.Value;
            }
        }

        public static object ToDBLong(object Value)
        {
            try
            {
                if (Convert.ToInt64(Value) == 0)
                {
                    return DBNull.Value;
                }
                else
                {
                    return Convert.ToInt64(Value);
                }
            }
            catch
            {
                return DBNull.Value;
            }
        }

        public static object ToDBDecimal(object Value)
        {
            try
            {
                if (Convert.ToDecimal(Value) == 0)
                {
                    return DBNull.Value;
                }
                else
                {
                    return Convert.ToDecimal(Value);
                }
            }
            catch
            {
                return DBNull.Value;
            }
        }


        public static object ToDBString(object Value)
        {
            try
            {
                if (Value.ToString().Trim().Length == 0)
                {
                    return DBNull.Value;
                }
                else
                {
                    return Value.ToString();
                }
            }
            catch
            {
                return DBNull.Value;
            }
        }

        public static object ToDBBoolean(object value)
        {
            try
            {
                return Convert.ToBoolean(value);
            }
            catch
            {
                return DBNull.Value;
            }
        }


        public static decimal ToDecimal(object Value)
        {
            try
            {
                return Convert.ToDecimal(Value);
            }
            catch
            {
                return 0;
            }
        }


        public static string FormatDate(DateTime value)
        {
            try
            {
                return Convert.ToDateTime(value).ToString("dd/MM/yyyy");
            }
            catch
            {
                return string.Empty;
            }
        }


        public static string DataExtenso(string str)
        {
            try
            {
                CultureInfo br = new CultureInfo("pt-PT"); //cultura Brasil
                DateTime DT; //Objeto Data

                DT = Convert.ToDateTime(str, br);
                return DT.ToString("dd" + " \\de " + "MMMM" + " \\de " + "yyyy", br);
            }
            catch
            {
                return string.Empty;
            }
        }


        private static string GrupoExtenso(int valor)
        {
            string aux = "";

            string[] arrUnd = { "", "Um", "Dois", "Três", "Quatro", "Cinco", "Seis", "Sete", "Oito", "Nove" };
            string[] arrDezes = { "Dez", "Onze", "Doze", "Treze", "Quatorze", "Quinze", "Dezesseis", "Dezessete", "Dezoito", "Dezenove" };
            string[] arrDez = { "", "Dez", "Vinte", "Trinta", "Quarenta", "Cinquenta", "Sessenta", "Setenta", "Oitenta", "Noventa" };
            string[] arrCem = { "Cem", "Cento", "Duzentos", "Trezentos", "Quatrocentos", "Quinhentos", "Seiscentos", "Setecentos", "Oitocentos", "Novecentos" };

            int tcem = valor / 100;
            int tdez = (valor % 100) / 10;
            int tund = (valor % 100) % 10;

            if (valor == 100)
                return arrCem[0];

            if (tcem > 0)
            {
                aux += arrCem[tcem];
                if (tcem != valor)
                    aux += " e ";
            }
            if (tdez == 1)
                aux += arrDezes[tund];
            else
            {
                aux += arrDez[tdez];
                if (tund > 0 && tdez > 0)
                    aux += " e ";
                aux += arrUnd[tund];
            }
            return aux.Trim();
        }




        public static string Extenso(string vr)
        {
            string aux = "";
            if (vr == "")
            {
                return "";
            }
            decimal valor = Convert.ToDecimal(vr);
            int real = (int)Decimal.Truncate(valor);
            ;
            int cents = (int)Math.Round((valor - real) * 100, 0);

            int t_val = real;
            int tbilhao = t_val / 1000000000;
            t_val = t_val % 1000000000;
            int tmilhao = t_val / 1000000;
            t_val = t_val % 1000000;
            int tmil = t_val / 1000;
            t_val = t_val % 1000;
            int tcem = t_val;

            if (tbilhao > 0)
            {
                aux += GrupoExtenso(tbilhao);
                if (tbilhao == 1)
                    aux += " Bilhão";
                else
                    aux += " Bilhões";
            }
            if (tmilhao > 0)
            {
                if (aux != "")
                    aux += " e ";
                aux += GrupoExtenso(tmilhao);
                if (tmilhao == 1)
                    aux += " Milhão";
                else
                    aux += " Milhões";
            }
            if (tmil > 0)
            {
                if (aux != "")
                    aux += " e ";
                aux += GrupoExtenso(tmil);
                aux += " Mil";
            }
            if (tcem > 0)
            {
                if (aux != "")
                    aux += " e ";
                aux += GrupoExtenso(tcem);
            }
            if (real == 1)
                aux += " Real";
            else if (real > 0)
                aux += " Reais";

            if (cents > 0)
            {
                if (aux != "")
                    aux += " e ";
                aux += GrupoExtenso(cents);
                if (cents == 1)
                    aux += " Centavo";
                else
                    aux += " Centavos";
            }
            return aux;
        }


        public static Bitmap DrawBarCode(long code, int barHeight, int singleBarWidth)
        {
            int f, f1, f2, i, xPos;
            int fino = singleBarWidth;
            int largo = fino * 3;
            string[] BarCodes = new string[100];
            string texto;
            string ftemp;

            Bitmap bitmap = new Bitmap(500, barHeight);

            Graphics g = Graphics.FromImage(bitmap);

            Brush preto = Brushes.Black;
            Brush branco = Brushes.White;

            if (BarCodes[0] == null)
            {
                BarCodes[0] = "00110";
                BarCodes[1] = "10001";
                BarCodes[2] = "01001";
                BarCodes[3] = "11000";
                BarCodes[4] = "00101";
                BarCodes[5] = "10100";
                BarCodes[6] = "01100";
                BarCodes[7] = "00011";
                BarCodes[8] = "10010";
                BarCodes[9] = "01010";
                for (f1 = 9; f1 >= 0; f1--)
                {
                    for (f2 = 9; f2 >= 0; f2--)
                    {
                        f = f1 * 10 + f2;
                        texto = "";
                        for (i = 0; i < 5; i++)
                        {
                            texto += BarCodes[f1][i].ToString() + BarCodes[f2][i].ToString();
                        }
                        BarCodes[f] = texto;
                    }
                }
            }


            //'Desenho da barra

            xPos = 0;
            //' Guarda inicial
            g.FillRectangle(preto, xPos, 0, fino, barHeight);
            xPos += fino;
            g.FillRectangle(branco, xPos, 0, fino, barHeight);
            xPos += fino;
            g.FillRectangle(preto, xPos, 0, fino, barHeight);
            xPos += fino;
            g.FillRectangle(branco, xPos, 0, fino, barHeight);
            xPos += fino;
            texto = code.ToString();
            while (texto.Length < 6 || texto.Length % 2 > 0) //6 digitos
            {
                texto = "0" + texto;
            }
            //			' Draw dos dados
            while (texto.Length > 0)
            {
                i = Convert.ToInt32(texto.Substring(0, 2));
                if (texto.Length > 2)
                    texto = texto.Substring(2, texto.Length - 2);
                else
                    texto = "";
                ftemp = BarCodes[i];
                for (i = 0; i < 10; i += 2)
                {
                    if (ftemp[i] == '0')
                        f1 = fino;
                    else
                        f1 = largo;
                    g.FillRectangle(preto, xPos, 0, f1, barHeight);
                    xPos += f1;

                    if (ftemp[i + 1] == '0')
                        f2 = fino;
                    else
                        f2 = largo;
                    g.FillRectangle(branco, xPos, 0, f2, barHeight);
                    xPos += f2;

                }
            }

            //			' Draw guarda final
            g.FillRectangle(preto, xPos, 0, largo, barHeight);
            xPos += largo;
            g.FillRectangle(branco, xPos, 0, fino, barHeight);
            xPos += fino;
            g.FillRectangle(preto, xPos, 0, 1, barHeight);
            xPos += 1;

            Bitmap bmp = new Bitmap(xPos + 1, barHeight);
            for (int r = 0; r < barHeight; r++)
            {
                for (int c = 0; c < xPos; c++)
                {
                    bmp.SetPixel(c, r, bitmap.GetPixel(c, r));
                }
            }


            return bmp;
        }


        public static Bitmap DrawBarCode(long code, int barHeight, int singleBarWidth, int numDigitos)
        {
            int f, f1, f2, i, xPos;
            int fino = singleBarWidth;
            int largo = fino * 3;
            string[] BarCodes = new string[100];
            string texto;
            string ftemp;

            Bitmap bitmap = new Bitmap(500, barHeight);

            Graphics g = Graphics.FromImage(bitmap);

            Brush preto = Brushes.Black;
            Brush branco = Brushes.White;

            if (BarCodes[0] == null)
            {
                BarCodes[0] = "00110";
                BarCodes[1] = "10001";
                BarCodes[2] = "01001";
                BarCodes[3] = "11000";
                BarCodes[4] = "00101";
                BarCodes[5] = "10100";
                BarCodes[6] = "01100";
                BarCodes[7] = "00011";
                BarCodes[8] = "10010";
                BarCodes[9] = "01010";
                for (f1 = 9; f1 >= 0; f1--)
                {
                    for (f2 = 9; f2 >= 0; f2--)
                    {
                        f = f1 * 10 + f2;
                        texto = "";
                        for (i = 0; i < 5; i++)
                        {
                            texto += BarCodes[f1][i].ToString() + BarCodes[f2][i].ToString();
                        }
                        BarCodes[f] = texto;
                    }
                }
            }


            //'Desenho da barra

            xPos = 0;
            //' Guarda inicial
            g.FillRectangle(preto, xPos, 0, fino, barHeight);
            xPos += fino;
            g.FillRectangle(branco, xPos, 0, fino, barHeight);
            xPos += fino;
            g.FillRectangle(preto, xPos, 0, fino, barHeight);
            xPos += fino;
            g.FillRectangle(branco, xPos, 0, fino, barHeight);
            xPos += fino;
            texto = code.ToString();

            if (numDigitos % 2 > 0)
                numDigitos++;

            while (texto.Length < numDigitos || texto.Length % 2 > 0) //numDigitos digitos
            {
                texto = "0" + texto;
            }
            //			' Draw dos dados
            while (texto.Length > 0)
            {
                i = Convert.ToInt32(texto.Substring(0, 2));
                if (texto.Length > 2)
                    texto = texto.Substring(2, texto.Length - 2);
                else
                    texto = "";
                ftemp = BarCodes[i];
                for (i = 0; i < 10; i += 2)
                {
                    if (ftemp[i] == '0')
                        f1 = fino;
                    else
                        f1 = largo;
                    g.FillRectangle(preto, xPos, 0, f1, barHeight);
                    xPos += f1;

                    if (ftemp[i + 1] == '0')
                        f2 = fino;
                    else
                        f2 = largo;
                    g.FillRectangle(branco, xPos, 0, f2, barHeight);
                    xPos += f2;

                }
            }

            //			' Draw guarda final
            g.FillRectangle(preto, xPos, 0, largo, barHeight);
            xPos += largo;
            g.FillRectangle(branco, xPos, 0, fino, barHeight);
            xPos += fino;
            g.FillRectangle(preto, xPos, 0, 1, barHeight);
            xPos += 1;

            Bitmap bmp = new Bitmap(xPos + 1, barHeight);
            for (int r = 0; r < barHeight; r++)
            {
                for (int c = 0; c < xPos; c++)
                {
                    bmp.SetPixel(c, r, bitmap.GetPixel(c, r));
                }
            }


            return bmp;
        }


        public static Bitmap DrawBarCode(string Code, int barHeight, int singleBarWidth, int numDigitos)
        {
            int f, f1, f2, i, xPos;
            int fino = singleBarWidth;
            int largo = fino * 3;
            string[] BarCodes = new string[100];
            string texto;
            string ftemp;

            Bitmap bitmap = new Bitmap(500, barHeight);

            Graphics g = Graphics.FromImage(bitmap);

            Brush preto = Brushes.Black;
            Brush branco = Brushes.White;

            if (BarCodes[0] == null)
            {
                BarCodes[0] = "00110";
                BarCodes[1] = "10001";
                BarCodes[2] = "01001";
                BarCodes[3] = "11000";
                BarCodes[4] = "00101";
                BarCodes[5] = "10100";
                BarCodes[6] = "01100";
                BarCodes[7] = "00011";
                BarCodes[8] = "10010";
                BarCodes[9] = "01010";
                for (f1 = 9; f1 >= 0; f1--)
                {
                    for (f2 = 9; f2 >= 0; f2--)
                    {
                        f = f1 * 10 + f2;
                        texto = "";
                        for (i = 0; i < 5; i++)
                        {
                            texto += BarCodes[f1][i].ToString() + BarCodes[f2][i].ToString();
                        }
                        BarCodes[f] = texto;
                    }
                }
            }


            //'Desenho da barra

            xPos = 0;
            //' Guarda inicial
            g.FillRectangle(preto, xPos, 0, fino, barHeight);
            xPos += fino;
            g.FillRectangle(branco, xPos, 0, fino, barHeight);
            xPos += fino;
            g.FillRectangle(preto, xPos, 0, fino, barHeight);
            xPos += fino;
            g.FillRectangle(branco, xPos, 0, fino, barHeight);
            xPos += fino;

            texto = Code.ToString();

            if (numDigitos % 2 > 0)
                numDigitos++;

            while (texto.Length < numDigitos || texto.Length % 2 > 0) //numDigitos digitos
            {
                texto = "0" + texto;
            }
            //			' Draw dos dados
            while (texto.Length > 0)
            {
                i = Convert.ToInt32(texto.Substring(0, 2));
                if (texto.Length > 2)
                    texto = texto.Substring(2, texto.Length - 2);
                else
                    texto = "";
                ftemp = BarCodes[i];
                for (i = 0; i < 10; i += 2)
                {
                    if (ftemp[i] == '0')
                        f1 = fino;
                    else
                        f1 = largo;
                    g.FillRectangle(preto, xPos, 0, f1, barHeight);
                    xPos += f1;

                    if (ftemp[i + 1] == '0')
                        f2 = fino;
                    else
                        f2 = largo;
                    g.FillRectangle(branco, xPos, 0, f2, barHeight);
                    xPos += f2;

                }
            }

            //			' Draw guarda final
            g.FillRectangle(preto, xPos, 0, largo, barHeight);
            xPos += largo;
            g.FillRectangle(branco, xPos, 0, fino, barHeight);
            xPos += fino;
            g.FillRectangle(preto, xPos, 0, 1, barHeight);
            xPos += 1;

            Bitmap bmp = new Bitmap(xPos + 1, barHeight);
            for (int r = 0; r < barHeight; r++)
            {
                for (int c = 0; c < xPos; c++)
                {
                    bmp.SetPixel(c, r, bitmap.GetPixel(c, r));
                }
            }


            return bmp;
        }


        public static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }


        public static byte[] streamImage(Bitmap bmp, string contentType)
        {
            MemoryStream mstream = new MemoryStream();
            ImageCodecInfo myImageCodecInfo = GetEncoderInfo(contentType);

            EncoderParameter myEncoderParameter0 = new EncoderParameter(Encoder.Quality, (long)100);
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            myEncoderParameters.Param[0] = myEncoderParameter0;

            bmp.Save(mstream, myImageCodecInfo, myEncoderParameters);

            return mstream.GetBuffer();
        }


        public void SendMailWebAdministrators(string Assunto, string Message)
        {
            try
            {
                char[] cChar = new char[] { ';' };
                Mail oMail = new Mail();
                oMail.Send(GetWebAdministrators(), "SGI.UI", MailPriority.High, Assunto, Message);
            }
            catch
            {

            }
        }

        public static string FormatHour(DateTime Date)
        {
            return Date.ToString("HH:mm");
        }

        public static string FormatHour(int Minute)
        {
            int iHour = (Minute / 60);
            int iMinute = (Minute % 60);
            return (iHour.ToString().Length == 1 ? "0" + iHour.ToString() : iHour.ToString()) + ":" + (iMinute.ToString().Length == 1 ? "0" + iMinute.ToString() : iMinute.ToString());

        }


        public static string FormatHour(int Initial, int Final)
        {
            return FormatHour(Initial) + " às " + FormatHour(Final);

        }


        public static string FormatHour(int Hour, int Minute, int Second)
        {
            string sHour = (Hour.ToString().Length == 1 ? "0" + Hour.ToString() : Hour.ToString());
            string sMinute = (Minute.ToString().Length == 1 ? "0" + Minute.ToString() : Minute.ToString());
            string sSecond = (Second.ToString().Length == 1 ? "0" + Second.ToString() : Second.ToString());
            string sHourFormated = sHour + ":" + sMinute + ":" + sSecond;
            return sHourFormated;

        }


        public static bool UploadFile(HttpPostedFile oFile, string strFileNameDestination, string strDirectoryDestination)
        {
            try
            {
                if (!Directory.Exists(strDirectoryDestination))
                {
                    Directory.CreateDirectory(strDirectoryDestination);
                }

                oFile.SaveAs(strDirectoryDestination + "\\" + strFileNameDestination);

                return true;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return false;
            }
        }

        public static bool DeleteFile(string PathFileDestination)
        {
            try
            {
                if (!File.Exists(PathFileDestination))
                {
                    return false;
                }

                File.Delete(PathFileDestination);

                return true;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return false;
            }
        }

        public static DataTable SortDataTable(DataTable data, string Sort)
        {
            data.DefaultView.Sort = Sort;
            DataTable dataTemp = ConvertDataViewToDataTable(data.DefaultView);
            return dataTemp;
        }


        public static DataTable ConvertDataViewToDataTable(DataView oDataView)
        {
            if (null == oDataView)
            {
                throw new ArgumentNullException
                    ("DataView", "DataView inválido");
            }

            DataTable obNewDt = oDataView.Table.Clone();
            int idx = 0;
            string[] strColNames = new string[obNewDt.Columns.Count];
            foreach (DataColumn col in obNewDt.Columns)
            {
                strColNames[idx++] = col.ColumnName;
            }

            IEnumerator viewEnumerator = oDataView.GetEnumerator();
            while (viewEnumerator.MoveNext())
            {
                DataRowView drv = (DataRowView)viewEnumerator.Current;
                DataRow dr = obNewDt.NewRow();
                try
                {
                    foreach (string strName in strColNames)
                    {
                        dr[strName] = drv[strName];
                    }
                }
                catch
                {

                }
                obNewDt.Rows.Add(dr);
            }

            return obNewDt;
        }


        public static DataTable FilterDataTable(DataTable oObj, string Filter)
        {
            DataView oDtv = oObj.DefaultView;
            oDtv.RowFilter = Filter;
            return ConvertDataViewToDataTable(oDtv);
        }
        public static AlertList ValidaData(DateTime oDateTime)
        {
            return ValidaData(oDateTime, false);
        }

        public static string ValidaIndicador(string icValue)
        {
            if (icValue != "S")
                return "N";
            else
                return "S";
        }

        public static AlertList ValidaData(DateTime oDateTime, bool now)
        {
            DateTime menorData = new DateTime(1900, 01, 01);
            DateTime maiorData = new DateTime(2050, 12, 31);
            if (oDateTime.Date < menorData || oDateTime.Date > maiorData)
            {
                return new AlertList("070");
            }
            if (oDateTime.Date > DateTime.Now && now)
            {
                return new AlertList("071");
            }
            return new AlertList();
        }

        public static string OpenWindowCenter(string url)
        {
            return ClientScript.OpenWindowScreenCenter(url);
        }

        public static void OpenWindowCenter(string url, Page page)
        {
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), Const.SGIPOPUP, ClientScript.OpenWindowScreenCenter(url), false);
        }

        public static bool ValidEmail(string email)
        {
            if (email.Length == 0)
                return false;
            if (email.IndexOf("@", 1) > 0 && email.IndexOf(".", 1) > 0)
                return true;
            return false;
        }


        #region Consulta no WebSite Correios
        public static string CorreiosChangeHtmlToXml(string value)
        {
            try
            {
                string sAux;
                int iStart = value.IndexOf("<table");
                int iStop = (value.IndexOf("</table>") + 8);

                sAux = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\n";
                sAux += value.Substring(iStart, iStop - iStart).ToLower();
                sAux = sAux.Replace("nowrap", "");
                sAux = sAux.Replace("  ", "");
                sAux = sAux.Replace("'", Convert.ToChar(34).ToString());
                sAux = sAux.Replace("&nbsp;", " ");
                return CorreiosClearHtml(sAux);
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return string.Empty;
            }
        }


        private static string CorreiosClearHtml(string value)
        {
            int iStart = -1;
            int iStop = 0;
            string sHtml = string.Empty;
            string sChar = string.Empty;
            ArrayList aTag = new ArrayList();

            for (int i = 0; i < value.Length; i++)
            {
                sChar = value.Substring(i, 1);

                if (sChar == "<") { iStart = i; }
                if (sChar == ">") { iStop = i; }
                if (iStart == -1) { sHtml += sChar; }
                if (iStart > -1 & iStop > 0)
                {
                    sHtml += CorreiosClearTag(value.Substring(iStart, (iStop + 1) - iStart));
                    iStart = -1;
                    iStop = 0;
                }
            }
            sHtml = sHtml.Replace("<br>", "");
            sHtml = sHtml.Replace("</br>", "");
            sHtml = sHtml.Replace("</a>", "");
            return sHtml;
        }


        private static string CorreiosClearTag(string value)
        {
            try
            {
                string sTag = value;

                if (value.IndexOf("<table") >= 0)
                {
                    sTag = "<table>";
                }
                if (value.IndexOf("<tr") >= 0)
                {
                    sTag = "<tr>";
                }
                if (value.IndexOf("<td") >= 0)
                {
                    sTag = "<td>";
                }
                if (value.IndexOf("<a") >= 0)
                {
                    sTag = "";
                }
                return sTag;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return string.Empty;
            }
        }

        #endregion

        public static DataRow NewDataRow(object oObj)
        {
            try
            {

                PropertyInfo[] aProperties = oObj.GetType().GetProperties();
                PropertyInfo oProperty;
                int i;
                //DataRow oRow;

                DataTable oDtt = new DataTable(oObj.GetType().Name + "List");
                DataColumn oCol;

                for (i = 0; i < aProperties.Length; i++)
                {
                    oProperty = aProperties[i];
                    if (oProperty.Name.ToLower() != "parent")
                    {
                        oCol = new DataColumn(oProperty.Name);
                        oCol.DataType = oProperty.PropertyType;
                        oCol.DefaultValue = oProperty.GetValue(oObj, null);
                        oDtt.Columns.Add(oCol);
                    }
                }

                return oDtt.Rows[0];
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw new ApplicationException(e.Message);
            }
        }

        public static DataTable NewDataTable(object oObj)
        {
            try
            {

                PropertyInfo[] aProperties = oObj.GetType().GetProperties();
                PropertyInfo oProperty;
                int i;
                DataRow oRow;

                DataTable oDtt = new DataTable(oObj.GetType().Name + "List");
                DataColumn oCol;

                for (i = 0; i < aProperties.Length; i++)
                {
                    oProperty = aProperties[i];
                    if (oProperty.PropertyType.ToString().Equals("SGI.Common.IObjectList"))
                        continue;
                    if (oProperty.Name.ToLower().Equals("parent"))
                        continue;

                    oCol = new DataColumn(oProperty.Name);
                    oCol.DataType = oProperty.PropertyType;
                    oCol.DefaultValue = oProperty.GetValue(oObj, null);
                    oDtt.Columns.Add(oCol);

                    oRow = oDtt.NewRow();
                    oDtt.Rows.Add(oRow);
                }
                oDtt.Rows.Clear();

                return oDtt;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw new ApplicationException(e.Message);
            }
        }
        public static void SortListBox(ref System.Web.UI.WebControls.ListBox oListBox, bool descending)
        {
            DataTable oDtt = new DataTable();
            oDtt.Columns.Add("Value");
            oDtt.Columns.Add("Text");
            foreach (ListItem oItem in oListBox.Items)
            {
                DataRow oRow = oDtt.NewRow();
                oRow["Text"] = oItem.Text;
                oRow["Value"] = oItem.Value;
                oDtt.Rows.Add(oRow);
            }
            DataRow[] arrDataRow = new DataRow[] { };
            if (!descending)
            {
                arrDataRow = oDtt.Select("", "Text ASC");
            }
            else
            {
                arrDataRow = oDtt.Select("", "Text DESC");
            }
            oListBox.Items.Clear();
            foreach (DataRow oRow in arrDataRow)
            {
                ListItem oItem = new ListItem((string)oRow["Text"], (string)oRow["Value"]);
                oListBox.Items.Add(oItem);
            }
        }

        public static string MesExtenso(int nuMes)
        {
            switch (nuMes)
            {
                case 1:
                    return "Janeiro";
                case 2:
                    return "Fevereiro";
                case 3:
                    return "Março";
                case 4:
                    return "Abril";
                case 5:
                    return "Maio";
                case 6:
                    return "Junho";
                case 7:
                    return "Julho";
                case 8:
                    return "Agosto";
                case 9:
                    return "Setembro";
                case 10:
                    return "Outubro";
                case 11:
                    return "Novembro";
                case 12:
                    return "Dezembro";
                default:
                    return String.Empty;
            }
        }
        public static void SaveBinaryData(string NameFile, object value)
        {
            try
            {
                string sPath = ConfigurationManager.AppSettings["PathSerializable"].ToString() + "/" + NameFile;

                if (File.Exists(sPath)) { File.Delete(sPath); }

                using (FileStream fs = new FileStream(sPath, FileMode.Create))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fs, value);
                    fs.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        public static object LoadBinaryData(string NameFile)
        {
            try
            {
                object o;
                using (FileStream fs = new FileStream(ConfigurationManager.AppSettings["PathSerializable"].ToString() + "/" + NameFile, FileMode.Open))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    o = bf.Deserialize(fs);
                    fs.Close();
                }
                return o;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return null;
            }
        }

        public static void AddRowHtmlTable(System.Web.UI.HtmlControls.HtmlTable oTable, string CssClass, params object[] oValues)
        {
            System.Web.UI.HtmlControls.HtmlTableRow oRow = new System.Web.UI.HtmlControls.HtmlTableRow();

            foreach (object oObj in oValues)
            {
                System.Web.UI.HtmlControls.HtmlTableCell oCell = new System.Web.UI.HtmlControls.HtmlTableCell();

                if (oObj is Control)
                {
                    oCell.Controls.Add(((Control)oObj));
                }
                else
                {
                    oCell.InnerText = oObj.ToString();
                }

                oRow.Cells.Add(oCell);
            }

            oTable.Rows.Add(oRow);
        }

        //		public static string ViewPDF(string nameFile, string title)
        //		{
        //			try
        //			{
        //				DataTable dataTable = (DataTable)LoadBinaryData(nameFile);
        //				if(dataTable != null & dataTable.Rows.Count > 0)
        //				{
        //					SGI.Common.Reports o = new SGI.Common.Reports(title, dataTable);
        //					o.Bind();
        //					return o.Path;
        //				}
        //				return string.Empty;
        //			}
        //			catch(Exception e)
        //			{
        //				new SGIException(e);
        //				return string.Empty;
        //			}
        //		}

        public static string GetUrlPage(Type nameClass)
        {
            string sAux = nameClass.ToString();
            sAux = sAux.Replace("SGI.UI.", "");
            sAux = GetUrlServer() + sAux.Replace(".", "/") + ".aspx";
            return sAux;
        }

        public static void FilterListBox(ListBox lstContains, ListBox lstFilter)
        {
            foreach (ListItem oItem in lstContains.Items)
            {
                ListItem o = lstFilter.Items.FindByValue(oItem.Value);
                lstFilter.Items.Remove(o);
            }
        }

        public static int GetDigitVerifyModule10(string Expression)
        {
            int iMult;
            int iTotal = 0;
            int iRes;
            int iModule10;

            iMult = Expression.Length % 2;
            iMult++;

            for (int iPos = 0; iPos < Expression.Length; iPos++)
            {
                iRes = ToInteger(Expression.Substring(iPos, 1)) * iMult;

                if (iRes > 9)
                    iRes = (iRes / 10) + (iRes % 10);

                iTotal += iRes;

                iMult = (iMult == 2 ? 1 : 2);
            }

            iModule10 = ((10 - (iTotal % 10)) % 10);

            return iModule10;
        }
        public static string GetPeriodoLetivoAtual()
        {
            int month = DateTime.Now.Month;
            int aaPeriodoLetivo = DateTime.Now.Year;
            int nuPeriodoLetivo;

            if (month < 7)
                nuPeriodoLetivo = 1;
            else
                nuPeriodoLetivo = 2;

            return string.Format("{0}/{1}", nuPeriodoLetivo.ToString(), aaPeriodoLetivo.ToString());

        }

        public static DataTable GeToObjectDataTable(object obj)
        {
            try
            {
                DataTable oDtt = new DataTable(obj.GetType().Name);

                PropertyInfo[] properties = obj.GetType().GetProperties();
                //"<" + property.Name + ">" + property.GetValue(obj, null) + "</" + property.Name + ">\n";
                for (int i = 0; i < properties.Length; i++)
                {
                    PropertyInfo property = properties[i];
                    DataColumn oCol = new DataColumn(property.Name);
                    oCol.DataType = property.PropertyType;
                    oCol.DefaultValue = property.GetValue(obj, null);
                    oDtt.Columns.Add(oCol);
                }
                DataRow oRow = oDtt.NewRow();
                oDtt.Rows.Add(oRow);

                return oDtt;
            }
            catch
            {
                return new DataTable();
            }
        }


        public static int LegendaToMencao(string mencao)
        {
            // retorna os valores refente a tabela ACDTB092_MENCAO
            switch (mencao)
            {
                case "SS":
                    return 10;
                case "MS":
                    return 9;
                case "MM":
                    return 8;
                case "MI":
                    return 3;
                case "II":
                    return 2;
                case "SR":
                    return 1;
                case "RF":
                    return 4;
                case "AP":
                    return 5;
                case "RP":
                    return 11;
                case "DP":
                    return 12;
                default:
                    return 0;
            }
        }

        public static string MencaoToLegenda(int mencao)
        {
            // retorna os valores refente a tabela ACDTB092_MENCAO
            switch (mencao)
            {
                case 10:
                    return "SS";
                case 9:
                    return "MS";
                case 8:
                    return "MM";
                case 3:
                    return "MI";
                case 2:
                    return "II";
                case 1:
                    return "SR";
                case 4:
                    return "RF";
                case 5:
                    return "AP";
                case 11:
                    return "RP";
                default:
                    return "";
            }
        }

        public static bool ValidCNPJ(string cnpj)
        {

            if (cnpj.Length != 14)
            {
                return false;
            }

            string l, inx, dig;
            int s1, s2, i, d1, d2, v, m1, m2;
            inx = cnpj.Substring(12, 2);
            cnpj = cnpj.Substring(0, 12);
            s1 = 0;
            s2 = 0;
            m2 = 2;
            for (i = 11; i >= 0; i--)
            {
                l = cnpj.Substring(i, 1);
                v = Convert.ToInt16(l);
                m1 = m2;
                m2 = m2 < 9 ? m2 + 1 : 2;
                s1 += v * m1;
                s2 += v * m2;
            }
            s1 %= 11;
            d1 = s1 < 2 ? 0 : 11 - s1;
            s2 = (s2 + 2 * d1) % 11;
            d2 = s2 < 2 ? 0 : 11 - s2;
            dig = d1.ToString() + d2.ToString();

            return (inx == dig);

        }

        public static void DownloadFile(System.Web.UI.Page page, string filename, string path, bool forceDownload)
        {
            page.Response.Clear();
            //string Path = page.MapPath(path +  filename);
            string Path = path + filename;
            string name = System.IO.Path.GetFileName(Path);
            string ext = System.IO.Path.GetExtension(Path);
            string type = "";
            // set known types based on file extension  
            if (ext != null)
            {
                switch (ext.ToLower())
                {
                    case ".htm":
                    case ".html":
                        type = "text/HTML";
                        break;

                    case ".txt":
                        type = "text/plain";
                        break;

                    case ".doc":
                    case ".rtf":
                        type = "Application/msword";
                        break;
                }
            }
            if (forceDownload)
            {
                page.Response.AppendHeader("content-disposition",
                    "attachment; filename=" + name);
            }
            if (type != "")
                page.Response.ContentType = type;

            page.Response.WriteFile(Path);
            page.Response.End();
        }

        public static bool UpdateColumnDataTable(DataTable dataTable, string column, object value)
        {
            if (dataTable == null)
                throw new Exception("Invalid reference of the DataTable.");
            else
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    dataTable.Rows[i][column] = value;
                }

            return true;
        }

        public static DateTime LastDayOfTheMonth(DateTime value)
        {
            try
            {
                return value.AddMonths(1).AddDays(-1);
            }
            catch (Exception e)
            {
                throw new ApplicationException(e.Message);
            }
        }

        public static string GetNameFileHint()
        {
            return ConfigurationManager.AppSettings["NameFileHint"];
        }

        public static string GetPathFileHint()
        {
            return ConfigurationManager.AppSettings["PathFileHint"];
        }

        public static string GetNameFileConfigXml()
        {
            return ConfigurationManager.AppSettings["NameFileConfigXml"];
        }

        public static string GetNameFileConfigKey()
        {
            return ConfigurationManager.AppSettings["NameFileConfigKey"];
        }

        public static string GetPathFileConfigXml()
        {
            return ConfigurationManager.AppSettings["PathFileConfigXml"];
        }

        public static string GetPathFileConfigKey()
        {
            return ConfigurationManager.AppSettings["PathFileConfigKey"];
        }

        public static string GetPathFileDataXml()
        {
            return ConfigurationManager.AppSettings["PathFileDataXml"];
        }

        public static int ToSmallInt(object value)
        {
            try
            {
                return Convert.ToInt16(value);
            }
            catch
            {
                return 0;
            }
        }

        public static string GetNameApplication()
        {
            return ConfigurationManager.AppSettings["PathFileDataXml"];
        }

        #region Methods Serializable
        /// <summary>
        /// Method to convert a custom Object to XML string
        /// </summary>
        /// <param name="obj">Object that is to be serialized to XML</param>
        /// <param name="type">Type of object that is to be serialized to XML</param>
        /// <returns>XML string</returns>
        public static String SerializeObject(object obj, Type type)
        {
            //try
            //{
            String XmlizedString;
            MemoryStream memoryStream = new MemoryStream();
            XmlSerializer xs = new XmlSerializer(type);
            XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);

            xs.Serialize(xmlTextWriter, obj);
            memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
            XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());
            return XmlizedString;
            //}
            //catch
            //{
            //    return null;
            //}
        }

        /// <summary>
        /// To convert a Byte Array of Unicode values (UTF-8 encoded) to a complete String.
        /// </summary>
        /// <param name="characters">Unicode Byte Array to be converted to String</param>
        /// <returns>String converted from Unicode Byte Array</returns>
        private static String UTF8ByteArrayToString(Byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            String constructedString = encoding.GetString(characters);
            return (constructedString);
        }

        /// <summary>
        /// Method to reconstruct an Object from XML string
        /// </summary>
        /// <param name="xmlSerializedString"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object DeserializeObject(String xmlSerializedString, Type type)
        {
            XmlSerializer xs = new XmlSerializer(type);
            MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(xmlSerializedString));
            XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
            return xs.Deserialize(memoryStream);
        }

        /// <summary>
        /// Converts the String to UTF8 Byte array and is used in De serialization
        /// </summary>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        private static Byte[] StringToUTF8ByteArray(String xmlString)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(xmlString);
            return byteArray;
        }
        #endregion Methods Serializable

        public static string GetNameFileLog()
        {
            return ConfigurationManager.AppSettings["NameFileLog"];
        }

        public static string GetPathFileAlert()
        {
            return ConfigurationManager.AppSettings["PathFileAlert"];
        }

        public static string GetNameFileAlert()
        {
            return ConfigurationManager.AppSettings["NameFileAlert"];
        }

        public static bool RegisteredConnectionString
        {
            get { return ToBoolean(ConfigurationManager.AppSettings["RegisteredConnectionString"]); }
        }

        public static bool EncryptedConnectionString
        {
            get { return ToBoolean(ConfigurationManager.AppSettings["EncryptedConnectionString"]); }
        }

        public static byte[] ConvertImageToByte(Image image)
        {
            byte[] bytes;
            if (image.GetType().ToString() == "System.Drawing.Image")
            {
                ImageConverter converter = new ImageConverter();
                bytes = (byte[])converter.ConvertTo(image, typeof(byte[]));
                return bytes;
            }
            else if (image.GetType().ToString() == "System.Drawing.Bitmap")
            {
                bytes = (byte[])TypeDescriptor.GetConverter(image).ConvertTo(image, typeof(byte[]));
                return bytes;

            }
            else
                throw new NotImplementedException("ConvertImageToByte invalid type " + image.GetType().ToString());
        }

        public static Image ConvertByteToImage(byte[] bytes)
        {
            Image image = null;
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                image = Image.FromStream(stream);
            }
            return image;
        }

        public static void ConvertImage(ref Byte[] inImage, out Byte[] outImage, ImageFormat fmt)
        {
            MemoryStream tmpInStream = new MemoryStream(inImage);
            Image tmpImg = Image.FromStream(tmpInStream);
            MemoryStream tmpOutStream = new MemoryStream();
            tmpImg.Save(tmpOutStream, fmt);
            outImage = new Byte[Convert.ToInt32(tmpOutStream.Length)];
            tmpOutStream.Seek(0, System.IO.SeekOrigin.Begin);
            tmpOutStream.Read(outImage, 0, Convert.ToInt32(tmpOutStream.Length));
        }


        public static string GetPathImage()
        {
            return ConfigurationManager.AppSettings["PathImage"];
        }

        public static string GetRemotingServer()
        {
            return ConfigurationManager.AppSettings["RemotingServer"];
        }

        public static int GetRemotingPort()
        {
            return ToInteger(ConfigurationManager.AppSettings["RemotingPort"]);
        }

        public static DataTable DataTableSelectDistinct(DataTable SourceTable, params string[] FieldNames)
        {
            object[] lastValues;
            DataTable newTable;
            DataRow[] orderedRows;

            if (FieldNames == null || FieldNames.Length == 0)
                throw new ArgumentNullException("FieldNames");

            lastValues = new object[FieldNames.Length];
            newTable = new DataTable();

            foreach (string fieldName in FieldNames)
                newTable.Columns.Add(fieldName, SourceTable.Columns[fieldName].DataType);

            orderedRows = SourceTable.Select("", string.Join(", ", FieldNames));

            foreach (DataRow row in orderedRows)
            {
                if (!fieldValuesAreEqual(lastValues, row, FieldNames))
                {
                    newTable.Rows.Add(createRowClone(row, newTable.NewRow(), FieldNames));

                    setLastValues(lastValues, row, FieldNames);
                }
            }

            return newTable;
        }

        private static bool fieldValuesAreEqual(object[] lastValues, DataRow currentRow, string[] fieldNames)
        {
            bool areEqual = true;

            for (int i = 0; i < fieldNames.Length; i++)
            {
                if (lastValues[i] == null || !lastValues[i].Equals(currentRow[fieldNames[i]]))
                {
                    areEqual = false;
                    break;
                }
            }

            return areEqual;
        }

        private static DataRow createRowClone(DataRow sourceRow, DataRow newRow, string[] fieldNames)
        {
            foreach (string field in fieldNames)
                newRow[field] = sourceRow[field];

            return newRow;
        }

        private static void setLastValues(object[] lastValues, DataRow sourceRow, string[] fieldNames)
        {
            for (int i = 0; i < fieldNames.Length; i++)
                lastValues[i] = sourceRow[fieldNames[i]];
        }

        #region Url
        public static string ResolveVirtualPathFromAppAbsolute(string path)
        {
            if (path[0] != '~')
                return path;

            if (path.Length == 1)
                return HttpRuntime.AppDomainAppVirtualPath;

            if (path[1] == '/' || path[1] == '\\')
            {
                string appPath = HttpRuntime.AppDomainAppVirtualPath;
                if (appPath.Length > 1)
                    return appPath + "/" + path.Substring(2);
                return "/" + path.Substring(2);
            }
            return path;
        }

        public static string ResolvePhysicalPathFromAppAbsolute(string path)
        {
            if (path[0] != '~')
                return path;

            if (path.Length == 1)
                return HttpRuntime.AppDomainAppPath;

            if (path[1] == '/' || path[1] == '\\')
            {
                string appPath = HttpRuntime.AppDomainAppPath;
                if (appPath.Length > 1)
                    return appPath + "/" + path.Substring(2);
                return "/" + path.Substring(2);
            }
            return path;
        }

        public static bool IsRooted(string path)
        {
            if (path == null || path == "")
                return true;

            char c = path[0];
            if (c == '/' || c == '\\')
                return true;

            return false;
        }

        public static string ResolveClientUrl(string relativeUrl)
        {
            if (relativeUrl == null)
                throw new ArgumentNullException("relativeUrl");

            if (relativeUrl == "")
                return "";

            if (relativeUrl[0] == '#')
                return relativeUrl;

            string ts = "~/";
            ;
            if (ts == "" || !IsRelativeUrl(relativeUrl))
                return relativeUrl;

            HttpResponse resp = HttpContext.Current.Response;
            string absoluteUrl = resp.ApplyAppPathModifier(Combine(ts, relativeUrl));
            if (absoluteUrl.StartsWith(ts + "/"))
                return absoluteUrl.Substring(ts.Length + 1);
            return absoluteUrl;
        }

        public static string RemoveDoubleSlashes(string input)
        {
            // MS VirtualPathUtility removes duplicate '/'
            string str = input;
            string x;
            while ((x = str.Replace("//", "/")) != str)
            {
                str = x;
            }

            return str;
        }

        public static bool IsRelativeUrl(string path)
        {
            return (path[0] != '/' && path.IndexOf(':') == -1);
        }

        static char[] path_sep = { '\\', '/' };

        public static string Canonic(string path)
        {
            string[] parts = path.Split(path_sep);
            int end = parts.Length;

            int dest = 0;

            for (int i = 0; i < end; i++)
            {
                string current = parts[i];
                if (current == ".")
                    continue;

                if (current == "..")
                {
                    if (dest == 0)
                    {
                        if (i == 1) // see bug 52599
                            continue;

                        throw new HttpException("Invalid path.");
                    }

                    dest--;
                    continue;
                }

                parts[dest++] = current;
            }

            if (dest == 0)
                return "/";

            return String.Join("/", parts, 0, dest);
        }

        public static string Combine(string basePath, string relPath)
        {
            if (relPath == null)
                throw new ArgumentNullException("relPath");

            int rlength = relPath.Length;
            if (rlength == 0)
                return "";

            relPath = relPath.Replace("\\", "/");
            if (IsRooted(relPath))
                return Canonic(relPath);

            char first = relPath[0];
            if (rlength < 3 || first == '~' || first == '/' || first == '\\')
            {
                if (basePath == null || (basePath.Length == 1 && basePath[0] == '/'))
                    basePath = String.Empty;

                string slash = (first == '/') ? "" : "/";
                if (first == '~')
                {
                    if (rlength == 1)
                    {
                        relPath = "";
                    }
                    else if (rlength > 1 && relPath[1] == '/')
                    {
                        relPath = relPath.Substring(2);
                        slash = "/";
                    }

                    string appvpath = HttpRuntime.AppDomainAppVirtualPath;
                    if (appvpath.EndsWith("/"))
                        slash = "";

                    return Canonic(appvpath + slash + relPath);
                }

                return Canonic(basePath + slash + relPath);
            }

            if (basePath == null || basePath == "" || basePath[0] == '~')
                basePath = HttpRuntime.AppDomainAppVirtualPath;

            if (basePath.Length <= 1)
                basePath = String.Empty;

            return Canonic(basePath + "/" + relPath);
        }

        #endregion Url

        public static string RemovePotentiallyDangerousHTMLTags(string text)
        {
            string[] tags = new string[]{"<applet>", "<body>", "<embed>", "<frame>", "<script>", 
            "<frameset>", "<html>", "<iframe>", "<img>", "<style>", 
            "<layer>", "<link>", "<ilayer>", "<meta>", "<object>", "<script type=\"text/javascript\">"};

            foreach (string tag in tags)
                text = text.Replace(tag, "");

            return text;
        }

        public static string EncodePotentiallyDangerousHTMLTags(string text)
        {
            string[] tags = new string[]{"<applet>", "<body>", "<embed>", "<frame>", "<script>", 
            "<frameset>", "<html>", "<iframe>", "<iframe", "<frame", "<img>", "<style>", 
            "<layer>", "<link>", "<ilayer>", "<meta>", "<object>", "<script type=\"text/javascript\">", 
            "</applet>", "</iframe>", "</script>", "</ilayer>", "</style>", "</frame>", "</frameset>",
            "</html>", "</body>"};

            foreach (string tag in tags)
                text = text.Replace(tag, HttpUtility.HtmlEncode(tag));

            return text;
        }

        public static bool IsHtmlEncoded(string text)
        {
            string[] tags = new[] { "&gt;", "&lt;",  
                        "&gt;", "&#243", "&quot;", "&#225" };

            foreach (string tag in tags)
            {
                if (text.ToLower().IndexOf(tag) > -1)
                    return true;
            }
            return false;
        }

        public static string FindUrl(string text)
        {

            string[] words = text.Split(' ');
            StringBuilder newText = new StringBuilder();
            int index = -1;
            string url = "";

            foreach (var word in words)
            {
                index = word.ToLower().IndexOf("http://");

                if (index > -1)
                {
                    url = word.Substring(index, word.Length - index);
                    newText.Append(word.Replace(url, string.Format("<a href=\"{0}\" class=\"link\" target=\"_blank\">{0}</a>", url)));
                    newText.Append(" ");
                    continue;
                }

                index = word.ToLower().IndexOf("https://");
                if (index > -1)
                {
                    url = word.Substring(index, word.Length - index);
                    newText.Append(word.Replace(url, string.Format("<a href=\"{0}\" class=\"link\" target=\"_blank\">{0}</a>", url)));
                    newText.Append(" ");
                    continue;
                }

                index = word.ToLower().IndexOf("ftp://");
                if (index > -1)
                {
                    url = word.Substring(index, word.Length - index);
                    newText.Append(word.Replace(url, string.Format("<a href=\"{0}\" class=\"link\" target=\"_blank\">{0}</a>", url)));
                    newText.Append(" ");
                    continue;
                }

                newText.Append(word);
                newText.Append(" ");
            }

            return newText.ToString().Trim();
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


        public static string StripHTML(string htmlText)
        {
            Regex reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
            return reg.Replace(htmlText, "");
        }

        public static void SaveStreamToFile(string fileFullPath, Stream stream)
        {
            if (stream.Length == 0) return;

            using (FileStream fileStream = System.IO.File.Create(fileFullPath, (int)stream.Length))
            {
                byte[] bytesInStream = new byte[stream.Length];
                stream.Read(bytesInStream, 0, (int)bytesInStream.Length);
                fileStream.Write(bytesInStream, 0, bytesInStream.Length);
            }
        }

        public static void BindDropDownList(DropDownList dropDownList, object dataSource, string dataValueField, string dataTextField)
        {
            dropDownList.DataTextField = dataTextField;
            dropDownList.DataValueField = dataValueField;
            dropDownList.DataSource = dataSource;
            dropDownList.DataBind();
        }

        public static void CreateDropDownListFistItem(DropDownList control, string text, string value)
        {
            //control.Items.Insert(0, new ListItem { Text = text, Value = value });
            CreateListControlFistItem(control, text, value);
        }

        #region Novos Métodos BindListControl para collections

        const string REGEX_DEFAULT_PATTERN = @"\{([0-9a-zA-Z_]*)\}";
        static Regex regex = new Regex(REGEX_DEFAULT_PATTERN);

        public static void ChangeRegexPattern(string newPattern)
        {
            regex = new Regex(newPattern);
        }

        private static bool verifyListControlDataMemberHasMatch(ListControl control)
        {
            return regex.IsMatch(control.DataValueField) || regex.IsMatch(control.DataTextField);
        }

        //Bind direto (com os member pré setados no controle)
        public static void BindListControl<T>(ListControl control, IEnumerable<T> dataSource)
        {
            control.DataSource = dataSource;
            control.DataBind();
        }

        //Bind direto com os members em parâmetro
        public static void BindListControl<T>(ListControl control, IEnumerable<T> dataSource, string dataValueMember, string dataTextMember)
        {
            control.DataValueField = dataValueMember;
            control.DataTextField = dataTextMember;
            BindListControl<T>(control, dataSource);
        }

        //Bind direto com os members em parâmetro e incluindo o primeiro item
        public static void BindListControl<T>(ListControl control, IEnumerable<T> dataSource, string dataValueMember, string dataTextMember, string itemText, object itemValue)
        {
            BindListControl<T>(control, dataSource, dataValueMember, dataTextMember);
            CreateListControlFistItem(control, itemText, itemValue);
        }

        //Bind direto com os members em parâmetro, incluindo o primeiro item e setando o valor default
        public static void BindListControl<T>(ListControl control, IEnumerable<T> dataSource, string dataValueMember, string dataTextMember, string itemText, object itemValue, object selectedValue)
        {
            BindListControl<T>(control, dataSource, dataValueMember, dataTextMember, itemText, itemValue);
            SetListControlSelectedValue(control, selectedValue);
        }

        ////Bind com string de formatação pra exibir o texto
        //private static void BindListControlCompositeText<T>(ListControl control, IEnumerable<T> dataSource, string dataValueMember, string dataTextMember)
        //{
        //    List<ListItem> boundList = new List<ListItem>();

        //    string textMemberPattern = Library.GetPatternString(regex, dataTextMember);
        //    string[] listFieldsNames = Library.GetFieldArray(regex, dataTextMember);

        //    foreach (var item in dataSource)
        //    {
        //        Type itemType = item.GetType();
        //        object _itemValue = itemType.GetProperty(dataValueMember).GetValue(item, null);

        //        List<object> _listFieldsValues = GetTypePropertiesValues<T>(item, listFieldsNames);

        //        string _itemText = String.Format(textMemberPattern, _listFieldsValues.ToArray());
        //        boundList.Add(new ListItem { Value = _itemValue.ToString(), Text = _itemText });
        //    }

        //    control.Items.AddRange(boundList.ToArray());
        //}

        //Bind com string de formatação para o valor e o texto
        public static void BindListControlComposite<T>(ListControl control, IEnumerable<T> dataSource, string dataValueMember, string textValueMember)
        {
            List<ListItem> boundList = new List<ListItem>();

            string valuePattern, textPattern;
            string[] valueFieldNames, textFieldNames;

            valuePattern = textPattern = "{0}";
            valueFieldNames = new string[] { dataValueMember };
            textFieldNames = new string[] { textValueMember };

            if (regex.IsMatch(dataValueMember))
            {
                valuePattern = Library.GetPatternString(regex, dataValueMember);
                valueFieldNames = Library.GetFieldArray(regex, dataValueMember);
            }

            if (regex.IsMatch(textValueMember))
            {
                textPattern = Library.GetPatternString(regex, textValueMember);
                textFieldNames = Library.GetFieldArray(regex, textValueMember);
            }

            foreach (var item in dataSource)
            {
                Type itemType = item.GetType();

                List<object> _listFieldsValues = GetTypePropertiesValues<T>(item, valueFieldNames);
                string _itemValue = String.Format(valuePattern, _listFieldsValues.ToArray());

                _listFieldsValues = GetTypePropertiesValues<T>(item, textFieldNames);
                string _itemText = String.Format(textPattern, _listFieldsValues.ToArray());

                boundList.Add(new ListItem { Value = _itemValue, Text = _itemText });
            }

            control.Items.AddRange(boundList.ToArray());
        }

        ////Bind com string de formatação pra exibir o texto
        //private static void BindListControlComposite<T>(ListControl control, IEnumerable<T> dataSource, string dataValueMember, string textMemberPattern, string listFieldsNames)
        //{
        //    List<ListItem> boundList = new List<ListItem>();

        //    foreach (var item in dataSource)
        //    {
        //        Type itemType = item.GetType();
        //        object _itemValue = itemType.GetProperty(dataValueMember).GetValue(item, null);

        //        List<object> _listFieldsValues = GetTypePropertiesValues<T>(item, listFieldsNames.Split(','));

        //        string _itemText = String.Format(textMemberPattern, _listFieldsValues.ToArray());
        //        boundList.Add(new ListItem { Value = _itemValue.ToString(), Text = _itemText });
        //    }

        //    control.Items.AddRange(boundList.ToArray());
        //}

        ////Bind com string de formatação para o valor e o texto
        //private static void BindListControlComposite<T>(ListControl control, IEnumerable<T> dataSource, string valuePattern, string valueFieldNames, string textPattern, string textFieldNames)
        //{
        //    List<ListItem> boundList = new List<ListItem>();

        //    foreach (var item in dataSource)
        //    {
        //        Type itemType = item.GetType();

        //        List<object> _listFieldsValues = GetTypePropertiesValues<T>(item, valueFieldNames.Split(','));
        //        string _itemValue = String.Format(valuePattern, _listFieldsValues.ToArray());

        //        _listFieldsValues = GetTypePropertiesValues<T>(item, textFieldNames.Split(','));
        //        string _itemText = String.Format(textPattern, _listFieldsValues.ToArray());

        //        boundList.Add(new ListItem { Value = _itemValue, Text = _itemText });
        //    }

        //    control.Items.AddRange(boundList.ToArray());
        //}

        //Bind com string de formatação de texto e inclusão do primeiro item
        public static void BindListControlComposite<T>(ListControl control, IEnumerable<T> dataSource, string dataValueMember, string dataTextMember, string firstItemText, object firstItemValue)
        {
            BindListControlComposite<T>(control, dataSource, dataValueMember, dataTextMember);
            CreateListControlFistItem(control, firstItemText, firstItemValue);
        }

        //Bind com string de formatação de texto, inclusão do primeiro item e setando o valor default
        public static void BindListControlComposite<T>(ListControl control, IEnumerable<T> dataSource, string dataValueMember, string dataTextMember, string firstItemText, object firstItemValue, object selectedValue)
        {
            BindListControlComposite<T>(control, dataSource, dataValueMember, dataTextMember, firstItemText, firstItemValue);
            SetListControlSelectedValue(control, selectedValue);
        }

        private static List<object> GetTypePropertiesValues<T>(T item, string[] propertiesNames)
        {
            Type itemType = item.GetType();

            List<object> _listFieldsValues = new List<object>();
            foreach (string fieldName in propertiesNames)
            {
                var propValue = itemType.GetProperty(fieldName);
                if (propValue != null)
                    _listFieldsValues.Add(propValue.GetValue(item, null));
                else
                    throw new MemberAccessException("Member does not exist: " + fieldName);
            }

            return _listFieldsValues;
        }

        public static void SetListControlSelectedValue(ListControl control, object selectedValue)
        {
            var listItem = control.Items.FindByValue(selectedValue.ToString());
            if (listItem != null)
                listItem.Selected = true;
        }

        public static void CreateListControlFistItem(ListControl control, string text, object value)
        {
            control.Items.Insert(0, new ListItem { Text = text, Value = value.ToString() });
        }

        public static string GetPatternString(Regex r, string originalString)
        {
            var matches = r.Matches(originalString);
            string resultString = originalString;

            for (int i = 0; i < matches.Count; i++)
            {
                resultString = resultString.Replace(matches[i].Value, String.Format("{{{0}}}", i));
            }

            return resultString;
        }

        public static string[] GetFieldArray(Regex r, string fieldString)
        {
            var matches = r.Matches(fieldString);

            var result = matches.Cast<Match>().Select(x => x.Value.Replace("{", "").Replace("}", "")).ToArray();

            return result;
        }

        #endregion


        #region TESTES - MATEUS FONSECA

        #region Metodos de Bind Por Reflexao - Testes (Mateus)
        // Os nomes das colunas devem ser o mesmo das propriedades da página;
        public static void BindToPageProperties(BasePage page, DataTable dados, int nuRow)
        {
            Type type = page.GetType();
            for (int i = 0; i < dados.Columns.Count; i++)
            {
                string columnName = dados.Columns[i].ColumnName;
                PropertyInfo prop = type.GetProperty(columnName);

                if (!prop.IsNull() && prop.CanWrite)
                {
                    try
                    {
                        prop.SetValue(page, Convert.ChangeType(dados.Rows[nuRow][columnName], prop.PropertyType), null);
                    }
                    catch (Exception) { }
                }
            }
        }

        #endregion

        #region Bind Table

        public static string BindTable(IDictionary<string, string> ColumnsInOrder, DataTable Dados)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(BindHeadTable(ColumnsInOrder));

            IList<IList<object>> AllListInOrder = new List<IList<object>>();

            foreach (DataRow row in Dados.Rows)
            {
                IList<object> ListInOrder = new List<object>();

                foreach (KeyValuePair<String, String> Column in ColumnsInOrder)
                {
                    string column = Column.Value;
                    string pre = column.Substring(0, 2);
                    if (pre == "dt")
                        ListInOrder.Add(Library.ToDate(row[column]).ToString("dd/MM/yyyy"));
                    else if (column == "empty")
                        ListInOrder.Add(" - ");
                    else
                        ListInOrder.Add(Library.ToString(row[column]));
                }

                AllListInOrder.Add(ListInOrder);
            }

            sb.Append(BindBody(AllListInOrder));

            return string.Format("<table class=\"gridview-completo\">{0}</table>", sb.ToString());
        }

        public static string BindHeadTable(IDictionary<string, string> ColumnsInOrder)
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<String, String> Column in ColumnsInOrder)
            {
                sb.Append(BindColumnHead(Column.Key));
            }

            return string.Format("<thead><tr>{0}</tr></thead>", sb.ToString());
        }

        public static string BindBody(IList<IList<object>> AllListInOrder)
        {
            StringBuilder sb = new StringBuilder();
            foreach (IList<object> ListInOrder in AllListInOrder)
            {
                sb.Append(BindRow(ListInOrder));
            }

            return string.Format("<tbody>{0}</tbody>", sb.ToString());
        }

        public static string BindBody(IList<IList<object>> AllListInOrder, bool PutBodyTag)
        {
            StringBuilder sb = new StringBuilder();
            foreach (IList<object> ListInOrder in AllListInOrder)
            {
                sb.Append(BindRow(ListInOrder));
            }
            if (PutBodyTag)
                return string.Format("<tbody>{0}</tbody>", sb.ToString());
            else
                return sb.ToString();
        }

        public static string BindBodyProperties(IDictionary<int, IList<object>> AllListInOrder, IDictionary<int, IDictionary<string, object>> prs)
        {
            StringBuilder sb = new StringBuilder();

            int idLinha = 0;

            foreach (IList<object> ListInOrder in AllListInOrder.Values)
            {
                IDictionary<string, object> properties = prs[idLinha];
                if (properties.Count > 0)
                {
                    BindRowProperties(sb, ListInOrder, properties);
                }
                else
                    sb.Append(BindRow(ListInOrder));

                idLinha++;
            }
            return sb.ToString();
        }

        public static void BindRowProperties(StringBuilder sb, IList<object> ListInOrder, IDictionary<string, object> properties)
        {
            foreach (KeyValuePair<string, object> pr in properties)
            {
                string val = pr.Value.ToString();
                if (pr.Key == "class")
                    sb.Append(BindRowBodyClass(ListInOrder, val));
                if (pr.Key == "style")
                    sb.Append(BindRowBodyStyle(ListInOrder, val));
            }
        }

        public static string BindRow(IList<object> ListInOrder)
        {
            StringBuilder temp = new StringBuilder();
            StringBuilder sb = new StringBuilder();
            foreach (object value in ListInOrder)
            {
                temp.Append(BindColumnBody(value));
            }
            return sb.AppendFormat("<tr>{0}</tr>", temp.ToString()).ToString();
        }

        public static string BindColumnBody(object value)
        {
            return string.Format("<td>{0}</td>", value);
        }

        public static string BindColumnHead(object value)
        {
            return string.Format("<th>{0}</th>", value);
        }

        public static string BindRowBodyClass(IList<object> ListInOrder, string Class)
        {
            StringBuilder temp = new StringBuilder();
            StringBuilder sb = new StringBuilder();
            foreach (object value in ListInOrder)
            {
                temp.Append(BindColumnBody(value));
            }
            return sb.AppendFormat("<tr class=\"{1}\">{0}</tr>", temp.ToString(), Class).ToString();
        }

        public static string BindRowBodyStyle(IList<object> ListInOrder, string style)
        {
            StringBuilder temp = new StringBuilder();
            StringBuilder sb = new StringBuilder();
            foreach (object value in ListInOrder)
            {
                temp.Append(BindColumnBody(value));
            }
            return sb.AppendFormat("<tr style=\"{1}\">{0}</tr>", temp.ToString(), style).ToString();
        }

        #endregion

        #endregion


        #region ReadExcelIntoDataTable

        public static DataTable ReadExcelIntoDataTable(Stream excelFile, bool hasHeader)
        {

            try
            {
                DataTable excelDataTable;

                // Importando os dados no datatable
                using (var excel = new ExcelPackage(excelFile))
                {
                    excelDataTable = new DataTable();

                    var excelWorkSheet = excel.Workbook.Worksheets.First();

                    var daLinha = hasHeader ? 2 : 1;
                    const int daColuna = 1;

                    var paraLinha = hasHeader ? 2 : 1;
                    var paraColuna = excelWorkSheet.Dimension.End.Column;

                    paraColuna = excelWorkSheet.Cells[daLinha, daColuna, paraLinha, paraColuna].ToList().Count;

                    excelWorkSheet.Cells[daLinha, daColuna, paraLinha, paraColuna].ToList().ForEach(cell => excelDataTable.Columns.Add(cell.Text));

                    //iteração por todas as linhas do excel.
                    var totalDeLinhas = excelWorkSheet.Dimension.End.Row;
                    var totalDeColunas = excelWorkSheet.Cells[daLinha, daColuna, paraLinha, paraColuna].ToList().Count;

                    var linhaInicial = hasHeader ? 3 : 2;

                    for (var linha = linhaInicial; linha <= totalDeLinhas; linha++)
                    {
                        var workSheetRow = excelWorkSheet.Cells[linha, 1, linha, totalDeColunas];
                        var dataTableRow = excelDataTable.NewRow();
                        foreach (var celulas in workSheetRow)
                        {
                            dataTableRow[celulas.Start.Column - 1] = celulas.Text;
                        }
                        excelDataTable.Rows.Add(dataTableRow);
                    }
                }
                return excelDataTable;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        #endregion
        #region ExportExcel

        /// <summary>
        /// Método que exporta um arquivo excel(em forma de byte[]) a partir de um DataTable
        /// </summary>
        /// <param name="dataTable">Objeto DataTable a ser exportado</param>
        /// <param name="worksheetName">Titulo da folha dentro do excel</param>
        /// <returns>Array de bytes para posterior escrita em arquivo</returns>
        public static byte[] ExportExcel(DataTable dataTable, string worksheetName)
        {
            using (ExcelPackage ep = new ExcelPackage())
            {
                // Setting workbook title
                ep.Workbook.Properties.Title = worksheetName;

                //Merging cells and create a center heading for out table
                ep.Workbook.Worksheets.Add(worksheetName);
                ExcelWorksheet ws = ep.Workbook.Worksheets[1];
                //ws.Cells[1, 1].Value = worksheetName;
                //ws.Cells[1, 1, 1, dataTable.Columns.Count].Merge = true;
                //ws.Cells[1, 1, 1, dataTable.Columns.Count].Style.Font.Bold = true;
                //ws.Cells[1, 1, 1, dataTable.Columns.Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                // Create header and rows
                int colIndex = 1;
                int rowIndex = 1;

                foreach (DataColumn dc in dataTable.Columns) //Creating Headings
                {
                    var cell = ws.Cells[rowIndex, colIndex];

                    //Setting Value in cell
                    cell.Style.Font.Bold = true;
                    cell.Value = dc.ColumnName;

                    colIndex++;
                }

                foreach (DataRow dr in dataTable.Rows) // Adding Data into rows
                {
                    colIndex = 1;
                    rowIndex++;
                    foreach (DataColumn dc in dataTable.Columns)
                    {
                        var cell = ws.Cells[rowIndex, colIndex];
                        //Setting Value in cell
                        cell.Style.Font.Bold = false;
                        cell.Value = dr[dc.ColumnName];

                        colIndex++;
                    }
                }
                return ep.GetAsByteArray();
            }
        }
        /// <summary>
        /// Método que exporta um arquivo excel(em forma de byte[]) a partir de um DataTable
        /// </summary>
        /// <param name="dataTable">Objeto DataTable a ser exportado</param>
        /// <param name="worksheetName">Titulo da folha dentro do excel</param>
        /// <param name="exclusionColumns">Um array com os titulos das colunas que nao serão exportadas para o arquivo excel</param>
        /// <returns>Array de bytes para posterior escrita em arquivo</returns>
        public static byte[] ExportExcel(DataTable dataTable, string worksheetName, string[] exclusionColumns)
        {
            using (ExcelPackage ep = new ExcelPackage())
            {
                // Setting workbook title
                ep.Workbook.Properties.Title = worksheetName;

                //Merging cells and create a center heading for out table
                ep.Workbook.Worksheets.Add(worksheetName);
                ExcelWorksheet ws = ep.Workbook.Worksheets[1];
                //ws.Cells[1, 1].Value = worksheetName; Nao havera mais titulo
                //ws.Cells[1, 1, 1, dataTable.Columns.Count].Merge = true;
                //ws.Cells[1, 1, 1, dataTable.Columns.Count].Style.Font.Bold = true;
                //ws.Cells[1, 1, 1, dataTable.Columns.Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                // Create header and rows
                int colIndex = 1;
                int rowIndex = 1;

                foreach (DataColumn dc in dataTable.Columns) //Creating Headings
                {
                    if (!exclusionColumns.Contains(dc.ColumnName))
                    {
                        var cell = ws.Cells[rowIndex, colIndex];

                        //Setting Value in cell
                        cell.Value = dc.ColumnName;
                        cell.Style.Font.Bold = true;

                        colIndex++;
                    }
                }

                foreach (DataRow dr in dataTable.Rows) // Adding Data into rows
                {
                    colIndex = 1;
                    rowIndex++;
                    foreach (DataColumn dc in dataTable.Columns)
                    {
                        if (!exclusionColumns.Contains(dc.ColumnName))
                        {
                            var cell = ws.Cells[rowIndex, colIndex];
                            //Setting Value in cell
                            cell.Style.Font.Bold = false;
                            cell.Value = dr[dc.ColumnName];

                            colIndex++;
                        }
                    }
                }
                return ep.GetAsByteArray();
            }
        }


        #endregion

        #region BytesToFile

        public static bool BytesToFile(string fileName, byte[] data)
        {
            bool result = false;
            using (System.IO.FileStream fileStream = new System.IO.FileStream(fileName, System.IO.FileMode.Create, FileAccess.ReadWrite))
            {
                try
                {
                    // Gravando os dados no arquivo
                    fileStream.Write(data, 0, data.Length);
                    result = true;
                }
                catch (Exception ex)
                {
                    //throw ex;
                }
            }
            return result;
        }

        #endregion

        /// <summary>
        /// Encode string para base64
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Base64Encode(string text)
        {
            var textBytes = System.Text.Encoding.UTF8.GetBytes(text);
            return System.Convert.ToBase64String(textBytes);
        }
        /// <summary>
        /// Decode base64 para string
        /// </summary>
        /// <param name="base64EncodedData"></param>
        /// <returns></returns>
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }


        /// <summary>
        /// Convert uma lista de objetos para DataTable
        /// </summary>
        /// <param name="data">List<T></param>
        /// <returns>DataTable</returns>
        public static DataTable ConvertListToDataTable<T>(List<T> data)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];

                DataColumn column = new DataColumn(prop.Name, prop.PropertyType);
                column.AllowDBNull = true;
                //if (!prop.PropertyType.ToString().Contains("Nullable"))
                table.Columns.Add(column);    
            }
            
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }
    }
}
