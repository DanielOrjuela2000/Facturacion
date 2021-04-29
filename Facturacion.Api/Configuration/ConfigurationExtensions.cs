using Facturacion.Core.Interfaces;
using Facturacion.Core.Interfaces.Commands;
using Facturacion.Infrastructure.Data;
using Facturacion.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace Facturacion.Api.Configuration
{
    public static class ConfigurationExtensions
    {
        public static IServiceCollection ConfigDbManager(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IDbManager>(dbm => new DbManager(connectionString));
            return services;
        }
        public static IServiceCollection ConfigSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(doc =>
            {
                doc.SwaggerDoc("v1", new OpenApiInfo { Title = "Facturación API", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                doc.IncludeXmlComments(xmlPath);
            });
            return services;
        }

        public static IServiceCollection ConfigInterfaces(this IServiceCollection services)
        {
            services.AddScoped<IProductoRepository, ProductoRepository>();
            services.AddScoped<IFacturaRepository, FacturaRepository>();
            services.AddScoped<IFacturaDetalleRepository, FacturaDetalleRepository>();
            services.AddScoped<IFacturaCommand, FacturaCommand>();
            return services;
        }
    }
}
