using Newtonsoft.Json;
using Storage.DataProvider;
using Storage.DTOs;
using Storage.Helper;
using Storage.Response;
using Storage.Response.ExportItemResponseDto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Storage.DAO
{
    internal class ExportItem_DAO
    {
        public static SQLServerProvider data = new SQLServerProvider();

        public static async Task<DataTable> GetExportItems()
        {
            //return data.GetData("SELECT *FROM EXPORT_ITEM", "ExportItems");
            using (HttpClient client = new HttpClient())
            {
                var url = $"{API.API_DOMAIN}{API.GET_EXPORT_ITEM}";
                string json = await client.GetStringAsync(url);
                var res = JsonConvert.DeserializeObject<List<ExportItemResponseDto>>(json).ToList();

                return API.ListToDataTable(res, "EXPORT_ITEM");
            }
        }

        public static async Task<bool> Add(ExportItemDto dto)
        {
            //string sql = $"SET DATEFORMAT YMD INSERT INTO EXPORT_ITEM VALUES('{dto.Id}', '{dto.Created}', '{dto.Bill_No}', {dto.Sum_Quantity})";

            //return data.Insert(sql) > 0;
            var a = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(JsonConvert.SerializeObject(dto),
                    Encoding.UTF8, "application/json");

            using (HttpClient httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync($"{API.API_DOMAIN}{API.POST_EXPORT_ITEM}", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    return false;
                }
            }
        }

        public static async Task<string> GetCurrentBillNoInDate(string date)
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{API.API_DOMAIN}{API.GET_CURRENT_BILL_NO_IN_DATE}{date}";
                string json = await client.GetStringAsync(url);

                return json;
            }
        }
    }

    internal class ExportItemDetail_DAO
    {
        public static SQLServerProvider data = new SQLServerProvider();

        public static DataTable dtEmportItems = GetData();

        public static DataTable GetData()
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{API.API_DOMAIN}{API.GET_EXPORT_ITEM_DETAIL}";
                string json = client.GetStringAsync(url).GetAwaiter().GetResult();
                var res = JsonConvert.DeserializeObject<List<ExportItemDetailResponseDto>>(json).ToList();

                return API.ListToDataTable(res, "EXPORT_ITEM_DETAILS");
            }
        }

        public static async Task<DataTable> GetEmportItemDetails()
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{API.API_DOMAIN}{API.GET_EXPORT_ITEM_DETAIL_FROM_PROC}";
                string json = await client.GetStringAsync(url);
                var res = JsonConvert.DeserializeObject<List<ExportItemDetailFromProcResponseDto>>(json).ToList();

                return API.ListToDataTable(res, "EXPORT_ITEM_DETAILS_PROC");
            }
        }

        public static async Task<bool> AddRange(List<ExportItemDetail> list)
        {
            foreach (ExportItemDetail item in list)
            {
                DataRow row = dtEmportItems.NewRow();
                row[0] = item.ExportItemId;
                row[1] = item.ItemId;
                row[2] = item.Qty;
                row[3] = item.Note;

                dtEmportItems.Rows.Add(row);
            }

            try
            {
                List<ExportItemDetail> data = new List<ExportItemDetail>();
                data = API.ConvertDataTable<ExportItemDetail>(dtEmportItems);

                StringContent content = new StringContent(JsonConvert.SerializeObject(data),
                                Encoding.UTF8, "application/json");

                var a = JsonConvert.SerializeObject(data);

                using (HttpClient httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsync($"{API.API_DOMAIN}{API.POST_EXPORT_ITEM_DETAIL}", content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            return true;
                        }
                        return false;
                    }
                }

            }
            catch (Exception ex)
            {
                return false;
                throw;
            }

            //try
            //{
            //    data.UpdateDatabase("SELECT *FROM EXPORT_ITEM_DETAIL", dtEmportItems);
            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    return false;
            //    throw;
            //}
        }
    }
}
