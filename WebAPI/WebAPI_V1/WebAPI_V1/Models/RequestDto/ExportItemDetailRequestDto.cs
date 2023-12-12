namespace WebAPI_V1.Models.RequestDto
{
    public class ExportItemDetailRequestDto
    {
        public Guid ExportItemId { get; set; }
        public Guid ItemId { get; set; }
        public long? Qty { get; set; }
        public string? Note { get; set; }
    }
}
