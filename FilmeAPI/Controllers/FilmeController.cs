using AutoMapper;
using FilmeAPI.Data;
using FilmeAPI.Data.DTOs;
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

    // esse _context serve para acessar o banco de dados
    private FilmeContext _context;
    // usado para usar o autoMapper no controller
    private IMapper _mapper;

    //esse contrutor serve para injetar o contexto do banco de dados e tambem o AutoMapper ao controller
    public FilmeController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    // o padrão de um post de acordo com a arquitetura restful é retornar o objeto que foi criado
    // e tambem deve ser informado o caminho que o usuario pode usar para acessar o objeto
    // por isso nesse endpoint estamos usando o CreatedAtAction.
    [HttpPost]
    //                                    O CreateFilmeDto é responsavel por mascarar o objeto filme e não deixar exposto a estrutura do banco de dados                       
    public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)// esse FromBody serve para
    {
        Filme filme = _mapper.Map<Filme>(filmeDto);// aqui estamos usando o AutoMapper para mapear o objeto filmeDto para o objeto filme
        _context.Filmes.Add(filme);
        // quando for inserir algum objeto no banco de dados é necessario usar depois do add o SaveChanges
        _context.SaveChanges();// seria como um commit no banco de dados

        return CreatedAtAction(nameof(RecuperaFilmePorId), new { id = filme.id }, filme);
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

    [HttpGet("LocalizarUnico/{id}")]
    // interface IActionResult: é responsavel por retornar uma resposta ao usuario de acordo com o endpoint
    public IActionResult RecuperaFilmePorId(int id)
    {
        // essa linha está fazendo uma consulta no banco de dados e retornando o primeiro filme que encontrar com o id informado
        var filme = _context.Filmes.FirstOrDefault(filme => filme.id == id);
        if (filme == null) return NotFound();
        return Ok(filme);

    }
    // esse endpoint serve para atualizar um filme
    [HttpPut("Atualizar/{id}")]
    public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
    {
        // reutilizando a linha do recuperaFilmePorId pois precisamos encontrar o filme para atualizar
        var filme = _context.Filmes.FirstOrDefault(filme => filme.id == id);
            if (filme == null) return NotFound();
        // O metodo Map, pega os dados que vieram da requisição (filmeDto) E "joga por cima"
        // do objeto filme já existente, que veio do banco de dados
        _mapper.Map(filmeDto, filme);
        //salvando as alterações no banco de dados
        _context.SaveChanges();
        return NoContent();
    }
}
