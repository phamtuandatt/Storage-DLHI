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
    internal class Unit_DAO
    {
        public static SQLServerProvider data = new SQLServerProvider();

        public static DataTable GetUnits()
        {
            string sql = "SELECT *FROM UNIT";

            return data.GetData(sql, "cboUnit");
        }

        public static bool Add(UnitDto type)
        {
            string sql = $"INSERT INTO UNIT VALUES('{type.Id}', N'{type.Name}')";

            return data.Insert(sql) > 0;
        }

        public static bool Update(UnitDto type)
        {
            string sql = $"UPDATE UNIT SET NAME = N'{type.Name}' WHERE ID = '{type.Id}'";

            return data.Update(sql) > 0;
        }

        public static bool Delete(Guid id)
        {
            string sql = $"DELETE FROM UNIT WHERE ID = '{id}'";

            return data.Delete(sql) > 0;
        }
    }
}
