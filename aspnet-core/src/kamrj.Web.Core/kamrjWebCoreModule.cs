using System;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.AspNetCore.SignalR;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.Configuration;
using kamrj.Authentication.JwtBearer;
using kamrj.Configuration;
using kamrj.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace kamrj
{
    [DependsOn(
         typeof(kamrjApplicationModule),
         typeof(kamrjEntityFrameworkModule),
         typeof(AbpAspNetCoreModule)
        ,typeof(AbpAspNetCoreSignalRModule)
     )]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class kamrjWebCoreModule : AbpModule
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public kamrjWebCoreModule(IWebHostEnvironment env)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public override void PreInitialize()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                kamrjConsts.ConnectionStringName
            );

            // Use database for language management
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            Configuration.Modules.AbpAspNetCore()
                 .CreateControllersForAppServices(
                     typeof(kamrjApplicationModule).GetAssembly()
                 );

            ConfigureTokenAuth();
        }

        private void ConfigureTokenAuth()
        {
            IocManager.Register<TokenAuthConfiguration>();
            var tokenAuthConfig = IocManager.Resolve<TokenAuthConfiguration>();

            tokenAuthConfig.SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appConfiguration["Authentication:JwtBearer:SecurityKey"]));
            tokenAuthConfig.Issuer = _appConfiguration["Authentication:JwtBearer:Issuer"];
            tokenAuthConfig.Audience = _appConfiguration["Authentication:JwtBearer:Audience"];
            tokenAuthConfig.SigningCredentials = new SigningCredentials(tokenAuthConfig.SecurityKey, SecurityAlgorithms.HmacSha256);
            tokenAuthConfig.Expiration = TimeSpan.FromDays(1);
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public override void Initialize()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            IocManager.RegisterAssemblyByConvention(typeof(kamrjWebCoreModule).GetAssembly());
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public override void PostInitialize()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(kamrjWebCoreModule).Assembly);
        }
    }
}
