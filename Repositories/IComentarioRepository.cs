using TuristeiRV.API.Models.Entidade;

namespace TuristeiRV.API.Repositories;
public interface IComentarioRepository
{
    Task<List<Comentario>> GetAllAsync();
    Task<Comentario> GetByIdAsync(string id);
    Task AddAsync(Comentario comentario);
    Task UpdateAsync(string id, Comentario comentario);
    Task DeleteAsync(string id);
}