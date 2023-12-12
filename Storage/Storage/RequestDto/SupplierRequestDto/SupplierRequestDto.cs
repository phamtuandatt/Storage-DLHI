using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.RequestDto.SupplierRequestDto
{
    internal class SupplierRequestDto
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string NameSuppier { get; set; }

        public string NameCompanySupplier { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Note { get; set; }
    }
}
