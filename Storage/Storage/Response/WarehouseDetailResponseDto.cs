using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Response
{
    internal class WarehouseDetailResponseDto
    {
        public Guid WarehouseId { get; set; }

        public Guid ItemId { get; set; }

        public long? Quantity { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }
    }
}
