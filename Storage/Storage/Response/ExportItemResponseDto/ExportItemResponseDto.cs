using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Response.ExportItemResponseDto
{
    internal class ExportItemResponseDto
    {
        public Guid Id { get; set; }

        public DateTime? Created { get; set; }

        public string BillNo { get; set; }

        public long SumQuantity { get; set; }
    }
}
