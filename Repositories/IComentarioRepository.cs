using TuristeiRV.API.Models.Entidade;

namespace TuristeiRV.API.Repositories;
public interface IComentarioRepository
{
    Task<List<Comentario>> GetComentariosAsync();
    Task<List<Comentario>> GetComentariosByPontoTuristicoIdAsync(string pontoTuristicoId);
    Task<Comentario> GetComentariosByIdAsync(string id);
    Task<double> AddComentarioAsync(Comentario comentario);
    Task UpdateComentarioAsync(string id, Comentario comentario);
    Task DeleteComentarioAsync(string id);
}