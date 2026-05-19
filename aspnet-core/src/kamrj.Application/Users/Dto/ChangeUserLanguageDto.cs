using System.ComponentModel.DataAnnotations;

namespace kamrj.Users.Dto
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ChangeUserLanguageDto
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        [Required]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string LanguageName { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}