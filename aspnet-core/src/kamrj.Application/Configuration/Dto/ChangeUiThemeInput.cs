using System.ComponentModel.DataAnnotations;

namespace kamrj.Configuration.Dto
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ChangeUiThemeInput
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        [Required]
        [StringLength(32)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string Theme { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
