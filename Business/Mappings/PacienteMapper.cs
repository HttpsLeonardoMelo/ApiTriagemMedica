using AutoMapper;
using Business.TransferObjects;
using Data.Models;

namespace Business.Mappings
{
    public class PacienteMapper : Profile
    {
        public PacienteMapper()
        {
            CreateMap<Paciente, PacienteDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PacienteId))
                .ReverseMap();
        
        }
    }
}
