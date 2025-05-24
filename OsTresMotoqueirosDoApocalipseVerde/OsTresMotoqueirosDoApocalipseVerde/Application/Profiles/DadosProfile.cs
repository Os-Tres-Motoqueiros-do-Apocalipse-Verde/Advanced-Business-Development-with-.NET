using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Entities;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.Profiles
{
    public class DadosProfile : Profile
    {
        public DadosProfile()
        {
            CreateMap<CreateDadosDto, Dados>()
                 .ForMember(dest => dest.Id, opt => opt.Ignore()); ;
            CreateMap<Dados, ReadDadosDto>();
            CreateMap<UpdateDadosDto, Dados>();

        }
    }
}
