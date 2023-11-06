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
    internal class LocationWareHouse_DAO
    {
        public static SQLServerProvider data = new SQLServerProvider();

        public static DataTable GetLocationWareHouses()
        {
            string sql = "SELECT *FROM LOCATION_WAREHOUSE";

            return data.GetData(sql, "cboLocationWareHouse");
        }

        public static bool Add(LocationWarehousseDto locationWarehousseDto)
        {
            string sql = $"INSERT INTO LOCATION_WAREHOUSE VALUES('{locationWarehousseDto.Id}', N'{locationWarehousseDto.Name}')";

            return data.Insert(sql) > 0;
        }

        public static bool Update(LocationWarehousseDto locationWhousseDto)
        {
            string sql = $"UPDATE LOCATION_WAREHOUSE SET NAME = N'{locationWhousseDto.Name}' WHERE ID = '{locationWhousseDto.Id}'";

            return data.Update(sql) > 0;
        }

        public static bool Delete(Guid locationId)
        {
            string sql = $"DELETE FROM LOCATION_WAREHOUSE WHERE ID = '{locationId}'";

            return data.Delete(sql) > 0;
        }
    }
}
