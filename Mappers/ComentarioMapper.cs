using Google.Cloud.Firestore;
using TuristeiRV.API.DTOs;
using TuristeiRV.API.Models.Entidade;

namespace TuristeiRV.API.Mappers;

public static class ComentarioMapper
{
    public static Comentario ToModel(ComentarioDto dto)
    {
        return new Comentario
        {
            Avaliacao = dto.Avaliacao,
            ComentarioTexto = dto.ComentarioTexto,
            Data = Timestamp.FromDateTime(dto.Data.ToUniversalTime()),
            Id = dto.Id,
            Nome = dto.Nome,
            PontoTuristicoId = dto.PontoTuristicoId,
            Resposta = dto.Resposta,
            UserId = dto.UserId
        };
    }

    public static ComentarioDto ToDto(this Comentario model)
    {
        return new ComentarioDto
        {
            Avaliacao = model.Avaliacao,
            ComentarioTexto = model.ComentarioTexto,
            Data = model.Data.ToDateTime(),
            Id = model.Id,
            Nome = model.Nome,
            PontoTuristicoId = model.PontoTuristicoId,
            Resposta = model.Resposta,
            UserId = model.UserId
        };
    }
}