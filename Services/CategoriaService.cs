using TuristeiRV.API.DTOs;
using TuristeiRV.API.Mappers;
using TuristeiRV.API.Repositories;

namespace TuristeiRV.API.Services;
public class CategoriaService : ICategoriaService
{
    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriaService(ICategoriaRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
    }

    public async Task<List<CategoriaDto>> GetCategoriasAsync()
    {
        var categorias = await _categoriaRepository.GetCategoriasAsync();
        return categorias.Select(c => c.ToDto()).ToList();
    }

    public async Task<CategoriaDto> GetCategoriaByIdAsync(string id)
    {
        var categoria = await _categoriaRepository.GetCategoriaByIdAsync(id);
        return categoria?.ToDto();
    }
}