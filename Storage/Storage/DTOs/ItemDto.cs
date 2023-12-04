using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.DTOs
{
    internal class ItemDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string PictureLink { get; set; }
        public byte[] Picture { get; set; }
        public byte[] Image { get; set; }
        public string Note { get; set; }
        public string EngName { get; set; }
        public Guid UnitId { get; set; }
        public Guid GroupId { get; set; }
        public Guid TypeId { get; set; }
        public Guid SupplierId { get; set; }
    }
}
