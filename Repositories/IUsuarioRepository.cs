using TuristeiRV.API.Models.Entidade;

namespace TuristeiRV.API.Repositories;
public interface IUsuarioRepository
{
    Task<string> AddUsuarioAsync(Usuario usuario);
    Task UpdateUsuarioAsync(string id, Usuario usuario);
    Task<Usuario> GetUsuarioByEmailAsync(string email);  
}
