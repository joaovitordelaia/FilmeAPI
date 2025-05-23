using System.ComponentModel.DataAnnotations;

namespace FilmeAPI.Models;

public class Cinema // depende do endereço para assistir com isso o EF seguira o texto a cima de EnderecoId 
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required(ErrorMessage = "Campo de Nome é obrigatório")]
    public string Nome { get; set; }

    // Na hora de criar uma chave estrangeira (FK), é recomendável nomear a propriedade
    // seguindo o padrão NomeDaClasseId. Assim, o Entity Framework consegue entender automaticamente
    // qual entidade está sendo referenciada como chave primária (PK), sem precisar de configuração extra.”
    public int EnderecoId { get; set; }

    //pelo fato de ter uma propriedade virtual equivalente a ambas em cada, como por exemplo
    //cinema tem endereço e endereço tem cinema. logo se eu consultar um cinema ele ira trazer
    //o endereço e se eu consultar um endereço ele trara o cinema.
    public virtual Endereco Endereco { get; set; }


}
