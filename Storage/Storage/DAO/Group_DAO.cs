using Newtonsoft.Json;
using Storage.DataProvider;
using Storage.DTOs;
using Storage.Helper;
using Storage.Response.GroupResponseDto;
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
    internal class Group_DAO
    {
        public static SQLServerProvider data = new SQLServerProvider();

        public static async Task<DataTable> GetGroups()
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{API.API_DOMAIN}{API.GET_GROUPS}";
                string json = await client.GetStringAsync(url);
                var res = JsonConvert.DeserializeObject<List<GroupResponseDto>>(json).ToList();

                return API.ListToDataTable(res, "GROUPS");
            }
            //string sql = $"SELECT *FROM GROUPS";

            //return data.GetData(sql, "cboGroup");
        }

        public static async Task<bool> Add(GroupDto group)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(group),
                    Encoding.UTF8, "application/json");

            using (HttpClient httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync($"{API.API_DOMAIN}{API.GET_GROUPS}", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    return false;
                }
            }
        }

        public static bool Update(GroupDto group)
        {
            string sql = $"UPDATE GROUPS SET NAME = N'{group.Name}' WHERE ID = '{group.Id}'";

            return data.Update(sql) > 0;
        }

        public static bool Delete(Guid groupId)
        {
            string sql = $"DELETE FROM GROUPS WHERE ID = '{groupId}'";

            return data.Delete(sql) > 0;
        }
    }
}
