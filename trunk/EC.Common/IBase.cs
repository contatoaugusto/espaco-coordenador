
using System.Data;

namespace EC.Common
{
    /// <summary>
    /// Summary description for IObject.
    /// </summary>
    public interface IBase
    {
        string ToXml();
        string ToXml(bool encrypt);
        string ToString();
        string ToNameString();
        DataTable ToDataTable();
        void SetProperties(DataRow oRow);
        void ToObject(string xml);
        void ToObject(string xml, bool decrypt);
    }
}
