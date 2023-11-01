using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.DTOs
{
    internal class Item
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string Note { get; set; }
        public string Eng_Name { get; set; }
        public string Unit { get; set; }
        public string Group { get; set; }
        public string Type { get; set; }
    }
}
