using PurchaseGallery.Api.Models.Comments;
using PurchaseGallery.Api.Models.PurchaseRequests;

namespace PurchaseGallery.Api.Dtos.Approval;

public record class ApprovalDto
{
    public Guid Id { get; set; }
    public Guid PurchaseRequestId { get; set; }
    public Guid ApproverId { get; set; }
    public Guid PurchaseRequestStatusId { get; set; }
    public DateTime ApprovalDate { get; set; }
    public ICollection<Comment>? Comment { get; set; }
}
