namespace Entities.Dtos.Stores
{
    public class UpdateStoreDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string TaxNumber { get; set; } = string.Empty;
        public string TaxOffice { get; set; } = string.Empty;
        public string Iban { get; set; } = string.Empty;
        public string LogoUrl { get; set; } = string.Empty;
        public decimal Rating { get; set; }
        public bool IsApproved { get; set; }
    }
}