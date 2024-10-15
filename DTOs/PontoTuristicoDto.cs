namespace TuristeiRV.API.DTOs;

public class PontoTuristicoDto
{
    public string Id { get; set; }
    public double Avaliacao { get; set; }
    public string Bairro { get; set; }
    public string CategoriaId { get; set; }
    public string Celular { get; set; }
    public string Cidade { get; set; }
    public string Complemento { get; set; }
    public string Descricao { get; set; }
    public string Email { get; set; }
    public string Endereco { get; set; }
    public string Estado { get; set; }
    public HorariosDto Horarios { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Nome { get; set; }
    public string Numero { get; set; }
    public string Site { get; set; }
    public string Telefone { get; set; }
    public List<ImagemDto> Imagens { get; set; }
}

public class HorariosDto
{
    public string Domingo { get; set; }
    public string Segunda { get; set; }
    public string Terca { get; set; }
    public string Quarta { get; set; }
    public string Quinta { get; set; }
    public string Sexta { get; set; }
    public string Sabado { get; set; }
}

public class ImagemDto
{
    public string Url { get; set; }
    public string Descricao { get; set; }
    public DateTime DataUpload { get; set; }
    public bool Principal { get; set; }
}