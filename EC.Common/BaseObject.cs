using System;

namespace EC.Common
{
	/// <summary>
	/// Summary description for BaseObject.
	/// </summary>
    public abstract class BaseObject
	{
		private System.Data.DataTable _dataSource = null;

        public virtual object Copy()
        {
            return MemberwiseClone();
        }

		public virtual string ToXml()
		{
			return Library.GetObjectXml(this);
		}

		public override string ToString()
		{
			return Library.GetObjectString(this);
		}

		public virtual string ToNameString()
		{
			return Library.GetObjectNameString(this);
		}

		public virtual System.Data.DataTable ToDataTable()
		{
			return Library.GetObjectDataTable(this);
		}

		public virtual System.Data.DataRow ToDataRow()
		{
			return Library.GetObjectDataRow(this);
		}
		public virtual void SetProperties(System.Data.DataRow oRow)
		{
			Library.ReadProperties(this, oRow);
		}
		public virtual void SetProperties(System.Data.DataSet dataSet)
		{
			Library.ReadProperties(this, dataSet.Tables[0].Rows[0]);
		}

		public virtual void SetProperties()
		{
			if(DataSource != null)
				Library.ReadProperties(this, DataSource.Rows[0]);
			else
				throw new Exception("Invalid reference of the DataTable.");
		}

		public virtual void ToObject(string xml)
		{
			System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
			xmlDocument.LoadXml(xml);
			Library.ReadProperties(this, xmlDocument.GetElementsByTagName("object").Item(0).ChildNodes);
		}

		public virtual void ToObject(System.Data.DataSet dataSet)
		{
			Library.ReadProperties(this, dataSet.Tables[0].Rows[0]);
		}

        public virtual void ToObject(System.Data.DataRow dataRow)
        {
            Library.ReadProperties(this, dataRow);
        }

		public virtual System.Data.DataSet ToDataSet()
		{
			System.Data.DataTable dataTable = Library.GeToObjectDataTable(this);
			System.Data.DataSet dataSet = new System.Data.DataSet(GetType().Name);
			dataSet.Tables.Add(dataTable);
			return dataSet;
		}

		public virtual System.Data.DataTable DataSource
		{
			get{return _dataSource;}
			set{_dataSource = value;}
		}

        public static implicit operator System.Data.DataTable(BaseObject baseObject)
        {
            return Library.GetObjectDataTable(baseObject);
        }
	}
}
