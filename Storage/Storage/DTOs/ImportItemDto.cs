using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.DTOs
{
    internal class ImportItemDto
    {
        public Guid Id { get; set; } 
        public DateTime Created { get; set; }
        public string Bill_No { get; set; }
        public int Quantity { get; set;  }    
        public int Price { get; set; }  
        public int Total { get; set; }

        public Guid Supplier_Id { get; set; }
        public Guid Item_Id { get; set; }
    }
}
