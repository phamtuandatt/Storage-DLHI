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
                    Eng_Name = res.Eng_Name
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
            ////return data.GetData($"EXEC GET_ITEMS_EXPORT '{guid}'", "ExportItems");
            //return data.GetData($"EXEC GET_ITEMS_EXPORT_V2 '{guid}'", "ExportItems");
        }

        public static string GetCode(string code)
        {
            string sql = $"EXEC GET_CURRENT_CODE_ITEM '{code}'";
            DataTable dt = data.GetData(sql, "code_ITEM");
            DataRow row = dt.Rows[0];
            string numberStr = row["NUMBER"].ToString();
            if (string.IsNullOrEmpty(numberStr))
            {
                return "0000001";
            }
            try
            {
                var number = int.Parse(numberStr);
                if (number >= 0 && number < 9)
                {
                    return "000000" + (number + 1);
                }
                else if (number >= 9 && number < 99)
                {
                    return "00000" + (number + 1);
                }
                else if (number >= 99 && number < 999)
                {
                    return "0000" + (number + 1);
                }
                else if (number >= 999 && number < 9999)
                {
                    return "000" + (number + 1);
                }
                else if (number >= 9999 && number < 99999)
                {
                    return "00" + (number + 1);
                }
                else if (number >= 99999 && number < 999999)
                {
                    return "0" + (number + 1);
                }
                else
                {
                    return numberStr + 1;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static bool Add(ItemDto item)
        {
            string sql = $"INSERT INTO ITEM(ID, CODE, NAME, PICTURE_LINK, PICTURE, NOTE, ENG_NAME, UNIT_ID, GROUP_ID, TYPE_ID, SUPPLIER_ID) " +
                $"VALUES ('{item.Id}', '{item.Code}', N'{item.Name}', N'{item.PictureLink}', " +
                $"(SELECT *FROM OPENROWSET(BULK N'{item.Picture}', SINGLE_BLOB) AS IMAGE), " +
                $"N'{item.Note}', N'{item.Eng_Name}', '{item.UnitId}', '{item.GroupId}', '{item.TypeId}' ,'{item.SupplierId}')";

            return data.Insert(sql) > 0;
        }

        public static bool AddNoIamge(ItemDto item)
        {
            string sql = $"INSERT INTO ITEM(ID, CODE, NAME, NOTE, ENG_NAME, UNIT_ID, GROUP_ID, TYPE_ID, SUPPLIER_ID) " +
                $"VALUES ('{item.Id}', '{item.Code}', N'{item.Name}', " +
                $"N'{item.Note}', N'{item.Eng_Name}', '{item.UnitId}', '{item.GroupId}', '{item.TypeId}' ,'{item.SupplierId}')";

            return data.Insert(sql) > 0;
        }

        public static bool Update(ItemDto item)
        {
            string sql = $"UPDATE ITEM SET CODE = '{item.Code}', NAME = N'{item.Name}', " +
                $"PICTURE_LINK = N'{item.PictureLink}', " +
                $"PICTURE = (SELECT *FROM OPENROWSET(BULK N'{item.Picture}', SINGLE_BLOB) AS IMAGE), " +
                $"NOTE = '{item.Note}', ENG_NAME = N'{item.Eng_Name}', UNIT_ID = '{item.UnitId}', " +
                $"GROUP_ID = '{item.GroupId}', TYPE_ID = '{item.TypeId}', SUPPLIER_ID = '{item.SupplierId}' " +
                $"WHERE ID = '{item.Id}'";

            return data.Insert(sql) > 0;
        }

        public static bool UpdateNoImage(ItemDto item)
        {
            string sql = $"UPDATE ITEM SET CODE = '{item.Code}', NAME = N'{item.Name}', " +
                $"NOTE = '{item.Note}', ENG_NAME = N'{item.Eng_Name}', UNIT_ID = '{item.UnitId}', " +
                $"GROUP_ID = '{item.GroupId}', TYPE_ID = '{item.TypeId}', SUPPLIER_ID = '{item.SupplierId}' " +
                $"WHERE ID = '{item.Id}'";

            return data.Insert(sql) > 0;
        }

        public static bool Delete(Guid itemId)
        {
            string sql = $"DELETE FROM ITEM WHERE ID = '{itemId}'";

            return data.Delete(sql) > 0;
        }
    }
}
