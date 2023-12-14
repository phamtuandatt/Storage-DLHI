using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Response.PODetailResponseDto
{
    internal class PODetailReponseDto
    {
        public Guid PoId { get; set; }

        public Guid ItemId { get; set; }

        public string MprNo { get; set; }

        public string PoNo { get; set; }

        public long? Price { get; set; }

        public long? Quantity { get; set; }
    }
}
