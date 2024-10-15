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
    public Timestamp Data { get; set; }

    [FirestoreProperty("id")]
    public int Id { get; set; }

    [FirestoreProperty("pontoTuristicoId")]
    public string PontoTuristicoId { get; set; }

    [FirestoreProperty("resposta")]
    public string Resposta { get; set; }

    [FirestoreProperty("userId")]
    public string UserId { get; set; }
}