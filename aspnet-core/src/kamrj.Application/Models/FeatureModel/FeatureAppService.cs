using Abp.Application.Services;
using Abp.Domain.Repositories;
using kamrj.Core.Models;
using kamrj.Models.FeatureModel.Dto;
using System.Collections.Generic;
using Abp.Authorization;
using System.Threading.Tasks;

namespace kamrj.Models.FeatureModel 
{ 
    public class FeatureAppService : AsyncCrudAppService<Feature, FeatureDto, int>, IFeatureAppService
    {
        public FeatureAppService(IRepository<Feature, int> repository) : base(repository) { }

        // [AbpAuthorize("Pages.user")]
        public async Task<List<FeatureDto>> GetFeatureByLayerIdAsync(int layerId)
        {
            var features = await Repository.GetAllListAsync(f => f.LayerId == layerId);
            return ObjectMapper.Map<List<FeatureDto>>(features);
        }
    }
}