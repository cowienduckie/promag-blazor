using AutoMapper;
using ProMag.Shared.DataTransferObjects.CreateDtos;
using ProMag.Shared.DataTransferObjects.ReadDtos;
using ProMag.Shared.DataTransferObjects.UpdateDtos;
using ProMag.Server.Core.Domain.Entities;

namespace ProMag.Server.Core.Domain.Mappings;

public class PropertyProfile : Profile
{
    public PropertyProfile()
    {
        // Property mappers
        CreateMap<Property, PropertyReadDto>();
        CreateMap<Property, PropertyUpdateDto>().ReverseMap();
        CreateMap<PropertyCreateDto, Property>();

        // PropertyType mappers
        CreateMap<PropertyType, PropertyTypeReadDto>();
    }
}