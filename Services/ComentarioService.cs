using TuristeiRV.API.DTOs;
using TuristeiRV.API.Mappers;
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
        var comentarios = await _comentarioRepository.GetAllAsync();
        return comentarios.Select(ComentarioMapper.ToDto).ToList();
    }

    public async Task<ComentarioDto> ObterComentarioPorIdAsync(string id)
    {
        var comentario = await _comentarioRepository.GetByIdAsync(id);
        return comentario?.ToDto();
    }

    public async Task AdicionarComentarioAsync(ComentarioDto comentarioDto)
    {
        var comentario = ComentarioMapper.ToModel(comentarioDto);
        await _comentarioRepository.AddAsync(comentario);
    }

    public async Task AtualizarComentarioAsync(string id, ComentarioDto comentarioDto)
    {
        var comentario = ComentarioMapper.ToModel(comentarioDto);
        await _comentarioRepository.UpdateAsync(id, comentario);
    }

    public async Task DeletarComentarioAsync(string id)
    {
        await _comentarioRepository.DeleteAsync(id);
    }
}