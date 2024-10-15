using Google.Cloud.Firestore;

namespace TuristeiRV.API.Models.Entidade;

[FirestoreData]
public class Categoria
{
    [FirestoreProperty("id")]
    public int Id { get; set; }

    [FirestoreProperty("nome")]
    public string Nome { get; set; }

}