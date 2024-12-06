using System;

namespace PurchaseGallery.Api.Models.Approval;

public class ApprovalStatus
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
}