using AutoMapper;
using BibliotecaAPIBitwise.DAL.Interfaces;
using BibliotecaAPIBitwise.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaAPIBitwise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly IGenericRepository<Libro> _repository;
        private readonly IMapper _mapper;

        public LibroController(IGenericRepository<Libro> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
