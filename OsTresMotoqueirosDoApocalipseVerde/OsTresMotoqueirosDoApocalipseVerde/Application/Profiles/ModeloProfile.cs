using AutoMapper;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entities;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.Profiles
{
    public class ModeloProfile : Profile
    {
        public ModeloProfile()
        {
            CreateMap<CreateModeloDto, Modelo>()
                 .ForMember(dest => dest.Id, opt => opt.Ignore()); ;
            CreateMap<Modelo, ReadModeloDto>();
            CreateMap<UpdateModeloDto, Modelo>();

        }
    }
}
