using System;
using System.Diagnostics;
using System.Net.Http.Json;
using PurchaseGallery.Web.Models.Auth;

namespace PurchaseGallery.Web.Mappers.Auth;

public static class UserDetailsMapper
{

    public static string ConvertUserDetailsToJson(UserDetails userDetails)
    {
        var options = new System.Text.Json.JsonSerializerOptions
        {
            PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase
        };
        return System.Text.Json.JsonSerializer.Serialize(userDetails, options);
    }

    public static UserDetails ConvertJsonToUserDetails(string json)
    {
        return System.Text.Json.JsonSerializer.Deserialize<UserDetails>(json)!;
    }

}
