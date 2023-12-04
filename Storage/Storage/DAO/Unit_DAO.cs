using Newtonsoft.Json;
using Storage.DataProvider;
using Storage.DTOs;
using Storage.Helper;
using Storage.Response;
using Storage.Response.UnitResponseDto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Storage.DAO
{
    internal class Unit_DAO
    {
        public static SQLServerProvider data = new SQLServerProvider();

        public static async Task<DataTable> GetUnits()
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{API.API_DOMAIN}{API.GET_UNITS}";
                string json = await client.GetStringAsync(url);
                var res = JsonConvert.DeserializeObject<List<UnitResponseDto>>(json).ToList();

                return API.ListToDataTable(res, "UNITS");
            }
        }

        public static async Task<bool> Add(UnitDto type)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(type),
                    Encoding.UTF8, "application/json");

            using (HttpClient httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync($"{API.API_DOMAIN}{API.POST_UNIT}", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    return false;
                }
            }
        }

        public static bool Update(UnitDto type)
        {
            string sql = $"UPDATE UNIT SET NAME = N'{type.Name}' WHERE ID = '{type.Id}'";

            return data.Update(sql) > 0;
        }

        public static bool Delete(Guid id)
        {
            string sql = $"DELETE FROM UNIT WHERE ID = '{id}'";

            return data.Delete(sql) > 0;
        }
    }
}
