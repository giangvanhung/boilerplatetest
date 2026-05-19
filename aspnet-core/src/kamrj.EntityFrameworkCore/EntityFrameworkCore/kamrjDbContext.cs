using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using kamrj.Authorization.Roles;
using kamrj.Authorization.Users;
using kamrj.MultiTenancy;
using kamrj.Security.Entities;

namespace kamrj.EntityFrameworkCore
{
    public class kamrjDbContext : AbpZeroDbContext<Tenant, Role, User, kamrjDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<SecurityPolicySetting> SecurityPolicySettings { get; set; }
        public kamrjDbContext(DbContextOptions<kamrjDbContext> options)
            : base(options)
        {
        }
    }
}
