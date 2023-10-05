using API.Dtos;
using AutoMapper;
using Domain.Entities;

namespace API.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile(){
        CreateMap<Producto, ProductoDto>().ReverseMap();
    }
}
