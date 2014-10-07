using System;
using System.Data;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Xml;
using UniCEUB.Core.Log;

namespace EC.Common
{
	public class List
	{
	
		private DataTable _dataTable;
		private int _indexRow = 0;
		private string _name = string.Empty;

		public int Count ()
		{
			return _dataTable.Rows.Count;
		}

	    public int Index
	    {
	        get { return _indexRow; }
	    }

	    public void Reset()
		{
			_indexRow = 0;
		}

		public void SetResult(DataTable dataTable)
		{
			_dataTable = dataTable;
		}

	    public void SetResult(DataSet dataSet)
	    {
	        if (dataSet.Tables.Count > 0)
	            _dataTable = dataSet.Tables[0];
	    }

	    public string Name
	    {
	        get { return _name; }
	        set { _name = value; }
	    }

	    public bool HasNext()
	    {
	        if (_dataTable == null)
	            return false;

	        return _indexRow < _dataTable.Rows.Count;
	    }

	    public DataSet ToDataSet()
	    {
	        var dataSet = new DataSet();
	        dataSet.Tables.Add(_dataTable.Copy());
	        return dataSet;
	    }

	    public double SumColumn(string nameColumn)
	    {
	        return DataTable.Rows.Cast<DataRow>().Sum(row => Convert.ToDouble(Library.ToDouble(row[nameColumn])));
	    }

	    public DataTable ToDataTable()
	    {
	        if (_dataTable == null)
	            throw new NullReferenceException("DataTable");

	        return _dataTable;
	    }


	    public DataTable CopyDataTable()
	    {
	        if (_dataTable == null)
	            throw new NullReferenceException("DataTable");

	        return _dataTable.Copy();

	    }

	    public DataTable ToDataTable(string sort)
	    {

	        Sort(sort);
	        return _dataTable;
	    }

	    public DataTable ToDataTable(string filter, string sort)
		{
			var dataTable = Library.FilterDataTable(_dataTable, filter);
			return Library.SortDataTable(dataTable, sort);
		}

		public DataView ToDataView() 
		{
			return _dataTable.DefaultView;
		}
		public DataRow Row(int index)
		{
			return _dataTable.Rows[index];
		}

		public DataRowCollection Rows
		{
			get{return _dataTable.Rows;}
		}

		public DataTable DataTable
		{
			get{return _dataTable;}
		}

		public DataRow GetNextRow()
		{
			_indexRow++;
			if (_indexRow > _dataTable.Rows.Count)
			{
				return null;
			}
			return _dataTable.Rows[_indexRow - 1];
		}

		public int CountRegister()
		{
			try
			{
				return (int)_dataTable.Rows[0]["NumRegister"];
			}
			catch
			{
				return 0;
			}
		}

		public void Add()
		{
			_indexRow++;
		}

		public int RowIndex()
		{
			return _indexRow;
		}

		public void MoveFirst ()
		{
			_indexRow = 0;
		}

		public void MoveLast ()
		{
			_indexRow = _dataTable.Rows.Count -1;
		}
		public void MovePrevious ()
		{
			if(_indexRow > 0)
				_indexRow--;
		}
		public void Sort(string sort) 
		{
            _dataTable = Library.SortDataTable(_dataTable, sort);
		}

		public void Filter(string filter) 
		{
			DataTable dataTable = Library.FilterDataTable(_dataTable, filter);
			SetResult(dataTable);
		}

		protected IObjectList Filter(IObjectList objLst, DataTable dataTable, string filter) 
		{
            var myTable = Library.FilterDataTable(dataTable, filter);
			objLst.SetResult( myTable );
			return objLst;
		}

