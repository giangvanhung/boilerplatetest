using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using kamrj.Authorization;

namespace kamrj
{
    [DependsOn(
        typeof(kamrjCoreModule), 
        typeof(AbpAutoMapperModule))]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class kamrjApplicationModule : AbpModule
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public override void PreInitialize()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            Configuration.Authorization.Providers.Add<kamrjAuthorizationProvider>();
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public override void Initialize()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            var thisAssembly = typeof(kamrjApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
