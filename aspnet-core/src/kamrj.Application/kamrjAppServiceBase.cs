using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using kamrj.Authorization.Users;
using kamrj.MultiTenancy;

namespace kamrj
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class kamrjAppServiceBase : ApplicationService
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public TenantManager TenantManager { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public UserManager UserManager { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        protected kamrjAppServiceBase()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            LocalizationSourceName = kamrjConsts.LocalizationSourceName;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        protected virtual async Task<User> GetCurrentUserAsync()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            var user = await UserManager.FindByIdAsync(AbpSession.GetUserId().ToString());
            if (user == null)
            {
                throw new Exception("There is no current user!");
            }

            return user;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        protected virtual Task<Tenant> GetCurrentTenantAsync()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        protected virtual void CheckErrors(IdentityResult identityResult)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
