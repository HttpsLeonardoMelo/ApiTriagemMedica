using AutoMapper;
using Business.TransferObjects;
using Data.Models;

namespace Business.Mappings
{
    public class UsuarioMapper : Profile
    {
        public UsuarioMapper()
        {
            CreateMap<Usuario, UsuarioDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UsuarioId))
                .ReverseMap();
        
        }
    }
}
