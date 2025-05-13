using FilmeAPI.Data;
using FilmeAPI.Models;
using Microsoft.AspNetCore.Mvc;// essa biblioteca serve para fazer requisições http
namespace FilmeAPI.Controllers;


[ApiController]// anotações
[Route("[Controller]")]// 
public class FilmeController : ControllerBase //  toda vez que você cria uma API em ASP.NET, você não começa do zero... 
{                                                   //  Você pega uma base pronta, que já tem várias funções e comportamentos que uma API precisa.

    // esse _context serve para acessar o banco de dados
    private FilmeContext _context;

    //esse contrutor serve para injetar o contexto do banco de dados ao controller
    public FilmeController(FilmeContext context)
    {
        _context = context;
    }


    // o padrão de um post de acordo com a arquitetura restful é retornar o objeto que foi criado
    // e tambem deve ser informado o caminho que o usuario pode usar para acessar o objeto
    // por isso nesse endpoint estamos usando o CreatedAtAction.
    [HttpPost]
    public IActionResult AdicionaFilme([FromBody] Filme filme)// esse FromBody serve para
    {
        _context.Filmes.Add(filme);
        // quando for inserir algum objeto no banco de dados é necessario usar depois do add o SaveChanges
        _context.SaveChanges();// seria como um commit no banco de dados

        return CreatedAtAction(nameof(RecuperaFilmePorId), new {id = filme.id}, filme);
    }

    [HttpGet]
    public IEnumerable<Filme> LerFilmes()
    {
        return _context.Filmes.ToList();
    }

    [HttpGet("SkipTake")]
    public IEnumerable<Filme> LerFilmesSkipTake([FromQuery] int skip, [FromQuery] int take = 50)
    {
        // **** o take está recebendo aqui 50 só para exibir todos, caso contra não iria trazer nada se fosse fazer uma consulta de todos
        //Take: serve para exibir a quantidade depois do pulo ou apenas aquela quantidade que existe na lista
        //Skip: serve para pular uma quantidade determinada
        //[FromQuery] serve para o usuario informar uma consulta atraves do link



        return _context.Filmes.Skip(skip).Take(take);
    }

    [HttpGet("localizarUnico/{id}")]
    // interface IActionResult: é responsavel por retornar uma resposta ao usuario de acordo com o endpoint
    public IActionResult RecuperaFilmePorId(int id)
    {
       var filme = _context.Filmes.FirstOrDefault(filme => filme.id == id);
        if (filme == null) return NotFound();
        return Ok(filme);
        
    }
}
