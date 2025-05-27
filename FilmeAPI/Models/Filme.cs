using System.ComponentModel.DataAnnotations;

namespace FilmeAPI.Models;

public class Filme
{
    [Key]// diz que sera a chave primaria
    [Required]
    public int id { get; set; }

    [Required(ErrorMessage = "O Titulo é obrigatório")]
    [MaxLength(50, ErrorMessage = "O tamanho do nome do Titulo excedeu o limite")]
    public string Titulo { get; set; }// sempre em maiusculo

    [Required(ErrorMessage = "O Genero é obrigatório")]
    [MaxLength(20, ErrorMessage = "O tamanho do nome do Gênero excedeu o limite")]// 
    public string Genero { get; set; }

    [Required]
    [Range(71, 300, ErrorMessage = "O tempo de duração aceitavel é entre 71 minutos e 300 minutos")]
    public int Duracao { get; set; }

    public virtual ICollection<Sessao>  Sessao { get; set; }

}
