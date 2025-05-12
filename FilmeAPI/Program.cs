using FilmeAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//conexão ao banco de dados
var connectionString = builder.Configuration.GetConnectionString("FilmeContext");

builder.Services.AddDbContext<FilmeContext>(opts => opts.UseMySql(connectionString,
    ServerVersion.AutoDetect(connectionString)));


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();// aqui está sendo realizado o registro do serviço de controllers,
                                  //// que é o que vai permitir que a API receba requisições e responda a elas.

var app = builder.Build();

app.MapControllers();// serve para mapear os controllers, ou seja, ele vai pegar todos os controllers que você criou e vai mapear eles para a API


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();

