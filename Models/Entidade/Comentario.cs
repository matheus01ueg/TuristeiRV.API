using Google.Cloud.Firestore;

namespace TuristeiRV.API.Models.Entidade;

[FirestoreData]
public class Comentario
{
    [FirestoreProperty("avaliacao")]
    public double Avaliacao { get; set; }

    [FirestoreProperty("comentario")]
    public string ComentarioTexto { get; set; }

    [FirestoreProperty("data")]
    public string Data { get; set; }

    [FirestoreDocumentId]
    public string Id { get; set; }

    [FirestoreProperty("nome")]
    public string Nome { get; set; }

    [FirestoreProperty("pontoTuristicoId")]
    public string PontoTuristicoId { get; set; }

    [FirestoreProperty("resposta")]
    public string Resposta { get; set; }

    [FirestoreProperty("userId")]
    public string UserId { get; set; }
}