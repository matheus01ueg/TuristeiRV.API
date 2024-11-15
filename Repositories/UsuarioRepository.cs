using FirebaseAdmin.Auth;
using Google.Cloud.Firestore;
using TuristeiRV.API.Models.Entidade;

namespace TuristeiRV.API.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly FirestoreDb firestoreDb;

    public UsuarioRepository(FirestoreDb firestoreDb)
    {
        this.firestoreDb = firestoreDb;
    }

    public async Task SalvarUsuarioAsync(string uid, Usuario usuario)
    {
        DocumentReference docRef = firestoreDb.Collection("users").Document(uid);
        await docRef.SetAsync(usuario);
    }

    public async Task<Usuario> ObterUsuarioAsync(string uid)
    {
        DocumentReference docRef = firestoreDb.Collection("users").Document(uid);
        DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
        return snapshot.Exists ? snapshot.ConvertTo<Usuario>() : null;
    }
}