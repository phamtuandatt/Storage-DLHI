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
    }

    internal class ImportItemDetailDto
    {
        public Guid ImportItemId { get; set; }
        public Guid ItemId { get; set; }
        public string Note { get; set; }
    }
}
