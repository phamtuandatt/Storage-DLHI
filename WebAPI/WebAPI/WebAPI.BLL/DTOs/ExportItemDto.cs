using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.DTOs
{
    internal class ExportItemDto
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public string Bill_No { get; set; }
        public int Sum_Quantity { get; set; }
    }

    internal class ExportItemDetail
    {
        public Guid ExportItemId { get; set; }
        public Guid ItemId { get; set; }
        public int Qty { get; set; }
        public string Note { get; set; }
    }
}
