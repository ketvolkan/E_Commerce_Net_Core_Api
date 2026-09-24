namespace Entities.Dtos.Stores
{
    public class CreateStoreDto
    {
        // ownership is assigned from authenticated user
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string TaxNumber { get; set; } = string.Empty;
        public string TaxOffice { get; set; } = string.Empty;
        public string Iban { get; set; } = string.Empty;
        public string LogoUrl { get; set; } = string.Empty;
    }
}