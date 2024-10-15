using TuristeiRV.API.DTOs;
using TuristeiRV.API.Models.Entidade;

namespace TuristeiRV.API.Mappers;

public static class UsuarioMapper
{
    public static Usuario ToModel(UsuarioDto dto)
    {
        return new Usuario
        {
            Nome = dto.Nome,
            Sobrenome = dto.Sobrenome,
            Cep = dto.Cep,
            Endereco = dto.Endereco,
            Numero = dto.Numero,
            Complemento = dto.Complemento,
            Bairro = dto.Bairro,
            Email = dto.Email,
            Senha = dto.Senha,
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha)
        };
    }

    public static UsuarioDto ToDto(Usuario model)
    {
        return new UsuarioDto
        {
            Nome = model.Nome,
            Sobrenome = model.Sobrenome,
            Cep = model.Cep,
            Endereco = model.Endereco,
            Numero = model.Numero,
            Complemento = model.Complemento,
            Bairro = model.Bairro,
            Email = model.Email,
        };
    }
}
