using TuristeiRV.API.Models.Entidade;

namespace TuristeiRV.API.Repositories;
public interface ICategoriaRepository
    {
        Task<List<Categoria>> GetCategoriasAsync();
        Task<Categoria> GetCategoriaByIdAsync(string id);
    }