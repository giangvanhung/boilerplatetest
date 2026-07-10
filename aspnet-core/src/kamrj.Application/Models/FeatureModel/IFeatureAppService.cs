using Abp.Application.Services;
using Abp.Domain.Repositories;
using kamrj.Core.Models;
using kamrj.Models.FeatureModel.Dto;
using System.Collections.Generic;
using Abp.Authorization;
using System.Threading.Tasks; 

namespace kamrj.Models.FeatureModel 
{ 
    public interface IFeatureAppService : IAsyncCrudAppService<FeatureDto, int>
    {
        Task<List<FeatureDto>> GetFeatureByLayerIdAsync(int layerId);
    }
}