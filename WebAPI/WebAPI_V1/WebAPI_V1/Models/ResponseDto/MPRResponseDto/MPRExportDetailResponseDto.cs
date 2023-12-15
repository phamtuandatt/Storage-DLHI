using Microsoft.Extensions.Primitives;

namespace WebAPI_V1.Models.ResponseDto.MPRResponseDto
{
    public class MPRExportDetailResponseDto
    {
        public Guid MPR_Id { get; set; }
        public Guid Item_Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Unit { get; set; }
        public byte[]? Picture { get; set; }
        public string? Usage { get; set; }
        public DateTime? Created { get; set; }
        public long? Quantity { get; set; }
        public string? Note { get; set; }
        public string? MPR_No { get; set; }
        public Guid MPR_Export_Id { get; set; }
    }
}
