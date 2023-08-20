using AutoMapper;
using BibliotecaAPIBitwise.DTO;
using BibliotecaAPIBitwise.Models;

namespace BibliotecaAPIBitwise.Utilidades
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Autor, AutorDTO>()
                .ForMember(d => d.FechaNacimiento,
                opt => opt.MapFrom(o => o.FechaNacimiento.ToString("dd/MM/yyyy")));

            CreateMap<AutorCreacionDTO, Autor>()
                .ForMember(d => d.FechaNacimiento,
                opt => opt.MapFrom(o => DateTime.Parse(o.FechaNacimiento)));

            CreateMap<Comentario, ComentarioDTO>().ReverseMap();
        }
    }
}
