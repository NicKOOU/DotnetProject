using LibraryApp;
using LibraryApp.DataAccess.EfModels;
using LibraryApp.DataAccess;
using LibraryApp.DataAccess.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
builder.Services.AddTransient<IBookRepository, BookRepository>();
builder.Services.AddTransient<IMovieRepository, MovieRepository>();
builder.Services.AddTransient<IComicsBookRepository, ComicBookRepository>();
builder.Services.AddEntityFrameworkSqlServer().AddDbContext<LibraryAppContext>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Book}/{id?}");

app.MapRazorPages();

// Create a scope to resolve the dependencies and run the InitializeDatabase method
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<LibraryAppContext>();

    // Run the database initialization method
    var startup = new Startup(builder.Configuration, builder.Environment);
    startup.InitializeDatabase(services);
}

app.Run();
