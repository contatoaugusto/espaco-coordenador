using System.Data;

namespace EC.Common
{
    /// <summary>
    /// Summary description for IObjectList.
    /// </summary>
    public interface IBaseList<T>
    {
        void Reset();
        T GetNext();
        void GetNext(T obj);
        int Count { get;}
        void SetDataSource(DataTable dataTable);
        void SetDataSource(DataSet dataSet);
        string Name { get;}
        bool HasNext();
        DataTable ToDataTable();
        DataTable ToDataTable(string sort);
        DataTable ToDataTable(string filter, string sort);
        DataView ToDataView();
        DataRow Row(int Index);
        DataRow GetNextRow();
        int CountRegister { get;}
        void Add();
        int RowIndex { get;set;}
        void Get(int index, T obj);
        void MoveFirst();
        void MoveLast();
        void MoveNext();
        void Sort(string Sort);
        string ToXml();
        string ToXml(bool encrypted);
        void Insert(T obj);
        DataTable NewDataTable();
        DataSet ToDataSet();
    }
}
