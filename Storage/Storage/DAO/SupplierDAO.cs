using Storage.DataProvider;
using Storage.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.DAO
{
    internal class SupplierDAO
    {
        public static SQLServerProvider data = new SQLServerProvider();

        public static bool Add(SupplierDto supplier)
        {
            string sql = $"INSERT INTO SUPPLIER VALUES ('{supplier.ID}', N'{supplier.NameSupplier}', N'{supplier.NameCompany}', N'{supplier.Address}', " +
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
    }
}
