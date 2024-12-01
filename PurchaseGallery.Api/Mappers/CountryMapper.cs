using System;
using PurchaseGallery.Api.Dtos.Country;
using PurchaseGallery.Api.Models.Countries;

namespace PurchaseGallery.Api.Mappers;

public class CountryMapper
{
    public static Country ToModel(CountryDto countryDto)
    {
        return new Country
        {
            Id = Guid.NewGuid(),
            Name = countryDto.Name
        };
    }

    public static CountryDto ToDto(Country country)
    {
        return new CountryDto
        {
            Name = country.Name
        };
    }
}
