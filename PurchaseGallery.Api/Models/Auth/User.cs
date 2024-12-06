using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using PurchaseGallery.Api.Models.Auth;
using PurchaseGallery.Api.Models.Countries;

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

        public Guid? CountryId { get; set; }
        public Country? Country { get; set; }
        public ICollection<Guid> RolesIds { get; set; } = [];
        public ICollection<Role> Roles { get; set; } = [];
    }
}
