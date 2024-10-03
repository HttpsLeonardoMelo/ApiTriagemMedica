using AutoMapper;
using Business.TransferObjects;
using Data.Models;

namespace Business.Mappings
{
    public class AtendimentoMapper : Profile
    {
        public AtendimentoMapper()
        {
            CreateMap<Atendimento, AtendimentoDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AtendimentoId))
                .ReverseMap();
        
        }
    }
}
