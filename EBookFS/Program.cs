using EBookFS.Models.Contracts;
using EBookFS.Models.Repositories;
using FastEndpoints;
using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddFastEndpoints();
builder.Services.AddScoped<IBookRepository, StaticBookRepository>();
builder.Services.SwaggerDocument();

var app = builder.Build();
app.UseAuthorization();
app.UseFastEndpoints();
app.UseSwaggerGen();
app.Run();
