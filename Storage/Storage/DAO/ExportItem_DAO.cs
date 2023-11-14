using Storage.DataProvider;
using Storage.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.DAO
{
    internal class ExportItem_DAO
    {
        public static SQLServerProvider data = new SQLServerProvider();

        public static DataTable GetExportItems()
        {
            return data.GetData("SELECT *FROM EXPORT_ITEM", "ExportItems");
        }

        public static bool Add(ExportItemDto dto)
        {
            string sql = $"SET DATEFORMAT YMD INSERT INTO EXPORT_ITEM VALUES('{dto.Id}', '{dto.Created}', '{dto.Bill_No}', {dto.Sum_Quantity})";

            return data.Insert(sql) > 0;
        }

        public static string GetCurrentBillNoInDate(string date)
        {
            string sql = $"EXEC GET_CURRENT_BILLNO_EXPORT '{date}'";
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

    internal class ExportItemDetail_DAO
    {
        public static SQLServerProvider data = new SQLServerProvider();

        public static DataTable dtEmportItems = data.GetData("SELECT *FROM EXPORT_ITEM_DETAIL", "EmportItems");

        public static DataTable GetEmportItemDetails()
        {
            return data.GetData("EXEC GET_EMPORT_ITEMS", "EmportDetails");
        }

        public static bool AddRange(List<ExportItemDetail> list)
        {
            foreach (ExportItemDetail item in list)
            {
                DataRow row = dtEmportItems.NewRow();
                row[0] = item.ExportItemId;
                row[1] = item.ItemId;
                row[2] = item.Qty;
                row[3] = item.Note;

                dtEmportItems.Rows.Add(row);
            }
            try
            {
                data.UpdateDatabase("SELECT *FROM EXPORT_ITEM_DETAIL", dtEmportItems);
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
