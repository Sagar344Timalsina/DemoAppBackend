
using Scalar.AspNetCore;
using Serilog;
using DemoAppBE.Infrastructure.Extensions;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}")
    .CreateLogger();


try
{
    Log.Information("starting server.");
    var builder = WebApplication.CreateBuilder(args);
    // Configure Serilog
    builder.Host.UseSerilog
        ((context, loggerConfiguration) =>
        loggerConfiguration.ConfigureSerilog(context.Configuration)
    );

    // Add Services to the DI container
    builder.Services.ConfigureServices(builder.Configuration);
    builder.Services.AddControllers();
    // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
    builder.Services.AddOpenApi();

    var app = builder.Build();
    //await app.ApplyMigrationsAndSeedAsync();
    app.ConfigureMiddleware();
    app.ConfigureApp(app.Environment);
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
    }
    app.UseCors("AllowAll");
    app.MapOpenApi();
    app.MapScalarApiReference();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "server terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
