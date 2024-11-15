using TuristeiRV.API.DTOs;
using TuristeiRV.API.Models.Entidade;

namespace TuristeiRV.API.Services;
public interface IUsuarioService
{
    Task<UsuarioDto> RegistrarUsuarioAsync(Usuario usuario, string senha);
    Task<UsuarioDto> AutenticarUsuarioAsync(string email, string senha);
}
