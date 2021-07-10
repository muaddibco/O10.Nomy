using Flurl.Http;
using Flurl.Http.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using O10.Core.Configuration;
using O10.Core.ExtensionMethods;
using O10.Core.Logging;
using O10.Core.Serialization;
using O10.Nomy.Data;
using O10.Nomy.ExtensionMethods;
using O10.Nomy.Hubs;
using O10.Nomy.Models;
using System.Threading;

namespace O10.Nomy
{
    public class Startup
    {
        private readonly Log4NetLogger _logger;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _logger = new Log4NetLogger(null);
            _logger.Initialize(nameof(Startup), "log4net.xml");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                var connString = Configuration.GetConnectionString("DefaultConnection").Replace("{DBSERVER}", Configuration.GetValue<string>("DBSERVER"));

                _logger.Debug($"Main DB Context: {connString}");
                options.UseSqlServer(connString);
            });

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            services.AddAuthentication()
                .AddIdentityServerJwt();
            services.AddControllersWithViews();
            services.AddRazorPages();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
            
            services.AddSignalR();

            services.AddBootstrapper<WebApiBootstrapper>(_logger);
            services.Replace(new ServiceDescriptor(typeof(IAppConfig), _ => new JsonAppConfig(Configuration), ServiceLifetime.Singleton));

            FlurlHttp.Configure(s =>
            {
                var jsonSettings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.None,
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new CamelCaseNamingStrategy
                        {
                            OverrideSpecifiedNames = false
                        }
                    },
                    TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
                    NullValueHandling = NullValueHandling.Ignore,
                    Formatting = Formatting.Indented,
                };

                jsonSettings.Converters.Add(new StringEnumConverter());
                s.JsonSerializer = new NewtonsoftJsonSerializer(jsonSettings);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.EnsureMigrationOfContext<ApplicationDbContext>();
            app.ApplicationServices.UseBootstrapper<WebApiBootstrapper>(CancellationToken.None, _logger);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();

                endpoints.MapHub<PaymentSessionHub>("/payments", o =>
                {
                    o.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.LongPolling | Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets;
                });

                endpoints.MapHub<ChatSessionHub>("/chat", o =>
                {
                    o.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.LongPolling | Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets;
                });
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
