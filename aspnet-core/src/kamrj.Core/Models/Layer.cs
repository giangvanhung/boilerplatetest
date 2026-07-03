using Abp.Domain.Entities;
using System.Collections.Generic;

namespace kamrj.Core.Models
{
    public class Layer : Entity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Feature> Features { get; set; }
        public ICollection<Style> Style { get; set; }
    }
}