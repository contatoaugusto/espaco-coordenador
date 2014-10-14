using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace EC.Common
{
    public static class DataBindExtensions
    {
        const string REGEX_DEFAULT_PATTERN = @"\{([0-9a-zA-Z_]*)\}";
        static Regex regex = new Regex(REGEX_DEFAULT_PATTERN);

        public static void ChangeRegexPattern(string newPattern)
        {
            regex = new Regex(newPattern);
        }

        private static bool VerifyListControlDataMemberHasMatch(ListControl control)
        {
            return regex.IsMatch(control.DataValueField) || regex.IsMatch(control.DataTextField);
        }

        //Bind direto (com os member pré setados no controle)
        public static void DataBind<T>(this ListControl control, IEnumerable<T> dataSource)
        {
            control.DataSource = dataSource;
            control.DataBind();
        }

        //Bind direto com os members em parâmetro
        public static void DataBind<T>(this ListControl control, IEnumerable<T> dataSource, string dataValueMember, string dataTextMember)
        {
            control.DataValueField = dataValueMember;
            control.DataTextField = dataTextMember;
            DataBind<T>(control, dataSource);
        }

        //Bind direto com os members em parâmetro e incluindo o primeiro item
        public static void DataBind<T>(this ListControl control, IEnumerable<T> dataSource, string dataValueMember, string dataTextMember, string itemText, object itemValue)
        {
            DataBind<T>(control, dataSource, dataValueMember, dataTextMember);
            CreateListControlFistItem(control, itemText, itemValue);
        }

        //Bind direto com os members em parâmetro, incluindo o primeiro item e setando o valor default
        public static void DataBind<T>(this ListControl control, IEnumerable<T> dataSource, string dataValueMember, string dataTextMember, string itemText, object itemValue, object selectedValue)
        {
            DataBind<T>(control, dataSource, dataValueMember, dataTextMember, itemText, itemValue);
            SetListControlSelectedValue(control, selectedValue);
        }

        //Bind com string de formatação para o valor e o texto
        public static void DataBindComposite<T>(this ListControl control, IEnumerable<T> dataSource, string dataValueMember, string textValueMember)
        {
            List<ListItem> boundList = new List<ListItem>();

            string valuePattern, textPattern;
            string[] valueFieldNames, textFieldNames;

            valuePattern = textPattern = "{0}";
            valueFieldNames = new string[] { dataValueMember };
            textFieldNames = new string[] { textValueMember };

            if (regex.IsMatch(dataValueMember))
            {
                valuePattern = GetPatternString(regex, dataValueMember);
                valueFieldNames = GetFieldArray(regex, dataValueMember);
            }

            if (regex.IsMatch(textValueMember))
            {
                textPattern = GetPatternString(regex, textValueMember);
                textFieldNames = GetFieldArray(regex, textValueMember);
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

        //Bind com string de formatação de texto e inclusão do primeiro item
        public static void DataBindComposite<T>(this ListControl control, IEnumerable<T> dataSource, string dataValueMember, string dataTextMember, string firstItemText, object firstItemValue)
        {
            DataBindComposite<T>(control, dataSource, dataValueMember, dataTextMember);
            CreateListControlFistItem(control, firstItemText, firstItemValue);
        }

        //Bind com string de formatação de texto, inclusão do primeiro item e setando o valor default
        public static void DataBindComposite<T>(this ListControl control, IEnumerable<T> dataSource, string dataValueMember, string dataTextMember, string firstItemText, object firstItemValue, object selectedValue)
        {
            DataBindComposite<T>(control, dataSource, dataValueMember, dataTextMember, firstItemText, firstItemValue);
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

        public static void SetListControlSelectedValue(this ListControl control, object selectedValue)
        {
            control.ClearSelection();

            var listItem = control.Items.FindByValue(selectedValue.ToString());

            if (listItem != null)
                listItem.Selected = true;
        }

        public static void CreateListControlFistItem(this ListControl control, string text, object value)
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

    }
}
