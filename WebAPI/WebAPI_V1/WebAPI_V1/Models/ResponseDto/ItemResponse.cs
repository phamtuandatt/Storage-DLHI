using Microsoft.EntityFrameworkCore;

namespace WebAPI_V1.Models.ResponseDto
{
    public class ItemResponse
    {
        public Guid Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? PICTURE_LINK { get; set; }
        public byte[]? PICTURE { get; set; }
        public string? Unit { get; set; }
        public string? GROUPS { get; set; }
        public string? Supplier { get; set; }
        public string? Note { get; set; }
        public string? Eng_Name { get;set; }
    }
}
