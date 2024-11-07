using Google.Cloud.Firestore;

namespace TuristeiRV.API.Models.Entidade;

[FirestoreData]
public class Categoria
{
    [FirestoreDocumentId]
    public string Id { get; set; }

    [FirestoreProperty("nome")]
    public string Nome { get; set; }

}