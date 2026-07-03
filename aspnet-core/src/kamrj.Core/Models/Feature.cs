using Abp.Domain.Entities;
using NetTopologySuite.Geometries;

namespace kamrj.Core.Models
{
    public class Feature : Entity<int>
    {
        public string Name { get; set; }
        public string Properties { get; set; }
        public Geometry Geometry { get; set; }
        public int LayerId { get; set; }
        public Layer Layer { get; set; }
    }
}