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
            string sql = string.Format("INSERT INTO SUPPILER \r\nVALUES ('{0}', N'{1}', N'{2}', N'{3}')",
                            supplier.ID, supplier.Name, supplier.Address, supplier.Note);

            return data.Insert(sql) > 0;
        }
    }
}
