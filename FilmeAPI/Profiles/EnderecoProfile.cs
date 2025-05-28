using AutoMapper;
using FilmeAPI.Data.DTOs;
using FilmeAPI.Models;

namespace FilmeAPI.Profiles;

public class EnderecoProfile : Profile
{
    public EnderecoProfile()
    {
        // o AutoMapper vai mapear os DTOs para as classes e vice-versa
        CreateMap<CreateEnderecoDto, Endereco>();
        CreateMap<UpdateEnderecoDto, Endereco>();
        CreateMap<Endereco, ReadEnderecoDto>();
        
        
    }
}
