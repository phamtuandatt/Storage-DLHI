namespace WebAPI_V1.Models.RequestDto
{
    public class SupplierRequestDto
    {
        public Guid ID { get; set; }
        public string? Code { get; set; }
        public string? NameSupplier { get; set; }
        public string? NameCompany { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Note { get; set; }
    }
}
