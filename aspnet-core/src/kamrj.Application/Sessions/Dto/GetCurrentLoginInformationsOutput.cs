namespace kamrj.Sessions.Dto
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class GetCurrentLoginInformationsOutput
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public ApplicationInfoDto Application { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public UserLoginInfoDto User { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public TenantLoginInfoDto Tenant { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
