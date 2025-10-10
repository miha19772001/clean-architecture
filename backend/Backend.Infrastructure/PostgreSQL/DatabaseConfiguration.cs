namespace Backend.Infrastructure.PostgreSQL
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class DatabaseConfiguration
    {
        public static void Connect(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString,
                    b => b.MigrationsAssembly("Backend.Infrastructure"))
            );
        }

        public static void CreateSchema(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            try
            {
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
//Add-Migration InitialCreate -Project Backend.Infrastructure -OutputDir PostgreSQL/Migrations -Context ApplicationDbContext
//dotnet ef migrations add InitialCreate --project Backend.Infrastructure --output-dir PostgreSQL/Migrations --context ApplicationDbContext