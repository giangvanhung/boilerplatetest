using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace kamrj.Security.Entities
{
    public class SecurityPolicySetting : FullAuditedEntity<long>, IMayHaveTenant
    {
        public int? TenantId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}