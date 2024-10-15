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

        await _comentarioService.AdicionarComentarioAsync(comentarioDto);
        return CreatedAtAction(nameof(ObterComentarioPorId), new { id = comentarioDto.Id }, comentarioDto);
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