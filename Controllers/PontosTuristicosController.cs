using Microsoft.AspNetCore.Mvc;
using TuristeiRV.API.DTOs;
using TuristeiRV.API.Services;
using TuristeiRV.API.Repositories;

namespace TuristeiRV.API.Controllers;

//[ApiController]
[Route("api/[controller]")]

public class PontosTuristicosController : ControllerBase
{
    private readonly IPontoTuristicoService _pontoTuristicoService;
    public PontosTuristicosController(IPontoTuristicoService pontoTuristicoService)
    {
        _pontoTuristicoService = pontoTuristicoService;
    }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPontoTuristicoPorId(string id)
        {
            var pontoTuristico = await _pontoTuristicoService.ObterPontoTuristicoPorIdAsync(id);
            if (pontoTuristico == null)
            {
                return NotFound(new { mensagem = "Ponto turístico não encontrado." });
            }
            return Ok(pontoTuristico);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarPontoTuristico([FromBody] PontoTuristicoDto pontoTuristicoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _pontoTuristicoService.AdicionarPontoTuristicoAsync(pontoTuristicoDto);
            return Ok(pontoTuristicoDto);
            //return CreatedAtAction(nameof(ObterPontoTuristicoPorId), new { id = pontoTuristicoDto.Id }, pontoTuristicoDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarPontoTuristico(string id, [FromBody] PontoTuristicoDto pontoTuristicoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pontoTuristicoExistente = await _pontoTuristicoService.ObterPontoTuristicoPorIdAsync(id);
            if (pontoTuristicoExistente == null)
            {
                return NotFound(new { mensagem = "Ponto turístico não encontrado." });
            }

            await _pontoTuristicoService.AtualizarPontoTuristicoAsync(id, pontoTuristicoDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarPontoTuristico(string id)
        {
            var pontoTuristicoExistente = await _pontoTuristicoService.ObterPontoTuristicoPorIdAsync(id);
            if (pontoTuristicoExistente == null)
            {
                return NotFound(new { mensagem = "Ponto turístico não encontrado." });
            }

            await _pontoTuristicoService.DeletarPontoTuristicoAsync(id);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> ListarTodosPontosTuristicos()
        {
            var pontosTuristicos = await _pontoTuristicoService.ListarTodosPontosTuristicosAsync();
            return Ok(pontosTuristicos);
        }
}