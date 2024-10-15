using Google.Cloud.Firestore;
using TuristeiRV.API.Models.Entidade;

namespace TuristeiRV.API.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly FirestoreDb _firestoreDb;
    private const string CollectionName = "categorias"; 

    public CategoriaRepository(FirestoreDb firestoreDb)
    {
        _firestoreDb = firestoreDb;
    }

    public async Task<List<Categoria>> GetCategoriasAsync()
    {
        Query query = _firestoreDb.Collection(CollectionName);
        QuerySnapshot snapshot = await query.GetSnapshotAsync();

        var categorias = snapshot.Documents
            .Select(doc => doc.ConvertTo<Categoria>())
            .ToList();

        return categorias;
    }

    public async Task<Categoria> GetCategoriaByIdAsync(string id)
    {
        DocumentReference docRef = _firestoreDb.Collection(CollectionName).Document(id);
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

        if (snapshot.Exists)
        {
            return snapshot.ConvertTo<Categoria>();
        }

        return null;
    }
}