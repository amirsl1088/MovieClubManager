using Microsoft.EntityFrameworkCore;
using MovieClubManager.Contracts.Interfaces;
using MovieClubManager.Infrastructure;
using MovieClubManager.Persistence.EF;
using MovieClubManager.Persistence.EF.Genres;
using MovieClubManager.Persistence.EF.Movies;
using MovieClubManager.Persistence.EF.Rents;
using MovieClubManager.Persistence.EF.Users;
using MovieClubManager.Service.Genres;
using MovieClubManager.Service.Genres.Contracts;
using MovieClubManager.Service.Movies;
using MovieClubManager.Service.Movies.Contracts;
using MovieClubManager.Service.Rents;
using MovieClubManager.Service.Rents.Contracts;
using MovieClubManager.Service.Users;
using MovieClubManager.Service.Users.Contrcts;

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
builder.Services.AddScoped<GenreService, GenreAppService>();
builder.Services.AddScoped<MovieManagerService, MovieManagerAppService>();
builder.Services.AddScoped<MovieManagerRepository, EFMovieManagerRepository>();
builder.Services.AddScoped<UserService, UserAppService>();
builder.Services.AddScoped<UserRepository, EFUserRepository>();
builder.Services.AddScoped<DateTimeService, DateTimeAppService>();
builder.Services.AddScoped<RentService, RentAppService>();
builder.Services.AddScoped<RentRepository, EFRentRepository>();
builder.Services.AddScoped<RentManagerService, RentManagerAppService>();


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
