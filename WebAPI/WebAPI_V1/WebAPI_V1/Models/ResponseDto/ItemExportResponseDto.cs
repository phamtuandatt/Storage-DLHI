﻿using MessagePack;

namespace WebAPI_V1.Models.ResponseDto
{
    public class ItemExportResponseDto
    {
        public Guid? WarehouseId { get; set; }
        public Guid? ItemId { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public byte[]? Picture { get; set; }
        public Int64 Quantity { get; set; }
        public string? Unit { get; set; }
        public string? Groups { get; set; }
        public string? Supplier { get;set; } 
    }
}
