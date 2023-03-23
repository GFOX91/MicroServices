using AutoMapper;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Profiles;

/// <summary>
/// used to add auto mappings between DTOs and Model 
/// </summary>
public class PlatformsProfile : Profile
{
    public PlatformsProfile()
    {
        // Source -> Target
        CreateMap<Platform, PlatformReadDto>();
        CreateMap<PlatformCreateDto, Platform>();
    }
}
