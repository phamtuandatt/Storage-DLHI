namespace WebAPI_V1.Models.RequestDto
{
    public class ExportItemRequestDto
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public string? BillNo { get; set; }
        public long? SumQuantity { get; set; }
    }
}
