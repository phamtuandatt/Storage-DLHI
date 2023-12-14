using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Response.POResponseDto
{
    internal class POResponseDto
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime Expected_Delivery { get; set; }

        public long Total { get; set; }

        public string Warehouse { get; set; }

        public string Payment { get; set; }
    }
}
