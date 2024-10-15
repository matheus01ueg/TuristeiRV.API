using Google.Cloud.Firestore;

namespace TuristeiRV.API.Models.Entidade;

[FirestoreData]
public class Usuario
{
    [FirestoreDocumentId]
    public string Id { get; set; }

    [FirestoreProperty("nome")]
    public string Nome { get; set; }

    [FirestoreProperty("sobrenome")]
    public string Sobrenome { get; set; }

    [FirestoreProperty("cep")]
    public string Cep { get; set; }

    [FirestoreProperty("endereco")]
    public string Endereco { get; set; }

    [FirestoreProperty("numero")]
    public string Numero { get; set; }

    [FirestoreProperty("complemento")]
    public string Complemento { get; set; }

    [FirestoreProperty("bairro")]
    public string Bairro { get; set; }

    [FirestoreProperty("email")]
    public string Email { get; set; }
    public string Senha { get; set; }

    [FirestoreProperty("senhaHash")]
    public string SenhaHash { get; set; }
}
