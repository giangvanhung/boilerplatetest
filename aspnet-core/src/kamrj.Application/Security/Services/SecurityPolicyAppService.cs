using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using kamrj.Security.Dto;
using kamrj.Security.Entities;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace kamrj.Security.Services
{
    [AbpAuthorize("Pages.Security")]
    public class SecurityPolicyAppService : ApplicationService
    {
        private readonly IRepository<SecurityPolicySetting, long> _repository;

        public SecurityPolicyAppService(
            IRepository<SecurityPolicySetting, long> repository)
        {
            _repository = repository;
        }

        public async Task<PasswordPolicyDto> Get()
        {
            return new PasswordPolicyDto
            {
                RequiredLength = await GetInt("RequiredLength", 8),
                PasswordExpirationDays = await GetInt("PasswordExpirationDays", 90),
                MaxFailedAccessAttempts = await GetInt("MaxFailedAccessAttempts", 5),
                LockoutMinutes = await GetInt("LockoutMinutes", 15)
            };
        }
        public async Task Update(PasswordPolicyDto input)
        {
            await Set("RequiredLength", input.RequiredLength.ToString());

            await Set(
                "PasswordExpirationDays",
                input.PasswordExpirationDays.ToString());

            await Set(
                "MaxFailedAccessAttempts",
                input.MaxFailedAccessAttempts.ToString());

            await Set(
                "LockoutMinutes",
                input.LockoutMinutes.ToString());
        }

        private async Task<string> Get(string key, string defaultValue)
        {
            var value = await _repository.FirstOrDefaultAsync(x => x.Name == key);

            return value?.Value ?? defaultValue;
        }

        private async Task<int> GetInt(string key, int defaultValue)
        {
            return int.Parse(await Get(key, defaultValue.ToString()));
        }

        private async Task Set(string key, string value)
        {
            var setting = await _repository.FirstOrDefaultAsync(x => x.Name == key);

            if (setting == null)
            {
                setting = new SecurityPolicySetting
                {
                    Name = key,
                    Value = value,
                    TenantId = AbpSession.TenantId
                };

                await _repository.InsertAsync(setting);
            }
            else
            {
                setting.Value = value;

                await _repository.UpdateAsync(setting);
            }
        }
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member