using DemoAppBE.Middleware;

namespace DemoAppBE.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            return app;
        }

        public static IApplicationBuilder ConfigureApp(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseAuthorization();
            return app;
        }
        public static IApplicationBuilder ConfigureMiddleware(this IApplicationBuilder app)
        {
            app.UseCustomMiddleware(); // Exception Handling, Logging, etc.
            app.UseHttpsRedirection();

            return app;
        }
    }
}

