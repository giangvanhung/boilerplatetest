using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kamrj.Models.FeatureModel.Dto
{
    public class FeatureDto : EntityDto<int>   // không gắn [AutoMap] nữa
    {
        public string Name { get; set; }
        public string Properties { get; set; }
        public int LayerId { get; set; }
        public string Geometry { get; set; }   // chuỗi GeoJSON, ví dụ {"type":"Point","coordinates":[105.85,21.03]}
    }
}
