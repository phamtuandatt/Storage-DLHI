﻿using Storage.DataProvider;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Storage.DTOs;

namespace Storage.DAO
{
    internal class Consumable_DAO
    {
        public static SQLServerProvider data = new SQLServerProvider();

        public static DataTable GetConsumable()
        {
            DataTable dt = new DataTable();
            string sql = $"SELECT *FROM CONSUMABLE";
            dt = data.GetData(sql, "CONSUMABLE");

            return dt;
        }

    }
}
