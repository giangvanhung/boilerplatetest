using System.Collections.Generic;
using kamrj.Models.StyleModel.Dto;
using kamrj.Models.FeatureModel.Dto;

namespace kamrj.Models.LayerModel.Dto
{
    public class CreateOrUpdateLayerDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<StyleDto> Styles { get; set; }
        public List<FeatureDto> Features { get; set; }
    }
}
