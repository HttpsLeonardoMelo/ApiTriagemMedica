using AutoMapper;
using Business.TransferObjects;
using Data.Models;

namespace Business.Mappings
{
    public class MedicoMapper : Profile
    {
        public MedicoMapper()
        {
            CreateMap<Medico, MedicoDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.MedicoId))
                .ReverseMap();
        
        }
    }
}
