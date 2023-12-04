using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Response
{
    internal class ItemsResponseDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string PICTURE_LINK { get; set; }
        public byte[] PICTURE { get; set; }
        public string Unit { get; set; }
        public string GROUPS { get; set; }
        public string Supplier { get; set; }
        public string Note { get; set; }
        public string Eng_Name { get; set; }

        public string EngName { get; set; }
        public Guid UnitId { get; set; }
        public Guid GroupId { get; set; }
        public Guid TypeId { get; set; }
        public Guid SupplierId { get; set; }
    }
}
