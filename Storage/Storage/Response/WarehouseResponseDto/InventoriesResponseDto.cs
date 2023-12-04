using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Response.WarehouseResponseDto
{
    internal class InventoriesResponseDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public string Groups { get; set; }
        public string Supplier { get; set; }
        public Int64? Inventory { get; set; } = 0;
        public Int64? Sum_Qty_Import { get; set; } = 0;
        public Int64? Sum_Price_Import { get; set; } = 0;
        public Int64? Sum_Qty_Export { get; set; } = 0;
        public Int64? Last_Amount { get; set; } = 0;
    }
}
