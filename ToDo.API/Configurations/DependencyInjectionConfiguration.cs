using Microsoft.Extensions.DependencyInjection;
using ToDo.API.Data;
using ToDo.API.Data.Repository;
using ToDo.API.Data.Repository.Interfaces;

namespace ToDo.API.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<Context>();
        }
    }
}
