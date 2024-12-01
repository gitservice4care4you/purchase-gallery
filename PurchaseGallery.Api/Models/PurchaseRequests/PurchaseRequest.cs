
using PurchaseGallery.Api.Models.Assets;
using PurchaseGallery.ApiService.Models.Auth;

namespace PurchaseGallery.Api.Models.PurchaseRequests;

public class PurchaseRequest
{
    public Guid Id { get; set; }
    public Guid RequsterId { get; set; }
    public required List<Guid> AssetIds { get; set; } = [];
    public DateTime RequestDate { get; set; }
    public PurchaseRequestStatus Status { get; set; }

    public string? Comment { get; set; }
    public Guid ApproverId { get; set; }
    public DateTime? ApprovalDate { get; set; }
    public required User RequesterUser { get; set; }
    public required ICollection<Asset> Assets { get; set; } = [];
    public User? ApprovedBy { get; set; }
}




