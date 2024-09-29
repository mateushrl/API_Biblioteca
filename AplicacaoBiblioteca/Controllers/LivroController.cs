using AplicacaoBiblioteca.Interfaces;
using AplicacaoBiblioteca.Models;
using AplicacaoBiblioteca.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AplicacaoBiblioteca.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class LivroController : ControllerBase
    {
        private readonly ILivroRepository _livroService;
        private readonly IMapper _mapper;

        public LivroController(ILivroRepository livroService, IMapper mapper)
        {
            _livroService = livroService;
            _mapper = mapper;
        }

        /// <summary>
        /// Endpoint para listar todos os livros do bando de dados
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("ListaLivros")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<LivroDTO> Livros = _mapper.Map<List<LivroDTO>>(_livroService.ObterLivros());
                return Ok(Livros);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Endpoint para deletar livro pelo Id
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("DeletaLivro/{id}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                int numLinhasAfetada = _livroService.DeletarLivro(id);

                if(numLinhasAfetada != 0)
                    return Ok();

                return NotFound("Registro não encontrado.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Endpoint para criar livro
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("CriaLivro")]
        [HttpPost]
        public IActionResult Post(LivroDTO livroDTO)
        {
            try
            {
                Livro novoLivro = _livroService.CriarLivro(_mapper.Map<Livro>(livroDTO));
                livroDTO = _mapper.Map<LivroDTO>(novoLivro);

                return Ok(livroDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Endpoint para criar livro
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("AtualizaLivro")]
        [HttpPut]
        public IActionResult Put(LivroDTO livroDTO)
        {
            try
            {
                int numLinhasAfetada = _livroService.AtualizarLivro(_mapper.Map<Livro>(livroDTO));
                
                if (numLinhasAfetada != 0)
                    return Ok();

                return NotFound("Registro não encontrado.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
