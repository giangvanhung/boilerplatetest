using Abp.Zero.EntityFrameworkCore;
using kamrj.Authorization.Roles;
using kamrj.Authorization.Users;
using kamrj.Core.Models;
using kamrj.MultiTenancy;
using kamrj.Security.Entities;
using Microsoft.EntityFrameworkCore;

namespace kamrj.EntityFrameworkCore
{
    public class kamrjDbContext : AbpZeroDbContext<Tenant, Role, User, kamrjDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Layer> Layer { get; set; }
        public DbSet<Style> Style { get; set; }
        public DbSet<Feature> Feature { get; set; }
        public DbSet<SecurityPolicySetting> SecurityPolicySettings { get; set; }
        public kamrjDbContext(DbContextOptions<kamrjDbContext> options)
            : base(options)
        {
        }
    }
}
