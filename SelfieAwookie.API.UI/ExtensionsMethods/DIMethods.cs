using MediatR;
using SelfieAWookies.Core.Selfies.Domain;
using SelfieAWookies.Core.Selfies.Infrastructures.Repositories;
using System.Reflection;

namespace SelfieAwookie.API.UI.ExtensionsMethods
{
    public static class DIMethods
    {
        #region Public methods
        /// <summary>
        /// Prepare customs dependencies injections
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddInjections(this IServiceCollection services)
        {
            services.AddScoped<ISelfieRepository, DefaultSelfieRepository>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));


            return services;
        }
        #endregion
    }
}
