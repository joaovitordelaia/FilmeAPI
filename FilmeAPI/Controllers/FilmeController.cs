using AutoMapper;
using FilmeAPI.Data;
using FilmeAPI.Data.DTOs;
using FilmeAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
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
[Produces("application/json")]
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

    /// <summary>
    /// Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="filmeDto">Objeto com os campos necessários para criação de um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    /// <remarks>
    /// Requisição deve conter um JSON válido com os campos obrigatórios do filme.
    /// Exemplo:
    /// {
    ///   "titulo": "Matrix",
    ///   "diretor": "Lana Wachowski",
    ///   "genero": "Ficção científica",
    ///   "duracao": 136
    /// }
    /// O filme será salvo no banco de dados e retornará o caminho para acessá-lo.
    /// </remarks>
    [HttpPost("AdicionarFilme")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    //                                    O CreateFilmeDto é responsavel por mascarar o objeto filme e não deixar exposto a estrutura do banco de dados                       
    public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)// esse FromBody serve para
    {
        Filme filme = _mapper.Map<Filme>(filmeDto);// aqui estamos usando o AutoMapper para mapear o objeto filmeDto para o objeto filme
        _context.Filmes.Add(filme);
        // quando for inserir algum objeto no banco de dados é necessario usar depois do add o SaveChanges
        _context.SaveChanges();// seria como um commit no banco de dados

        return CreatedAtAction(nameof(RecuperaFilmePorId), new { id = filme.id }, filme);
    }

    /// <summary>
    /// Localiza todos os filmes no banco de dados
    /// </summary>
    /// <returns>IEnumerable</returns>
    /// <response code="200">Trazendo todos os filmes no banco</response>
    /// <remarks>
    /// Retorna todos os filmes cadastrados no banco de dados.
    /// Requisição não exige parâmetros.
    /// </remarks>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("LocalizarTudo")]
    public IEnumerable<ReadFilmeDto> LerFilmes()
    {
        // boa pratica é por o resultado vindo do context em uma variavel
        var filme = _context.Filmes.ToList();
        // com essa variavel que recebeu o resultado nos fazemos conversão para o DTO
        return _mapper.Map<List<ReadFilmeDto>>(filme);
    }

    /// <summary>
    /// Localiza todos os filmes no BD dentro de um escopo ou pulando uma quantia
    /// </summary>
    /// <returns>IEnumerable</returns>
    /// <response code="200">Trazendo todos os filmes no banco</response>
    /// <remarks>
    /// Permite paginação com os parâmetros 'skip' e 'take' passados na URL.
    /// Exemplo de uso: /Filme/SkipTake?skip=10Etake=5
    /// Isso irá pular os 10 primeiros filmes e retornar os 5 seguintes.
    /// </remarks>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("SkipTake")]
    public IEnumerable<ReadFilmeDto> LerFilmesSkipTake([FromQuery] int skip = 0, [FromQuery] int take = 50)
    {
        // **** o take está recebendo aqui 50 só para exibir todos, caso contra não iria trazer nada se fosse fazer uma consulta de todos
        //Take: serve para exibir a quantidade depois do pulo ou apenas aquela quantidade que existe na lista
        //Skip: serve para pular uma quantidade determinada
        //[FromQuery] serve para o usuario informar uma consulta atraves do link

        // boa pratica é por o resultado vindo do context em uma variavel
        var filme = _context.Filmes.Skip(skip).Take(take);
        // com essa variavel que recebeu o resultado nos fazemos conversão para o DTO
        return _mapper.Map<List<ReadFilmeDto>>(filme);

    }
    /// <summary>
    /// Localiza um filme pelo seu codigo identificador
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <param name="id">Codigo identificador</param>
    /// <response code="200">Trazendo todos os filmes no banco</response>
    /// <response code="404">caso não encontre pelo id o que a requisição pediu</response>  
    /// <remarks>
    /// Busca um único filme pelo seu identificador.
    /// Exemplo de uso: /Filme/LocalizarUnico/3
    /// Retorna 404 caso o filme não exista.
    /// </remarks>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("LocalizarUnico/{id}")]
    // interface IActionResult: é responsavel por retornar uma resposta ao usuario de acordo com o endpoint
    public IActionResult RecuperaFilmePorId(int id)
    {
        // essa linha está fazendo uma consulta no banco de dados e retornando o primeiro filme que encontrar com o id informado
        var filme = _context.Filmes.FirstOrDefault(filme => filme.id == id);
        if (filme == null) return NotFound();

        // aqui sera criado uma varial que recebera a conversão do resultado encontrado no banco
        // para uma DTO, e essa variavel sera entregue
        var filmeDto = _mapper.Map<ReadFilmeDto>(filme);

        return Ok(filmeDto);

    }

    /// <summary>
    /// Atualiza um filme por completo
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <param name="id">Codigo identificador</param>
    /// <param name = "filmeDto">Objeto vindo do usuario</param>
    /// <response code="204">Filme atualizado com sucesso (sem conteúdo na resposta)</response>
    /// <response code="404">caso não encontre pelo id o que a requisição pediu</response>
    /// <remarks>
    /// Substitui completamente os dados de um filme existente.
    /// Todos os campos devem ser enviados no corpo da requisição.
    /// Exemplo de JSON:
    /// {
    ///   "titulo": "Interstellar",
    ///   "diretor": "Christopher Nolan",
    ///   "genero": "Ficção científica",
    ///   "duracao": 169
    /// }
    /// Retorna 204 se a atualização for bem-sucedida ou 404 se o filme não for encontrado.
    /// </remarks>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
    /// <summary>
    /// Atualiza um filme parcialmente
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <param name="id">Codigo identificador</param>
    /// <param name = "patch">Sera a requisição do usuario</param>
    /// <response code="204">Atualização parcial feita com sucesso</response>
    /// <response code="404">caso não encontre pelo id o que a requisição pediu</response>
    /// <remarks>
    /// Permite a atualização parcial dos dados de um filme.
    /// Usa um JSON Patch (RFC 6902) com operações como "replace" ou "add".
    /// Exemplo de JSON:
    /// [
    ///   { "op": "replace", "path": "/titulo", "value": "Novo Título" },
    ///   { "op": "replace", "path": "/duracao", "value": 150 }
    /// ]
    /// Retorna 204 se a operação for válida ou 404 se o filme não for encontrado.
    /// </remarks>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPatch("Patch/{id}")]
    public IActionResult AtualizaFilmePatch(int id, JsonPatchDocument<UpdateFilmeDto> patch)
    {
        // reutilizando a linha do recuperaFilmePorId pois precisamos encontrar o filme para atualizar
        var filme = _context.Filmes.FirstOrDefault(filme => filme.id == id);
        if (filme == null) return NotFound();

        // o resultado encontrado no BD foi convertido para DTO UpdateFilmeDto
        // pois as regras de validação não foram aplicadas ao receber os dados do usuario
        // convertendo para o DTO é possivel aplicar as regras de validação
        var filmeParaAtualizar = _mapper.Map<UpdateFilmeDto>(filme);

        // esse patch faz a atualização dos dados
        patch.ApplyTo(filmeParaAtualizar, ModelState);

        // aqui estamos validando se o patch foi aplicado corretamente
        if (!TryValidateModel(filmeParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(filmeParaAtualizar, filme);
        //salvando as alterações no banco de dados
        _context.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// Deleta um filme do BD
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <param name="id">Codigo identificador</param>
    /// <response code="204">Filme deletado com sucesso</response>
    /// <response code="404">caso não encontre pelo id o que a requisição pediu</response>
    /// <remarks>
    /// Remove permanentemente o filme do banco de dados pelo ID informado.
    /// Exemplo de uso: /Filme/Deletar/5
    /// Retorna 204 se deletado com sucesso ou 404 se o filme não for encontrado.
    /// </remarks>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("Deletar/{id}")]
    public IActionResult DeletarFilmes(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.id == id);
        if (filme == null) return NotFound();

        _context.Remove(filme);
        _context.SaveChanges();
        return NoContent();
    }


}
