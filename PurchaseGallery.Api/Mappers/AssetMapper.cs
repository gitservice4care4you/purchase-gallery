using System;
using PurchaseGallery.Api.Dtos.Asset;
using PurchaseGallery.Api.Models.Assets;

namespace PurchaseGallery.Api.Mappers;

public static class AssetMapper
{
    public static Asset ToModel(AssetDto dto, AssetCategory assetCategory, AssetType assetType)
    {
        return new Asset
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Description = dto.Description,
            ImageUrl = dto.ImageUrl,
            ThumbnailUrl = dto.ThumbnailUrl,
            AssetUrl = dto.AssetUrl,
            Price = dto.Price,
            AvailableQuantity = dto.AvailableQuantity,
            Currency = dto.Currency,
            IsAvailable = dto.IsAvailable,
            AssetTypeId = dto.AssetTypeId,
            AssetCategoryId = dto.AssetCategoryId,
            AssetCategory = assetCategory,
            AssetType = assetType
        };
    }

    public static AssetDto ToDto(Asset asset)
    {
        return new AssetDto
        {
            Name = asset.Name,
            Description = asset.Description,
            ImageUrl = asset.ImageUrl,
            ThumbnailUrl = asset.ThumbnailUrl,
            AssetUrl = asset.AssetUrl,
            Price = asset.Price,
            AvailableQuantity = asset.AvailableQuantity,
            Currency = asset.Currency,
            IsAvailable = asset.IsAvailable,
            AssetTypeId = asset.AssetTypeId,
            AssetCategoryId = asset.AssetCategoryId
        };
    }
}
