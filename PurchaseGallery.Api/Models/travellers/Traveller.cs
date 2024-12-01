using System;
using PurchaseGallery.Api.Models.Countries;

namespace PurchaseGallery.Api.Models.travellers;

public class Traveller
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid FromWhereId { get; set; }
    public Guid ToWhereId { get; set; }
    public required Country FromWhere { get; set; }
    public required Country ToWhere { get; set; }
}
