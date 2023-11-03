using Storage.DataProvider;
using Storage.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.DAO
{
    internal class Item_DAO
    {
        public static SQLServerProvider data = new SQLServerProvider();

        public static bool Add(ItemDto item)
        {
            string sql = string.Format("INSERT INTO ITEM VALUES ('{0}', '{1}', N'{2}', N'{3}', N'{4}', N'{5}', '{6}', '{7}', '{8}')",
                            item.Id, item.Code, item.Name, item.Picture, item.Note, item.Eng_Name, item.Unit, item.Group, item.Type);

            return data.Insert(sql) > 0;
        }

        public static bool Update(ItemDto item)
        {
            string sql = $"UPDATE ITEM SET CODE = '{item.Code}', NAME = N'{item.Name}', PICTURE = N'{item.Picture}', " +
                                        $"NOTE = N'{item.Note}', ENG_NAME = N'{item.Eng_Name}', UNIT_ID = '{item.Unit}', " +
                                        $"GROUP_ID = '{item.Group}', TYPE_ID = '{item.Type}' WHERE ID = '{item.Id}'";

            return data.Insert(sql) > 0;
        }

        public static bool Delete(Guid itemId)
        {
            string sql = $"DELETE FROM ITEM WHERE ID = '{itemId}'";

            return data.Delete(sql) > 0;
        }
    }
}
