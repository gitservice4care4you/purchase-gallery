using System;

namespace PurchaseGallery.Api.Models.Assets;

public class AssetType
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public string? ThumbnailUrl { get; set; }
}
