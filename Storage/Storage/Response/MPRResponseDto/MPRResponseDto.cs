using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Response.MPRResponseDto
{
    internal class MPRResponseDto
    {
        public Guid MPR_Id { get; set; }
        public Guid Item_Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public byte[] Picture { get; set; }
        public string Usage { get; set; }
        public DateTime Created { get; set; }
        public long Quantity { get; set; }
        public string Note { get; set; }
        public string MPR_No { get; set; }
    }
}
