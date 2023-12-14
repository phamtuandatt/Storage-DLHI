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

        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        public static T GetItem<T>(DataRow dr)
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


        public static string API_DOMAIN
        {
            get 
            {
                return ConfigurationManager.AppSettings.Get("route-api");
            }  
        }

        // Item
        public const string GET_ITEMS = "/Items/get-item-v2";
        public const string GET_ITEM = "/Items/";
        public const string GET_ITEM_BY_WAREHOUSE = "/Items/get-item-export-v2/";
        public const string GET_ITEM_CODE = "/Items/get-code/";
        public const string POST_ITEM = "/Items";
        public const string POST_ITEM_NO_IMAGE = "/Items/AddItemNoImage";
        public const string PUT_ITEM = "/Items/";
        public const string PUT_ITEM_NO_IMAGE = "/Items/UpdateItemNoImage/";
        public const string DELETE_ITEM = "/Items/";

        // Unit
        public const string GET_UNITS = "/Units";
        public const string POST_UNIT = "/Units";

        // Group
        public const string GET_GROUPS = "/Groups";
        public const string POST_GROUP = "/Groups";

        // Type
        public const string GET_TYPES = "/Types";
        public const string POST_TYPE = "/Types";

        // Warehouse
        public const string GET_WAREHOSE_COMBOXBOX = "/Warehouses";
        public const string GET_WAREHOUSE_INVENTORIES = "/Warehouses/GetInventories";
        public const string POST_WAREHOUSE = "/Warehouses";

        // Warehouse Detail
        public const string GET_WAREHOUSE_DETAIL = "/WarehouseDetails";
        public const string POST_WAREHOUSE_DETAIL = "/WarehouseDetails";

        // Export Item 
        public const string GET_EXPORT_ITEM = "/ExportItems";
        public const string POST_EXPORT_ITEM = "/ExportItems";
        public const string GET_CURRENT_BILL_NO_IN_DATE = "/ExportItems/get-current-bill-no-in-date/";

        // Export Item Detail
        public const string GET_EXPORT_ITEM_DETAIL = "/ExportItemDetails";
        public const string GET_EXPORT_ITEM_DETAIL_FROM_PROC = "/ExportItemDetails/get-export-item-details";
        public const string POST_EXPORT_ITEM_DETAIL = "/ExportItemDetails";

        // Supplier
        public const string GET_SUPPLIERS = "/Suppliers";
        public const string GET_SUPPLIER_BY_ID = "/Suppliers/";
        public const string GET_CURRENT_CODE_SUPPLIER = "/Suppliers/get-code-supplier/";
        public const string POST_SUPPLIERS = "/Suppliers";
        public const string PUT_SUPPLIERS = "/Suppliers/";

        // Supplier Type
        public const string GET_SUPPLIER_TYPES = "/SupplierTypes";
        public const string POST_SUPPLIER_TYPES = "/SupplierTypes";

        // PO
        public const string GET_POs = "/Poes";
        public const string POST_POs = "/Poes";

        // PO Detail
        public const string GET_PO_Detail = "/PoDetails";
        public const string GET_PO_Detail_By_Proc = "/PoDetails/get-po-detail-by-proc";
        public const string GET_PO_Export = "/PoDetails/get-po-export";
        public const string POST_PO_Detail = "/PoDetails";
    }
}
