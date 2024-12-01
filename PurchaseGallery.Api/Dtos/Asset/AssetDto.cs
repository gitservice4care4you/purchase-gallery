using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using PurchaseGallery.Api.Models.Assets;

namespace PurchaseGallery.Api.Dtos.Asset;

public record class AssetDto
{
    [Required]
    [StringLength(50)]
    public required string Name { get; set; }
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public string? ThumbnailUrl { get; set; }
    public string? AssetUrl { get; set; }

    [Precision(18, 2)]
    public decimal Price { get; set; }
    [Required]
    [Precision(18, 2)]
    public int AvailableQuantity { get; set; }

    [Required]
    public Currency? Currency { get; set; }
    [Required]
    public bool IsAvailable { get; set; }

    public Guid AssetTypeId { get; set; }

    public Guid AssetCategoryId { get; set; }

}
