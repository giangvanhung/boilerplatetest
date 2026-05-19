namespace kamrj.Authorization.Accounts.Dto
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class IsTenantAvailableOutput
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public TenantAvailabilityState State { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public int? TenantId { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IsTenantAvailableOutput()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public IsTenantAvailableOutput(TenantAvailabilityState state, int? tenantId = null)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            State = state;
            TenantId = tenantId;
        }
    }
}
