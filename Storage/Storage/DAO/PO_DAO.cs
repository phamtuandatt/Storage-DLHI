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
    internal class PO_DAO
    {
        public static SQLServerProvider data = new SQLServerProvider();

        public static DataTable GetPOs()
        {
            string sql = "EXEC GET_POs";

            return data.GetData(sql, "POs");
        }

        public static bool Add(PODto dto)
        {
            string sql = $"SET DATEFORMAT YMD INSERT INTO PO VALUES ('{dto.Id}', '{dto.Created}', '{dto.ExpectedDelivery}', {dto.Total}, " +
                $" '{dto.LocationWareHouse_Id}', '{dto.PaymentMethod_Id}')";

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

        public static DataTable dtPODetails = data.GetData("SELECT *FROM PO_DETAIL", "PODetails"); 

        public static DataTable GetPODetails()
        {
            return data.GetData("EXEC GET_PO_DETAIL", "PODetails");
        }

        public static DataTable GetPOExport()
        {
            return data.GetData("EXEC GET_PO_EXPORT", "ExportPO");
        }

        public static bool AddRange(List<PO_DetailDto> list)
        {
            foreach (PO_DetailDto item in list)
            {
                DataRow addPO = dtPODetails.NewRow();
                addPO[0] = item.PO_Id;
                addPO[1] = item.Item_Id;
                addPO[2] = item.MPR_No;
                addPO[3] = item.PO_No;
                addPO[4] = item.Price;
                addPO[5] = item.Quantity;

                dtPODetails.Rows.Add(addPO);
            }

            try
            {
                data.UpdateDatabase("SELECT *FROM PO_DETAIL", dtPODetails);
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
