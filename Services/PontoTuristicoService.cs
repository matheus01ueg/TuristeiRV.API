using Google.Cloud.Firestore;
using TuristeiRV.API.DTOs;
using TuristeiRV.API.Mappers;
using TuristeiRV.API.Models.Entidade;
using TuristeiRV.API.Repositories;

namespace TuristeiRV.API.Services;
public class PontoTuristicoService : IPontoTuristicoService
{
    private readonly IPontoTuristicoRepository _pontoTuristicoRepository;

    public PontoTuristicoService(IPontoTuristicoRepository pontoTuristicoRepository)
    {
        _pontoTuristicoRepository = pontoTuristicoRepository;
    }

    public async Task<PontoTuristicoDto> ObterPontoTuristicoPorIdAsync(string id)
    {
        var pontoTuristico = await _pontoTuristicoRepository.GetByIdAsync(id);
        return pontoTuristico?.ToDto(); 
    }

    public async Task AdicionarPontoTuristicoAsync(PontoTuristicoDto dto)
    {
        var pontoTuristico = dto.ToModel();

        // Gera um ID único se o campo Id estiver nulo ou vazio
        if (string.IsNullOrEmpty(pontoTuristico.Id))
        {
            pontoTuristico.Id = Guid.NewGuid().ToString();
        }

        await _pontoTuristicoRepository.AddAsync(pontoTuristico);
    }

    public async Task AtualizarPontoTuristicoAsync(string id, PontoTuristicoDto pontoTuristicoDto)
    {
        var pontoTuristico = pontoTuristicoDto.ToModel();
        await _pontoTuristicoRepository.UpdateAsync(id, pontoTuristico);
    }
    public async Task AtualizarImagensPontoTuristicoAsync(string id, List<Imagem> imagens)
    {
        await _pontoTuristicoRepository.UpdateImagensAsync(id, imagens);
    }

    public async Task DeletarPontoTuristicoAsync(string id)
    {
        await _pontoTuristicoRepository.DeleteAsync(id);
    }

    public async Task<List<PontoTuristicoDto>> ListarTodosPontosTuristicosAsync()
    {
        var pontosTuristicos = await _pontoTuristicoRepository.GetAllAsync();
        return pontosTuristicos.Select(p => p.ToDto()).ToList(); 
    }
}