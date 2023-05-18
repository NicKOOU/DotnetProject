using Microsoft.EntityFrameworkCore;

namespace LibraryApp.DataAccess.EfModels
{
    public class LibraryAppContext : DbContext
    {
        public LibraryAppContext(DbContextOptions<LibraryAppContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Server=LibrarySQLServer;Database=LibraryDB;User Id=sa;Password=Dotnet2024!;MultipleActiveResultSets=true;Encrypt=false;";
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<TBook> Books { get; set; }

        public DbSet<TMovie> Movies { get; set; }

        public DbSet<TComicsBook> ComicsBook { get; set; }
    }
}
