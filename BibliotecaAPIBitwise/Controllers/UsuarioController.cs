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
    public class UsuarioController : ControllerBase
    {
        private readonly IGenericRepository<Usuario> _repository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioController(IGenericRepository<Usuario> repository, IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> ObtenerTodos()
        {
            var usuarios = await _repository.ObtenerTodos();
            var usuariosDto = _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);
            return Ok(usuariosDto);
        }

        [HttpPost("registro")]
        public async Task<IActionResult> Registro([FromBody] UsuarioRegistroDTO usuarioRegistroDTO)
        {

        }
    }
}
