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
        private readonly IGeneroRepository _generoRepository;
        private readonly IMapper _mapper;

        public GeneroController(IGenericRepository<Genero> repository, IGeneroRepository generoRepository, IMapper mapper)
        {
            _repository = repository;
            _generoRepository = generoRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<GeneroDTO>>> ObtenerTodos()
        {
            var generos = await _repository.ObtenerTodos();
            var generosDTO = _mapper.Map<IEnumerable<GeneroDTO>>(generos);
            return Ok(generosDTO);
        }

        [HttpGet("{id}", Name = "GetGenero")]
        public async Task<ActionResult<GeneroDTO>> ObtenerPorid(int id)
        {
            var genero = await _repository.Obtener(id);
            if (genero == null)
                return NotFound();

            var generoDto = _mapper.Map<GeneroDTO>(genero);
            return Ok(generoDto);
        }

        [HttpGet("conLibros")]
        public async Task<ActionResult<IEnumerable<GeneroDTO>>> GeneroConLibros()
        {
            var generosConLibros = await _generoRepository.ObtenerConLibros();

            var generosConLibrosDTO = _mapper.Map<IEnumerable<GeneroDTO>>(generosConLibros);

            return Ok(generosConLibrosDTO);
        }

        [HttpPost]
        public async Task<ActionResult<GeneroDTO>> Crear(GeneroCreacionDTO generoCreacionDTO) 
        {
            var genero = _mapper.Map<Genero>(generoCreacionDTO);
            await _repository.Insertar(genero);
            var generoDTO = _mapper.Map<GeneroDTO>(genero);
            return Ok(generoDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, GeneroCreacionDTO generoCreacionDTO)
        {
            var generoBd = await _repository.Obtener(id);
            if (generoBd == null)
                return NotFound();

            _mapper.Map(generoCreacionDTO, generoBd);
            var resultado = await _repository.Actualizar(generoBd);
            if (resultado)
                return NoContent();

            return BadRequest();
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> Eliminar(int id)
        {
            var generoBd = await _repository.Obtener(id);
            if (generoBd == null)
                return NotFound();

            var resultado = await _repository.Eliminar(id);

            if (resultado)
                return NoContent();

            return BadRequest();

        }
    }
}
