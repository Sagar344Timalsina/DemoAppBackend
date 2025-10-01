using DemoAppBE.Data;
using DemoAppBE.Shared;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace DemoAppBE.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {


            // Register FluentValidation - Automatically scans for validators
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDBContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            return services;
        }
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("AppSettings:JWTSecret"));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration.GetValue<string>("AppSettings:JWTIssuer"),
                    ValidAudience = configuration.GetValue<string>("AppSettings:JWTAudience"),
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = context =>
                    {
                        context.HandleResponse();

                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";

                        var error = new Shared.Error(
                              code: "Validation.Error",
                              description: "Unauthorized access. Token is missing or invalid.",
                              ErrorType.UnAuthorized
                          );
                        var result = Result.Failure(error);
                        var json = JsonSerializer.Serialize(result);
                        return context.Response.WriteAsync(json);
                    },

                    OnForbidden = context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        context.Response.ContentType = "application/json";

                        var error = new Shared.Error(
                              code: "Validation.Error",
                              description: "Forbidden. You are forbiden to access this resources.",
                              ErrorType.Forbidden
                          );

                        var result = Result.Failure(error);

                        var json = JsonSerializer.Serialize(result);
                        return context.Response.WriteAsync(json);
                    }
                };
            });

            return services;

        }
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {


            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
            services.AddSingleton<IAppSettings>(sp =>
                sp.GetRequiredService<IOptions<AppSettings>>().Value);
            services.AddHttpContextAccessor();
            services.AddScoped<IUserContext,UserContext>();
            services.AddInfrastructureServices(configuration);
            services.AddApplicationServices();
            services.AddService();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.WithOrigins("http://localhost:5173") // Your frontend origin
                          .AllowCredentials() // Allow cookies & authentication headers
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
            services.AddControllers();
            services.AddJwtAuthentication(configuration);

            //services.AddOpenApi();

            return services;
        }
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            var managers = typeof(IService);

            var types = managers
                .Assembly
                .GetExportedTypes()
                .Where(t => t.IsClass && !t.IsAbstract)
                .Select(t => new
                {
                    Service = t.GetInterface($"I{t.Name}"),
                    Implementation = t
                })
                .Where(t => t.Service != null);

            foreach (var type in types)
            {
                if (managers.IsAssignableFrom(type.Service))
                {
                    services.AddTransient(type.Service, type.Implementation);
                }
            }

            return services;
        }

    }
}
