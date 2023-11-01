using Storage.DataProvider;
using Storage.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.DAO
{
    internal class Unit_DAO
    {
        public static SQLServerProvider data = new SQLServerProvider();

        public static bool Add(UnitDto unit)
        {
            string sql = string.Format("INSERT INTO UNIT \r\nVALUES ('{0}', N'{1}')\r\n",unit.Id, unit.Name);

            return data.Insert(sql) > 0;
        }
    }
}
