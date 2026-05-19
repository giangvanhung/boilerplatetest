using System.ComponentModel.DataAnnotations;

namespace kamrj.Users.Dto
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ResetPasswordDto
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        [Required]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string AdminPassword { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        [Required]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public long UserId { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        [Required]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string NewPassword { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
