

using System.ComponentModel;
using System.Data;
using System;
using System.Reflection;

using System.Drawing;
using System.IO;

namespace EC.Common
{
    [Serializable()]
    public abstract class _BaseObject : _IBaseObject, INotifyPropertyChanged, INotifyPropertyChanging
    {
        private event PropertyChangedEventHandler _changed;
        private event PropertyChangingEventHandler _changing;

        protected void ReadProperties(object sender, DataRow row)
        {
            foreach (DataColumn column in row.Table.Columns)
            {
                foreach (PropertyInfo property in sender.GetType().GetProperties())
                {
                    if (property.Name.ToLower() == column.ColumnName.ToLower())
                    {
                        if (property.CanWrite)
                        {
                            property.SetValue(sender, ValidateTypeProperty(property, row), null);
                            break;
                        }
                    }
                }
            }
        }

        protected void ReadProperties(object obj, System.Xml.XmlNodeList xmlNodeList)
        {
            foreach (PropertyInfo property in GetType().GetProperties())
            {
                if (property.CanWrite)
                    foreach (System.Xml.XmlNode node in xmlNodeList)
                        if (node.Name == property.Name)
                            property.SetValue(obj, ValidateTypeProperty(node.InnerText, node.Attributes["type"].InnerText), null);
            }
        }

        #region SetProperties
        public void SetProperties(System.Data.DataRow dataRow)
        {
            ReadProperties(this, dataRow);
        }

        public void SetProperties(System.Data.DataTable dataTable)
        {
            ReadProperties(this, dataTable.Rows[0]);
        }
        public void SetProperties(System.Data.DataSet dataSet)
        {
            ReadProperties(this, dataSet.Tables[0].Rows[0]);
        }
        #endregion SetProperties

