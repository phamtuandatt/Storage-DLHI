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
        public string BillNo { get; set; }
        public Int64 SumQuantity { get; set; }
    }

    internal class ExportItemDetail
    {
        public Guid ExportItemId { get; set; }
        public Guid ItemId { get; set; }
        public Int64 Qty { get; set; }
        public string Note { get; set; }
    }
}
