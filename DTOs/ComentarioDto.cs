namespace TuristeiRV.API.DTOs;

public class ComentarioDto
{
    public double Avaliacao { get; set; }
    public string ComentarioTexto { get; set; }
    public DateTime Data { get; set; }
    public int Id { get; set; }
    public string PontoTuristicoId { get; set; }
    public string Resposta { get; set; }
    public string UserId { get; set; }
}