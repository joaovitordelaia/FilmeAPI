using System.ComponentModel.DataAnnotations;

namespace FilmeAPI.Data.DTOs
{
    public class ReadFilmeDto
    {

        public string Titulo { get; set; }// sempre em maiusculo

        public string Genero { get; set; }

        public int Duracao { get; set; }

        public DateTime HoraDaConsulta { get; set; } = DateTime.Now;
    }
}
