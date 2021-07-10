using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using O10.Core.Logging;
using System;

namespace O10.Nomy.ExtensionMethods
{
    public static class ApplicationBuilderExtensions
    {
        public static void EnsureMigrationOfContext<T>(this IApplicationBuilder app, ILogger? logger = null) where T : DbContext
        {
            try
            {
                logger?.Debug("Ensuring DB is created and migration is up to date...");
                using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
                var context = serviceScope.ServiceProvider.GetService<T>();
                context.Database.Migrate();
                context.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                logger?.Error("Failed migration due to the error ", ex);
                throw;
            }        
        }
    }
}
