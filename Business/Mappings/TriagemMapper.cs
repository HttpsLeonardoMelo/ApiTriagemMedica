using AutoMapper;
using Business.TransferObjects;
using Data.Models;

namespace Business.Mappings
{
    public class TriagemMapper : Profile
    {
        public TriagemMapper()
        {
            CreateMap<Triagem, TriagemDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.TriagemId))
                .ReverseMap();
        
        }
    }
}
