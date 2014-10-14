using System;
using System.Data;
using System.Reflection;
using UniCEUB.Core.Log;

namespace EC.Common
{
    [Serializable()]
    public abstract class BaseList<T>
    {

        #region Variables
        private DataTable _dataTable;
        private int _row = 0;
        #endregion Variables

        public int Count
        {
            get { return _dataTable.Rows.Count; }
        }

        public string Name
        {
            get { return GetType().Name; }
        }

        public DataTable DataSource
        {
            get { return _dataTable; }
        }

        public object GetColumn(int index)
        {
            return Row(RowIndex)[index];
        }

        #region Methods
        public void SetDataSource(DataTable dataTable)
        {
            _dataTable = dataTable;
        }

        public void SetDataSource(DataSet dataSet)
        {
            _dataTable = dataSet.Tables[0];
        }

        public bool HasNext()
        {
            try
            {
                return _row < _dataTable.Rows.Count;
            }
            catch
            {
                return false;
            }
        }

        public DataTable ToDataTable()
        {
            if (_dataTable == null)
            {
                throw new Exception("DataTable not reference.");
            }
            return _dataTable;
        }

        public DataTable ToDataTable(string sort)
        {
            Sort(sort);
            return _dataTable;
        }

        public DataTable ToDataTable(string filter, string sort)
        {
            DataTable dataTable = Library.FilterDataTable(_dataTable, filter);
            return Library.SortDataTable(dataTable, sort);
        }

        public void Filter(string filter)
        {
            DataTable dataTable = Library.FilterDataTable(_dataTable, filter);
            SetDataSource(dataTable);
        }

        protected IBaseList<T> Filter(IBaseList<T> list, DataTable dataTable, string filter)
        {
            DataTable dataTableFilter = Library.FilterDataTable(dataTable, filter);
            list.SetDataSource(dataTableFilter);
            return list;
        }

        public DataView ToDataView()
        {
            return _dataTable.DefaultView;
        }
        public DataRow Row(int Index)
        {
            return _dataTable.Rows[Index];
        }

        public DataRow GetNextRow()
        {
            _row++;
            
            if (_row > _dataTable.Rows.Count)
                return null;
            
            return _dataTable.Rows[_row - 1];
        }

        public DataRow GetNextRow(bool next)
        {
            if (next)
            {
                _row++;

                if (_row > _dataTable.Rows.Count)
                    return null;

                return _dataTable.Rows[_row - 1];
            }

            if (_row + 1 > _dataTable.Rows.Count)
                return null;

            return _dataTable.Rows[_row];

        }

        public int CountRegister
        {
            get { return Library.ToInteger(_dataTable.Rows[0]["NumRegister"]); }
        }

        public void Add()
        {
            _row++;
        }

        public int RowIndex
        {
            get { return _row; }
            set
            {
                _row = value;
                if (_row < 0)
                    throw new Exception("Invalid row index < 0");

                if (_row > (_dataTable.Rows.Count - 1))
                    throw new Exception("Invalid row index > " + _dataTable.Rows.Count);
            }
        }

        /// <summary>
        /// Inicializa o index para 0.
        /// </summary>
        public void Reset()
        {
            _row = 0;
        }

        /// <summary>
        /// Move o index para o primeiro registro.
        /// </summary>
        public void MoveFirst()
        {
            _row = 0;
        }

