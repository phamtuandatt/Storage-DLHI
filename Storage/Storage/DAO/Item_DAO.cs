using ComponentFactory.Krypton.Toolkit;
using Newtonsoft.Json;
using Storage.DataProvider;
using Storage.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Storage.Response;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Reflection;
using static System.Resources.ResXFileRef;
using Storage.Helper;
using OfficeOpenXml.Export.ToDataTable;
using Storage.Response.ItemResponseDto;

namespace Storage.DAO
{
    internal class Item_DAO
    {
        public static SQLServerProvider data = new SQLServerProvider();

        public static async Task<DataTable> GetItemsAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{API.API_DOMAIN}{API.GET_ITEMS}";
                string json = await client.GetStringAsync(url);
                var res = JsonConvert.DeserializeObject<List<ItemsResponseDto>>(json).ToList();

                return API.ListToDataTable(res, "ITEM_V2");
            }
        }

        public static async Task<ItemDto> GetItemAsync(Guid id)
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{API.API_DOMAIN}{API.GET_ITEM}{id}";
                string json = await client.GetStringAsync(url);
                var res = JsonConvert.DeserializeObject<ItemsResponseDto>(json);

                ItemDto itemDto = new ItemDto()
                {
                    Id = Guid.Parse(res.Id.ToString()),
                    Code = res.Code,
                    Name = res.Name,
                    PictureLink = res.PICTURE_LINK,
                    Image = res.PICTURE != null ? (byte[])res.PICTURE : new byte[100],
                    UnitId = Guid.Parse(res.UnitId.ToString()),
                    GroupId = Guid.Parse(res.GroupId.ToString()),
                    SupplierId = Guid.Parse(res.SupplierId.ToString()),
                    TypeId = Guid.Parse(res.TypeId.ToString()),
                    Note = res.Note,
                    EngName = res.EngName
                };

                return itemDto ?? new ItemDto();
            }
        }

        public static async Task<DataTable> GetItemByWarehouseIdAsync(Guid guid)
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{API.API_DOMAIN}{API.GET_ITEM_BY_WAREHOUSE}{guid}";
                string json = await client.GetStringAsync(url);
                var res = JsonConvert.DeserializeObject<List<ItemByWarehouseResponseDto>>(json).ToList();

                return API.ListToDataTable(res, "ExportItems");
            }
        }

        public static async Task<string> GetCode(string code)
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{API.API_DOMAIN}{API.GET_ITEM_CODE}{code}";
                string json = await client.GetStringAsync(url);

                return json;           
            }
        }


        public static async Task<bool> Add(ItemDto item)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(item),
                    Encoding.UTF8, "application/json");

            using (HttpClient httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync($"{API.API_DOMAIN}{API.POST_ITEM}", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    return false;
                }
            }
        }

        public static async Task<bool> AddNoIamge(ItemDto item)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(item),
                    Encoding.UTF8, "application/json");

            using (HttpClient httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync($"{API.API_DOMAIN}{API.POST_ITEM_NO_IMAGE}", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    return false;
                }
            }
        }

        public static async Task<bool> Update(ItemDto item)
        {
            var putData = JsonConvert.SerializeObject(item);
            StringContent content = new StringContent(putData,
                    Encoding.UTF8, "application/json");

            using (HttpClient httpClient = new HttpClient())
            {
                using (var response = await httpClient.PutAsync($"{API.API_DOMAIN}{API.PUT_ITEM}{item.Id}", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    return false;
                }
            }
        }

        public static async Task<bool> UpdateNoImage(ItemDto item)
        {
            var putData = JsonConvert.SerializeObject(item);
            StringContent content = new StringContent(putData,
                    Encoding.UTF8, "application/json");

            using (HttpClient httpClient = new HttpClient())
            {
                using (var response = await httpClient.PutAsync($"{API.API_DOMAIN}{API.PUT_ITEM_NO_IMAGE}{item.Id}", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    return false;
                }
            }
        }

        public static async Task<bool> Delete(Guid itemId)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync($"{API.API_DOMAIN}{API.DELETE_ITEM}{itemId}"))
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
