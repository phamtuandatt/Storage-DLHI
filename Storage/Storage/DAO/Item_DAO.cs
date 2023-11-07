using ComponentFactory.Krypton.Toolkit;
using Storage.DataProvider;
using Storage.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.DAO
{
    internal class Item_DAO
    {
        public static SQLServerProvider data = new SQLServerProvider();

        public static DataTable GetItems()
        {
            //string sql = "EXEC GET_ITEMS";
            string sql = "EXEC GET_ITEMS_V2";

            return data.GetData(sql, "ITEMS");
        }

        public static ItemDto GetItem(Guid id)
        {
            string sql = $"SELECT *FROM ITEM WHERE ID = '{id}'";
            DataTable dt = data.GetData(sql, "Item");
            DataRow row = dt.Rows[0];
            ItemDto dto = new ItemDto()
            {
                Id = Guid.Parse(row["ID"].ToString()),
                Code = row["CODE"].ToString(),
                Name = row["NAME"].ToString(),
                PictureLink = row["PICTURE_LINK"].ToString(),
                Image = (byte[])row["PICTURE"],
                UnitId = Guid.Parse(row["UNIT_ID"].ToString()),
                GroupId = Guid.Parse(row["GROUP_ID"].ToString()),
                SupplierId =Guid.Parse(row["SUPPLIER_ID"].ToString()),
                TypeId = Guid.Parse(row["TYPE_ID"].ToString()),
                Note = row["NOTE"].ToString(),
                Eng_Name = row["ENG_NAME"].ToString()
            };

            return dto ?? new ItemDto();
        }

        public static string GetCode(string code)
        {
            string sql = $"EXEC GET_CURRENT_CODE_ITEM '{code}'";
            DataTable dt = data.GetData(sql, "code_ITEM");
            DataRow row = dt.Rows[0];
            string numberStr = row["NUMBER"].ToString();
            if (string.IsNullOrEmpty(numberStr))
            {
                return "0001";
            }
            try
            {
                var number = int.Parse(numberStr);
                if (number >= 0 && number < 10)
                {
                    return "000" + (number + 1);
                }
                else if (number >= 10 && number < 100)
                {
                    return "00" + (number + 1);
                }
                else if (number >= 100 && number < 1000)
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

        public static bool Delete(Guid itemId)
        {
            string sql = $"DELETE FROM ITEM WHERE ID = '{itemId}'";

            return data.Delete(sql) > 0;
        }
    }
}
