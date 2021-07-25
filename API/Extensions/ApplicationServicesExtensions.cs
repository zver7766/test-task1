using Core.Intefraces;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
            services.AddScoped<IStatisticService, StatisticService>();
            services.AddScoped<IAdvertisementService, AdvertisementService>();

            return services;
        }
    }
}
