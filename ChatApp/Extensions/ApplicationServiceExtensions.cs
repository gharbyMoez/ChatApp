using ChatApp.Data;
using ChatApp.Interfaces;
using ChatApp.Services;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        /* services.AddScoped<ITokenService, TokenService>();
         services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
         services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
         services.AddScoped<IPhotoService, PhotoService>();
         services.AddScoped<LogUserActivity>();
         services.AddSignalR();
         *//*services.AddSingleton<PresenceTracker>();*//*
         services.AddScoped<IUnitOfWork, UnitOfWork>();*/
        services.AddDbContext<DataContext>(opt =>
         {
             opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
         });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddCors();
        services.AddScoped<ITokenService, TokenService>();


        return services;
    }
}
