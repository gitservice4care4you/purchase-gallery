using System;

namespace PurchaseGallery.Api.Models.Countries;

public class Country
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
}
