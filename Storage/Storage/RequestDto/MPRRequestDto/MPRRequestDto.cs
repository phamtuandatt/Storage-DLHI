using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.RequestDto.MPRRequestDto
{
    internal class MPRRequestDto
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime ExpectedDelivery { get; set; }

        public string Note { get; set; }

        public Guid ItemId { get; set; }

        public string MprNo { get; set; }

        public string Usage { get; set; }

        public long? Quantity { get; set; }
    }
}
