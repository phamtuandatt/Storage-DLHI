
using Newtonsoft.Json;
using Storage.DataProvider;
using Storage.DTOs;
using Storage.Helper;
using Storage.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Storage.DAO
{
    internal class PaymentMethod_DAO
    {
        public static SQLServerProvider data = new SQLServerProvider();

        public static async Task<DataTable> GetPaymentMethods()
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{API.API_DOMAIN}{API.GET_PAYMENT_METHOD}";
                string json = await client.GetStringAsync(url);
                var res = JsonConvert.DeserializeObject<List<PaymentMethodDto>>(json).ToList();

                return API.ListToDataTable(res, "PAYMENT_METHODS");
            }
        }

        public static async Task<bool> Add(PaymentMethodDto dto)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(dto),
                                Encoding.UTF8, "application/json");

            var a = JsonConvert.SerializeObject(dto);

            using (HttpClient httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync($"{API.API_DOMAIN}{API.POST_PAYMENT_METHOD}", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    return false;
                }
            }
        }

        public static bool Update(PaymentMethodDto dto)
        {
            string sql = $"UPDATE PAYMENT_METHOD SET NAME = N'{dto.Name}' WHERE ID = '{dto.Id}'";

            return data.Update(sql) > 0;
        }

        public static bool Delete(Guid id)
        {
            string sql = $"DELETE FROM PAYMENT_METHOD WHERE ID = '{id}'";

            return data.Delete(sql) > 0;
        }
    }
}
