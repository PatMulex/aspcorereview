using SelfieAWookies.Core.Selfies.Infrastructures.Configuration;

namespace SelfieAwookie.API.UI.ExtensionsMethods
{
    /// <summary>
    /// Custom options from config (json)
    /// </summary>
    public static class OptionsMethods
    {
        /// <summary>
        /// Add custom options from config (json)
        /// </summary>
        /// <param name="services"></param>
        #region Public methods
        public static void AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SecurityOption>(configuration.GetSection("Jwt"));
        }
        #endregion
    }
}
