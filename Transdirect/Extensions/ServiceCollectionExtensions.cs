using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Transdirect;

namespace Transdirect.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddTransdirect(
            this IServiceCollection services,
            TransdirectOptions configureOptions
        )
        {
            configureOptions.CheckArgumentNull(nameof(configureOptions));

            services.TryAddSingleton(Options.Create(configureOptions));
            services.TryAddTransient<ITransdirectService, TransdirectService>();
        }

        public static void AddTransdirect(
            this IServiceCollection services,
            Action<TransdirectOptions> configuration)
        {
            configuration.CheckArgumentNull(nameof(configuration));

            var configureOptions = new TransdirectOptions();

            configuration(configureOptions);

            AddTransdirect(services, configureOptions);
        }

        public static void AddTransdirect(
            this IServiceCollection services,
            IConfigurationRoot configuration
        )
        {
            configuration.CheckArgumentNull(nameof(configuration));

            services.Configure<TransdirectOptions>(
                configuration.GetSection("Transdirect")
            );

            services.TryAddSingleton<ITransdirectService, TransdirectService>();
        }
    }
}