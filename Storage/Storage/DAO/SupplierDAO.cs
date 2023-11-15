using Storage.DataProvider;
using Storage.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;

namespace Storage.DAO
{
    internal class SupplierDAO
    {
        public static SQLServerProvider data = new SQLServerProvider();

        public static DataTable GetSuppiers()
        {
            string sql = "SELECT *FROM SUPPLIER";

            return data.GetData(sql, "cboSupplier");
        }

        public static DataTable GetSuppierTypes()
        {
            string sql = "SELECT *FROM SUPPLIER_TYPE";

            return data.GetData(sql, "cboSupplierType");
        }

        public static SupplierDto GetSupplier(Guid guid)
        {
            string sql = $"SELECT *FROM SUPPLIER WHERE ID = '{guid}'";
            DataTable dt = data.GetData(sql, "GetSingleSupplier");
            DataRow row = dt.Rows[0];
            SupplierDto dto = new SupplierDto()
            {
                ID = Guid.Parse(row["ID"].ToString()),
                Code = row["CODE"].ToString(),
                NameSupplier = row["NAME_SUPPIER"].ToString(),
                NameCompany = row["NAME_COMPANY_SUPPLIER"].ToString(),
                Address = row["ADDRESS"].ToString(),
                Phone = row["PHONE"].ToString(),
                Email = row["EMAIL"].ToString(),
                Note = row["NOTE"].ToString(),
            };


            return dto ?? new SupplierDto();
        }

        public static string GetCurrentCodeSupplier(string code)
        {
            string sql = $"EXEC GET_CURRENT_CODE_SUPPLIER '{code}'";
            DataTable dt = data.GetData(sql, "code");
            DataRow row = dt.Rows[0];
            string numberStr = row["NUMBER"].ToString();
            if (string.IsNullOrEmpty(numberStr)) 
            {
                return "0000001";
            }
            try
            {
                var number = int.Parse(numberStr);
                if (number >= 0 && number < 9)
                {
                    return "000000" + (number + 1);
                } 
                else if (number >= 9 && number < 99) 
                {
                    return "00000" + (number + 1);
                }
                else if (number >= 99 && number < 999)
                {
                    return "0000" + (number + 1);
                }
                else if (number >= 999 && number < 9999)
                {
                    return "000" + (number + 1);
                }
                else if (number >= 9999 && number < 99999)
                {
                    return "00" + (number + 1);
                }
                else if (number >= 99999 && number < 999999)
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

        public static bool Add(SupplierDto supplier)
        {
            string sql = $"INSERT INTO SUPPLIER VALUES ('{supplier.ID}', '{supplier.Code}', N'{supplier.NameSupplier}', N'{supplier.NameCompany}', N'{supplier.Address}', " +
                            $"N'{supplier.Phone}', N'{supplier.Email}', N'{supplier.Note}')";

            return data.Insert(sql) > 0;
        }

        public static bool Update(SupplierDto supplier)
        {
            string sql = $"UPDATE SUPPLIER SET NAME_SUPPIER = N'{supplier.NameSupplier}', NAME_COMPANY_SUPPLIER = N'{supplier.NameCompany}', ADDRESS = N'{supplier.Address}', " +
                            $"PHONE = N'{supplier.Phone}', EMAIL = N'{supplier.Email}', NOTE = N'{supplier.Note}' WHERE ID = '{supplier.ID}'";

            return data.Update(sql) > 0;
        }

        public static bool Delete(Guid id)
        {
            string sql = $"DELETE FROM SUPPLIER WHERE ID = '{id}'";

            return data.Delete(sql) > 0;
        }

        public static bool AddSupplierType(SupplierTypeDto dto)
        {
            string sql = $"INSERT INTO SUPPLIER_TYPE VALUES ('{dto.ID}', N'{dto.Name}')";

            return data.Insert(sql) > 0;
        }
    }
}
