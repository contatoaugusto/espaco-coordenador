

using System.Data;
namespace EC.Common
{
    public interface _IBaseObject
    {
        DataTable ToDataTable();
        DataRow ToDataRow();
        string ToNameString();
        string ToString();
        string ToXml();
        void ToObject(string xml);
        void ToObject(System.Data.DataRow dataRow);
        void ToObject(System.Data.DataTable dataTable);
        void ToObject(System.Data.DataSet dataSet);
        void SetProperties(System.Data.DataRow dataRow);
        void SetProperties(System.Data.DataTable dataTable);
        void SetProperties(System.Data.DataSet dataSet);
    }
}
