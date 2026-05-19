using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Zero.Configuration;
using kamrj.Authorization.Accounts.Dto;
using kamrj.Authorization.Users;

namespace kamrj.Authorization.Accounts
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class AccountAppService : kamrjAppServiceBase, IAccountAppService
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        // from: http://regexlib.com/REDetails.aspx?regexp_id=1923
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public const string PasswordRegex = "(?=^.{8,}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\\s)[0-9a-zA-Z!@#$%^&*()]*$";
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        private readonly UserRegistrationManager _userRegistrationManager;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public AccountAppService(
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
            UserRegistrationManager userRegistrationManager)
        {
            _userRegistrationManager = userRegistrationManager;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public async Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            var tenant = await TenantManager.FindByTenancyNameAsync(input.TenancyName);
            if (tenant == null)
            {
                return new IsTenantAvailableOutput(TenantAvailabilityState.NotFound);
            }

            if (!tenant.IsActive)
            {
                return new IsTenantAvailableOutput(TenantAvailabilityState.InActive);
            }

            return new IsTenantAvailableOutput(TenantAvailabilityState.Available, tenant.Id);
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public async Task<RegisterOutput> Register(RegisterInput input)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            var user = await _userRegistrationManager.RegisterAsync(
                input.Name,
                input.Surname,
                input.EmailAddress,
                input.UserName,
                input.Password,
                true // Assumed email address is always confirmed. Change this if you want to implement email confirmation.
            );

            var isEmailConfirmationRequiredForLogin = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin);

            return new RegisterOutput
            {
                CanLogin = user.IsActive && (user.IsEmailConfirmed || !isEmailConfirmationRequiredForLogin)
            };
        }
    }
}
