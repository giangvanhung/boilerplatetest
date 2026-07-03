using Abp.Application.Services;
using Abp.Domain.Repositories;
using kamrj.Core.Models;
using kamrj.Models.FeatureModel.Dto;

namespace kamrj.Models.FeatureModel 
{ 
    public class FeatureAppService : AsyncCrudAppService<Feature, FeatureDto, int>
    {
        public FeatureAppService(IRepository<Feature, int> repository) : base(repository) { }
    }
}