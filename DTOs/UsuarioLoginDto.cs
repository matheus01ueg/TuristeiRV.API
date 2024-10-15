using System.ComponentModel.DataAnnotations;

namespace TuristeiRV.API.DTOs;
public class UsuarioLoginDto
{
    [Required]
    [EmailAddress]
    public string email { get; set; }

    [Required]
    public string senha { get; set; }
}