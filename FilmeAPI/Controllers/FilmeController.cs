using FilmeAPI.Models;
using Microsoft.AspNetCore.Mvc;// essa biblioteca serve para fazer requisições http
namespace FilmeAPI.Controllers;


//[ApiController]
//[Route("api/[controller]")]
//public class FilmeController : ControllerBase
//{
//    private static List<Filme> filmes = new List<Filme>();

//    [HttpPost("adiciona")]
//    public void AdicionaFilme([FromBody] Filme filme)
//    {
//        filmes.Add(filme);
//        Console.WriteLine(filme.Titulo);
//        Console.WriteLine(filme.Duracao);
//    }
//}


[ApiController]// anotações
[Route("[Controller]")]// 
public class FilmeController : ControllerBase //  toda vez que você cria uma API em ASP.NET, você não começa do zero... 
{                                                   //  Você pega uma base pronta, que já tem várias funções e comportamentos que uma API precisa.

    private static List<Filme> filmes = new List<Filme>();// lista de filmes, que é uma lista de objetos do tipo Filme que se encontra em Models
    private static int id;

    // o padrão de um post de acordo com a arquitetura restful é retornar o objeto que foi criado
    // e tambem deve ser informado o caminho que o usuario pode usar para acessar o objeto
    // por isso nesse endpoint estamos usando o CreatedAtAction.
    [HttpPost]
    public IActionResult AdicionaFilme([FromBody] Filme filme)// esse FromBody serve para
    {
        filme.id = ++id;
        filmes.Add(filme);
        return CreatedAtAction(nameof(RecuperaFilmePorId), new {id = filme.id}, filme);
    }

    [HttpGet]
    public IEnumerable<Filme> LerFilmes([FromQuery] int skip,[FromQuery] int take)
    {
        //Take: serve para exibir a quantidade depois do pulo ou apenas aquela quantidade que existe na lista
        //Skip: serve para pular uma quantidade determinada
        //[FromQuery] serve para o usuario informar uma consulta atraves do link
        return filmes.Skip(skip).Take(take);
    }

    [HttpGet("localizarUnico/{id}")]
    // interface IActionResult: é responsavel por retornar uma resposta ao usuario de acordo com o endpoint
    public IActionResult RecuperaFilmePorId(int id)
    {
       var filme = filmes.FirstOrDefault(filme => filme.id == id);
        if (filme == null) return NotFound();
        return Ok(filme);
        
    }
}
