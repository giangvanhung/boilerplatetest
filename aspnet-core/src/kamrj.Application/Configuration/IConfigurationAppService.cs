using System.Threading.Tasks;
using kamrj.Configuration.Dto;

namespace kamrj.Configuration
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public interface IConfigurationAppService
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        Task ChangeUiTheme(ChangeUiThemeInput input);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
