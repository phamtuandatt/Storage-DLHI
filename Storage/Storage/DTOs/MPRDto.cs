using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.DTOs
{
    internal class MPRDto
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime ExpectDelivery { get; set; }
        public string Note { get; set; }
        public Guid Supplier_Id { get; set; }

    }

    internal class MPR_DetailDto
    {
        public Guid MPR_Id { get; set; }
        public Guid Item_Id { get; set; }
        public string MPR_No { get; set; }
        public string Usage { get; set; }
        public int Quantity { get; set; }
    }
}
