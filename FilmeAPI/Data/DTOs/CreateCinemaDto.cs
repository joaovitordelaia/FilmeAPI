using System.ComponentModel.DataAnnotations;

namespace FilmeAPI.Data.DTOs;

public class CreateCinemaDto
{
    [Required(ErrorMessage = "Campo de Nome é obrigatório")]
    public string Nome { get; set; }

    public int EnderecoId { get; set; }
    
}
