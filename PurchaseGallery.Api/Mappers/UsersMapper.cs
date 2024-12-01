using System;
using PurchaseGallery.Api.Dtos.Users;
using PurchaseGallery.ApiService.Models;
using PurchaseGallery.ApiService.Models.Auth;

namespace PurchaseGallery.Api.Mappers;

public static class UsersMappers
{
    public static User MapLoginRegisterUserDtoToUser(LoginRegisterUserDto createUserDto)
    {
        return new User
        {
            AzureAdId = createUserDto.AzureAdId,
            FullName = createUserDto.FullName,
            Email = createUserDto.Email,
            Department = createUserDto.Department,
            JobTitle = createUserDto.JobTitle,
            Country = createUserDto.Country
        };
    }

    public static LoginRegisterUserDto MapUserToLoginRegisterUserDto(User user)
    {
        return new LoginRegisterUserDto
        (
             user.AzureAdId,
             user.FullName,
             user.Email!,
             user.Department,
             user.JobTitle,
             user.Country
        );
    }
}