		public void ToObject(string xml)
		{
			var doc = new XmlDocument();
			doc.LoadXml(xml);

			var nodeList = doc.GetElementsByTagName("objectlist");
			string nameDataTable = nodeList.Item(0).Attributes[0].Value;
			var dt = new DataTable(nameDataTable);
			
			foreach(XmlNode n in nodeList.Item(0).ChildNodes[0].ChildNodes)
			{
				var col = new DataColumn(n.Name);

				if(ValidType(n.Attributes[0].Value.ToLower()))
				{
					col.DataType = GetType(n.Attributes[0].Value.ToLower());
					col.DefaultValue = System.DBNull.Value;
					dt.Columns.Add(col);
				}
			}

			for(int i = 0; i < nodeList.Item(0).ChildNodes.Count; i++)
			{
				var row = dt.NewRow();
				foreach(XmlNode n in nodeList.Item(0).ChildNodes[i].ChildNodes)
				{
					try
					{
						row[n.Name] = GetValue(n.InnerText, n.Attributes[0].Value.ToLower());
					}
					catch{}
					
				}
				dt.Rows.Add(row);
			}
			SetResult(dt);
		}

		private bool ValidType(string type)
		{
			switch (type)
			{
				case "int32" :
					return true;

				case "string" :
					return true;

				case "int64" :
					return true;

				case "double" :
					return true;

				case "datetime" :
					return true;

				case "decimal" :
					return true;

				case "char" :
					return true;
				default:
					return false;
			}
		}

		private System.Type GetType(string type)
		{
			switch (type)
			{
				case "int32" :
					return typeof(System.Int32);

				case "string" :
					return typeof(System.String);

				case "int64" :
					return typeof(System.Int64);

				case "double" :
					return typeof(System.Double);

				case "datetime" :
					return typeof(System.DateTime);

				case "decimal" :
					return typeof(System.Decimal);

				case "char" :
					return typeof(System.Char);
				default:
					return null;
			}
		}

		private object GetValue(object value, string type)
		{
			switch (type)
			{
				case "int32" :
					return Library.ToInteger(value);

				case "string" :
					return Library.ToString(value);

				case "int64" :
					return Library.ToLong(value);

				case "double" :
					return Library.ToDouble(value);

				case "datetime" :
					return Library.ToDate(value);

				case "decimal" :
					return Library.ToDecimal(value);

				case "char" :
					return Library.ToChar(value);
				default:
					return null;
			}
		}

		public string ToXml()
		{
			string sAux = string.Empty;

			sAux += "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\n";
			sAux += "<objectlist name=\"" + GetType().Name + "\">\n";

			for (int i = 0; i < _dataTable.Rows.Count; i++)
			{
				DataRow oRow = _dataTable.Rows[i];
				sAux += "  <object>\n";

				for (int j = 0; j < _dataTable.Columns.Count; j++)
				{
					DataColumn oColumn = _dataTable.Columns[j];
					System.Type oType = oColumn.DataType;
					if (oColumn.ColumnName == "Id" | oColumn.ColumnName == "NumRegister" ){continue;};
					sAux += "    <" + oColumn.ColumnName + " type=\"" + oType.Name + "\">" + oRow[j] + "</" + oColumn.ColumnName + ">\n";

				}

				sAux += "  </object>\n";
			}

			sAux += "</objectlist>";
			return sAux;
			
		}

	    /// <summary>
	    /// It verifies if it exists a list to be chore.
	    /// </summary>
	    /// <returns>bool</returns>
	    public bool Read()
	    {
	        if (_dataTable == null)
	            return false;

	        if (_dataTable.Rows.Count == 0)
	            return false;

	        return true;
	    }

	    /// <summary>
	    /// Get a current object.
	    /// </summary>
	    /// <param name="obj"></param>
	    public object Get(object obj)
	    {
	        PropertyInfo[] properties = obj.GetType().GetProperties();

	        if (_indexRow > DataTable.Rows.Count)
	            return null;
            
	        var row = GetDataRow(_indexRow);

	        foreach (var property in from DataColumn column in row.Table.Columns
	            from property in properties
	            where property.Name.ToLower() == column.ColumnName.ToLower()
	            where property.CanWrite
	            select property)
	        {
	            property.SetValue(obj, Library.ValidTypeProperty(property, Row(_indexRow)), null);
	        }
	        return obj;
	    }

