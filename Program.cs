using Microsoft.IdentityModel.Tokens;
using Serilog;
using SmartPocketAPI.Helpers.Extensions;
using SmartPocketAPI.Middlewares;
using SmartPocketAPI.Options;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(
        path: "logs/log-.txt",
        rollingInterval: RollingInterval.Day,
        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
    )
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddOptions<ConfigurationOptions>()
    .Bind(builder.Configuration.GetSection("ConfigurationOptions"))
    .Validate(options => !string.IsNullOrWhiteSpace(options.ConnectionString), "La cadena de conexión no puede estar vacía.")
    .Validate(options => options.CommandTimeOut > 0, "El tiempo de espera debe ser mayor a 0.")
    .ValidateDataAnnotations();

builder.Services.AddOptions<JwtOptions>()
    .Bind(builder.Configuration.GetSection("JwtSettings"))
    .Validate(options => !string.IsNullOrWhiteSpace(options.Secret), "El secreto no puede estar vacio.")
    .ValidateDataAnnotations();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        var jwtSettings = builder.Configuration.GetSection("JwtSettings");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings["Secret"]!)
            )
        };
    });

ServiceCollectionExtensions.AddMyServices(builder.Services);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Aplicación iniciada");
}

// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowAll");

app.UseMiddleware<UserInfoMiddleware>();

app.MapControllers();

await app.RunAsync();
