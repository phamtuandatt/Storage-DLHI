using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.DTOs
{
    internal class MPR_Export
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public int ItemCount { get; set; }
        public int Status { get; set; } // 0 / 1 / 2
    }

    internal class MPR_Export_Detail
    {
        public Guid MPR_Export_Id { get; set; }
        public Guid MPR_Id { get; set; }
    }
}
