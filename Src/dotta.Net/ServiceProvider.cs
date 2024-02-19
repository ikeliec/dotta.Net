using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

namespace dotta.Net
{
    public static class ServiceProvider
    {
        public static IServiceCollection AddDotta(this IServiceCollection services, DottaServiceOptions options)
        {
            services.AddScoped<Dotta>((serviceProvider) =>
            {
                var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
                var httpClient = httpClientFactory.CreateClient();

                return new Dotta(new DottaOptions
                {
                    ApiKey = options.ApiKey,
                    BaseUrlProduction = options.BaseUrlProduction,
                    BaseUrlSandbox = options.BaseUrlSandbox,
                    Environment = options.Environment,
                    HttpClient = httpClient
                });
            });

            return services;
        }
    }
}
