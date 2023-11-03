using Storage.DataProvider;
using Storage.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.DAO
{
    internal class PaymentMethod_DAO
    {
        public static SQLServerProvider data = new SQLServerProvider();

        public static bool Add(PaymentMethodDto dto)
        {
            string sql = $"INSERT INTO PAYMENT_METHOD VALUES ('{dto.Id}', N'{dto.Name}')";

            return data.Insert(sql) > 0;
        }

        public static bool Update(PaymentMethodDto dto)
        {
            string sql = $"UPDATE PAYMENT_METHOD SET NAME = N'{dto.Name}' WHERE ID = '{dto.Id}'";

            return data.Update(sql) > 0;
        }

        public static bool Delete(Guid id)
        {
            string sql = $"DELETE FROM PAYMENT_METHOD WHERE ID = '{id}'";

            return data.Delete(sql) > 0;
        }
    }
}
