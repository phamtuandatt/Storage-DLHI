using Storage.DataProvider;
using Storage.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.DAO
{
    internal class PO_DAO
    {
        public static SQLServerProvider data = new SQLServerProvider();

        public static bool Add(PODto dto)
        {
            string sql = $"SET DATEFORMAT DMY INSERT INTO PO VALUES ('{dto.Id}', '{dto.Created}', '{dto.ExpectedDelivery}', {dto.Total}, " +
                $"'{dto.Supplier_Id}', '{dto.LocationWareHouse_Id}', '{dto.PaymentMethod_Id}')";

            return data.Insert(sql) > 0;
        }

        public static bool Update(PODto dto)
        {
            string sql = $"UPDATE PO SET CREATED = '{dto.Created}', EXPECTED_DELIVERY = '{dto.ExpectedDelivery}', TOTAL = {dto.Total}, SUPPLIER_ID = '{dto.Supplier_Id}', " +
                            $"LOCATION_WAREHOUSE_ID = '{dto.LocationWareHouse_Id}', PAYMENT_METHOD_ID = '{dto.PaymentMethod_Id}' WHERE ID = '{dto.Id}'";

            return data.Update(sql) > 0;
        }

        public static bool Delete(Guid id)
        {
            string sql = $"DELETE FROM PO WHERE ID = '{id}'";

            return data.Delete(sql) > 0;
        }
    }

    internal class PO_Detail_DAO
    {
        public static SQLServerProvider data = new SQLServerProvider();

        public static bool Add(PO_DetailDto dto)
        {
            string sql = $"INSERT INTO PO_DETAIL VALUES ('{dto.PO_Id}', '{dto.Item_Id}', N'{dto.MPR_No}', N'{dto.PO_No}', {dto.Price}, {dto.Quantity})";

            return data.Insert(sql) > 0;
        }
    }
}
