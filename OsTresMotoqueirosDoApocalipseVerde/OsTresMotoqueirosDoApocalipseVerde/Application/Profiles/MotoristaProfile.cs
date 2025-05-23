using AutoMapper;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entities;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.Profiles
{
    public class MotoristaProfile : Profile
    {
        public MotoristaProfile()
        {
            CreateMap<CreateMotoristaDto, Motorista>();

            CreateMap<Motorista, ReadMotoristaDto>()
                .ForMember(dest => dest.Dados, opt => opt.MapFrom(src => src.Dados));

            CreateMap<UpdateMotoristaDto, Motorista>();
        }
    }
}
