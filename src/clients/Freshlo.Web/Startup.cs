using System.Threading.Tasks;
using Freshlo.Web.Middleware;
using Freshlo.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SecurityHeadersMiddleware;
using Rotativa.AspNetCore;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Freshlo.DomainEntities;
using Microsoft.Extensions.DependencyInjection;
using Freshlo.Web.SuscribeTableDependencies;
using Newtonsoft.Json.Serialization;
using DemoDecodeURLParameters.Security;

namespace Freshlo.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment evn)
        {
            Configuration = configuration;
            HostingEnvironment = evn;
        }
        public IConfiguration Configuration { get; }

        public IHostingEnvironment HostingEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();

            // Configure and Add framework services
            services.AddCustomAuthentication();
            services.AddCustomMvc();

            // Services needed to initialize DI services
            services.AddDbConfig_Development(Configuration);

            // DI services
            services.AddCustomServices();
            services.AddSignalR();
            //services.AddSingleton<OrderNotification>();
            //services.AddSingleton<SubscribeOrderTableDependency>();

            services.ConfigureCookiePolicyOptions();
            //Ip
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.AddDataProtection();

            services.AddSingleton<UniqueCode>();
            services.AddSingleton<CustomIDataProtection>();

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en"),
                    new CultureInfo("ar"),
                };

                // State what the default culture for your application is. This will be used if no specific culture
                // can be determined for a given request.
                options.DefaultRequestCulture = new RequestCulture("en");

                // You must explicitly state which cultures your application supports.
                // These are the cultures the app supports for formatting numbers, dates, etc.
                options.SupportedCultures = supportedCultures;

                // These are the cultures the app supports for UI strings, i.e. we have localized resources for.
                options.SupportedUICultures = supportedCultures;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSecurityHeadersMiddleware(new SecurityHeadersBuilder()
                .AddDefaultSecurePolicy());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseStatusCodePagesWithReExecute("/Home/Error");
                app.UseExceptionHandler(a => {
                    a.Run(ctx => {
                        ctx.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        return Task.CompletedTask;
                    });
                });
            }
            RotativaConfiguration.Setup(env);

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseSignalR(route => {
                route.MapHub<SignalServer>("/signalServer");
            });

            app.UseSignalR(route => {
                route.MapHub<SignalRserver1>("/SignalRserver1");
            });

            app.UseSignalR(route =>
            {
                route.MapHub<OrderNotification>("/OrderNotification");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=Login}/{id?}");
            });
            //app.UseOrderTableDependency<SubscribeOrderTableDependency>(Configuration.GetConnectionString("DefaultConnection"));
        }
    }
}