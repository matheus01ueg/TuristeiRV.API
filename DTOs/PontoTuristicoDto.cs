using System.ComponentModel.DataAnnotations;
namespace TuristeiRV.API.DTOs;

public class PontoTuristicoDto
{
    public string Id { get; set; }

    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [StringLength(100, ErrorMessage = "O Nome não pode ter mais de 100 caracteres.")]
    public string Nome { get; set; }

    [StringLength(200, ErrorMessage = "A Descrição não pode ter mais de 200 caracteres.")]
    public string Descricao { get; set; } // Opcional

    [Required(ErrorMessage = "O campo CategoriaId é obrigatório.")]
    public string CategoriaId { get; set; }
    public string Celular { get; set; }
    public string Complemento { get; set; }

    [Required(ErrorMessage = "O campo Bairro é obrigatório.")]
    [StringLength(100, ErrorMessage = "O Bairro não pode ter mais de 100 caracteres.")]
    public string Bairro { get; set; } // Opcional

    [Required(ErrorMessage = "O campo Email é obrigatório.")]
    [EmailAddress(ErrorMessage = "E-mail inválido.")]
    public string Email { get; set; } // Opcional

    [Required(ErrorMessage = "O campo Endereco é obrigatório.")]
    [StringLength(100, ErrorMessage = "O Endereco não pode ter mais de 100 caracteres.")]
    public string Endereco { get; set; } // Opcional

    [Required(ErrorMessage = "O campo Endereco é obrigatório.")]
    public string Numero { get; set; } // Opcional
    public string Site { get; set; }
    public string Telefone { get; set; }

    [Range(-90, 90, ErrorMessage = "Latitude deve estar entre -90 e 90.")]
    public double? Latitude { get; set; } // Opcional

    [Range(-180, 180, ErrorMessage = "Longitude deve estar entre -180 e 180.")]
    public double? Longitude { get; set; } // Opcional
    public double Avaliacao { get; set; } // Opcional
    public List<ImagemDto> Imagens { get; set; } // Opcional
    public HorariosDto Horarios { get; set; } // Opcional
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
}