using System.ComponentModel.DataAnnotations;

namespace kamrj.Users.Dto
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ChangePasswordDto
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        [Required]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string CurrentPassword { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        [Required]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string NewPassword { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
