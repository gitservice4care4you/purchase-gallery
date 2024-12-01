using System;
using PurchaseGallery.Api.Dtos.Approval;
using PurchaseGallery.Api.Models.PurchaseRequests;
using PurchaseGallery.ApiService.Models.Auth;

namespace PurchaseGallery.Api.Mappers;

public static class ApprovalMapper
{
    public static ApprovalDto ToDto(Approval approval)
    {
        return new ApprovalDto
        {
            PurchaseRequestId = approval.PurchaseRequestId,
            Status = approval.Status,
            Comment = approval.Comment
        };
    }

    public static Approval ToModel(ApprovalDto approvalDto, PurchaseRequest purchaseRequest, User approver)
    {
        return new Approval
        {
            Id = Guid.NewGuid(),
            PurchaseRequestId = approvalDto.PurchaseRequestId,
            Status = approvalDto.Status,
            Comment = approvalDto.Comment,
            PurchaseRequest = purchaseRequest,
            Approver = approver
        };
    }

}
