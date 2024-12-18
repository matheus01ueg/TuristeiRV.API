using Microsoft.AspNetCore.Mvc;
using TuristeiRV.API.DTOs;
using TuristeiRV.API.Services;
using TuristeiRV.API.Repositories;
using TuristeiRV.API.Models.Entidade;

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

        var pontoTuristicoId = await _pontoTuristicoService.AdicionarPontoTuristicoAsync(pontoTuristicoDto);

        pontoTuristicoDto.Id = pontoTuristicoId;
        
        return Ok(pontoTuristicoDto);
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
    public async Task<IActionResult> ListarPontosTuristicos([FromQuery] string? categoriaId, [FromQuery] string? searchText)
    {
        List<PontoTuristicoDto> pontosTuristicos;

        if (!string.IsNullOrEmpty(categoriaId) && !string.IsNullOrEmpty(searchText))
        {
            pontosTuristicos = await _pontoTuristicoService.ListarPontosTuristicosPorSearchTextAndCategoriaAsync(categoriaId, searchText);
        }
        else if (!string.IsNullOrEmpty(categoriaId))
        {
            pontosTuristicos = await _pontoTuristicoService.ListarPontosTuristicosPorCategoriaAsync(categoriaId);
        }
        else if (!string.IsNullOrEmpty(searchText))
        {
            pontosTuristicos = await _pontoTuristicoService.ListarPontosTuristicosPorSearchTextAsync(searchText);
        }
        else
        {
            pontosTuristicos = await _pontoTuristicoService.ListarTodosPontosTuristicosAsync();
        }

        return Ok(pontosTuristicos);
    }

    [HttpPut("{id}/imagens")]
    public async Task<IActionResult> AtualizarImagensPontoTuristico(string id, [FromBody] List<Imagem> imagens)
    {
        try
        {
            // Verifica se o ponto turístico existe
            var pontoTuristicoExistente = await _pontoTuristicoService.ObterPontoTuristicoPorIdAsync(id);
            if (pontoTuristicoExistente == null)
            {
                return NotFound(new { mensagem = "Ponto turístico não encontrado." });
            }

            // Verifica se a lista de imagens está vazia
            if (imagens == null || !imagens.Any())
            {
                return BadRequest(new { mensagem = "A lista de imagens não pode estar vazia." });
            }

            // Tenta atualizar as imagens
            await _pontoTuristicoService.AtualizarImagensPontoTuristicoAsync(id, imagens);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            // Trata erros de argumento inválido (por exemplo, ID inválido)
            return BadRequest(new { mensagem = ex.Message });
        }
        catch (Exception ex)
        {
            // Trata erros gerais
            return StatusCode(500, new { mensagem = "Ocorreu um erro ao atualizar as imagens do ponto turístico.", detalhes = ex.Message });
        }
    }
}