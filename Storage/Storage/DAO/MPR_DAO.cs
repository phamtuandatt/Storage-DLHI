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
using System.CodeDom;

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

            var a = JsonConvert.SerializeObject(entity);

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

        public static async Task<bool> CreateMPR_Export_Detail(MPR_Export_Detail dto)
        {
            var entity = new MPRExportDetailRequestDto()
            {
                MprExportId = dto.MPR_Export_Id,
                MprId = dto.MPR_Id,
                Sl = Guid.NewGuid(),
                SlV2 = "",
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(entity),
                                Encoding.UTF8, "application/json");
            using (HttpClient httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync($"{API.API_DOMAIN}{API.POST_MPR_DETAIL}", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    return false;
                }
            }
        }

        public static async Task<bool> InsertDetailExportIntoCurrentMPRExport(MPR_Export_Detail dto)
        {
            //string sql = "SELECT *FROM MPR_EXPORT WHERE STATUS = 2";
            //DataTable dt = data.GetData(sql, "status_2");
            //if (dt.Rows.Count == 0) { return false; }
            //DataRow row = dt.Rows[0];

            //string sql_Insert = $"INSERT INTO MPR_EXPORT_DETAIL VALUES ('{Guid.Parse(row["ID"].ToString())}', '{dto.MPR_Id}')";

            //if (data.Insert(sql_Insert) > 0)
            //{
            //    return data.Update($"EXEC UPDATE_ITEM_COUNT_MPR_EXPORT '{Guid.Parse(row["ID"].ToString())}'") > 0;
            //}
            //return false;

            var entity = new MPRExportDetailRequestDto()
            {
                MprExportId = dto.MPR_Export_Id,
                MprId = dto.MPR_Id,
                Sl = Guid.NewGuid(),
                SlV2 = "",
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(entity),
                                Encoding.UTF8, "application/json");
            var a = JsonConvert.SerializeObject(entity);
            using (HttpClient httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync($"{API.API_DOMAIN}{API.POST_MPR_EXPORT_DETAIL}", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    return false;
                }
            }
        }

        public static async Task<DataTable> GetMPRExports()
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{API.API_DOMAIN}{API.GET_MPR_Export}";
                string json = await client.GetStringAsync(url);
                var res = JsonConvert.DeserializeObject<List<MPR_Export>>(json).ToList();

                return API.ListToDataTable(res, "MPRss");
            }
        }

        public static DataTable GetMPRExportDetail(Guid Id)
        {
            string sql = $"EXEC GET_MPR_EXPORT_DETAIL '{Id}'";

            return data.GetData(sql, "MPRExportDetail");
        }

        public static async Task<DataTable> GetMPRExportDetails()
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{API.API_DOMAIN}{API.GET_MPR_Export_Detail}";
                string json = await client.GetStringAsync(url);
                var res = JsonConvert.DeserializeObject<List<MRPExportDetailResponseDto>>(json).ToList();

                return API.ListToDataTable(res, "MPRExportDetails");
            }
        }

        public static async Task<DataTable> GetMPRExportExcel()
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{API.API_DOMAIN}{API.GET_MPR_Export_Excel}";
                string json = await client.GetStringAsync(url);
                var res = JsonConvert.DeserializeObject<List<MRPExportExcelResponseDto>>(json).ToList();

                return API.ListToDataTable(res, "MPRExportDetails");
            }
        }

        public static async Task<bool> UpdateStatusExportExcel(Guid id)
        {
            StringContent content = new StringContent("", Encoding.UTF8, "application/json");
            using (HttpClient httpClient = new HttpClient())
            {
                using (var response = await httpClient.PutAsync($"{API.API_DOMAIN}{API.PUT_MPR_Export}{id}", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    return false;
                }
            }
        }
    }
}
