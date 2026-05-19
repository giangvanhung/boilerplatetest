using System;
using Microsoft.IdentityModel.Tokens;

namespace kamrj.Authentication.JwtBearer
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class TokenAuthConfiguration
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public SymmetricSecurityKey SecurityKey { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string Issuer { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string Audience { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public SigningCredentials SigningCredentials { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public TimeSpan Expiration { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
