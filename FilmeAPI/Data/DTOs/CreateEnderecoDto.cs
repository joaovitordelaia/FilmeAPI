using System.ComponentModel.DataAnnotations;

namespace FilmeAPI.Data.DTOs;

public class CreateEnderecoDto
{
    [Required(ErrorMessage = "Campo de Logradouro é obrigatório")]
    public string Logradouro { get; set; }

    [Required(ErrorMessage = "Campo de Número é obrigatório")]
    public int Numero { get; set; }
}
