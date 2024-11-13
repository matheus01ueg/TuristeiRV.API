using Google.Cloud.Firestore;
using TuristeiRV.API.Models.Entidade;

namespace TuristeiRV.API.Repositories;

public class PontoTuristicoRepository : IPontoTuristicoRepository
{
    private readonly FirestoreDb _firestoreDb;
    private const string CollectionName = "pontosTuristicos";
    public PontoTuristicoRepository(FirestoreDb firestoreDb)
    {
        _firestoreDb = firestoreDb;
    }

    public async Task<List<PontoTuristico>> GetAllAsync()
    {
        Query query = _firestoreDb.Collection(CollectionName);
        QuerySnapshot snapshot = await query.GetSnapshotAsync();

        return snapshot.Documents.Select(d => d.ConvertTo<PontoTuristico>()).ToList();
    }

    public async Task<PontoTuristico> GetByIdAsync(string id)
    {
        DocumentReference docRef = _firestoreDb.Collection(CollectionName).Document(id);
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

        if (snapshot.Exists)
        {
            return snapshot.ConvertTo<PontoTuristico>();
        }
        return null;
    }

    public async Task AddAsync(PontoTuristico pontoTuristico)
    {
        if (string.IsNullOrEmpty(pontoTuristico.Id))
        {
            throw new ArgumentException("O campo Id é obrigatório para adicionar um novo ponto turístico.");
        }

        DocumentReference docRef = _firestoreDb.Collection(CollectionName).Document(pontoTuristico.Id.ToString());
        await docRef.SetAsync(pontoTuristico);
    }

    public async Task UpdateAsync(string id, PontoTuristico pontoTuristico)
    {
        DocumentReference docRef = _firestoreDb.Collection(CollectionName).Document(id);
        await docRef.SetAsync(pontoTuristico, SetOptions.Overwrite);
    }

    public async Task UpdateImagensAsync(string id, List<Imagem> imagens)
    {
        DocumentReference docRef = _firestoreDb.Collection(CollectionName).Document(id);

        Dictionary<string, object> update = new Dictionary<string, object>
        {
            { "imagens", imagens }
        };

        await docRef.UpdateAsync(update);
    }

    public async Task DeleteAsync(string id)
    {
        DocumentReference docRef = _firestoreDb.Collection(CollectionName).Document(id);
        await docRef.DeleteAsync();
    }
}