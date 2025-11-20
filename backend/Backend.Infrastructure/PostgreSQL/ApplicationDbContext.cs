namespace Backend.Infrastructure.PostgreSQL;

using Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        var entityAssembly = Assembly.Load("Backend.Domain");

        var entityType = typeof(Entity);

        var entityTypes = entityAssembly
            .GetTypes()
            .Where(t => entityType.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);

        foreach (var type in entityTypes)
        {
            modelBuilder.Entity(type);
            modelBuilder.Entity(type).UseTptMappingStrategy();
        }

        var joinEntityType = typeof(JoinEntity<,>);

        var joinEntityTypes = entityAssembly
            .GetTypes()
            .Where(t => entityType.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);

        foreach (var type in joinEntityTypes)
        {
            modelBuilder.Entity(type);
            modelBuilder.Entity(type).UseTptMappingStrategy();
        }
    }
}