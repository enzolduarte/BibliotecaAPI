using BibliotecaAPI.Data;
using BibliotecaAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<BibliotecaContext>(options =>
    options.UseInMemoryDatabase("BibliotecaDB")); 


builder.Services.AddScoped<LivroService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<EmprestimoService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
