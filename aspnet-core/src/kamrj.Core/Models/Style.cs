using Abp.Domain.Entities;

namespace kamrj.Core.Models
{
    public class Style : Entity<int>
    {
        public string Name { get; set; }
        public string StyleJson { get; set; }
        public int LayerId { get; set; }
        public Layer Layer { get; set; }
    }
}