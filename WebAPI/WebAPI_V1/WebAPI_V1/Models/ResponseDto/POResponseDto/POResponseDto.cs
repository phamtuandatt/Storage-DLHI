namespace WebAPI_V1.Models.ResponseDto.POResponseDto
{
    public class POResponseDto
    {
        public Guid Id { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Expected_Delivery { get; set; }

        public long? Total { get; set; }

        public string? Warehouse { get; set; }

        public string? Payment { get; set; }
    }
}
