using Abp.Application.Services;
using Abp.Domain.Repositories;
using kamrj.Core.Models;
using kamrj.Models.LayerModel.Dto;

namespace kamrj.Models.LayerModel
{
    public class LayerAppService : AsyncCrudAppService<Layer, LayerDto, int>
    {
        public LayerAppService(IRepository<Layer, int> repository) : base(repository) { }
    }
}
