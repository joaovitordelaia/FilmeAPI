using FilmeAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//conex�o ao banco de dados
var connectionString = builder.Configuration.GetConnectionString("FilmeContext");

builder.Services.AddDbContext<FilmeContext>(opts => opts.UseMySql(connectionString,
    ServerVersion.AutoDetect(connectionString)));


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();// aqui est� sendo realizado o registro do servi�o de controllers,
                                  //// que � o que vai permitir que a API receba requisi��es e responda a elas.

var app = builder.Build();

app.MapControllers();// serve para mapear os controllers, ou seja, ele vai pegar todos os controllers que voc� criou e vai mapear eles para a API


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();

