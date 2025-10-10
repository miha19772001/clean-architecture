namespace Backend.API.DI.Extensions
{

    using Domain.Common;
    using Infrastructure.PostgreSQL;
    using Microsoft.Extensions.DependencyInjection;

    public static class RepositoryCollectionExtensions
    {
        public static void AddRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.Scan(scan => scan
                .FromApplicationDependencies()
                .AddClasses(classes => classes.AssignableTo(typeof(IIncludeDetails<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());
        }
    }

}