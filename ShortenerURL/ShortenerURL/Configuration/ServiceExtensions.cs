using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortenerURL.Configuration
{
    public static class ServiceExtensions
    {
       

        public static void RegisterDependencies(this IServiceCollection services, IConfiguration Configuration)
        {
            string mongoConnectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddTransient<IShortUrlRepository>(s => new ShortUrlRepository(mongoConnectionString, "Url", "ShortUrl"));
            services.AddTransient<IShortUrlServices, ShortUrlServices>();
        }
    }
}
