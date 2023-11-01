using Storage.DataProvider;
using Storage.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.DAO
{
    internal class TypeConsumable_DAO
    {
        public static SQLServerProvider data = new SQLServerProvider();

        public static bool Add(TypeConsumableDto type)
        {
            string sql = string.Format("INSERT INTO TYPE_CONSUMABLE \r\nVALUES ('{0}', N'{1}')", type.Id, type.Name);

            return data.Insert(sql) > 0;
        }
    }
}
