﻿using System.ComponentModel.DataAnnotations;

namespace FilmeAPI.Data.DTOs;

public class ReadCinemaDto
{
    public int Id { get; set; }

    public string Nome { get; set; }

    public ReadEnderecoDto ReadEndereco { get; set; }

    public ICollection<ReadSessaoDto> Sessao { get; set; }

}
