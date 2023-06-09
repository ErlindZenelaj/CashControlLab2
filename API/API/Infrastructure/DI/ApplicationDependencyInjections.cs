using Application.Common.Interfaces.Services;
using Application.Helpers;
using Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

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