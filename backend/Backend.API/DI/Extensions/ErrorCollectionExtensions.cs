namespace Backend.API.DI.Extensions;

using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

public static class ErrorCollectionExtensions
{
    public static void AddErrors(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblies([
            Assembly.Load("Backend.API"),
            Assembly.Load("Backend.Application"),
            Assembly.Load("Backend.Infrastructure"),
            Assembly.Load("Backend.Domain")
        ]);

        services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
    }
}