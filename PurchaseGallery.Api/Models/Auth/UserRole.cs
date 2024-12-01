using System;
using PurchaseGallery.ApiService.Models;
using PurchaseGallery.ApiService.Models.Auth;

namespace PurchaseGallery.Api.Models.Auth;

public class UserRole
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
    public required User User { get; set; }
    public required Role Role { get; set; }
}
