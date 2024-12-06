using System;
using System.Collections.Generic;
using PurchaseGallery.ApiService.Models;
using PurchaseGallery.ApiService.Models.Auth;

namespace PurchaseGallery.Api.Models.Auth
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Guid> UserIds { get; set; } = [];
        public List<User> Users { get; set; } = [];
    }
}
