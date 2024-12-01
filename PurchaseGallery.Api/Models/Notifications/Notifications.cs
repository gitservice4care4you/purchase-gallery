using System;
using PurchaseGallery.Api.Models.PurchaseRequests;
namespace PurchaseGallery.Api.Models.Notifications;

public class Notifications
{
    public Guid Id { get; set; }
    public Guid PurchaseRequestId { get; set; }
    public required string Title { get; set; }
    public required string Message { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime SentDate { get; set; }

    public required PurchaseRequest PurchaseRequest { get; set; }
}
