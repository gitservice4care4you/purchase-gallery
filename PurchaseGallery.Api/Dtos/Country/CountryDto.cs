using System.ComponentModel.DataAnnotations;

namespace PurchaseGallery.Api.Dtos.Country;

public record class CountryDto
{
    [Required]
    public required string Name { get; set; }
}
