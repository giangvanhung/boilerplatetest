using Abp.Application.Services.Dto;
using System;

namespace kamrj.Users.Dto
{
    //custom PagedResultRequestDto
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class PagedUserResultRequestDto : PagedResultRequestDto
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string Keyword { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public bool? IsActive { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
