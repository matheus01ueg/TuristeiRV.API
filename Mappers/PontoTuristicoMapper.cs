using Google.Cloud.Firestore;
using TuristeiRV.API.DTOs;
using TuristeiRV.API.Models.Entidade;

namespace TuristeiRV.API.Mappers;

public static class PontoTuristicoMapper
{
    public static PontoTuristicoDto ToDto(this PontoTuristico model)
    {
        return new PontoTuristicoDto
        {
            Id = model.Id,
            Avaliacao = model.Avaliacao,
            Bairro = model.Bairro,
            CategoriaId = model.CategoriaId,
            Celular = model.Celular,
            Cidade = model.Cidade,
            Complemento = model.Complemento,
            Descricao = model.Descricao,
            Email = model.Email,
            Endereco = model.Endereco,
            Estado = model.Estado,
            Horarios = model.Horarios?.ToDto(),
            Latitude = model.Latitude,
            Longitude = model.Longitude,
            Nome = model.Nome,
            Numero = model.Numero,
            Site = model.Site,
            Telefone = model.Telefone,
            Imagens = model.Imagens?.ConvertAll(i => i.ToDto())
        };
    }

    public static PontoTuristico ToModel(this PontoTuristicoDto dto)
    {
        return new PontoTuristico
        {
            Id = dto.Id,
            Avaliacao = dto.Avaliacao,
            Bairro = dto.Bairro,
            CategoriaId = dto.CategoriaId,
            Celular = dto.Celular,
            Cidade = dto.Cidade,
            Complemento = dto.Complemento,
            Descricao = dto.Descricao,
            Email = dto.Email,
            Endereco = dto.Endereco,
            Estado = dto.Estado,
            Horarios = dto.Horarios?.ToModel(),
            Latitude = dto.Latitude,
            Longitude = dto.Longitude,
            Nome = dto.Nome,
            Numero = dto.Numero,
            Site = dto.Site,
            Telefone = dto.Telefone,
            Imagens = dto.Imagens?.ConvertAll(i => i.ToModel())
        };
    }

    public static HorariosDto ToDto(this Horarios model)
    {
        return new HorariosDto
        {
            Domingo = model.Domingo,
            Segunda = model.Segunda,
            Terca = model.Terca,
            Quarta = model.Quarta,
            Quinta = model.Quinta,
            Sexta = model.Sexta,
            Sabado = model.Sabado
        };
    }

    public static Horarios ToModel(this HorariosDto dto)
    {
        return new Horarios
        {
            Domingo = dto.Domingo,
            Segunda = dto.Segunda,
            Terca = dto.Terca,
            Quarta = dto.Quarta,
            Quinta = dto.Quinta,
            Sexta = dto.Sexta,
            Sabado = dto.Sabado
        };
    }

    public static ImagemDto ToDto(this Imagem model)
    {
        return new ImagemDto
        {
            Url = model.Url,
            Descricao = model.Descricao,
            DataUpload = model.DataUpload.ToDateTime(),
            Principal = model.Principal
        };
    }

    public static Imagem ToModel(this ImagemDto dto)
    {
        return new Imagem
        {
            Url = dto.Url,
            Descricao = dto.Descricao,
            DataUpload = Timestamp.FromDateTime(dto.DataUpload),
            Principal = dto.Principal
        };
    }
}