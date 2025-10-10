namespace Backend.API.DI.Extensions;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        //services.AddTransient<IAuthenticationService, AuthenticationService>();
        //services.AddTransient<IFileService, FileService>();
    }
}