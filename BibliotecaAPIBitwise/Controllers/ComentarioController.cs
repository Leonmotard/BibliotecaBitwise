﻿using AutoMapper;
using BibliotecaAPIBitwise.DAL.Interfaces;
using BibliotecaAPIBitwise.DTO;
using BibliotecaAPIBitwise.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaAPIBitwise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentarioController : ControllerBase
    {
        private readonly IGenericRepository<Comentario> _repository;
        private readonly IMapper _mapper;

        public ComentarioController(IGenericRepository<Comentario> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Post(ComentarioDTO comentarioDTO)
        {
            var comentario = _mapper.Map<Comentario>(comentarioDTO);
            var respuesta = await _repository.Insertar(comentario);

            if (!respuesta)
            {
                return BadRequest();
            }

            var dto = _mapper.Map<ComentarioDTO>(comentario);
            return Ok(dto);
        }
    }
}
