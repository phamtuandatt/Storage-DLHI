namespace WebAPI_V1.Models.ResponseDto.ImportItemResponseDto
{
    public class ImportItemResponseDto
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public string? Bill_No { get; set; }
        public long? Sum_Quantity { get; set; }
        public long? Sum_Price { get; set; }
        public long? Sum_Payment { get; set; }
    }
}
