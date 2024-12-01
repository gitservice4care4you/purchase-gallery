using System;
using Microsoft.EntityFrameworkCore;

namespace PurchaseGallery.Api.Models.Assets
{
    public class Asset
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public string? ThumbnailUrl { get; set; }
        public string? AssetUrl { get; set; }

        [Precision(18, 2)]
        public decimal Price { get; set; }
        public int AvailableQuantity { get; set; }


        public Currency? Currency { get; set; }
        public bool IsAvailable { get; set; }
        public Guid AssetTypeId { get; set; }
        public required AssetCategory AssetCategory
        { get; set; }
        public Guid AssetCategoryId { get; set; }
        public required AssetType AssetType { get; set; }
    }
}
