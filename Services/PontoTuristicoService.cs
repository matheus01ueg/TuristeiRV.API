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

    public async Task<string> AdicionarPontoTuristicoAsync(PontoTuristicoDto dto)
    {
        var pontoTuristico = dto.ToModel();

        if (string.IsNullOrEmpty(pontoTuristico.Id))
        {
            pontoTuristico.Id = Guid.NewGuid().ToString();
        }

        var DocumentId = await _pontoTuristicoRepository.AddAsync(pontoTuristico);
        return DocumentId;
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

    public async Task<List<PontoTuristicoDto>> ListarPontosTuristicosPorCategoriaAsync(string categoriaId)
    {
        var pontosTuristicos = await _pontoTuristicoRepository.GetByCategoriaIdAsync(categoriaId);
        return pontosTuristicos.Select(p => p.ToDto()).ToList(); 
    }

    public async Task<List<PontoTuristicoDto>> ListarPontosTuristicosPorSearchTextAsync(string searchText)
    {
        var pontosTuristicos = await _pontoTuristicoRepository.GetByTextAsync(searchText);
        return pontosTuristicos.Select(p => p.ToDto()).ToList();
    }

    public async Task<List<PontoTuristicoDto>> ListarPontosTuristicosPorSearchTextAndCategoriaAsync(string categoriaId, string searchText)
    {
        var pontosTuristicos = await _pontoTuristicoRepository.GetByCategoriaIdAndTextAsync(categoriaId, searchText);
        return pontosTuristicos.Select(p => p.ToDto()).ToList();
    }
}