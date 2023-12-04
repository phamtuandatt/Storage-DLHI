using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Response.ItemResponseDto
{
    internal class ItemResponseDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string PICTURELINK { get; set; }
        public byte[] PICTURE { get; set; }
        public string Note { get; set; }
        public string Eng_Name { get; set; }
        public Guid UNITID { get; set; }
        public Guid GROUPID { get; set; }
        public Guid TYPEID { get; set; }
        public Guid SUPPLIERID { get; set; }
        public Guid LOCATIONWAREHOUSE_ID { get; set;}
    }
}
