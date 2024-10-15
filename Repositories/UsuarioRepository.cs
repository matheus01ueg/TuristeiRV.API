using FirebaseAdmin.Auth;
using Google.Cloud.Firestore;
using TuristeiRV.API.Models.Entidade;

namespace TuristeiRV.API.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly FirestoreDb _firestoreDb;
    private const string CollectionName = "users";
    public UsuarioRepository(FirestoreDb firestoreDb)
    {
        _firestoreDb = firestoreDb;
    }

    public async Task<string> AddUsuarioAsync(Usuario usuario)
    {
        DocumentReference docRef = _firestoreDb.Collection(CollectionName).Document(usuario.Id);
        await docRef.SetAsync(usuario);
        return usuario.Id;
    }

    public async Task UpdateUsuarioAsync(string id, Usuario usuario)
    {
        DocumentReference docRef = _firestoreDb.Collection(CollectionName).Document(id);
        await docRef.SetAsync(usuario, SetOptions.Overwrite);  
    }

    public async Task<Usuario> GetUsuarioByEmailAsync(string email)
    {
        Query query = _firestoreDb.Collection(CollectionName).WhereEqualTo("email", email);
        QuerySnapshot snapshot = await query.GetSnapshotAsync();

        if (snapshot.Documents.Count > 0)
        {
            return snapshot.Documents[0].ConvertTo<Usuario>();  
        }

        return null;
    }
}