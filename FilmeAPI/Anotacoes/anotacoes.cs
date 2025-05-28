//O que é uma api?
// "API" é a sigla para o termo "Application Programming Interface"
//// API é um conjunto de rotinas e padrões estabelecidos por uma aplicação para utilização de seus recursos por outros aplicativos.

// O que é REST?
// REST é um estilo arquitetural para sistemas distribuídos, que utiliza os princípios do protocolo HTTP para comunicação entre cliente e servidor.
// REST é um padrão arquitetural que visa padronizar os meios de tráfego de dados.
// RESTful é quem implementa esse conceito.

/// FaSnfR5qZuWpbCTv mongodb

// o que é controllers?
// Controllers são classes que contêm métodos que respondem a solicitações HTTP.


//Anotações ou tambem chamados de DataAnnotations
//// USAR using System.ComponentModel.DataAnnotations;
/// Serve para controlar os dados informados pelo usuario vindo de uma requisão
/// Limitam ou definem como deve ser recebido tal dado em cada propriedade
/// [Required] - campo se torna obrigatorio ter sido informado um dado
/// [Range(min, max)] - Escopo de uma propriedade Int
/// [StringLength(X)] - Limita o numero de caracteres de uma propriedade String  
/// [MaxLength(X)] Define o tamanho maximo de uma lista ou string
/// [MinLength(X)] Define o tamanho minimo de uma lista ou string
/// [EmailAddress] Verifica se é um email valido
/// [Key] define a chave primaria do model


// IEnumerable
//// ideal para retornar listas, arrays ou coleções
/// essecial pois com ele praticamos o polimorfismo
/// sigifica que podemos retornar diferentes tipos de dados em um unico metodo


// CreatedAtAction
/// o padrão de um post de acordo com a arquitetura restful é retornar o objeto que foi criado
/// e tambem deve ser informado o caminho que o usuario pode usar para acessar o objeto
/// por isso no endpoint de post estamos usando o CreatedAtAction.
/// 


// interface IActionResult
/// é responsavel por retornar uma resposta ao usuario de acordo com o endpoint



// Criando migrations
/// 1 - apos configurar a string de conexão, e definir o dbcontext
/// 2 - é importante revisar as classes de models, para saber se tem a [key] e outros como anotação sobre a propriedade identificadora
/// 3 - abrir o terminal e rodar o comando: Add-migration CriandoTabelaDeFilme
/// 4 - abrir o terminal e rodar o comando: Update-Database

/// 


// DTOs chamado de Data Transfer Object
/// * O CreateFilmeDto é responsavel por mascarar o objeto filme
/// e não deixar exposto a estrutura do banco de dados.
/// Com DTOs podemos definir os parâmetros enviados de maneira isolada do nosso modelo do banco de dados.
/// 
/// * por isso no controller os endpoints deverão usar o CreateFilmeDto
/// e o vinculo com do DTO com o model é usado o AutoMapper
/// 
/// * o AutoMapper é uma biblioteca que serve para mapear objetos
/// em program.cs é posto o seguinte codigo, para registrar o AutoMapper no projeto ASP.NET Core
/// builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
///  
/// * Deve é criado uma pasta dentro da pasta Data, chamada DTOs,
///     º e dentro dela é criado o arquivo CreateFilmeDto.cs ele sera responsavel por mascarar o objeto filme
///           º com isso mudaremos nos controllers a assinatura dos endpoints o objeto filme para CreateFilmeDto
/// * Tambem tem que criar uma pasta profile para abrigar o arquivo de mapeamento 
///     º dentro dela é criado o arquivo FilmeProfile.cs que sera para transformar um tipo em outro.
///     º cada endpoint que for usar um DTO, como update, delete, Create, etc 
///     º Deve ser criado aquele vinculo das classes DTO com o model
/// * No controllador, deve ser injetado o IMapper no construtor
///     º e dentro do endpoint que usar o objeto filme, deve ser usado o _mapper.Map<Filme>(filmeDto)
///     º pois ira realizar a conversão do model para o DTO
///     
///     
///  * quando é criado o outro endpoint normalmente cria outro DTO por motivos de semantica



=======