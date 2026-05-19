using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace kamrj.Controllers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public abstract class kamrjControllerBase: AbpController
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        protected kamrjControllerBase()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            LocalizationSourceName = kamrjConsts.LocalizationSourceName;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        protected void CheckErrors(IdentityResult identityResult)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
