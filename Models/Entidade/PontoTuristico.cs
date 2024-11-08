using Google.Cloud.Firestore;

namespace TuristeiRV.API.Models.Entidade;

[FirestoreData]
public class PontoTuristico
{
    [FirestoreDocumentId]
    public string Id { get; set; }

    [FirestoreProperty("avaliacao")]
    public double Avaliacao { get; set; }

    [FirestoreProperty("bairro")]
    public string Bairro { get; set; }

    [FirestoreProperty("categoriaId")]
    public string CategoriaId { get; set; }

    [FirestoreProperty("celular")]
    public string Celular { get; set; }

    [FirestoreProperty("cidade")]
    public string Cidade { get; set; }

    [FirestoreProperty("complemento")]
    public string Complemento { get; set; }

    [FirestoreProperty("descricao")]
    public string Descricao { get; set; }

    [FirestoreProperty("email")]
    public string Email { get; set; }

    [FirestoreProperty("endereco")]
    public string Endereco { get; set; }

    [FirestoreProperty("estado")]
    public string Estado { get; set; }

    [FirestoreProperty("horarios")]
    public Horarios Horarios { get; set; }

    [FirestoreProperty("latitude")]
    public double? Latitude { get; set; }

    [FirestoreProperty("longitude")]
    public double? Longitude { get; set; }

    [FirestoreProperty("nome")]
    public string Nome { get; set; }

    [FirestoreProperty("numero")]
    public string Numero { get; set; }

    [FirestoreProperty("site")]
    public string Site { get; set; }

    [FirestoreProperty("telefone")]
    public string Telefone { get; set; }

    // Subcoleção de Imagens
    [FirestoreProperty("imagens")]
    public List<Imagem> Imagens { get; set; }
}

[FirestoreData]
public class Horarios
{
    [FirestoreProperty("domingo")]
    public string Domingo { get; set; }

    [FirestoreProperty("segunda")]
    public string Segunda { get; set; }

    [FirestoreProperty("terca")]
    public string Terca { get; set; }

    [FirestoreProperty("quarta")]
    public string Quarta { get; set; }

    [FirestoreProperty("quinta")]
    public string Quinta { get; set; }

    [FirestoreProperty("sexta")]
    public string Sexta { get; set; }

    [FirestoreProperty("sabado")]
    public string Sabado { get; set; }
}

[FirestoreData]
public class Imagem
{
    [FirestoreProperty("url")]
    public string Url { get; set; }

    [FirestoreProperty("descricao")]
    public string Descricao { get; set; }
}