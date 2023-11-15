using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.DTOs
{
    internal class WareHouseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int TotalItem { get; set; }
    }

    internal class WareHouse_DetailDto
    {
        public Guid WarehouseId { get; set; }
        public Guid Item_Id { get; set; }
        public int Quantity { get; set; }
        public int Month { get; set; }
    }
}
