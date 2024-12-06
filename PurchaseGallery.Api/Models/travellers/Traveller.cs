using System;
using PurchaseGallery.Api.Models.Countries;

namespace PurchaseGallery.Api.Models.travellers;

public class Traveller
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public ICollection<Guid>? FromWhereId { get; set; }
    public ICollection<Guid>? ToWhereId { get; set; }
    public ICollection<Country>? FromWhere { get; set; }
    public ICollection<Country>? ToWhere { get; set; }
}
