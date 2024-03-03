using Microsoft.EntityFrameworkCore;
using MovieClubManager.Entities.Genres;

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly
            (typeof(EFDataContext).Assembly);
       
    }
}