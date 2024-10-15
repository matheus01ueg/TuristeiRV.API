using TuristeiRV.API.DTOs;

namespace TuristeiRV.API.Services;
public interface IPontoTuristicoService
{
    Task<PontoTuristicoDto> ObterPontoTuristicoPorIdAsync(string id);
    Task AdicionarPontoTuristicoAsync(PontoTuristicoDto pontoTuristicoDto);
    Task AtualizarPontoTuristicoAsync(string id, PontoTuristicoDto pontoTuristicoDto);
    Task DeletarPontoTuristicoAsync(string id);
    Task<List<PontoTuristicoDto>> ListarTodosPontosTuristicosAsync();
}