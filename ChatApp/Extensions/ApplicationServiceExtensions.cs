using ChatApp.Data;
using ChatApp.Helpers;
using ChatApp.Interfaces;
using ChatApp.Services;

namespace ChatApp.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
        services.AddScoped<IPhotoService, PhotoService>();
        services.AddScoped<LogUserActivity>();
        services.AddSignalR();
        /*services.AddSingleton<PresenceTracker>();*/
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
