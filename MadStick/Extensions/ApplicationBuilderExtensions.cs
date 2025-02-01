using MadStickWebAppTester.Middleware;

namespace MadStickWebAppTester.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseHeaderLogging(this IApplicationBuilder app)
        {
            return app.UseMiddleware<HeaderLoggingMiddleware>();
        }

    }
}
