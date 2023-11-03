using Storage.DataProvider;
using Storage.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.DAO
{
    internal class Type_DAO
    {
        public static SQLServerProvider data = new SQLServerProvider();

        public static bool Add(TypeDto type)
        {
            string sql = $"INSERT INTO TYPES VALUES('{type.Id}', N'{type.Name}')";

            return data.Insert(sql) > 0;
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
