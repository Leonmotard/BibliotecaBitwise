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
    [ResponseCache (CacheProfileName = "Por defecto")]
    public class LibroController : ControllerBase
    {
        private readonly IGenericRepository<Libro> _repository;
        private readonly ILibroRepository _libroRepository;
        private readonly IMapper _mapper;

        public LibroController(IGenericRepository<Libro> repository, ILibroRepository libroRepository, IMapper mapper)
        {
            _repository = repository;
            _libroRepository = libroRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibroDTO>>> ObtenerTodos()
        {
            var libros = await _repository.ObtenerTodos();
            var librosDto = _mapper.Map<IEnumerable<LibroDTO>>(libros);
            return Ok(librosDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LibroDTO>> Obtener(int id)
        {
            var libro = await _repository.Obtener(id);

            if (libro == null)
                return NotFound();

            var libroDTO = _mapper.Map<LibroDTO>(libro);
            return Ok(libroDTO);
        }

       
        [HttpGet("dataRelacionada/{id}")]
        [ResponseCache(CacheProfileName = "Mas rapido")]
        public async Task<ActionResult<LibroDTO>> ObtenerRelacionada(int id)
        {
            var libro = await _libroRepository.ObtenerPorIdConRelacion(id);

            if (libro == null)
                return NotFound();

            var libroDTO = _mapper.Map<LibroDTO>(libro);
            return Ok(libroDTO);
        }

        [HttpPost]
        public async Task<ActionResult<LibroDTO>> Crear(LibroCreacionDTO libroCreacionDTO)
        {
            var libro = _mapper.Map<Libro>(libroCreacionDTO);
            await _repository.Insertar(libro);

            var libroDTO = _mapper.Map<LibroDTO>(libro);
            return CreatedAtAction(nameof(Obtener), new {id = libro.Id}, libroDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, LibroCreacionDTO libroCreacionDTO)
        {
            var libroBd = await _repository.Obtener(id);
            if (libroBd == null)
                return NotFound();

            _mapper.Map(libroCreacionDTO, libroBd);
            var resultado = await _repository.Actualizar(libroBd);
            if (resultado)
                return NoContent();

            return BadRequest();
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> Eliminar(int id)
        {
            var libroBd = await _repository.Obtener(id);
            if (libroBd == null)
                return NotFound();

            var resultado = await _repository.Eliminar(id);

            if (resultado)
                return NoContent();

            return BadRequest();

        }
    }

}
