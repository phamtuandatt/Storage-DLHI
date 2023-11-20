using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Storage.DTOs
{
    internal class SupplierDto
    {
        public Guid ID { get; set; }
        public string Code { get; set; }
        public string NameSupplier { get; set; }
        public string NameCompany { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }

    }

    internal class SupplierTypeDto
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
    }
}