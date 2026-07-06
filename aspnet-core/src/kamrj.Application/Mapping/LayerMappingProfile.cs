using AutoMapper;
using kamrj.Core.Models;
using kamrj.Models.LayerModel.Dto;
using kamrj.Models.StyleModel.Dto;
using Newtonsoft.Json;

namespace kamrj.Mapping
{
    public class LayerMappingProfile : Profile
    {
        public LayerMappingProfile()
        {
            CreateMap<LayerDto, Layer>()
                .ForMember(d => d.Style, o => o.MapFrom(s => s.Styles));
            CreateMap<Layer, LayerDto>()
                .ForMember(d => d.Styles, o => o.MapFrom(s => s.Style));
            CreateMap<StyleDto, Style>();
            CreateMap<Style, StyleDto>();
        }
    }
}
