using AutoMapper;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entities;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.Profiles
{
    public class MotoProfile : Profile
    {
        public MotoProfile()
        {
            CreateMap<CreateMotoDto, Moto>()
                 .ForMember(dest => dest.Id, opt => opt.Ignore()); ;

            CreateMap<Moto, ReadMotoDto>()
                .ForMember(dest => dest.Modelo, opt => opt.MapFrom(src => src.Modelo));

            CreateMap<UpdateMotoDto, Moto>();
        }
    }
}

