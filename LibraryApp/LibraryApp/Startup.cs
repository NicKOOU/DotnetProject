using LibraryApp.DataAccess.EfModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace LibraryApp
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment environment;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<LibraryAppContext>(options =>
                 options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                 );
            InitializeDatabase(services.BuildServiceProvider());
        }
        public void InitializeDatabase(IServiceProvider serviceProvider)
        {
            // Run the script only when the application is running in Development environment
            if (environment.IsDevelopment())
            {
                using var scope = serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<LibraryAppContext>();

                //drop Database
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();

                var setupSql = File.ReadAllText("setup.sql");
                dbContext.Database.Migrate();

                dbContext.Database.ExecuteSqlRaw(setupSql);

            }
        }


    }
}
