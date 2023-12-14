using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.RequestDto.PORequestDto
{
    internal class PORequestDto
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime ExpectedDelivery { get; set; }

        public long Total { get; set; }

        public Guid LocationWarehouseId { get; set; }

        public Guid PaymentMethodId { get; set; }
    }
}
