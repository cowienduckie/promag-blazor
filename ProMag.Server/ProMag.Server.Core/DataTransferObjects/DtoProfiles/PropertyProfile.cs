using AutoMapper;
using ProMag.Server.Core.DataTransferObjects.CreateDtos;
using ProMag.Server.Core.DataTransferObjects.ReadDtos;
using ProMag.Server.Core.DataTransferObjects.UpdateDtos;
using ProMag.Server.Core.Domain.Entities;

namespace ProMag.Server.Core.DataTransferObjects.DtoProfiles;

public class PropertyProfile : Profile
{
    public PropertyProfile()
    {
        // Property mappers
        CreateMap<Property, PropertyReadDto>();
        CreateMap<Property, PropertyUpdateDto>();
        CreateMap<PropertyCreateDto, Property>();
        CreateMap<PropertyUpdateDto, Property>();

        // PropertyType mappers
        CreateMap<PropertyType, PropertyTypeReadDto>();
    }
}