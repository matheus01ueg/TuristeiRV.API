using Google.Cloud.Firestore;
using TuristeiRV.API.Models.Entidade;

namespace TuristeiRV.API.Repositories;

public class ComentarioRepository : IComentarioRepository
{
    private readonly FirestoreDb _firestoreDb;
    private readonly IPontoTuristicoRepository _pontoTuristicoRepository;
    private const string CollectionName = "comentarios";
    public ComentarioRepository(FirestoreDb firestoreDb, IPontoTuristicoRepository pontoTuristicoRepository)
    {
        _firestoreDb = firestoreDb;
        _pontoTuristicoRepository = pontoTuristicoRepository;
    }

    public async Task<List<Comentario>> GetComentariosAsync()
    {
        Query query = _firestoreDb.Collection(CollectionName);
        QuerySnapshot snapshot = await query.GetSnapshotAsync();
        return snapshot.Documents.Select(d => d.ConvertTo<Comentario>()).ToList();
    }

    public async Task<Comentario> GetComentariosByIdAsync(string id)
    {
        DocumentReference docRef = _firestoreDb.Collection(CollectionName).Document(id);
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

        if (snapshot.Exists)
        {
            return snapshot.ConvertTo<Comentario>();
        }
        return null;
    }

    public async Task<List<Comentario>> GetComentariosByPontoTuristicoIdAsync(string pontoTuristicoId)
    {
        Query query = _firestoreDb.Collection(CollectionName).WhereEqualTo("pontoTuristicoId", pontoTuristicoId);
        QuerySnapshot snapshot = await query.GetSnapshotAsync();

        return snapshot.Documents.Select(d => d.ConvertTo<Comentario>()).ToList();
    }

    public async Task<double> AddComentarioAsync(Comentario comentario)
    {
        DocumentReference docRef = _firestoreDb.Collection(CollectionName).Document();
        await docRef.SetAsync(comentario);

        return await _pontoTuristicoRepository.AtualizarMediaAvaliacaoAsync(comentario.PontoTuristicoId);
    }

    public async Task UpdateComentarioAsync(string id, Comentario comentario)
    {
        DocumentReference docRef = _firestoreDb.Collection(CollectionName).Document(id);
        await docRef.SetAsync(comentario, SetOptions.Overwrite);

        await _pontoTuristicoRepository.AtualizarMediaAvaliacaoAsync(comentario.PontoTuristicoId);
    }

    public async Task DeleteComentarioAsync(string id)
    {
        DocumentReference docRef = _firestoreDb.Collection(CollectionName).Document(id);
        await docRef.DeleteAsync();        
    }
}