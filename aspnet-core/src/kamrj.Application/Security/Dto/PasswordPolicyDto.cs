#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace kamrj.Security.Dto
{
    public class PasswordPolicyDto
    {
        public  bool ForceChangePasswordFirstLogin { get; set; }
        public int RequiredLength { get; set; }
        public bool RequireUppercase { get; set; }
        public bool RequireLowercase { get; set; }
        public bool RequireDigit { get; set; }
        public bool RequireNonAlphanumeric { get; set; }
        public int PasswordExpirationDays { get; set; }
        public int MaxFailedAccessAttempts { get; set; }
        public int LockoutMinutes { get; set; }
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member