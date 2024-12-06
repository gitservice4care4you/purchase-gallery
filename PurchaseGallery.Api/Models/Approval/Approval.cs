using System;
using PurchaseGallery.Api.Models.Comments;
using PurchaseGallery.Api.Models.PurchaseRequests;
using PurchaseGallery.ApiService.Models;
using PurchaseGallery.ApiService.Models.Auth;

namespace PurchaseGallery.Api.Models.PurchaseRequests;

public class Approval
{
    public Guid Id { get; set; }
    public Guid PurchaseRequestId { get; set; }
    public required PurchaseRequest PurchaseRequest { get; set; }
    public Guid ApproverId { get; set; }
    public required User Approver { get; set; }
    public Guid PurchaseRequestStatusId { get; set; }
    public required PurchaseRequestStatus Status { get; set; }
    public DateTime ApprovalDate { get; set; }
    public ICollection<Comment>? Comment { get; set; }
}
