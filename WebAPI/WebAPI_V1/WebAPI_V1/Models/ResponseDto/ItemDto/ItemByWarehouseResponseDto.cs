using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI_V1.Models.ResponseDto.ItemResponse
{
    public partial class ItemByWarehouseResponseDto
    {
        public Guid Warehouse_Id { get; set; }
        public Guid Item_Id { get;set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public byte[]? Picture { get; set; }
        public Int64 Quantity { get; set; }
        public string? Unit { get; set; }
        public string? Groups { get; set; }
        public string? Supplier { get; set; }
    }
}
