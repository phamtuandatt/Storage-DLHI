using Newtonsoft.Json;
using Storage.DataProvider;
using Storage.DTOs;
using Storage.Helper;
using Storage.RequestDto.PORequestDto;
using Storage.Response;
using Storage.Response.PODetailResponseDto;
using Storage.Response.POResponseDto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Storage.DAO
{
    internal class PO_DAO
    {
        public static SQLServerProvider data = new SQLServerProvider();

        public static async Task<DataTable> GetPOs()
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{API.API_DOMAIN}{API.GET_POs}";
                string json = await client.GetStringAsync(url);
                var res = JsonConvert.DeserializeObject<List<POResponseDto>>(json).ToList();

                return API.ListToDataTable(res, "POs");
            }
        }

        public static async Task<bool> Add(PODto dto)
        {
            var entity = new PORequestDto()
            {
                Id = dto.Id,
                Created = dto.Created,
                ExpectedDelivery = dto.ExpectedDelivery,
                Total = dto.Total,
                LocationWarehouseId = dto.LocationWareHouse_Id,
                PaymentMethodId = dto.PaymentMethod_Id
            };
            var a = JsonConvert.SerializeObject(entity);
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity),
                                Encoding.UTF8, "application/json");

            using (HttpClient httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync($"{API.API_DOMAIN}{API.POST_POs}", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    return false;
                }
            }
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

        //public static DataTable dtPODetails = data.GetData("SELECT *FROM PO_DETAIL", "PODetails");

        public static DataTable dtPODetails = GetData();

        public static DataTable GetData()
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{API.API_DOMAIN}{API.GET_PO_Detail}";
                string json = client.GetStringAsync(url).GetAwaiter().GetResult();
                var res = JsonConvert.DeserializeObject<List<PODetailReponseDto>>(json).ToList();

                return API.ListToDataTable(res, "PODetailsV1");
            }
        }

        public static async Task<DataTable> GetPODetails()
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{API.API_DOMAIN}{API.GET_PO_Detail_By_Proc}";
                string json = await client.GetStringAsync(url);
                var res = JsonConvert.DeserializeObject<List<PODetailByProcResponseDto>>(json).ToList();

                return API.ListToDataTable(res, "PODetails");
            }
        }

        public static async Task<DataTable> GetPOExport()
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{API.API_DOMAIN}{API.GET_PO_Export}";
                string json = await client.GetStringAsync(url);
                var res = JsonConvert.DeserializeObject<List<GetPoExportResponseDto>>(json).ToList();

                return API.ListToDataTable(res, "ExportPO");
            }
        }

        public static async Task<bool> AddRange(List<PO_DetailDto> list)
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
                List<PODetailRequestDto> data = new List<PODetailRequestDto>();
                data = API.ConvertDataTable<PODetailRequestDto>(dtPODetails);

                StringContent content = new StringContent(JsonConvert.SerializeObject(data),
                                Encoding.UTF8, "application/json");

                var a = JsonConvert.SerializeObject(data);

                using (HttpClient httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsync($"{API.API_DOMAIN}{API.POST_PO_Detail}", content))
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
        }
    }
}
