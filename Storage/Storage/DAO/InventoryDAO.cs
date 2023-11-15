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
    internal class InventoryDAO
    {
        public static SQLServerProvider data = new SQLServerProvider();

        public static DataTable GetImportExportByLastMonth(int month)
        {
            return data.GetData($"EXEC GET_IMPORT_EXPORT_ITEM_BY_LAST_MONTH {month}", "Inventories");
        }

        public static bool AddRange(List<InventoryDto> inventoryDtos)
        {
            DataTable dt = data.GetData("SELECT *FROM INVENTORY", "CurrentInventory");
            foreach (InventoryDto item in inventoryDtos)
            {
                DataRow datarow = dt.NewRow();
                datarow[0] = item.Id;
                datarow[1] = item.Item_Id;
                datarow[2] = item.Month;
                datarow[3] = item.Amount;

                dt.Rows.Add(datarow);
            }

            try
            {
                data.UpdateDatabase("SELECT *FROM INVENTORY", dt);
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
