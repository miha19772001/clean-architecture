namespace Backend.API.DI.Extensions;

using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

public static class MappingColletionExtensions
{
    public static void AddAutoMapperProfiles(this IServiceCollection services)
    {
        services.AddAutoMapper(
            (serviceProvider, config) => { },
            Assembly.Load("Backend.API"),
            Assembly.Load("Backend.Application"),
            Assembly.Load("Backend.Infrastructure"),
            Assembly.Load("Backend.Domain")
        );
    }
}