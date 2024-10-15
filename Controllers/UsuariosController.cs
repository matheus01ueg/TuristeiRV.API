using Microsoft.AspNetCore.Mvc;
using TuristeiRV.API.DTOs;
using TuristeiRV.API.Services;
using TuristeiRV.API.Repositories;
using System.Net.Mail;

namespace TuristeiRV.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuariosController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpPost("cadastrar")]
    public async Task<IActionResult> Cadastrar([FromBody] UsuarioDto usuarioDto, string senha)
    {
        try
        {
            var id = await _usuarioService.CadastrarUsuarioAsync(usuarioDto, senha);
            return Ok(new { Id = id });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("editar/{id}")]
    public async Task<IActionResult> Editar(string id, [FromBody] UsuarioDto usuarioDto)
    {
        try
        {
            await _usuarioService.EditarUsuarioAsync(id, usuarioDto);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("autenticar")]
    public async Task<IActionResult> Autenticar([FromBody] UsuarioLoginDto usuarioLoginDto)
    {
        var autenticado = await _usuarioService.AutenticarUsuarioAsync(usuarioLoginDto.email, usuarioLoginDto.senha);

        if (!autenticado)
        {
            return Unauthorized("Email ou senha inválidos.");
        }

        return Ok("Usuário autenticado com sucesso.");
    }
}
