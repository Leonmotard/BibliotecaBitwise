using AutoMapper;
using BibliotecaAPIBitwise.DAL.Interfaces;
using BibliotecaAPIBitwise.DTO;
using BibliotecaAPIBitwise.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaAPIBitwise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneroController : ControllerBase
    {
        private readonly IGenericRepository<Genero> _repository;
        private readonly IMapper _mapper;

        public GeneroController(IGenericRepository<Genero> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<GeneroDTO>> Crear(GeneroCreacionDTO generoCreacionDTO) 
        {
            var genero = _mapper.Map<Genero>(generoCreacionDTO);
            await _repository.Insertar(genero);
            var generoDTO = _mapper.Map<GeneroDTO>(genero);
            return Ok(generoDTO);
        } 
    }
}
