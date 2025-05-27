using AutoMapper;
using FilmeAPI.Data.DTOs;
using FilmeAPI.Models;

namespace FilmeAPI.Profiles;

public class CinemaProfile : Profile
{
    public CinemaProfile()
    {
        
        CreateMap<CreateCinemaDto, Cinema>();
        CreateMap<UpdateCinemaDto, Cinema>();
        CreateMap<Cinema, ReadCinemaDto>()
            .ForMember
            (cinemaDto => cinemaDto.ReadEndereco, 
            opts => opts.MapFrom(cinema => cinema.Endereco))
            .ForMember
            (cinemaDto => cinemaDto.Sessao,
            opts => opts.MapFrom(cinema => cinema.Sessao)); ;



        // Basicamente as linhas acima está fazendo um mapeamento de Endereco com o
        // ReadEnderecoDto, pois dentro da Dto do ReadCinemaDto tem uma propriedade
        // do tipo ReadEnderecoDto chamada ReadEndereco e dentro do model Cinema tem
        // uma propriedade do tipo Endereco chamada Endereco então essa linha está
        // fazendo o mapeamento entre os dois


    }
}
