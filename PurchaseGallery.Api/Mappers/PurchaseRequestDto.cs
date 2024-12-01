using System;
using PurchaseGallery.Api.Dtos.PurchaseRequest;
using PurchaseGallery.Api.Models.PurchaseRequests;

namespace PurchaseGallery.Api.Mappers;

public static class PurchaseRequestMapper
{
    public static PurchaseRequestDto ToDto(PurchaseRequest purchaseRequest)
    {
        return new PurchaseRequestDto
        {
            RequsterId = purchaseRequest.RequsterId,
            AssetIds = purchaseRequest.AssetIds,
            RequestDate = purchaseRequest.RequestDate,
            Status = purchaseRequest.Status,
            Comment = purchaseRequest.Comment,
            ApproverId = purchaseRequest.ApproverId,
            ApprovalDate = purchaseRequest.ApprovalDate
        };
    }
}