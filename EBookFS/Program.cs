using EBookFS.Models;
using EBookFS.Models.Contracts;
using EBookFS.Models.Repositories;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddFastEndpoints();
builder.Services.AddJWTBearerAuth("Refah_Yourself_Do_It_Again_To_Going_to_Pardis");
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IUserManagerRepository, StaticUserManagerRepository>();
builder.Services.AddDbContext<BookDBContext>(c => c.UseSqlServer("Server=.;Database=EBookFS;Trusted_Connection=True;"));
builder.Services.AddResponseCaching();

builder.Services.SwaggerDocument();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.UseResponseCaching();
app.UseFastEndpoints(c =>
{
    c.Endpoints.RoutePrefix = "api";
});
app.UseSwaggerGen();
app.Run();
