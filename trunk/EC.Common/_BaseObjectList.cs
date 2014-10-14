using System.ComponentModel;
using System.Data;
using System.Reflection;
using System;
using EC.Common;
using System.Collections.Generic;

namespace EC.Common
{
    [Serializable()]
    public class _BaseObjectList<T> : List<T>
    {
        public DataTable ToDataTable()
        {
            DataTable data = new DataTable();
            if (Count > 0)
            {
                data.TableName = this[0].GetType().Name;
                CreateDataTableSchema(this[0], data);
            }

            foreach (object o in this)
                data.Rows.Add(GetItemArray(o));

            return data;
        }

        public DataTable ToDataTable(string filterExpression)
        {
            return Library.FilterDataTable(this, filterExpression);
        }

        public DataTable ToDataTable(string filterExpression, string sortExpression)
        {
            return Library.SortDataTable(Library.FilterDataTable(this, filterExpression), sortExpression);
        }

        public DataTable Sort(string sortExpression)
        {
            return Library.SortDataTable(this, sortExpression);
        }

        public DataTable Filter(string filterExpression)
        {
            return Library.FilterDataTable(this, filterExpression);
        }

        #region Operators
        public static implicit operator System.Data.DataTable(_BaseObjectList<T> list)
        {
            DataTable data = new DataTable();
            if (list.Count > 0)
            {
                data.TableName = list[0].GetType().Name;
                CreateDataTableSchema(list[0], data);
            }

            foreach (T o in list)
                data.Rows.Add(GetItemArray(o));

            return data;
        }
        #endregion Operators

        #region Private

        private static void CreateDataTableSchema(object o, DataTable data)
        {
            PropertyInfo[] properties = o.GetType().GetProperties();

            for (int i = 0; i < properties.Length; i++)
            {
                PropertyInfo property = properties[i];
                if (!data.Columns.Contains(property.Name))
                {
                    DataColumn col = new DataColumn(property.Name);
                    col.DataType = property.PropertyType;
                    data.Columns.Add(col);
                }
            }
        }

        private static object[] GetItemArray(object o)
        {
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
            return data.NewRow().ItemArray;
        }
        #endregion Private
    }
}