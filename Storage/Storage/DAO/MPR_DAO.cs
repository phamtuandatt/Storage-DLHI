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
using System.Windows.Forms;

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

        //------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------

        public static bool CreateMPR_Export(MPR_Export dto)
        {
            string sql = $"SET DATEFORMAT DMY INSERT INTO MPR_EXPORT VALUES ('{dto.Id}', '{dto.Created}', {dto.ItemCount}, {dto.Status})";

            return data.Insert(sql) > 0;
        }

        public static bool UpdateMPR_Export_Status()
        {
            string sql = "SELECT *FROM MPR_EXPORT WHERE STATUS = 2";
            DataTable dt = data.GetData(sql, "status_2");
            if (dt.Rows.Count == 0) return false;
            DataRow row = dt.Rows[0];

            string sql_Update = $"UPDATE MPR_EXPORT SET STATUS = 1 WHERE ID = '{Guid.Parse(row["ID"].ToString())}'";

            return data.Update(sql_Update) > 0;
        }

        public static bool CreateMPR_Export_Detail(MPR_Export_Detail dto)
        {
            string sql = $"INSERT INTO MPR_EXPORT_DETAIL VALUES ('{dto.MPR_Export_Id}', '{dto.MPR_Id}')";

            return data.Insert(sql) > 0;
        }

        public static bool InsertDetailExportIntoCurrentMPRExport(MPR_Export_Detail dto)
        {
            string sql = "SELECT *FROM MPR_EXPORT WHERE STATUS = 2";
            DataTable dt = data.GetData(sql, "status_2");
            DataRow row = dt.Rows[0];

            string sql_Insert = $"INSERT INTO MPR_EXPORT_DETAIL VALUES ('{Guid.Parse(row["ID"].ToString())}', '{dto.MPR_Id}')";

            return data.Insert(sql_Insert) > 0;
        }

        public static DataTable GetMPRExports()
        {
            string sql = "SELECT *FROM MPR_EXPORT";

            return data.GetData(sql, "MPRExports");
        }

        public static DataTable GetMPRExportDetail(Guid Id)
        {
            string sql = $"EXEC GET_MPR_EXPORT_DETAIL '{Id}'";

            return data.GetData(sql, "MPRExportDetail");
        }

        public static DataTable GetMPRExportDetails()
        {
            string sql = "EXEC GET_MPR_EXPORT_DETAILS";

            return data.GetData(sql, "MPRExportDetails");
        }
    }
}
