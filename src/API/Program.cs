using CidadeAtivaApi.Data;
using CidadeAtivaApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDB>(opt =>
    opt.UseInMemoryDatabase("CityHelpDb"));


builder.Services.AddScoped<ProblemasService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title       = "Cidade Ativa API",
        Description = "API para registro de problemas urbanos — ODS 11",
        Version     = "v1"
    });
});


var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.MapControllers();


app.Run();