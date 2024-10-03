using AutoMapper;
using Business.TransferObjects;
using Data.Models;


namespace Business.Mappings
{
    public class EspecialidadeMapper : Profile
    {
        public EspecialidadeMapper()
        {
            CreateMap<Especialidade, EspecialidadeDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.EspecialidadeId))
                .ReverseMap();
        
        }
    }
}
