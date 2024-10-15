using TuristeiRV.API.DTOs;

namespace TuristeiRV.API.Services;
public interface ICategoriaService
{
    Task<List<CategoriaDto>> GetCategoriasAsync();
    Task<CategoriaDto> GetCategoriaByIdAsync(string id);
}