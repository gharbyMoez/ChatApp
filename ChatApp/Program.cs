using ChatApp.Data;
using ChatApp.Extensions;
using ChatApp.Middleware;
using Microsoft.EntityFrameworkCore;

namespace ChatApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddApplicationServices(builder.Configuration);
            builder.Services.AddIdentityServices(builder.Configuration);
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseMiddleware<ExceptionMiddleware>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(builder =>
            {
                builder.WithOrigins("http://localhost:4200") // Ajoutez l'URL de votre client Angular
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();



            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<DataContext>();
                /* var userManager = services.GetRequiredService<UserManager<AppUser>>();
                 var roleManager = services.GetRequiredService<RoleManager<AppRole>>();*/
                await context.Database.MigrateAsync();
                /* await Seed.ClearConnections(context);*/
                await Seed.SeedUsers(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetService<ILogger<Program>>();
                logger.LogError(ex, "An error occured during migration");
            }
            app.Run();
        }
    }
}
