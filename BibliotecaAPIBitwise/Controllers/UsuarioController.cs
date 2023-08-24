using AutoMapper;
using BibliotecaAPIBitwise.DAL.Interfaces;
using BibliotecaAPIBitwise.DTO;
using BibliotecaAPIBitwise.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BibliotecaAPIBitwise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IGenericRepository<Usuario> _repository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        protected RespuestaApi _respuesta;

        public UsuarioController(IGenericRepository<Usuario> repository, IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
            _usuarioRepository = usuarioRepository;
            this._respuesta = new(); 
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
            var validacionNombre = await _usuarioRepository.IsUniqueUser(usuarioRegistroDTO.NombreUsuario);
            if (!validacionNombre)
            {
                _respuesta.StatusCode = HttpStatusCode.BadRequest;
                _respuesta.IsSuccess = false;
                _respuesta.ErrorMenssages.Add("El nombre del usuario ya existe");
                return BadRequest(_respuesta);
            }

            var usuario = await _usuarioRepository.Registrar(usuarioRegistroDTO);
            if(usuario == null)
            {
                _respuesta.StatusCode = HttpStatusCode.BadRequest;
                _respuesta.IsSuccess = false;
                _respuesta.ErrorMenssages.Add("El nombre del usuario ya existe");
                return BadRequest(_respuesta);
            }

            _respuesta.StatusCode = HttpStatusCode.OK;
            _respuesta.IsSuccess = true;
            return Ok(_respuesta);

        }
    }
}
