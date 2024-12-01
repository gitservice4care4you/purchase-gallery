using System;
using PurchaseGallery.ApiService.Models;

namespace PurchaseGallery.Api.Models.Auth;

public class Role

{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<UserRole> UserRoles { get; set; } = [];


}

