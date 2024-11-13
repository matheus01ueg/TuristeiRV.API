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

    public async Task<string> AddAsync(PontoTuristico pontoTuristico)
    {
        DocumentReference docRef;

        if (string.IsNullOrEmpty(pontoTuristico.Id))
        {
            docRef = _firestoreDb.Collection(CollectionName).Document(); 
            pontoTuristico.Id = docRef.Id; 
        }
        else
        {
            docRef = _firestoreDb.Collection(CollectionName).Document(pontoTuristico.Id); 
        }

        await docRef.SetAsync(pontoTuristico); 
        return docRef.Id; 
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