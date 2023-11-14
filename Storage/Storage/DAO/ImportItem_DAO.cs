using Storage.DataProvider;
using Storage.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Storage.DAO
{
    internal class ImportItem_DAO
    {
        public static SQLServerProvider data = new SQLServerProvider();

        public static DataTable GetImportItems()
        {
            return data.GetData("SELECT *FROM IMPORT_ITEM", "ImportItems");
        }

        public static bool Add(ImportItemDto ex)
        {
            string sql = $"SET DATEFORMAT YMD INSERT INTO IMPORT_ITEM " +
                $"VALUES ('{ex.Id}', '{ex.Created}', N'{ex.Bill_No}', " +
                  $"{ex.SumQuantity}, {ex.SumPrice}, {ex.Total})";

            return data.Insert(sql) > 0;
        }

        public static string GetCurrentBillNoInDate(string date)
        {
            string sql = $"EXEC GET_CURRENT_BILLNO '{date}'";
            DataTable dt = data.GetData(sql, "CurrentBillNo");
            DataRow datarow = dt.Rows[0];
            string numberStr = datarow["NUMBER"].ToString();
            if (string.IsNullOrEmpty(numberStr))
            {
                return "001";
            }
            try
            {
                var number = int.Parse(numberStr);
                if (number > 0 && number < 9)
                {
                    return "00" + (number + 1);
                }
                else if (number >= 9 && number < 99) 
                {
                    return "0" + (number + 1);
                }
                else
                {
                    return "" + (number + 1);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

    internal class ImportItemDetailDAO
    {
        public static SQLServerProvider data = new SQLServerProvider();

        public static DataTable dtImportItems = data.GetData("SELECT *FROM IMPORT_ITEM_DETAIL", "ImportItems");

        public static DataTable GetImportItemDetails()
        {
            return data.GetData("EXEC GET_IMPORT_ITEMS", "ImportDetails");
        }

        public static bool AddRange(List<ImportItemDetailDto> list)
        {
            foreach (ImportItemDetailDto item in list)
            {
                DataRow row = dtImportItems.NewRow();
                row[0] = item.ImportItemId;
                row[1] = item.ItemId;
                row[2] = item.Qty;
                row[3] = item.Price;
                row[4] = item.Total;

                dtImportItems.Rows.Add(row);
            }
            try
            {
                data.UpdateDatabase("SELECT *FROM IMPORT_ITEM_DETAIL", dtImportItems);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }
    }
}
