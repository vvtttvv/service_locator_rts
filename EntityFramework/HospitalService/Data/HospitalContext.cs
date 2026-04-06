using Microsoft.EntityFrameworkCore;
using HospitalService.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace HospitalService.Data;

public class HospitalContext : DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Appointment> Appointments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var logFile = new StreamWriter("ef_log.txt", append: true);
        optionsBuilder
            .UseNpgsql(config.GetConnectionString("DefaultConnection"))
            .LogTo(logFile.WriteLine, LogLevel.Information);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HospitalContext).Assembly);
    }
}
