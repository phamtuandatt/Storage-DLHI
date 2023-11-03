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
    internal class MPR_Detail_DAO
    {
        public static SQLServerProvider data = new SQLServerProvider();

        public static bool Add(MPR_DetailDto dto)
        {
            string sql = $"INSERT INTO MPR_DETAIL VALUES ('{dto.MPR_Id}', '{dto.Item_Id}', N'{dto.MPR_No}', N'{dto.Usage}', {dto.Quantity})";

            return data.Insert(sql) > 0;
        }

        //public static bool Update(MPRDto dto)
        //{
        //    string sql = $"UPDATE MPR SET CREATED = '{dto.Created}', EXPECTED_DELIVERY = '{dto.ExpectDelivery}', NOTE = N'{dto.Note}', SUPPLIER_ID = '{dto.Supplier_Id}' WHERE ID = '{dto.Id}'";

        //    return data.Update(sql) > 0;
        //}

        //public static bool Delete(Guid mprId)
        //{
        //    string sql = $"DELETE FROM MPR WHERE ID = '{mprId}'";

        //    return data.Delete(sql) > 0;
        //}
    }
}