        public void MoveNext()
        {
            _row++;
            if (_row > (_dataTable.Rows.Count - 1))
                _row = (_dataTable.Rows.Count - 1);
        }
        public void MovePrevious()
        {
            _row--;
            if (_row < 0)
                _row = 0;
        }
        public void MoveLast()
        {
            _row = (_dataTable.Rows.Count - 1);
        }
        public void Sort(string Sort)
        {
            _dataTable = Library.SortDataTable(_dataTable, Sort);
        }
        public string ToXml(bool encrypted)
        {
            if (encrypted)
                return Cryptography.Encryption(ToXml(), Const.KEY);
            else
                return ToXml();
        }
        public string ToXml()
        {
            string sAux = string.Empty;

            sAux += "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\n";
            sAux += "<objectlist name=\"" + GetType().Name + "\">\n";

            if (_dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < _dataTable.Rows.Count; i++)
                {
                    DataRow row = _dataTable.Rows[i];
                    sAux += "  <object>\n";

                    for (int j = 0; j < _dataTable.Columns.Count; j++)
                    {
                        DataColumn oColumn = _dataTable.Columns[j];
                        Type oType = oColumn.DataType;
                        if (oColumn.ColumnName == "Id" | oColumn.ColumnName == "NumRegister") { continue; };
                        sAux += "    <" + oColumn.ColumnName + " type=\"" + oType.Name + "\">" + row[j] + "</" + oColumn.ColumnName + ">\n";

                    }

                    sAux += "  </object>\n";
                }
            }

            sAux += "</objectlist>";
            return sAux;

        }

