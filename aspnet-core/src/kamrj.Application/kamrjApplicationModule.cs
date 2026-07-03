using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using kamrj.Authorization;
using kamrj.Core.Models;
using kamrj.Models.FeatureModel.Dto;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;

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
            Configuration.Modules.AbpAutoMapper().Configurators.Add(cfg =>
            {
                var writer = new GeoJsonWriter();
                var reader = new GeoJsonReader();

                cfg.CreateMap<Feature, FeatureDto>()
                   .ForMember(d => d.Geometry, o => o.MapFrom(s => writer.Write(s.Geometry)));

                cfg.CreateMap<FeatureDto, Feature>()
                   .ForMember(d => d.Geometry, o => o.MapFrom(s => reader.Read<Geometry>(s.Geometry)));
            });

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