	    private DataRow GetDataRow(int index)
	    {
            return _dataTable.Rows[index];
	    }

	    protected object ReadProperties(object obj)
		{
            PropertyInfo[] properties = obj.GetType().GetProperties();
            DataRow row = GetNextRow();
            foreach (DataColumn column in row.Table.Columns)
            {
                for (int i = 0; i < properties.Length; i++)
                {
                    PropertyInfo property = properties[i];
                    if (property.Name.ToLower() == column.ColumnName.ToLower())
                    {
                        if (property.CanWrite)
                        {
                            property.SetValue(obj, Library.ValidTypeProperty(property, row), null);
                            break;
                        }
                    }
                }
            }
			return obj;
		}

		public void GetNext(object obj)
		{	
			ReadProperties(obj);
		}

		public void Insert(object obj)
		{
			GetObjectSchemaDataRow(ref _dataTable, obj);
		}

	    public void InsertColumn(string column, Type type)
	    {
	        for (int i = 0; i < _dataTable.Columns.Count; i++)
	        {
	            if (_dataTable.Columns[i].ColumnName.ToLower() == column.ToLower())
                    throw new Exception(string.Format("A coluna já \"{0}\" existe.", column));
	        }
	        _dataTable.Columns.Add(column, type);
	    }

	    public void UpdateColumn(string column, object value)
	    {
	        for (int i = 0; i < _dataTable.Rows.Count; i++)
	        {
	            _dataTable.Rows[i][column] = value;
	        }
	    }

	    private DataRow GetObjectSchemaDataRow(ref DataTable dataTable, object obj)
		{
			try
			{
				System.Reflection.PropertyInfo[] aProperties = obj.GetType().GetProperties();
				PropertyInfo property;
				int i;
				DataRow oRow;

				if (dataTable == null)
				{
					dataTable = new DataTable(obj.GetType().Name + "List");
					DataColumn oCol;

					// Add Column Id in Table
					oCol = new DataColumn("Id");
					oCol.DataType = System.Type.GetType("System.Int32");
					oCol.AutoIncrement = true;
					oCol.AutoIncrementStep = 1;
					dataTable.Columns.Add(oCol);

					for (i = 0; i<aProperties.Length; i++)
					{
						property = aProperties[i];
						if (property.Name.ToLower() != "parent")
						{
							oCol = new DataColumn(property.Name);
							oCol.DataType = property.PropertyType;
							oCol.DefaultValue = property.GetValue(obj, null);
							dataTable.Columns.Add(oCol);
						}
					}

					// Add Column NumRegister in Table
					oCol = new DataColumn("NumRegister");
					oCol.DataType = System.Type.GetType("System.Int32");
					oCol.DefaultValue = 0;
					dataTable.Columns.Add(oCol);

					oRow = dataTable.NewRow();
					dataTable.Rows.Add(oRow);
				}
				else
				{
					oRow = dataTable.NewRow();
                    foreach (DataColumn column in oRow.Table.Columns)
                    {
                        for (i = 0; i < aProperties.Length; i++)
                        {
                            property = aProperties[i];
                            if (property.Name.ToLower() == column.ColumnName.ToLower())
                            {
                                if (property.Name.ToLower() != "parent" & property.Name.IndexOf("List") == -1)
                                    if (property.Name.ToLower() != "datasource")
                                        oRow[property.Name] = property.GetValue(obj, null);
                            }
                        }
                    }
				    dataTable.Rows.Add(oRow);
				}
				return dataTable.Rows[0];
			}
			catch(Exception e)
			{
                Logger.Error(e);
				throw new Exception(e.Message);
			}
		}

        public void RemoveRow(int index)
        {
            if(_dataTable == null)
                throw new Exception("Invalid reference of the DataTable.");
            
            if(_dataTable.Rows.Count == 0)
                throw new Exception("Index was out of range. Must be non-negative and less than the size of the collection.");
            
             _dataTable.Rows.RemoveAt(index);
        }
	}
}
