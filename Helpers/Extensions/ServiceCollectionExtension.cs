using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SmartPocketAPI.Auth;
using SmartPocketAPI.Database;
using SmartPocketAPI.Options;
using SmartPocketAPI.Services;
using SmartPocketAPI.Services.Interface;
using SmartPocketAPI.Services.Interfaces;

namespace SmartPocketAPI.Helpers.Extensions;

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

        // Configura las políticas de CORS
        services.AddCors(options =>
        {
            //options.AddPolicy("AllowSpecificOrigins", policy =>
            //{
            //    policy.WithOrigins("https://example.com", "https://anotherdomain.com") // Dominios permitidos
            //          .AllowAnyHeader() // Permitir cualquier encabezado
            //          .AllowAnyMethod() // Permitir cualquier método HTTP (GET, POST, etc.)
            //          .AllowCredentials(); // Permitir cookies o autenticación
            //});

            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin() // Permite todas las solicitudes de cualquier origen
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });

        services.AddAuthorization();
        services.AddLogging();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IMovementService, MovementService>();
        services.AddScoped<IFrequencyService, FrequencyService>();
        services.AddScoped<IMovementTypeService, MovementTypeService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IPaymentMethodService, PaymentMethodService>();
        services.AddScoped<IRecurringPaymentService, RecurringPaymentService>();

        services.AddHttpClient();

        return services;
    }
}