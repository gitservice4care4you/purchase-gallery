using PurchaseGallery.Api.Models.PurchaseRequests;

namespace PurchaseGallery.Api.Dtos.PurchaseRequest;

public record class PurchaseRequestDto
{
    public Guid RequsterId { get; set; }
    public required List<Guid> AssetIds { get; set; } = [];
    public DateTime RequestDate { get; set; }
    public PurchaseRequestStatus Status { get; set; }

    public string? Comment { get; set; }
    public Guid ApproverId { get; set; }
    public DateTime? ApprovalDate { get; set; }


}
