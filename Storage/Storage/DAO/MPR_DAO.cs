using Storage.DataProvider;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Storage.DTOs;

namespace Storage.DAO
{
    internal class MPR_DAO
    {
        public static SQLServerProvider data = new SQLServerProvider();

        public static DataTable GetMPRs()
        {
            string sql = $"EXEC GET_MPR_LIST";

            return data.GetData(sql, "MPRs");
        }

        public static bool Add(MakeNewRequestDto dto)
        {
            string sql = $"SET DATEFORMAT DMY INSERT INTO MPR VALUES ('{dto.Id}', '{dto.Created}', '{dto.ExpectDelivery}', " +
                $"'{dto.Note}', '{dto.Item_Id}', '{dto.MPR_No}', '{dto.Usage}', {dto.Quantity})";

            return data.Insert(sql) > 0;
        }

        public static bool Update(MPRDto dto)
        {
            string sql = $"UPDATE MPR SET CREATED = '{dto.Created}', EXPECTED_DELIVERY = '{dto.ExpectDelivery}', NOTE = N'{dto.Note}', SUPPLIER_ID = '{dto.Supplier_Id}' WHERE ID = '{dto.Id}'";

            return data.Update(sql) > 0;
        }

        public static bool Delete(Guid mprId)
        {
            string sql = $"DELETE FROM MPR WHERE ID = '{mprId}'";

            return data.Delete(sql) > 0;
        }
    }
}
