namespace kamrj.Models.TokenAuth
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ExternalAuthenticateResultModel
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string AccessToken { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string EncryptedAccessToken { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public int ExpireInSeconds { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public bool WaitingForActivation { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