        protected T ReadProperties(T obj)
        {
            PropertyInfo[] properties = obj.GetType().GetProperties();
            DataRow row = GetNextRow();

            foreach (DataColumn column in row.Table.Columns)
            {
                for (int i = 0; i < properties.Length; i++)
                {
                    var property = properties[i];
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

            return obj;
        }

        /// <summary>
        /// Get a current object.
        /// </summary>
        /// <param name="obj"></param>
        public object Get(object obj)
        {
            PropertyInfo[] properties = obj.GetType().GetProperties();

            if (RowIndex > DataSource.Rows.Count)
            {
                return null;
            }
            else
            {
                DataRow row = GetDataRow(RowIndex);
                foreach (DataColumn column in row.Table.Columns)
                {
                    for (int i = 0; i < properties.Length; i++)
                    {
                        PropertyInfo property = properties[i];
                        if (property.Name.ToLower() == column.ColumnName.ToLower())
                        {
                            if (property.CanWrite)
                                property.SetValue(obj, Library.ValidTypeProperty(property, Row(RowIndex)), null);
                        }
                    }
                }
                return obj;
            }
        }

        public void Get(int index, T obj)
        {
            RowIndex = index;        
            ReadProperties(obj);
        }

        public void GetNext(T obj)
        {
            ReadProperties(obj);
        }

        public void Insert(T obj)
        {
            try
            {
                //if (_dataTable == null)
                //{
                //    GetObjectSchemaDataRow(ref _dataTable, obj);
                //}
                //else
                //{
                    GetObjectSchemaDataRow(ref _dataTable, obj);
                //}
            }
            catch
            {
            }
        }

        public void InsertColumn(string column, Type type)
        {
            try
            {
                if (_dataTable == null)
                    throw new Exception("Invalid reference of the DataTable.");
                else
                {
                    for (int i = 0; i < _dataTable.Columns.Count; i++)
                    {
                        if (_dataTable.Columns[i].ColumnName.ToLower() == column.ToLower())
                            throw new Exception("Column name is exists.");
                    }
                    _dataTable.Columns.Add(column, type);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        public void UpdateColumn(string column, object value)
        {
            try
            {
                if (_dataTable == null)
                    throw new Exception("Invalid reference of the DataTable.");
                
                    for (int i = 0; i < _dataTable.Rows.Count; i++)
                    {
                        _dataTable.Rows[i][column] = value;
                    }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        public DataTable NewDataTable(T obj)
        {
            try
            {
                PropertyInfo[] properties = obj.GetType().GetProperties();
                PropertyInfo property;
                int i;
                DataRow row;

                DataTable dataTable = new DataTable(obj.GetType().Name + "List");
                DataColumn oCol;

                for (i = 0; i < properties.Length; i++)
                {
                    property = properties[i];
                    if (property.Name.ToLower() != "parent")
                    {
                        oCol = new DataColumn(property.Name);
                        oCol.DataType = property.PropertyType;
                        oCol.DefaultValue = property.GetValue(obj, null);
                        dataTable.Columns.Add(oCol);
                    }

                    row = dataTable.NewRow();
                    dataTable.Rows.Add(row);
                }
                dataTable.Rows.Clear();
                _dataTable = dataTable;
                return dataTable;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw new Exception(e.Message);
            }
        }

        private DataRow GetObjectSchemaDataRow(ref DataTable dataTable, T obj)
        {
            try
            {
                PropertyInfo[] properties = obj.GetType().GetProperties();
                PropertyInfo property;
                int i;
                DataRow row;

                if (dataTable == null)
                {
                    dataTable = new DataTable(obj.GetType().Name + "List");
                    DataColumn oCol;

                    // Add Column Id in Table
                    oCol = new DataColumn("Id");
                    oCol.DataType = Type.GetType("System.Int32");
                    oCol.AutoIncrement = true;
                    oCol.AutoIncrementStep = 1;
                    dataTable.Columns.Add(oCol);

                    for (i = 0; i < properties.Length; i++)
                    {
                        property = properties[i];
                        if (property.Name.ToLower() != "parent")
                        {
                            oCol = new DataColumn(property.Name);
                            
                            if (property.PropertyType != typeof(System.Drawing.Image))
                            {
                                oCol.DefaultValue = property.GetValue(obj, null);
                                oCol.DataType = property.PropertyType;
                            }
                            else
                            {
                                System.Drawing.Image img = (System.Drawing.Image)property.GetValue(obj, null);
                                oCol.DataType = typeof(byte[]);
                                oCol.DefaultValue = Library.ConvertImageToByte(img);
                            }
                            dataTable.Columns.Add(oCol);
                        }
                    }

                    // Add Column NumRegister in Table
                    oCol = new DataColumn("NumRegister");
                    oCol.DataType = Type.GetType("System.Int32");
                    oCol.DefaultValue = 0;
                    dataTable.Columns.Add(oCol);

                    row = dataTable.NewRow();
                    dataTable.Rows.Add(row);
                }
                else
                {
                    row = dataTable.NewRow();
                    for (i = 0; i < properties.Length; i++)
                    {
                        property = properties[i];
                        if (property.Name.ToLower() != "parent")
                        {
                            if (property.PropertyType == typeof(System.Drawing.Image))
                            {
                                System.Drawing.Image img = (System.Drawing.Image)property.GetValue(obj, null);
                                row[property.Name] = Library.ConvertImageToByte(img);
                            }
                            else
                                row[property.Name] = property.GetValue(obj, null);
                        }
                    }
                    dataTable.Rows.Add(row);
                }
                return dataTable.Rows[0];
            }
            catch(Exception e)
            {
                Logger.Error(e);
                return null;
            }
        }

        public DataSet ToDataSet()
        {
            var dataSet = new DataSet();
            dataSet.Tables.Add(_dataTable.Copy());
            return dataSet;
        }

        protected void ReadProperties(DataRow row)
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

        private object ValidTypeProperty(PropertyInfo property, DataRow row)
        {
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

                case "DataTable":
                    return row.Table;

                case "System.Char":
                    return Library.GetChar(row, property.Name);

                case "System.Boolean":
                    return Library.GetBoolean(row, property.Name);

                case "System.Drawing.Image":
                    return Library.GetImage(row, property.Name);

                case "System.Nullable`1[System.Guid]":
                    return Library.GetGuid(row, property.Name);
                default:
                    if (property.PropertyType.BaseType.ToString() == "System.Enum")
                        return Library.GetInt32(row, property.Name);
                    return null;
            }
        }

        public void RemoveRow(int index)
        {
            if (_dataTable == null)
                throw new Exception("Invalid reference of the DataTable.");
            else if (_dataTable.Rows.Count == 0)
                throw new Exception("Index was out of range. Must be non-negative and less than the size of the collection.");
            else
                _dataTable.Rows.RemoveAt(index);
        }

        public DataTable CopyDataTable()
        {
            return _dataTable.Copy();
        }

        private DataRow GetDataRow(int index)
        {
            return _dataTable.Rows[index];
        }

        #endregion Methods
    }
}
