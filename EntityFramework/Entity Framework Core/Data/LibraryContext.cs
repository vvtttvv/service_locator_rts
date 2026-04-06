using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Entity_Framework_Core.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace Entity_Framework_Core.Data;

public class LibraryContext : DbContext
{
     public DbSet<Author> Authors { get; set; }
     public DbSet<Book> Books { get; set; }

     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
     {
         var config = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json")
             .AddUserSecrets<LibraryContext>()
             .Build();

         var logFile = new StreamWriter("ef_log.txt", append: true);
         optionsBuilder
             .UseNpgsql(config.GetConnectionString("DefaultConnection"))
             .LogTo(logFile.WriteLine, LogLevel.Information);
     }
     
     // protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
     // {
     //     configurationBuilder.Conventions.Remove(typeof(ForeignKeyIndexConvention));
     // }

     protected override void OnModelCreating(ModelBuilder modelBuilder)
     {
         modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibraryContext).Assembly);
     }
}