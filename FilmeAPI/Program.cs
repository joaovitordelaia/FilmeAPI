using FilmeAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//conex�o ao banco de dados
//var connectionString = builder.Configuration.GetConnectionString("FilmeConnection");
var connectionString = builder.Configuration.GetConnectionString("Filme2Connection");

builder.Services.AddDbContext<FilmeContext>(options =>
    options.UseSqlServer(connectionString));

//builder.Services.AddDbContext<FilmeContext>(opts => opts.UseMySql(connectionString,
//    ServerVersion.AutoDetect(connectionString)));

// essa linha de codigo � respons�vel por registrar o AutoMapper no projeto ASP.NET Core 
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
//Estamos definindo qual � a informa��o da API que estamos documentando. 
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FilmesAPI", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

// aqui est� sendo realizado o registro do servi�o de controllers,
//// que � o que vai permitir que a API receba requisi��es e responda a elas.
builder.Services.AddControllers().AddNewtonsoftJson();

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

