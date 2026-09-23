namespace Entities.Dtos.Addresses
{
    public class CreateAddressDto
    {
        public string Title { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string AddressDetail { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
    }
}