namespace Backend.API.DI;

using Extensions;

public static class DIConfiguration
{
    public static void AddCollectionExtensions(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddServices();
        services.AddMediatoR();
        services.AddErrors();

        services.AddControllers();

        services.AddApiAuthentication(configuration);
    }
}