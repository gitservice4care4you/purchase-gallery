using System.ComponentModel.DataAnnotations;
using PurchaseGallery.Api.Models.PurchaseRequests;

namespace PurchaseGallery.Api.Dtos.Notification;

public record class NotificationDto
{
    [Required]
    public Guid PurchaseRequestId { get; set; }
    [Required]
    public required string Title { get; set; }
    [Required]
    public required string Message { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime SentDate { get; set; }
}
