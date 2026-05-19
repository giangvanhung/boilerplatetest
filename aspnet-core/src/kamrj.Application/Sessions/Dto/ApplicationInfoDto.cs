using System;
using System.Collections.Generic;

namespace kamrj.Sessions.Dto
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ApplicationInfoDto
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string Version { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public DateTime ReleaseDate { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public Dictionary<string, bool> Features { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
