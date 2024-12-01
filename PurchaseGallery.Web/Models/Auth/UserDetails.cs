using System.ComponentModel.DataAnnotations;

namespace PurchaseGallery.Web.Models.Auth;

public record class UserDetails
{
    public required string MicrosoftAccountId { get; set; }
    public required string FullName { get; set; }

    public required string EmailAddress { get; set; }
    public string? Department { get; set; }
    public string? Country { get; set; }
    public string? JobTitle { get; set; }


}
