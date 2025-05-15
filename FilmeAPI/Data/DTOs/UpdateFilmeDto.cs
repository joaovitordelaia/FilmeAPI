using System.ComponentModel.DataAnnotations;

namespace FilmeAPI.Data.DTOs;
//DTOs são objetos de transferência de dados
public class UpdateFilmeDto
{
    //devemos adicionar os atributos que queremos que sejam exibidos na API


    [Required(ErrorMessage = "O Titulo é obrigatório")]
    [MaxLength(50, ErrorMessage = "O tamanho do nome do Titulo excedeu o limite")]
    public string Titulo { get; set; }// sempre em maiusculo

    [Required(ErrorMessage = "O Genero é obrigatório")]
    [StringLength(20, ErrorMessage = "O tamanho do nome do Gênero excedeu o limite")]// 
    public string Genero { get; set; }

    [Required]
    [Range(71, 300, ErrorMessage = "O tempo de duração aceitavel é entre 71 minutos e 300 minutos")]
    public int Duracao { get; set; }

}
