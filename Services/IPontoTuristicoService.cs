using TuristeiRV.API.DTOs;
using TuristeiRV.API.Models.Entidade;

namespace TuristeiRV.API.Services;
public interface IPontoTuristicoService
{
    Task<PontoTuristicoDto> ObterPontoTuristicoPorIdAsync(string id);
    Task<string> AdicionarPontoTuristicoAsync(PontoTuristicoDto pontoTuristicoDto);
    Task AtualizarPontoTuristicoAsync(string id, PontoTuristicoDto pontoTuristicoDto);
    Task AtualizarImagensPontoTuristicoAsync(string id, List<Imagem> imagens);
    Task DeletarPontoTuristicoAsync(string id);
    Task<List<PontoTuristicoDto>> ListarTodosPontosTuristicosAsync();
    Task<List<PontoTuristicoDto>> ListarPontosTuristicosPorCategoriaAsync(string categoriaId);
    Task<List<PontoTuristicoDto>> ListarPontosTuristicosPorSearchTextAsync(string searchText);
    Task<List<PontoTuristicoDto>> ListarPontosTuristicosPorSearchTextAndCategoriaAsync(string categoriaId, string searchText);
}