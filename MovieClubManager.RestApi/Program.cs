using Microsoft.EntityFrameworkCore;
using MovieClubManager.Contracts.Interfaces;
using MovieClubManager.Persistence.EF;
using MovieClubManager.Persistence.EF.Genres;
using MovieClubManager.Service.Genres;
using MovieClubManager.Service.Genres.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Configuration.AddJsonFile("appsettings.json");

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<EFDataContext>(
    options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<GenreManagerService, GenreManagerAppService>();
builder.Services.AddScoped<GenreManagerRepository, EFGenreManagerRepository>();
builder.Services.AddScoped<UnitOfWork, EFUnitOfWork>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();