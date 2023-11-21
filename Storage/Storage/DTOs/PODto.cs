using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.DTOs
{
    internal class PODto
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; } 
        public DateTime ExpectedDelivery { get; set; }
        public Int64 Total { get; set; }

        public Guid Supplier_Id { get; set; }  
        public Guid LocationWareHouse_Id { get; set; }
        public Guid PaymentMethod_Id { get; set; }
    }

    internal class PO_DetailDto
    {
        public Guid PO_Id { get; set; }
        public Guid Item_Id { get; set; } 
        public string MPR_No { get; set; } 
        public string PO_No { get; set; }
        public Int64 Price { get; set; }
        public Int64 Quantity { get; set; }
    }
}
