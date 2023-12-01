using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Helper
{
    internal class API
    {
        public static DataTable ToDataTables<T>(List<T> items, DataTable dataTable)
        {
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public static DataTable ToDataTable<T>(T item, DataTable dataTable)
        {
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var values = new object[Props.Length];
            for (int i = 0; i < Props.Length; i++)
            {
                //inserting property values to datatable rows
                values[i] = Props[i].GetValue(item, null);
            }
            dataTable.Rows.Add(values);

            //put a breakpoint here and check datatable
            return dataTable;
        }

        public static DataTable ListToDataTable<T>(List<T> list, string _tableName)
        {
            DataTable dt = new DataTable(_tableName);

            foreach (PropertyInfo info in typeof(T).GetProperties())
            {
                dt.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }
            foreach (T t in list)
            {
                DataRow row = dt.NewRow();
                foreach (PropertyInfo info in typeof(T).GetProperties())
                {
                    row[info.Name] = info.GetValue(t, null) ?? DBNull.Value;
                }
                dt.Rows.Add(row);
            }
            return dt;
        }


        public static string API_ROUTER 
        {
            get 
            {
                return ConfigurationManager.AppSettings.Get("route-api");
            }  
        }

        // Item
        public const string GET_ITEMS = "/Items/get-item-v2";
        public const string GET_ITEM = "/Items/";
    }
}
