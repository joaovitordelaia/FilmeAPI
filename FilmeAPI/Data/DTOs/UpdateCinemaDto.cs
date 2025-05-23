using System.ComponentModel.DataAnnotations;

namespace FilmeAPI.Data.DTOs;

public class UpdateCinemaDto
{
    [Required(ErrorMessage = "Campo de Nome é obrigatório")]
    public string Nome { get; set; }
}
