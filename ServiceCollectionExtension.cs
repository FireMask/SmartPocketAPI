using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SmartPocketAPI.Auth;
using SmartPocketAPI.Database;
using SmartPocketAPI.Models;
using SmartPocketAPI.Options;
using SmartPocketAPI.Services;
using SmartPocketAPI.Services.Interface;
using SmartPocketAPI.Services.Interfaces;

namespace SmartPocketAPI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMyServices(this IServiceCollection services)
    {
        //services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("Data Source=SmartPocket.db"));

        services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
        {
            var dbOptions = serviceProvider.GetRequiredService<IOptions<ConfigurationOptions>>().Value;
            options.UseSqlite(dbOptions.ConnectionString, sqloptions =>
            {
                sqloptions.CommandTimeout(dbOptions.CommandTimeOut);
            });
        });

        services.AddAuthorization();
        services.AddLogging();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IFrequencyService, FrequencyService>();
        services.AddScoped<IMovementTypeService, MovementTypeService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IRecurringPaymentService, RecurringPaymentService>();
        services.AddScoped<IAuthService, AuthService>();

        services.AddHttpClient();

        return services;
    }
}