using Abp.AutoMapper;
using kamrj.Authentication.External;

namespace kamrj.Models.TokenAuth
{
    [AutoMapFrom(typeof(ExternalLoginProviderInfo))]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ExternalLoginProviderInfoModel
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string Name { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string ClientId { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
