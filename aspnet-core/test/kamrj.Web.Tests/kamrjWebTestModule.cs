using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using kamrj.EntityFrameworkCore;
using kamrj.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace kamrj.Web.Tests
{
    [DependsOn(
        typeof(kamrjWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class kamrjWebTestModule : AbpModule
    {
        public kamrjWebTestModule(kamrjEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(kamrjWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(kamrjWebMvcModule).Assembly);
        }
    }
}