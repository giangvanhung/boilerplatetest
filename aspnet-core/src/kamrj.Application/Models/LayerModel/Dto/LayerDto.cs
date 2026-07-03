using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using kamrj.Core.Models;

namespace kamrj.Models.LayerModel.Dto
{
    [AutoMap(typeof(Layer))]              
    public class LayerDto : EntityDto<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Style { get; set; }
    }
}
