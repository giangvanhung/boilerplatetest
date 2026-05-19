using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using kamrj.Configuration;

namespace kamrj.Web.Host.Startup
{
    [DependsOn(
       typeof(kamrjWebCoreModule))]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class kamrjWebHostModule: AbpModule
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public kamrjWebHostModule(IWebHostEnvironment env)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public override void Initialize()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            IocManager.RegisterAssemblyByConvention(typeof(kamrjWebHostModule).GetAssembly());
        }
    }
}
