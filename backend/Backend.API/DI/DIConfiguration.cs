namespace Backend.API.DI;

using Extensions;

public static class DiConfiguration
{
    public static void AddCollectionExtensions(this IServiceCollection services)
    {
        services.AddRepositoryServices();

        services.AddServices();
        services.AddMediatoR();
        services.AddErrors();

        services.AddAutoMapperProfiles();
        services.AddControllers();
    }
}