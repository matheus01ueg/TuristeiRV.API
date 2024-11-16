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

    public async Task<List<PontoTuristico>> GetByCategoriaIdAsync(string categoriaId)
    {
        Query query = _firestoreDb.Collection(CollectionName).WhereEqualTo("categoriaId", categoriaId);
        QuerySnapshot snapshot = await query.GetSnapshotAsync();

        return snapshot.Documents.Select(d => d.ConvertTo<PontoTuristico>()).ToList();
    }

    public async Task<List<PontoTuristico>> GetByTextAsync(string searchText)
    {
        var pontosTuristicos = new List<PontoTuristico>();

        Query nomeQuery = _firestoreDb.Collection(CollectionName).WhereGreaterThanOrEqualTo("nome", searchText).WhereLessThanOrEqualTo("nome", searchText + "\uf8ff");
        QuerySnapshot nomeSnapshot = await nomeQuery.GetSnapshotAsync();
        pontosTuristicos.AddRange(nomeSnapshot.Documents.Select(d => d.ConvertTo<PontoTuristico>()));

        Query descricaoQuery = _firestoreDb.Collection(CollectionName).WhereGreaterThanOrEqualTo("descricao", searchText).WhereLessThanOrEqualTo("descricao", searchText + "\uf8ff");
        QuerySnapshot descricaoSnapshot = await descricaoQuery.GetSnapshotAsync();
        pontosTuristicos.AddRange(descricaoSnapshot.Documents.Select(d => d.ConvertTo<PontoTuristico>()));

        Query enderecoQuery = _firestoreDb.Collection(CollectionName).WhereGreaterThanOrEqualTo("endereco", searchText).WhereLessThanOrEqualTo("endereco", searchText + "\uf8ff");
        QuerySnapshot enderecoSnapshot = await enderecoQuery.GetSnapshotAsync();
        pontosTuristicos.AddRange(enderecoSnapshot.Documents.Select(d => d.ConvertTo<PontoTuristico>()));

        return pontosTuristicos.GroupBy(p => p.Id).Select(g => g.First()).ToList();
    }

    public async Task<List<PontoTuristico>> GetByCategoriaIdAndTextAsync(string categoriaId, string searchText)
    {
        var pontosTuristicos = new List<PontoTuristico>();

        Query nomeQuery = _firestoreDb.Collection(CollectionName)
            .WhereEqualTo("categoriaId", categoriaId)
            .WhereGreaterThanOrEqualTo("nome", searchText)
            .WhereLessThanOrEqualTo("nome", searchText + "\uf8ff");
        QuerySnapshot nomeSnapshot = await nomeQuery.GetSnapshotAsync();
        pontosTuristicos.AddRange(nomeSnapshot.Documents.Select(d => d.ConvertTo<PontoTuristico>()));

        Query descricaoQuery = _firestoreDb.Collection(CollectionName)
            .WhereEqualTo("categoriaId", categoriaId)
            .WhereGreaterThanOrEqualTo("descricao", searchText)
            .WhereLessThanOrEqualTo("descricao", searchText + "\uf8ff");
        QuerySnapshot descricaoSnapshot = await descricaoQuery.GetSnapshotAsync();
        pontosTuristicos.AddRange(descricaoSnapshot.Documents.Select(d => d.ConvertTo<PontoTuristico>()));

        Query enderecoQuery = _firestoreDb.Collection(CollectionName)
            .WhereEqualTo("categoriaId", categoriaId)
            .WhereGreaterThanOrEqualTo("endereco", searchText)
            .WhereLessThanOrEqualTo("endereco", searchText + "\uf8ff");
        QuerySnapshot enderecoSnapshot = await enderecoQuery.GetSnapshotAsync();
        pontosTuristicos.AddRange(enderecoSnapshot.Documents.Select(d => d.ConvertTo<PontoTuristico>()));

        return pontosTuristicos.GroupBy(p => p.Id).Select(g => g.First()).ToList();
    }

    public async Task AtualizarMediaAvaliacaoAsync(string pontoTuristicoId)
    {
        var comentarios = await _firestoreDb
            .Collection("comentarios")
            .WhereEqualTo("pontoTuristicoId", pontoTuristicoId)
            .GetSnapshotAsync();

        if (comentarios.Documents.Count == 0)
        {
            await _firestoreDb.Collection(CollectionName)
                .Document(pontoTuristicoId)
                .UpdateAsync(new Dictionary<string, object> { { "avaliacao", 0 } });
            return;
        }

        var somaAvaliacoes = comentarios.Documents
            .Select(doc => doc.GetValue<int>("avaliacao"))
            .Sum();

        var media = (double)somaAvaliacoes / comentarios.Documents.Count;

        await _firestoreDb.Collection(CollectionName)
            .Document(pontoTuristicoId)
            .UpdateAsync(new Dictionary<string, object> { { "avaliacao", media } });
    }
}