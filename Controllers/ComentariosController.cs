using Microsoft.AspNetCore.Mvc;
using TuristeiRV.API.DTOs;
using TuristeiRV.API.Services;
using TuristeiRV.API.Repositories;

namespace TuristeiRV.API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ComentariosController : ControllerBase
{
    private readonly IComentarioService _comentarioService;

    public ComentariosController(IComentarioService comentarioService)
    {
        _comentarioService = comentarioService;
    }

    [HttpGet]
    public async Task<IActionResult> ListarTodosComentarios()
    {
        var comentarios = await _comentarioService.ListarTodosComentariosAsync();
        return Ok(comentarios);
    }

    [HttpGet("ponto-turistico/{pontoTuristicoId}")]
    public async Task<IActionResult> ListarComentariosPontoTuristicoAsync(string pontoTuristicoId)
    {
        var comentarios = await _comentarioService.ListarComentariosPontoTuristicoAsync(pontoTuristicoId);
        if (comentarios == null || !comentarios.Any())
        {
            return NotFound(new { mensagem = "Nenhum comentário encontrado para o ponto turístico especificado." });
        }
        return Ok(comentarios);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterComentarioPorId(string id)
    {
        var comentario = await _comentarioService.ObterComentarioPorIdAsync(id);
        if (comentario == null)
        {
            return NotFound(new { mensagem = "Comentário não encontrado." });
        }
        return Ok(comentario);
    }

    [HttpPost]
    public async Task<IActionResult> AdicionarComentario([FromBody] ComentarioDto comentarioDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        double novaMedia = await _comentarioService.AdicionarComentarioAsync(comentarioDto);
        return Ok(new { mediaavaliacao = novaMedia });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarComentario(string id, [FromBody] ComentarioDto comentarioDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var comentarioExistente = await _comentarioService.ObterComentarioPorIdAsync(id);
        if (comentarioExistente == null)
        {
            return NotFound(new { mensagem = "Comentário não encontrado." });
        }

        await _comentarioService.AtualizarComentarioAsync(id, comentarioDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletarComentario(string id)
    {
        var comentarioExistente = await _comentarioService.ObterComentarioPorIdAsync(id);
        if (comentarioExistente == null)
        {
            return NotFound(new { mensagem = "Comentário não encontrado." });
        }

        await _comentarioService.DeletarComentarioAsync(id);
        return NoContent();
    }
}