using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.RequestDto.MPRRequestDto
{
    internal class MPRExportDetailRequestDto
    {
        public Guid MprExportId { get; set; }

        public Guid MprId { get; set; }

        public Guid Sl { get; set; }

        public string SlV2 { get; set; }
    }
}
