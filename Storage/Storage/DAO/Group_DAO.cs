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
    internal class Group_DAO
    {
        public static SQLServerProvider data = new SQLServerProvider();

        public static DataTable GetGroups()
        {
            string sql = $"SELECT *FROM GROUPS";

            return data.GetData(sql, "cboGroup");
        }

        public static bool Add(GroupDto group)
        {
            string sql = $"INSERT INTO GROUPS VALUES('{group.Id}', N'{group.Name}')";

            return data.Insert(sql) > 0;
        }

        public static bool Update(GroupDto group)
        {
            string sql = $"UPDATE GROUPS SET NAME = N'{group.Name}' WHERE ID = '{group.Id}'";

            return data.Update(sql) > 0;
        }

        public static bool Delete(Guid groupId)
        {
            string sql = $"DELETE FROM GROUPS WHERE ID = '{groupId}'";

            return data.Delete(sql) > 0;
        }
    }
}
