﻿using Storage.DataProvider;
using Storage.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.DAO
{
    internal class Warehouse_DAO
    {
        public static SQLServerProvider data = new SQLServerProvider();

        public static DataTable GetLocationWareHouses()
        {
            string sql = "SELECT ID, NAME FROM WAREHOUSE";

            return data.GetData(sql, "cboLocationWareHouse");
        }

        public static bool Add(LocationWarehousseDto locationWarehousseDto)
        {
            string sql = $"INSERT INTO WAREHOUSE(ID, NAME) VALUES('{locationWarehousseDto.Id}', N'{locationWarehousseDto.Name}')";

            return data.Insert(sql) > 0;
        }

        public static bool Update(LocationWarehousseDto locationWhousseDto)
        {
            string sql = $"UPDATE LOCATION_WAREHOUSE SET NAME = N'{locationWhousseDto.Name}' WHERE ID = '{locationWhousseDto.Id}'";

            return data.Update(sql) > 0;
        }

        public static bool Delete(Guid locationId)
        {
            string sql = $"DELETE FROM LOCATION_WAREHOUSE WHERE ID = '{locationId}'";

            return data.Delete(sql) > 0;
        }
    }

    internal class WarehouseDetail_DAO
    {
        public static SQLServerProvider data = new SQLServerProvider();

        public static DataTable dtWarehouseDetail = data.GetData("SELECT *FROM WAREHOUSE_DETAIL", "WarehouseDetails");

        public static bool UpdateItemAtWarehouse(List<WareHouse_DetailDto> items)
        {
            foreach (WareHouse_DetailDto item in items)
            {
                DataRow row = dtWarehouseDetail.NewRow();
                row[0] = item.WarehouseId;
                row[1] = item.Item_Id;
                row[2] = item.Quantity;

                dtWarehouseDetail.Rows.Add(row);
            }
            try
            {
                data.UpdateDatabase("SELECT *FROM WAREHOUSE_DETAIL", dtWarehouseDetail);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

        public static bool UpdateQuantityItemAtWarehouse(List<WareHouse_DetailDto> items)
        {
            // Update datatable
            foreach (WareHouse_DetailDto item in items)
            {
                DataRow row = dtWarehouseDetail.NewRow();
                row[0] = item.WarehouseId;
                row[1] = item.Item_Id;
                row[2] = item.Quantity;

                dtWarehouseDetail.Rows.Add(row);
            }
            try
            {
                data.UpdateDatabase("SELECT *FROM WAREHOUSE_DETAIL", dtWarehouseDetail);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }
    }
}
