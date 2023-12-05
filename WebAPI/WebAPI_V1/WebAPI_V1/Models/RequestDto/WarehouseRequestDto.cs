namespace WebAPI_V1.Models.RequestDto
{
    public class WarehouseRequestDto
    {
        public Guid WarehouseId { get; set; }

        public Guid ItemId { get; set; }

        public long? Quantity { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }
    }
}
