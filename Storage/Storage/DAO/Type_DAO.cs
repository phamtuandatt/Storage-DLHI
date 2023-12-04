using Newtonsoft.Json;
using Storage.DataProvider;
using Storage.DTOs;
using Storage.Helper;
using Storage.Response.GroupResponseDto;
using Storage.Response.TypeResponseDto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Storage.DAO
{
    internal class Type_DAO
    {
        public static SQLServerProvider data = new SQLServerProvider();

        public static async Task<DataTable> GetTypes()
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{API.API_DOMAIN}{API.GET_TYPES}";
                string json = await client.GetStringAsync(url);
                var res = JsonConvert.DeserializeObject<List<TypeResponseDto>>(json).ToList();

                return API.ListToDataTable(res, "TYPES");
            }
        }

        public static async Task<bool> Add(TypeDto type)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(type),
                                Encoding.UTF8, "application/json");

            using (HttpClient httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync($"{API.API_DOMAIN}{API.POST_TYPE}", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    return false;
                }
            }
        }

        public static bool Update(TypeDto type)
        {
            string sql = $"UPDATE TYPES SET NAME = N'{type.Name}' WHERE ID = '{type.Id}'";

            return data.Update(sql) > 0;
        }

        public static bool Delete(Guid id)
        {
            string sql = $"DELETE FROM TYPES WHERE ID = '{id}'";

            return data.Delete(sql) > 0;
        }
    }
}
