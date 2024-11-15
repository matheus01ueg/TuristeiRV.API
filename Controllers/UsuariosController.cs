using Microsoft.AspNetCore.Mvc;
using TuristeiRV.API.DTOs;
using TuristeiRV.API.Services;
using TuristeiRV.API.Repositories;
using System.Net.Mail;
using TuristeiRV.API.Models.Entidade;

namespace TuristeiRV.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioService usuarioService;

    public UsuariosController(IUsuarioService usuarioService)
    {
        this.usuarioService = usuarioService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegistrarUsuario([FromBody] UsuarioDto usuarioDto, string senha)
    {
        try
        {
            // Converte o DTO para o modelo
            var usuario = new Usuario
            {
                Email = usuarioDto.Email,
                Nome = usuarioDto.Nome,
                Sobrenome = usuarioDto.Sobrenome,
                Endereco = usuarioDto.Endereco,
                Numero = usuarioDto.Numero,
                Bairro = usuarioDto.Bairro,
                Cep = usuarioDto.Cep,
                Complemento = usuarioDto.Complemento
            };

            var result = await usuarioService.RegistrarUsuarioAsync(usuario, senha);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> AutenticarUsuario([FromBody] LoginRequestDto request)
    {
        try
        {
            var result = await usuarioService.AutenticarUsuarioAsync(request.Email, request.Password);
            return Ok(result);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized(new { error = "Credenciais inválidas." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}

public class LoginRequestDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}
