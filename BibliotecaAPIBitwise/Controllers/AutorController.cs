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
    public class AutorController : ControllerBase
    {
        private readonly IGenericRepository<Autor> _repository;
        private readonly IMapper _mapper;

        public AutorController(IGenericRepository<Autor> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AutorDTO>>> ObtenerTodos()
        {
            var autores = await _repository.ObtenerTodos();
            var autoresDTO = _mapper.Map<IEnumerable<AutorDTO>>(autores);
            return Ok(autoresDTO);
        }

        [HttpGet("{id}", Name = "GetAutor")]
        public async Task<ActionResult<AutorDTO>> ObtenerPorid(int id)
        {
            var autor = await _repository.Obtener(id);
            if (autor == null)
                return NotFound();

            var autorDto = _mapper.Map<AutorDTO>(autor);
            return Ok(autorDto);
        }

        [HttpPost]
        public async Task<ActionResult> Crear(AutorCreacionDTO autorCreacionDTO)
        {
            var autor = _mapper.Map<Autor>(autorCreacionDTO);

            var resultado = await _repository.Insertar(autor);
            if (!resultado)
            {
                return NotFound();
            }
            var autorDto = _mapper.Map<AutorDTO>(autor);

            return new CreatedAtRouteResult("GetAutor", new { id = autor.Id }, autorDto);
           ;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, AutorCreacionDTO autorCreacionDTO)
        {
            var autorBd = await _repository.Obtener(id);
            if (autorBd == null)
                return NotFound();

            _mapper.Map(autorCreacionDTO, autorBd);
            var resultado = await _repository.Actualizar(autorBd);
            if (resultado)
                return NoContent();

            return BadRequest();
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult>Eliminar(int id)
        {
            var autorBd = await _repository.Obtener(id);
            if (autorBd == null)
                return NotFound();

            var resultado = await _repository.Eliminar(id);

            if (resultado)
              return NoContent();

            return BadRequest();

        }


    }
}
