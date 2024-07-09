using Freshlo.DomainEntities;
using Freshlo.Web.SuscribeTableDependencies;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SecurityHeadersMiddleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freshlo.Web.Middleware
{
    public static class MiddlerwareExtensions
    {
        public static IApplicationBuilder UseSecurityHeadersMiddleware(this IApplicationBuilder app, SecurityHeadersBuilder builder)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return app.UseMiddleware<SecurityHeaders>(builder.Build());
        }
        public static void UseOrderTableDependency<SalesCountData>(this IApplicationBuilder applicationBuilder, string connectionString)
                 where SalesCountData : ISubscribeTableDependency
        {
            var serviceProvider = applicationBuilder.ApplicationServices;
            var service = serviceProvider.GetService<SalesCountData>();
            service.SubscribeTableDependency(connectionString);
        }
    }
}
