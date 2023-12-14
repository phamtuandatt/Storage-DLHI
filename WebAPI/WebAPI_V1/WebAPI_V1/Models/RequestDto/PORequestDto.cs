namespace WebAPI_V1.Models.RequestDto
{
    public class PORequestDto
    {
        public Guid PoId { get; set; }

        public Guid ItemId { get; set; }

        public string? MprNo { get; set; }

        public string? PoNo { get; set; }

        public long? Price { get; set; }

        public long? Quantity { get; set; }
    }
}
