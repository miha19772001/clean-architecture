namespace Backend.API.DI.Extensions;

using Backend.Application.Services.File;
using Application.Services.Account;
using Infrastructure.JwtProvider;
using Infrastructure.PasswordHasher;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddTransient<IJwtProvider, JwtProvider>();
        services.AddTransient<IPasswordHasher, PasswordHasher>();

        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IFileService, FileService>();
    }
}