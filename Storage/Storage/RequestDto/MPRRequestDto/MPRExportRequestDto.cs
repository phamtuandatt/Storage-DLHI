using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.RequestDto.MPRRequestDto
{
    internal class MPRExportRequestDto
    {
        public Guid Id { get; set; }

        public DateTime? Created { get; set; }

        public long? ItemCount { get; set; }

        public int? Status { get; set; }
    }
}
