using API.Dtos;
using AutoMapper;
using Domain.Entities;

namespace API.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile(){
        CreateMap<Producto, ProductoDto>()
        .ForMember(x => x.Marca, dest => dest.MapFrom(r => r.Marca.Descripcion))
        .ReverseMap();

        CreateMap<Marca, MarcaDto>().ReverseMap();
    }
}
