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
using Newtonsoft.Json;
using Storage.Helper;
using Storage.Response;
using System.Net.Http;
using Storage.Response.MPRResponseDto;
using Storage.RequestDto.MPRRequestDto;

namespace Storage.DAO
{
    internal class MPR_DAO
    {
        public static SQLServerProvider data = new SQLServerProvider();

        public static async Task<DataTable> GetMPRs()
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{API.API_DOMAIN}{API.GET_MPRs}";
                string json = await client.GetStringAsync(url);
                var res = JsonConvert.DeserializeObject<List<MPRResponseDto>>(json).ToList();

                return API.ListToDataTable(res, "MPRs");
            }
        }

        public static async Task<bool> Add(MakeNewRequestDto dto)
        {
            var entity = new MPRRequestDto()
            {
                Id = dto.Id,
                Created = dto.Created,
                ExpectedDelivery = dto.ExpectDelivery,
                Note = dto.Note,
                ItemId = dto.Item_Id,
                MprNo = dto.MPR_No,
                Usage = dto.Usage,
                Quantity = dto.Quantity,
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(entity),
                    Encoding.UTF8, "application/json");

            using (HttpClient httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync($"{API.API_DOMAIN}{API.POST_MPRs}", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    return false;
                }
            }
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

        public static async Task<bool> CreateMPR_Export(MPR_Export dto)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(dto),
                    Encoding.UTF8, "application/json");

            using (HttpClient httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync($"{API.API_DOMAIN}{API.POST_MPR_Export}", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    return false;
                }
            }
        }

        public static async Task<bool> UpdateMPR_Export_Status()
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(new MPR_Export_Detail()),
                    Encoding.UTF8, "application/json");
            using (HttpClient httpClient = new HttpClient())
            {
                using (var response = await httpClient.PutAsync($"{API.API_DOMAIN}{API.UPDATE_MPR_EXPORT_STATUS}", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    return false;
                }
            }
            //string sql = "SELECT *FROM MPR_EXPORT WHERE STATUS = 2";
            //DataTable dt = data.GetData(sql, "status_2");
            //if (dt.Rows.Count == 0) return false;
            //DataRow row = dt.Rows[0];

            //string sql_Update = $"UPDATE MPR_EXPORT SET STATUS = 1 WHERE ID = '{Guid.Parse(row["ID"].ToString())}'";

            //return data.Update(sql_Update) > 0;
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
            if (dt.Rows.Count == 0) { return false; }
            DataRow row = dt.Rows[0];

            string sql_Insert = $"INSERT INTO MPR_EXPORT_DETAIL VALUES ('{Guid.Parse(row["ID"].ToString())}', '{dto.MPR_Id}')";

            if (data.Insert(sql_Insert) > 0)
            {
                return data.Update($"EXEC UPDATE_ITEM_COUNT_MPR_EXPORT '{Guid.Parse(row["ID"].ToString())}'") > 0;
            }
            return false;
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

        public static DataTable GetMPRExportExcel()
        {
            string sql = "EXEC GET_MPR_EXPORT_EXCEL";

            return data.GetData(sql, "Export Data");
        }

        public static bool UpdateStatusExportExcel(Guid id)
        {
            return data.Update($"UPDATE MPR_EXPORT SET STATUS = 0 WHERE ID = '{id}'") > 0;
        }
    }
}
