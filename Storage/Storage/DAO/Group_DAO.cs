using Storage.DataProvider;
using Storage.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.DAO
{
    internal class Group_DAO
    {
        public static SQLServerProvider data = new SQLServerProvider();

        public static bool Add(GroupDto group)
        {
            string sql = string.Format("INSERT INTO GROUPS \r\nVALUES ('{0}', N'{1}')", group.Id, group.Name);

            return data.Insert(sql) > 0;
        }
    }
}
