using System;

namespace EC.Common
{
	/// <summary>
	/// Summary description for IObjectList.
	/// </summary>
	public interface IObjectList
	{
		bool Read();
		void Reset();
		object Get(object obj);
		object GetNext();
		void GetNext(object obj);
		int Count ();
		void SetResult(System.Data.DataTable dataTable);
		string Name{get;set;}
		System.Data.DataRowCollection Rows{get;}
		bool HasNext();
		System.Data.DataTable ToDataTable();
		System.Data.DataTable ToDataTable(string sort);
		System.Data.DataTable ToDataTable(string filter, string sort);
		System.Data.DataView ToDataView();
		System.Data.DataRow Row(int index);
		System.Data.DataRow GetNextRow();
		int CountRegister();
		void Add();
		int RowIndex();
		void MoveFirst ();
		void MoveLast();
		void MovePrevious();
		void Sort(string sort);
		string ToXml();
		void Insert(object obj);
		void NewDataTable();
		void Filter(string filter);
		System.Data.DataSet ToDataSet();
		double SumColumn(string nameColumn);
		void ToObject(string xml);
	}
}
