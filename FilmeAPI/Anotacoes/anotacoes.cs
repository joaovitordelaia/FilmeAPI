//O que é uma api?
// "API" é a sigla para o termo "Application Programming Interface"
//// API é um conjunto de rotinas e padrões estabelecidos por uma aplicação para utilização de seus recursos por outros aplicativos.

// O que é REST?
// REST é um estilo arquitetural para sistemas distribuídos, que utiliza os princípios do protocolo HTTP para comunicação entre cliente e servidor.
// REST é um padrão arquitetural que visa padronizar os meios de tráfego de dados.
// RESTful é quem implementa esse conceito.


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
