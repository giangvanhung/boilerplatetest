using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace kamrj.Configuration
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public static class HostingEnvironmentExtensions
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public static IConfigurationRoot GetAppConfiguration(this IWebHostEnvironment env)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            return AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName, env.IsDevelopment());
        }
    }
}
