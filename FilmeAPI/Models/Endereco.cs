using System.ComponentModel.DataAnnotations;

namespace FilmeAPI.Models;

public class Endereco// não necessita do cinema para existir logo não tem chave estrangeira do cinema
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "Campo de Logradouro é obrigatório")]
    public string Logradouro { get; set; }

    [Required(ErrorMessage = "Campo de Número é obrigatório")]

    public int Numero { get; set; }

    //pelo fato de ter uma propriedade virtual equivalente a ambas em cada, como por exemplo
    //cinema tem endereço e endereço tem cinema. logo se eu consultar um cinema ele ira trazer
    //o endereço e se eu consultar um endereço ele trara o cinema.
    public virtual Cinema Cinema { get; set; }
}
