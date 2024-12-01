using PurchaseGallery.Api.Models.PurchaseRequests;

namespace PurchaseGallery.Api.Dtos.Approval;

public record class ApprovalDto
{
    public Guid PurchaseRequestId { get; set; }
    public Guid ApproverId { get; set; }
    public PurchaseRequestStatus Status { get; set; }
    public DateTime ApprovalDate { get; set; }
    public string? Comment { get; set; }


}
