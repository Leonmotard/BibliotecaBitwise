﻿using AutoMapper;
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
            CreateMap<ComentarioListaDTO, Comentario>().ReverseMap();

            CreateMap<Libro, LibroDTO>()
                .ForMember(d => d.NombreAutor, o => o.MapFrom(src => src.Autor.Nombre))
                .ForMember(d => d.NombreGenero, o => o.MapFrom(src => src.Genero.Nombre))
                .ForMember(d => d.FechaLanzamiento
                ,opt=> opt.MapFrom(o => o.FechaLanzamiento.ToString("dd/MM/yyyy")));

            CreateMap<LibroCreacionDTO, Libro>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Autor, o => o.Ignore())
                .ForMember(d => d.Genero, o => o.Ignore());

            CreateMap<Genero, GeneroDTO>().ReverseMap();
            CreateMap<GeneroCreacionDTO, Genero>();

            CreateMap<Usuario, UsuarioDTO>().ReverseMap(); 


            


        }
    }
}
