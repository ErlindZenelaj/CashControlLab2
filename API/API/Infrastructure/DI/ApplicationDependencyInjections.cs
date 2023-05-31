using Application.Common.Interfaces.Services;
using Application.Helpers;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DI
{
    public static class ApplicationDependencyInjection
    {
        public static void ConfigureCache(this IServiceCollection services)
        {
            //services.AddScoped<ICache, MemoryCache>();
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();

        }

    }
}