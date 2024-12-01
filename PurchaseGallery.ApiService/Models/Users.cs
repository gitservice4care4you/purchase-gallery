using System.ComponentModel.DataAnnotations;

namespace PurchaseGallery.ApiService.Models
{
    public class Users
    {

        public int Id { get; set; }

        public required string FullName { get; set; }

        public required string EmailAddress { get; set; }

        public string? department { get; set; }

        public string? JobTitle { get; set; }

        public string? Country {  get; set; }


    }
}
