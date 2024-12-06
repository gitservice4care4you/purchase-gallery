using PurchaseGallery.Api.Models.Assets;
using PurchaseGallery.Api.Models.Comments;
using PurchaseGallery.ApiService.Models.Auth;

namespace PurchaseGallery.Api.Models.PurchaseRequests;

public class PurchaseRequest
{
    public Guid Id { get; set; }
    public Guid RequsterId { get; set; }
    public required User RequesterUser { get; set; }
    public required List<Guid> AssetIds { get; set; } = [];
    public required ICollection<Asset> Assets { get; set; } = [];
    public DateTime RequestDate { get; set; }
    public ICollection<Comment>? CommentIds { get; set; }
    public ICollection<Comment>? Comments { get; set; }
    public DateTime? ApprovalDate { get; set; }
    public required Guid StatusId { get; set; }
    public required PurchaseRequestStatus Status { get; set; }
    public List<Guid> ApproverIds { get; set; } = [];
    public ICollection<User> Approvers { get; set; } = [];
}
