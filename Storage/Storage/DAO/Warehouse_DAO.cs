using Newtonsoft.Json;
using Storage.DataProvider;
using Storage.DTOs;
using Storage.Helper;
using Storage.Response;
using Storage.Response.UnitResponseDto;
using Storage.Response.WarehouseResponseDto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Storage.DAO
{
    internal class Warehouse_DAO
    {
        public static SQLServerProvider data = new SQLServerProvider();

        public static async Task<DataTable> GetLocationWareHouses()
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{API.API_DOMAIN}{API.GET_WAREHOSE_COMBOXBOX}";
                string json = await client.GetStringAsync(url);
                var res = JsonConvert.DeserializeObject<List<WarehouseForComboboxResponseDto>>(json).ToList();

                return API.ListToDataTable(res, "WAREHOUSES");
            }
        }

        public static async Task<DataTable> GetInventories(int month, int year)
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{API.API_DOMAIN}{API.GET_WAREHOUSE_INVENTORIES}?month={month}&year={year}";
                string json = await client.GetStringAsync(url);
                var res = JsonConvert.DeserializeObject<List<InventoriesResponseDto>>(json).ToList();

                return API.ListToDataTable(res, "WAREHOUSES_INVENTORIES");
            }
            //return data.GetData($"EXEC GET_INVENTORY {month}, {year}", "Inventories");
        }

        public static async Task<bool> Add(LocationWarehousseDto locationWarehousseDto)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(locationWarehousseDto),
                                Encoding.UTF8, "application/json");

            using (HttpClient httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync($"{API.API_DOMAIN}{API.POST_WAREHOUSE}", content))
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

    internal class WarehouseDetail_DAO
    {
        public static SQLServerProvider data = new SQLServerProvider();

        public static DataTable dtWarehouseDetail = GetData();

        public static DataTable GetData()
        {
            using (HttpClient client = new HttpClient())
            {
                var url = $"{API.API_DOMAIN}{API.GET_WAREHOUSE_DETAIL}";
                string json = client.GetStringAsync(url).GetAwaiter().GetResult();
                var res = JsonConvert.DeserializeObject<List<WarehouseDetailResponseDto>>(json).ToList();

                return API.ListToDataTable(res, "WAREHOUSES_INVENTORIES");
            }
        }

        public static async Task<bool> UpdateItemAtWarehouse(List<WareHouse_DetailDto> items)
        {
            foreach (var item in items)
            {
                if (dtWarehouseDetail.AsEnumerable()
                                .Any(row => item.WarehouseId == row.Field<Guid>("WAREHOUSEID")
                                && item.ItemId == row.Field<Guid>("ITEMID")
                                && item.Month == row.Field<int>("MONTH")
                                && item.Year == row.Field<int>("YEAR")))
                {
                    DataRow row = dtWarehouseDetail.Select($"ITEMID = '{item.ItemId}' AND WAREHOUSEID = '{item.WarehouseId}' AND MONTH = {item.Month} AND YEAR = {item.Year}").FirstOrDefault();
                    row["QUANTITY"] = int.Parse(row["QUANTITY"].ToString()) + item.Quantity;
                }
                else
                {
                    DataRow row = dtWarehouseDetail.NewRow();
                    row[0] = item.WarehouseId;
                    row[1] = item.ItemId;
                    row[2] = item.Quantity;
                    row[3] = item.Month;
                    row[4] = item.Year;

                    dtWarehouseDetail.Rows.Add(row);
                }
            }

            try
            {
                List<WareHouse_DetailDto> data = new List<WareHouse_DetailDto>();
                data = API.ConvertDataTable<WareHouse_DetailDto>(dtWarehouseDetail);

                StringContent content = new StringContent(JsonConvert.SerializeObject(data),
                                Encoding.UTF8, "application/json");

                using (HttpClient httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsync($"{API.API_DOMAIN}{API.POST_WAREHOUSE_DETAIL}", content))
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

        public static async Task<bool> UpdateQuantityItemAtWarehouse(List<WareHouse_DetailDto> items)
        {
            foreach (var item in items)
            {
                if (dtWarehouseDetail.AsEnumerable()
                                .Any(row => item.WarehouseId == row.Field<Guid>("WAREHOUSEID")
                                && item.ItemId == row.Field<Guid>("ITEMID")
                                && item.Month == row.Field<int>("MONTH")
                                && item.Year == row.Field<int>("YEAR")))
                {
                    DataRow row = dtWarehouseDetail.Select($"ITEMID = '{item.ItemId}' AND WAREHOUSEID = '{item.WarehouseId}' AND MONTH = {item.Month} AND YEAR = {item.Year}").FirstOrDefault();
                    row["QUANTITY"] = int.Parse(row["QUANTITY"].ToString()) - item.Quantity;
                }
                else
                {
                    DataRow row = dtWarehouseDetail.NewRow();
                    row[0] = item.WarehouseId;
                    row[1] = item.ItemId;
                    row[2] = item.Quantity;
                    row[3] = item.Month;
                    row[4] = item.Year;

                    dtWarehouseDetail.Rows.Add(row);
                }
            }

            try
            {
                List<WareHouse_DetailDto> data = new List<WareHouse_DetailDto>();
                data = API.ConvertDataTable<WareHouse_DetailDto>(dtWarehouseDetail);

                StringContent content = new StringContent(JsonConvert.SerializeObject(data),
                                Encoding.UTF8, "application/json");

                using (HttpClient httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsync($"{API.API_DOMAIN}{API.POST_WAREHOUSE_DETAIL}", content))
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
