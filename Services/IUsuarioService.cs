using TuristeiRV.API.DTOs;

namespace TuristeiRV.API.Services;
public interface IUsuarioService
{
    Task<string> CadastrarUsuarioAsync(UsuarioDto usuarioDto, string senha);
    Task EditarUsuarioAsync(string id, UsuarioDto usuarioDto);
    Task<bool> AutenticarUsuarioAsync(string email, string senha);
}
