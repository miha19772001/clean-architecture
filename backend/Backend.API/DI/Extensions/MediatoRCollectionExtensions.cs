namespace Backend.API.DI.Extensions;

using System.Reflection;
using Application.Validation;

public static class MediatoRCollectionExtensions
{
    public static void AddMediatoR(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies([
                Assembly.Load("Backend.API"),
                Assembly.Load("Backend.Application"),
                Assembly.Load("Backend.Infrastructure"),
                Assembly.Load("Backend.Domain")
            ]);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
    }
}