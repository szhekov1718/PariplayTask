using FootballLeague.Utilities.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace FootballLeague.Extensions
{
    public static class AppBuilderExtension
    {
        public static void UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
