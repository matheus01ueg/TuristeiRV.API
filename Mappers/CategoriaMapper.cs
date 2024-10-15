using Google.Cloud.Firestore;
using TuristeiRV.API.DTOs;
using TuristeiRV.API.Models.Entidade;

namespace TuristeiRV.API.Mappers;

public static class CategoriaMapper
{
    public static Categoria ToModel(CategoriaDto dto)
    {
        return new Categoria
        {
            Id = dto.Id,
            Nome = dto.Nome
        };
    }
    public static CategoriaDto ToDto(this Categoria model)
    {
        return new CategoriaDto
        {
            Id = model.Id,
            Nome = model.Nome
        };
    }
}

