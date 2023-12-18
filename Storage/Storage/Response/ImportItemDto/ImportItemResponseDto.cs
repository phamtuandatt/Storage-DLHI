using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Response.ImportItemDto
{
    internal class ImportItemResponseDto
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public string Bill_No { get; set; }
        public long? Sum_Quantity { get; set; }
        public long? Sum_Price { get; set; }
        public long? Sum_Payment { get; set; }
    }
}
