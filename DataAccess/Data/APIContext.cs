
using System.Reflection;
using Business.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data;
public class APIContext : DbContext
{
    public DbSet<Flight> Flights {get; set;}
    public DbSet<Journey> Journies {get; set;}
    public DbSet<Transport> Transports {get; set;}
    public APIContext(DbContextOptions<APIContext> options) :base(options){

    }

    public APIContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}