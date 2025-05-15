using AutoMapper;
using FilmeAPI.Data.DTOs;
using FilmeAPI.Models;

namespace FilmeAPI.Profiles;

public class FilmeProfile : Profile
{
    public FilmeProfile()
    {
        // o AutoMapper vai mapear os DTOs para as classes e vice-versa
        CreateMap<CreateFilmeDto, Filme>();
        CreateMap<UpdateFilmeDto, Filme>();
    }
}
