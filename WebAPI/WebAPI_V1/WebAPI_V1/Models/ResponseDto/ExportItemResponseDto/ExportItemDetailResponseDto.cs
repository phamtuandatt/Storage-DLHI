namespace WebAPI_V1.Models.ResponseDto.ExportItemResponseDto
{
    public class ExportItemDetailResponseDto
    {
        public Guid Export_Item_Id { get; set; }
        public Guid? Item_Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public byte[]? Picture { get; set; }
        public long Qty { get; set; }
        public string? Note { get; set; }
        public string? Supplier_Code { get; set; }
        public string? Name_Suppier { get; set; }
    }
}
