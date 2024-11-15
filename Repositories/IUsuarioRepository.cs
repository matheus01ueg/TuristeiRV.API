using TuristeiRV.API.Models.Entidade;

namespace TuristeiRV.API.Repositories;
public interface IUsuarioRepository
{
    Task SalvarUsuarioAsync(string uid, Usuario usuario);
    Task<Usuario> ObterUsuarioAsync(string uid);
}
