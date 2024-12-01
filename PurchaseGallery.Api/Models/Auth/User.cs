using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using PurchaseGallery.Api.Models.Auth;

namespace PurchaseGallery.ApiService.Models.Auth
{
    public class User
    {
        public Guid Id { get; set; }
        public required string AzureAdId { get; set; }

        public required string FullName { get; set; }

        public required string Email { get; set; }

        public string? Department { get; set; }

        public string? JobTitle { get; set; }

        public string? Country { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = [];

    }
}
