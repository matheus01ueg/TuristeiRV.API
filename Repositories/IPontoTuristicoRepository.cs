using TuristeiRV.API.Models.Entidade;

namespace TuristeiRV.API.Repositories;
public interface IPontoTuristicoRepository
{
    Task<List<PontoTuristico>> GetAllAsync();
    Task<PontoTuristico> GetByIdAsync(string id);
    Task<string> AddAsync(PontoTuristico pontoTuristico);
    Task UpdateAsync(string id, PontoTuristico pontoTuristico);
    Task UpdateImagensAsync(string id, List<Imagem> imagens);
    Task DeleteAsync(string id);
}