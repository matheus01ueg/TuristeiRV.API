namespace TuristeiRV.API.DTOs;

public class ComentarioDto
{
    public double Avaliacao { get; set; }
    public string ComentarioTexto { get; set; }
    public string Data { get; set; }
    public string? Id { get; set; }
    public string Nome { get; set; }
    public string PontoTuristicoId { get; set; }
    public string? Resposta { get; set; }
    public string UserId { get; set; }
}