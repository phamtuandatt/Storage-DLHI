using Storage.DataProvider;
using Storage.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.DAO
{
    internal class ExportItem_DAO
    {
        public static SQLServerProvider data = new SQLServerProvider();

        public static bool Add(ExportItemDto ex)
        {
            string sql = $"SET DATEFORMAT DMY INSERT INTO EXPORT_ITEM VALUES ('{ex.Id}', '{ex.Created}', N'{ex.Bill_No}', " +
                            $"{ex.Quantity}, {ex.Price}, {ex.Total}, '{ex.Supplier_Id}', '{ex.Item_Id}')";

            return data.Insert(sql) > 0;
        }

        public static bool Update(ExportItemDto ex)
        {
            string sql = $"UPDATE EXPORT_ITEM SET CREATED = '{ex.Created}', BILL_NO = N'{ex.Bill_No}', QUANTITY = {ex.Quantity}, " +
                        $"PRICE = {ex.Price}, TOTAL = {ex.Total}, SUPPLIER_ID = '{ex.Supplier_Id}', ITEM_ID = '{ex.Item_Id}' WHERE ID = '{ex.Id}'";

            return data.Update(sql) > 0;
        }

        public static bool Delete(Guid exportId)
        {
            string sql = $"DELETE FROM EXPORT_ITEM WHERE ID = '{exportId}'";

            return data.Delete(sql) > 0;
        }
    }
}
