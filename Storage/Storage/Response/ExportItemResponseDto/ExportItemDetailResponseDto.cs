using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Response.ExportItemResponseDto
{
    internal class ExportItemDetailResponseDto
    {
        public Guid ExportItemId { get; set; }

        public Guid ItemId { get; set; }

        public long? Qty { get; set; }

        public string Note { get; set; }
    }
}
