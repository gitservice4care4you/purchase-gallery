using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PurchaseGallery.Api.Dtos.Users;

public record class LoginRegisterUserDto
(

    [Required]
    [StringLength(50)]
    string AzureAdId,

    [Required]
    [StringLength(50)]
    string FullName,
    [Required][EmailAddress] string Email,
     // [Required][PasswordPropertyText][StringLength(50)][MinLength(8)] string Password,
     string? Department,
    string? Country,
     string? JobTitle
);

