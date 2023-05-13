using Microsoft.EntityFrameworkCore;

namespace LibraryApp
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Server=LibrarySQLServer;Database=LibraryDB;User Id=sa;Password=Dotnet2024!;MultipleActiveResultSets=true;Encrypt=false;";
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<Book> Books { get; set; }
    }
}
