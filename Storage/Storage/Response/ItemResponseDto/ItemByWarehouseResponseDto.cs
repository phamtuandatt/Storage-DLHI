using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Response.ItemResponseDto
{
    internal class ItemByWarehouseResponseDto
    {
        public Guid Warehouse_Id { get; set; }
        public Guid Item_Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public byte[] Picture { get; set; }
        public Int64 Quantity { get; set; }
        public string Unit { get; set; }
        public string Groups { get; set; }
        public string Supplier { get; set; }
    }
}
