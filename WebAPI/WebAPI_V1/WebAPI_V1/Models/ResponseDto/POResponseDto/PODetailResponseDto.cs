namespace WebAPI_V1.Models.ResponseDto.POResponseDto
{
    public class PODetailResponseDto
    {
        public Guid PO_Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public byte[]? Picture { get; set; }
        public string? MPR_No { get; set; }
        public string? PO_No { get; set; }
        public long? Quantity { get; set; }
        public long? Price { get; set; }
    }
}
