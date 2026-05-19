using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using kamrj.Configuration.Dto;

namespace kamrj.Configuration
{
    [AbpAuthorize]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ConfigurationAppService : kamrjAppServiceBase, IConfigurationAppService
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
