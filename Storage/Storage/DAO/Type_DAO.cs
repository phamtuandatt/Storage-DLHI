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
            string sql = string.Format("INSERT INTO TYPES \r\nVALUES ('{0}', N'{1}')", type.Id, type.Name);

            return data.Insert(sql) > 0;
        }
    }
}
