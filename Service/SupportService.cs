using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Reflection;
namespace ServiceProvider.Service
{
    public class SupportService
    {   

        public DataTable convertProperDataTable(DataTable dt)
        {
            DataTable proptable = new DataTable();
            foreach (DataColumn column in dt.Columns)
            {
                proptable.Columns.Add(column.ColumnName, typeof(string));
            }

           
            int columnCount = dt.Columns.Count;

           
            for(int row=0;row<dt.Rows.Count;row++)
            {
                object[] array = new object[columnCount];
                for (int col = 0; col < dt.Columns.Count; col++)
                {
                    array[col] = dt.Rows[row][col].ToString();
                }
                proptable.Rows.Add(array);
            } 
            return proptable;
        }
        public List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

         
        public T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
    }
}