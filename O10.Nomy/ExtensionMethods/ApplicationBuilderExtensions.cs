using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace O10.Nomy.ExtensionMethods
{
    public static class ApplicationBuilderExtensions
    {
        public static void EnsureMigrationOfContext<T>(this IApplicationBuilder app) where T : DbContext
        {
            try
            {
                using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
                var context = serviceScope.ServiceProvider.GetService<T>();
                context.Database.Migrate();
                context.Database.EnsureCreated();
            }
            catch (Exception ex)
            {

                throw;
            }        
        }
    }
}
