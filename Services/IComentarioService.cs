using TuristeiRV.API.DTOs;

namespace TuristeiRV.API.Services;
public interface IComentarioService
{
    Task<List<ComentarioDto>> ListarTodosComentariosAsync();
    Task<ComentarioDto> ObterComentarioPorIdAsync(string id);
    Task AdicionarComentarioAsync(ComentarioDto comentarioDto);
    Task AtualizarComentarioAsync(string id, ComentarioDto comentarioDto);
    Task DeletarComentarioAsync(string id);
}