        #region Convertion
        private short GetInt16(DataRow row, string columnName)
        {
            return row.IsNull(columnName) ? (short)0 : Convert.ToInt16(row[columnName]);
        }
        private int GetInt32(DataRow row, string columnName)
        {
            return row.IsNull(columnName) ? 0 : Convert.ToInt32(row[columnName]);
        }
        private long GetInt64(DataRow row, string columnName)
        {
            return row.IsNull(columnName) ? 0 : Convert.ToInt64(row[columnName]);
        }
        private string GetString(DataRow row, string columnName)
        {
            return row.IsNull(columnName) ? string.Empty : Convert.ToString(row[columnName]);
        }
        private DateTime GetDateTime(DataRow row, string columnName)
        {
            return row.IsNull(columnName) ? new DateTime(1, 1, 1) : Convert.ToDateTime(row[columnName]);
        }
        private char GetChar(DataRow row, string columnName)
        {
            return row.IsNull(columnName) ? new char() : Convert.ToChar(row[columnName]);
        }
        private bool GetBoolean(DataRow row, string columnName)
        {
            return row.IsNull(columnName) ? false : Library.ToBoolean(row[columnName]);
        }
        private Image GetImage(DataRow row, string columnName)
        {
            if (row.IsNull(columnName))
                return null;
            else
                return Library.ConvertByteToImage(row[columnName] as byte[]);
        }
        private Bitmap GetBitmap(DataRow row, string columnName)
        {
            if (row.IsNull(columnName))
                return null;
            else
                return (Bitmap)row[columnName];
        }
        private decimal GetDecimal(DataRow row, string columnName)
        {
            return row.IsNull(columnName) ? 0 : Convert.ToDecimal(row[columnName]);
        }
        private double GetDouble(DataRow row, string columnName)
        {
            return row.IsNull(columnName) ? 0 : Convert.ToDouble(row[columnName]);
        }
        private TimeSpan GetTimeSpan(DataRow row, string columnName)
        {
            return row.IsNull(columnName) ? TimeSpan.Zero : new TimeSpan(Convert.ToDateTime(row[columnName].ToString()).Hour, Convert.ToDateTime(row[columnName].ToString()).Minute, Convert.ToDateTime(row[columnName].ToString()).Second);
        }
        public static byte[] GetByteArray(DataRow oRow, string Field)
        {
            return oRow.IsNull(Field) ? new byte[0] : (Byte[])(oRow[Field]);
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
        #endregion Convertion

        #region IObject Members

        public static implicit operator DataTable(_BaseObject baseObject)
        {
            return GetObjectDataTable(baseObject);
        }

        public static implicit operator DataRow(_BaseObject baseObject)
        {
            return GetObjectDataTable(baseObject).Rows[0];
        }

        public DataTable ToDataTable()
        {
            return GetObjectDataTable(this);
        }


        public DataRow ToDataRow()
        {
            return GetObjectDataTable(this).Rows[0];
        }

        #region ToObject
        public virtual void ToObject(string xmlString)
        {
            System.Xml.XmlReader reader = System.Xml.XmlReader.Create(new StringReader(xmlString));

            DataTable data = new DataTable();
            data.ReadXml(reader);

            ReadProperties(this, data.Rows[0]);
        }

        public virtual void ToObject(System.Data.DataRow dataRow)
        {
            ReadProperties(this, dataRow);
        }

        public virtual void ToObject(System.Data.DataTable dataTable)
        {
            ReadProperties(this, dataTable.Rows[0]);
        }

        public virtual void ToObject(System.Data.DataSet dataSet)
        {
            ReadProperties(this, dataSet.Tables[0].Rows[0]);
        }
        #endregion ToObject

        public static DataTable GetObjectDataTable(object o)
        {
            if (o == null)
                throw new NullReferenceException();

            DataTable data = new DataTable(o.GetType().Name);

            PropertyInfo[] properties = o.GetType().GetProperties();

            for (int i = 0; i < properties.Length; i++)
            {
                PropertyInfo property = properties[i];
                if (!data.Columns.Contains(property.Name))
                {
                    DataColumn col = new DataColumn(property.Name);
                    col.DataType = property.PropertyType;
                    col.DefaultValue = property.GetValue(o, null);
                    data.Columns.Add(col);
                }
            }
            DataRow row = data.NewRow();
            data.Rows.Add(row);

            return data;
        }

        public string ToXml()
        {
            DataTable data = this.ToDataTable();
            System.IO.StringWriter writer = new System.IO.StringWriter();
            data.WriteXml(writer, XmlWriteMode.WriteSchema);
            return string.Format("<?xml version=\"1.0\" encoding=\"utf-8\" ?>\n{0}", writer.ToString());
        }

        public string ToNameString()
        {
            string aux = string.Empty;

            foreach (PropertyInfo property in this.GetType().GetProperties())
                aux += property.Name + "=" + property.GetValue(this, null) + "|";
            return aux;
        }

        public override string ToString()
        {
            string aux = string.Empty;

            foreach (PropertyInfo property in this.GetType().GetProperties())
                aux += property.GetValue(this, null) + "|";
            return aux;
        }

        #endregion

        #region Private Methods

        private string ValidateCDATA(PropertyInfo property)
        {
            bool include = false;

            switch (property.PropertyType.ToString())
            {
                case "System.Char":
                case "System.String":
                    include = true;
                    break;
            }

            if (include)
                return string.Format("<![CDATA[{0}]]>", property.GetValue(this, null).ToString());
            else
                return property.GetValue(this, null).ToString();
        }

        private object ValidateTypeProperty(PropertyInfo property, DataRow row)
        {
            switch (property.PropertyType.ToString())
            {
                case "System.Int16":
                    return GetInt16(row, property.Name);
                case "System.Int32":
                    return GetInt32(row, property.Name);
                case "System.DateTime":
                    return GetDateTime(row, property.Name);
                case "System.String":
                    return GetString(row, property.Name);
                case "System.Int64":
                    return GetInt64(row, property.Name);
                case "System.Double":
                    return GetDouble(row, property.Name);
                case "System.Decimal":
                    return GetDecimal(row, property.Name);
                case "System.TimeSpan":
                    return GetTimeSpan(row, property.Name);
                case "System.Byte[]":
                    return GetByteArray(row, property.Name);
                case "System.Drawing.Bitmap":
                    return GetBitmap(row, property.Name);
                case "DataTable":
                    return row.Table;
                case "System.Char":
                    return GetChar(row, property.Name);
                case "System.Boolean":
                    return GetBoolean(row, property.Name);
                case "System.Drawing.Image":
                    return GetImage(row, property.Name);
                case "System.Nullable`1[System.Guid]":
                    return GetGuid(row, property.Name);
                case "System.Nullable`1[System.Int16]":
                    return GetInt16(row, property.Name);
                case "System.Nullable`1[System.Int32]":
                    return GetInt32(row, property.Name);
                case "System.Nullable`1[System.DateTime]":
                    return GetDateTime(row, property.Name);
                case "System.Nullable`1[System.Int64]":
                    return GetInt64(row, property.Name);
                case "System.Nullable`1[System.Double]":
                    return GetDouble(row, property.Name);
                case "System.Nullable`1[System.Decimal]":
                    return GetDecimal(row, property.Name);
                default:
                    return null;
            }
        }

        private object ValidateTypeProperty(object value, string type)
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
                        return DateTime.Parse(value.ToString(), new System.Globalization.CultureInfo("pt-BR", false).DateTimeFormat);
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

        #endregion Private Methods

        public void Reset()
        {
            // Force clients to re-read thier data
            OnPropertyChanged(null);
        }

        #region INotifyPropertyChanged Members
        protected void OnPropertyChanged(string prop)
        {
            if (null != _changed)
            {
                _changed(this, new PropertyChangedEventArgs(prop));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged
        {
            add { _changed += new PropertyChangedEventHandler(value); }
            remove { _changed -= new PropertyChangedEventHandler(value); }
        }
        #endregion

        #region INotifyPropertyChanging Members

        protected void OnPropertyChanging(string prop)
        {
            if (null != _changing)
            {
                _changing(this, new PropertyChangingEventArgs(prop));
            }
        }

        public event PropertyChangingEventHandler PropertyChanging
        {
            add { _changing += new PropertyChangingEventHandler(value); }
            remove { _changing -= new PropertyChangingEventHandler(value); }
        }
        #endregion
    }
}