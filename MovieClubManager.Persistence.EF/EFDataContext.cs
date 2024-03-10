using Microsoft.EntityFrameworkCore;
using MovieClubManager.Entities.Genres;
using MovieClubManager.Entities.Movies;
using MovieClubManager.Entities.Rents;
using MovieClubManager.Entities.Users;

namespace MovieClubManager.Persistence.EF;

public class EFDataContext : DbContext
{
    public EFDataContext(string connectionString) :
        this(new DbContextOptionsBuilder().UseSqlServer(connectionString).Options)
    { }


    public EFDataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Genre> Genres { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Rent> Rents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly
            (typeof(EFDataContext).Assembly);
       
    }
}