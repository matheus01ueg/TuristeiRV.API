using Google.Cloud.Firestore;
using TuristeiRV.API.DTOs;
using TuristeiRV.API.Mappers;
using TuristeiRV.API.Models.Entidade;
using TuristeiRV.API.Repositories;

namespace TuristeiRV.API.Services;
public class ComentarioService : IComentarioService
{
    private readonly IComentarioRepository _comentarioRepository;

    public ComentarioService(IComentarioRepository comentarioRepository)
    {
        _comentarioRepository = comentarioRepository;
    }

    public async Task<List<ComentarioDto>> ListarTodosComentariosAsync()
    {
        var comentarios = await _comentarioRepository.GetComentariosAsync();
        return comentarios.Select(ComentarioMapper.ToDto).ToList();
    }

    public async Task<List<ComentarioDto>> ListarComentariosPontoTuristicoAsync(string pontoTuristicoId)
    {
        var comentarios = await _comentarioRepository.GetComentariosByPontoTuristicoIdAsync(pontoTuristicoId);
        return comentarios.Select(ComentarioMapper.ToDto).ToList();
    }

    public async Task<ComentarioDto> ObterComentarioPorIdAsync(string id)
    {
        var comentario = await _comentarioRepository.GetComentariosByIdAsync(id);
        return comentario?.ToDto();
    }

    public async Task<double> AdicionarComentarioAsync(ComentarioDto comentarioDto)
    {
        var comentario = ComentarioMapper.ToModel(comentarioDto);

        if (string.IsNullOrEmpty(comentario.Id))
        {
            comentario.Id = Guid.NewGuid().ToString();
        }

        return await _comentarioRepository.AddComentarioAsync(comentario);
    }

    public async Task AtualizarComentarioAsync(string id, ComentarioDto comentarioDto)
    {
        var comentario = ComentarioMapper.ToModel(comentarioDto);
        await _comentarioRepository.UpdateComentarioAsync(id, comentario);
    }

    public async Task DeletarComentarioAsync(string id)
    {
        await _comentarioRepository.DeleteComentarioAsync(id);
    }
}