using System;
using System.Data;
using System.Reflection;
using EC.Common;

namespace EC.Common
{
    /// <summary>
    /// Summary description for ObjectBase.
    /// </summary>
    [Serializable()]
    public class Base : IBase
    {
        #region Variables
        #endregion Variables

        #region Properties
        public string Name
        {
            get { return GetType().Name; }
        }
        #endregion Properties

        #region Methods
        public virtual string ToXml()
        {
            return Library.GetObjectXml(this);
        }

        public virtual string ToXml(bool encrypted)
        {
            return Cryptography.Encryption(Library.GetObjectXml(this), Const.KEY);
        }

        public override string ToString()
        {
            return Library.GetObjectString(this);
        }

        public virtual string ToNameString()
        {
            return Library.GetObjectNameString(this);
        }

        public virtual DataTable ToDataTable()
        {
            return Library.GetObjectDataTable(this);
        }

        public virtual DataRow ToDataRow()
        {
            return Library.GetObjectDataRow(this);
        }
        public virtual void SetProperties(DataRow row)
        {
            ReadProperties(row);
        }
        public virtual void SetProperties(DataTable dataTable)
        {
            ReadProperties(dataTable.Rows[0]);
        }
        public virtual void SetProperties(DataSet dataSet)
        {
            ReadProperties(dataSet.Tables[0].Rows[0]);
        }

        public virtual void ToObject(string xml, bool decrypt)
        {
            if (decrypt)
                ToObject(Cryptography.Decryption(xml, Const.KEY));
            else
                ToObject(xml);
        }

        public virtual void ToObject(string xml)
        {
            System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
            xmlDocument.LoadXml(xml);
            ReadProperties(xmlDocument.GetElementsByTagName("object").Item(0).ChildNodes);
        }

        public virtual void ToObject(DataSet dataSet)
        {
            ReadProperties(dataSet.Tables[0].Rows[0]);
        }

        public virtual DataSet ToDataSet()
        {
            DataTable dataTable = Library.GetObjectDataTable(this);
            DataSet dataSet = new DataSet(GetType().Name);
            dataSet.Tables.Add(dataTable);
            return dataSet;

        }

        private void ReadProperties(DataRow row)
        {
            PropertyInfo[] properties = GetType().GetProperties();
            foreach (DataColumn column in row.Table.Columns)
            {
                for (int i = 0; i < properties.Length; i++)
                {
                    PropertyInfo property = properties[i];
                    if (property.Name.ToLower() == column.ColumnName.ToLower())
                    {
                        if (property.CanWrite)
                        {
                            property.SetValue(this, ValidTypeProperty(property, row), null);
                            break;
                        }
                    }
                }
            }
        }

        private object ValidTypeProperty(object value, string type)
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
                    return Convert.ToDateTime(value);

                case "System.Decimal":
                    return Convert.ToDecimal(value);

                case "System.Drawing.Bitmap":
                    return (System.Drawing.Bitmap)(value);

                case "DataTable":
                    return null;

                case "System.Char":
                    return Convert.ToChar(value);

                case "System.Boolean":
                    return Convert.ToBoolean(value);
                    
                case "System.Drawing.Image":
                    return (System.Drawing.Image)(value);
                default:
                    return null;
            }
        }

        private void ReadProperties(System.Xml.XmlNodeList xmlNodeList)
        {
            PropertyInfo[] properties = GetType().GetProperties();

            for (int i = 0; i < properties.Length; i++)
            {
                PropertyInfo property = properties[i];
                if (property.CanWrite)
                    foreach (System.Xml.XmlNode node in xmlNodeList)
                        if (node.Name == property.Name)
                            property.SetValue(this, ValidTypeProperty(node.InnerText, node.Attributes["type"].InnerText), null);
            }
        }

        private object ValidTypeProperty(PropertyInfo property, DataRow row)
        {

            //--System.Web.HttpContext.Current.Response.Write("ValidTypeProperty(PropertyInfo property, DataRow row) property = " + property.PropertyType.ToString() + "; value=" + Library.GetString(row, property.Name) + "; property.Name=" + property.Name + "<br>");
            switch (property.PropertyType.ToString())
            {
                case "System.Int32":
                    return Library.GetInt32(row, property.Name);
                case "System.String":
                    return Library.GetString(row, property.Name);

                case "System.Long":
                    return Library.GetLong(row, property.Name);

                case "System.Int64":
                    return Library.GetLong(row, property.Name);

                case "System.Double":
                    return Library.GetDouble(row, property.Name);

                case "System.DateTime":
                    return Library.GetDateTime(row, property.Name);

                case "System.Decimal":
                    return Library.GetDecimal(row, property.Name);

                case "System.Drawing.Bitmap":
                    return Library.GetBitmap(row, property.Name);

                case "System.Drawing.Image":
                    return Library.GetImage(row, property.Name);

                case "System.Byte[]":
                    return Library.GetByteArray(row, property.Name);

                case "DataTable":
                    return row.Table;

                case "System.Char":
                    return Library.GetChar(row, property.Name);

                case "System.Boolean":
                    return Library.GetBoolean(row, property.Name);
                default:
                    if (property.PropertyType.ToString().Contains("SGI.BusinessObject.Enums.")) return Library.GetInt32(row, property.Name);
                    return null;
            }
        }

        public string ToXmlSerialized()
        {
            return Library.SerializeObject(this, GetType());
        }

        public object ToXmlDeserialized(string xmlSerialized)
        {
            return Library.DeserializeObject(xmlSerialized, GetType());
        }
        #endregion Methods
    }
}