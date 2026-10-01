using AutoMapper;
using webpract4.DTOs;
using webpract4.Models;

namespace webpract4.Mapping;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Team, TeamDto>().ReverseMap();
    }
}
