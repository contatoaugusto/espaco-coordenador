using System;

namespace EC.Common
{
	/// <summary>
	/// Summary description for IObject.
	/// </summary>
	public interface IObject
	{
		string ToXml();
		string ToString();
		string ToNameString();
		System.Data.DataTable ToDataTable();
		void SetProperties(System.Data.DataRow oRow);
		//void SetProperties(System.Data.DataSet dataSet);
		System.Data.DataRow ToDataRow();
		void ToObject(string xml);
		void ToObject(System.Data.DataSet dataSet);
		System.Data.DataSet ToDataSet();
		System.Data.DataTable DataSource {get;set;}
	}
}
